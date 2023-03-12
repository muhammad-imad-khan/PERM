using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Perm.Common;
using Perm.Core.CacheManager;
using Perm.Core.ExceptionManager;
using Perm.Core.TenantManager.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Security.SecurityKeys;

namespace Perm.Security.AuthenticateManager
{
    public class Authenticate
    {
        private readonly ITenantIdentificationService _tenantIdentificationServiceBase;
        private readonly PermDataContext _permDataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Authenticate(ITenantIdentificationService tenantIdentificationServiceBase, PermDataContext permDataContext, IHttpContextAccessor httpContextAccessor)
        {
            _tenantIdentificationServiceBase = tenantIdentificationServiceBase;
            _permDataContext = permDataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Validate()
        {
            StringValues requestHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (requestHeader.Count != 0)
            {
                string token = requestHeader[0];
                Dictionary<string, string> decryptedToken = AuthenticateHelper.ValidateToken(token);
                decryptedToken.TryGetValue("TenantID", out string tenantID);
                _permDataContext.CurrentTenant = _tenantIdentificationServiceBase.GetCurrentTenantByID(tenantID);

                decryptedToken.TryGetValue("isValid", out string isValid);
                if (isValid == "0")
                {
                    throw new PermSecurityException(9001);
                }

                decryptedToken.TryGetValue("UserID", out string userID);
                decryptedToken.TryGetValue("RoleID", out string roleID);
                _httpContextAccessor.HttpContext.Items["UserID"] = userID.ParseTo<long>();
                _httpContextAccessor.HttpContext.Items["RoleID"] = roleID;
                _httpContextAccessor.HttpContext.Items["TenantID"] = tenantID;
                _httpContextAccessor.HttpContext.Items["SessionID"] = DateTime.Now.ToString(Constant.SESSIONID_FORMAT);
            }
            else
            {
                throw new AuthenticationException();
            }
        }
    }

    public static class AuthenticateHelper
    {
        /// <summary>
        /// Generate the JWT token
        /// </summary>
        /// <param name="tokenValues">A dictionary object that will add claims to JWT token</param>
        /// <param name="timeout">timeout in minutes</param>
        /// <returns></returns>
        public static string GenerateToken(Dictionary<string, string> tokenValues, double timeout = 20)
        {
            SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key.JWT_SECRET));

            string myIssuer = "http://mysite.com";
            string myAudience = "http://myaudience.com";

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(timeout * 60),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity()
            };

            if (!tokenValues.ContainsKey("timeoutInMinutes"))
            {
                tokenDescriptor.Subject = new ClaimsIdentity(new[]
                {
                    new Claim("timeoutInMinutes", timeout.ToString())
                });
            }

            foreach (KeyValuePair<string, string> tokenValue in tokenValues)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(tokenValue.Key, tokenValue.Value));
            }

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);
            ApplicationCache.Set(generatedToken, string.Empty);
            return generatedToken;
        }

        public static Dictionary<string, string> ValidateToken(string token)
        {
            string originalToken = token;
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("token can not be null or empty");

            string updatedToken = ApplicationCache.Get(token).ParseTo<string>();
            if (!string.IsNullOrEmpty(updatedToken))
            {
                token = updatedToken;
            }

            SymmetricSecurityKey mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key.JWT_SECRET));

            string myIssuer = "http://mysite.com";
            string myAudience = "http://myaudience.com";
            Dictionary<string, string> tokenValues = new Dictionary<string, string>();

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.ReadToken(token) is JwtSecurityToken securityToken)
            {
                tokenValues = new Dictionary<string, string>();
                foreach (var claim in securityToken.Claims)
                {
                    tokenValues[claim.Type] = claim.Value;
                }
            }

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);


                updatedToken = GenerateToken(tokenValues, tokenValues["timeoutInMinutes"].ParseTo<double>());
                ApplicationCache.Set(originalToken, updatedToken);
                tokenValues.Add("isValid", "1");
            }

            catch (Exception)
            {
                tokenValues.Add("isValid", "0");
                // throw new UnauthorizedAccessException(ex.Message,ex);
            }

            return tokenValues;
        }
    }
}
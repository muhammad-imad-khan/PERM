using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.Core.ExceptionManager;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Model.Config;

namespace Perm.Core.RequestManager;

public static class RequestHandler
{
    /// <summary>
    /// Process the request with handled exceptions. We wrapped the action with exceptions, so that we can re-use exception handling for request processor
    /// </summary>
    /// <param name="action"></param>
    /// <param name="provider"></param>
    /// <param name="serviceBase"></param>
    /// <returns></returns>
    public static async Task<ResponseModel<object>> ProcessRequestWithExceptions(Func<Task<ResponseModel<object>>> action, IServiceProvider provider, ServiceBase serviceBase)
    {
        ResponseModel<object> processRequestAsync;
        PermDataContext dataContext = provider.GetRequiredService<PermDataContext>();

        try
        {
            processRequestAsync = await action();
        }
        catch (PermArgumentException ex)
        {
            processRequestAsync = new ResponseModel<object>
            {
                Code = ex.ResponseCode,
                Message = ex.Message,
                Data = ex.OriginalException
            };
        }
        catch (PermException ex)
        {

            ResponseMessageModel responseMessageModel = dataContext.ResponseMessage.ToList().FirstOrDefault(s => s.Code == ex.ResponseCode);

            string message;
            if (responseMessageModel == null)
            {
                message = string.IsNullOrEmpty(ex.Message) ? $"Error code {ex.ResponseCode} is not defined." : ex.Message;
            }
            else
            {
                if (ex.Param == null || ex.Param.Length == 0 || ex.Param.Any(p => p == null))
                {
                    message = responseMessageModel.Message;
                }
                else
                {
                    message = string.Format(responseMessageModel.Message, ex.Param);
                }
            }

            processRequestAsync = new ResponseModel<object>
            {
                Code = ex.ResponseCode,
                Message = responseMessageModel == null ? ex.Message : message,
                Data = ex.OriginalException
            };

        }
        catch (AuthenticationException)
        {
            processRequestAsync = new ResponseModel<object>
            {
                Code = "8001",
                Message = "This service require Authorization token to validate.",
            };
        }
        catch (UnauthorizedAccessException)
        {
            processRequestAsync = new ResponseModel<object>
            {
                Code = "8002",
                Message = "You are not allowed to access requested resource."
            };
        }
        catch (Exception ex)
        {
            processRequestAsync = new ResponseModel<object>
            {
                Code = "9999",
                Message = ex.Message,
                Data = ex
            };
        }

        return processRequestAsync;
    }
}
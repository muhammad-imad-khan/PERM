using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Core.RequestManager.Processor
{
    public abstract class ServiceBase
    {
        public IServiceProvider ServiceProvider { get; set; }

        public virtual string ContentType { get; } = "application/json; charset=utf-8";

        public abstract string URL { get; }

        /// <summary>
        /// Get http method of service
        /// </summary>
        public virtual HttpMethod HttpMethod
        {
            get;
        }

        protected abstract Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel);

        public async Task<ResponseModel<T>> ProcessRequestAsync<T>(IRequestModel requestModel)
        {
            (bool isError, string message) validate = Validate(requestModel);
            if (validate.isError)
            {
                return new ResponseModel<T>
                {
                    Code = "9998",
                    Message = validate.message
                };
            }

            try
            {
                ResponseModel<T> executeBusinessLogicAsync = await ExecuteComponentAsync<T>(requestModel);
                return executeBusinessLogicAsync;
            }
            catch (Exception ex)
            {
                if (!HandleErrorBase(requestModel, ex))
                {
                    throw;
                }
            }

            return null;
        }

        protected T CastToObject<T>(IRequestModel requestModel)
        {
            if (requestModel is RequestModelBase request)
            {
                T castToObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(request.RequestBodyRaw);
                return castToObject;
            }

            T model = (T)requestModel;
            return model;
        }

        /// <summary>
        /// Override this method to handle your service level exception
        /// </summary>
        /// <param name="requestModel">Request model</param>
        /// <param name="ex">Raised exception</param>
        /// <returns></returns>
        protected virtual bool HandleError(IRequestModel requestModel, Exception ex)
        {
            return false;
        }

        /// <summary>
        /// Override this method to customize your validation for further processing
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual (bool isError, string message) Validate(IRequestModel requestModel)
        {
            return (false, string.Empty);
        }

        private bool HandleErrorBase(IRequestModel requestModel, Exception ex)
        {
            if (ex is DbUpdateException)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627)
                        throw new PermDuplicateRecordException(sqlEx);
                    else if (sqlEx.Number == 2601)
                    {
                        Match valueMatch = Regex.Match(sqlEx.Message, @"\((.*?)\)");
                        MatchCollection indexMatch = Regex.Matches(sqlEx.Message, @"\'(.*?)\'");

                        string indexName = indexMatch[1].Groups[1].ToString();
                        string value = valueMatch.Groups[1].ToString();
                        HandleError(requestModel, new PermDuplicateRecordException(sqlEx, indexName, value));
                    }
                }
            }
            if (!HandleError(requestModel, ex))
            {
                return false;
            }
            return false;
        }
    }
}
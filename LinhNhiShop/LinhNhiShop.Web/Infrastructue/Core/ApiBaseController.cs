using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LinhNhiShop.Web.Infrastructue.Core
{
    public class ApiBaseController : ApiController
    {
        private IErrorService _errorService;
        public ApiBaseController(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException dbEntityValdationEx)
            {
                foreach (var eve in dbEntityValdationEx.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(dbEntityValdationEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEntityValdationEx.InnerException.Message);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                LogError(dbUpdateEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbUpdateEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;

                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {

            }
        }
    }
}
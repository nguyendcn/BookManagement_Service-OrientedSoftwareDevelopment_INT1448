using INT1448.Shared.CommonType;
using INT1448.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace INT1448.Application.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        //private IErrorService _errorService;

        //public ApiControllerBase(IErrorService errorService)
        //{
        //    this._errorService = errorService;
        //}

        protected async Task<HttpResponseMessage> CreateHttpResponseAsync(HttpRequestMessage requestMessage, Func<Task<HttpResponseMessage>> taskHandleRequest)
        {
            Func<Task<HttpResponseMessage>> CreateHttpResponse = async () =>
            {
                HttpResponseMessage response = null;
                try
                {
                    response = await Task.Run(taskHandleRequest);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                    LogError(ex);
                    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, new NotificationResponse("false", ex.InnerException.Message));
                }
                catch (DbUpdateException dbEx)
                {
                    LogError(dbEx);
                    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, new NotificationResponse("false", dbEx.InnerException.Message));
                }
                catch(Exception ex)
                {
                    if(ex is INT1448Exception)
                    {
                        INT1448Exception appEx = (ex as INT1448Exception);
                        LogError(appEx);
                        response = requestMessage.CreateResponse((HttpStatusCode)appEx.StatusCode, new NotificationResponse("false", appEx.Message));
                    }
                    LogError(ex);
                    response = requestMessage.CreateResponse(HttpStatusCode.NotFound, new NotificationResponse("false", ex.InnerException.Message));
                }
                return response;
            };
            
            return await Task.Run(CreateHttpResponse);
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                //Error error = new Error();
                //error.CreatedDate = DateTime.Now;
                //error.Message = ex.Message;
                //error.StackTrace = ex.StackTrace;
                //_errorService.Create(error);
                //_errorService.Save();
                Debug.WriteLine(DateTime.Now + ":  " + ex.Message);
            }
            catch
            {
            }
        }
    }
}

using Financial_App.Domain.Enums;
using Financial_App.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Financial_App.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public ActionResult HandleError<T>(Response<T> response)
        {
            ObjectResult DefaultError()
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Notificacao { DetalheErro = response.DetalheErro });
            }

            return response.MotivoErro switch
            {
                MotivoErro.Conflict => Conflict(new Notificacao { DetalheErro = response.DetalheErro }),
                MotivoErro.BadRequest => BadRequest(new Notificacao { DetalheErro = response.DetalheErro }),
                MotivoErro.NotFound => NotFound(new Notificacao { DetalheErro = response.DetalheErro }),
                MotivoErro.NoContent => NoContent(),
                MotivoErro.InternalServerError => DefaultError(),
                _ => DefaultError()
            };
        }
    }
}

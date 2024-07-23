using BuildYourHead.Api.Controllers.RequestHandlers;
using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers.Core;

public class ControllerBase : Controller
{
    protected T GetRequestHandler<T>() where T : IRequestHandler => HttpContext.RequestServices.GetRequiredService<T>();
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web
{
    public class NotFountFilter<T>:IAsyncActionFilter where T:BaseEntity
    {
        private readonly IService<T> _service;

        public NotFountFilter(Core.Services.IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            var errorsViewModel = new ErrorViewModel();
            errorsViewModel.Errors.Add($"{typeof(T).Name}({id}) not fount ");
            context.Result = new RedirectToActionResult("Error", "Home", errorsViewModel);
        }
    }
}

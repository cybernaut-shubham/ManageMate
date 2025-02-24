using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageMate.Service.Validations
{
    public class RouteParameterValidationAttribute : ActionFilterAttribute
    {
        private readonly string[] _parameterNames;
        public RouteParameterValidationAttribute(params string[] parameterNames)
        {
            _parameterNames = parameterNames;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var parameterName in _parameterNames)
            {
                if (!context.ActionArguments.ContainsKey(parameterName))
                {
                    context.Result = new BadRequestObjectResult($"Missing input parameter: {parameterName}");
                    return;
                }
                var inputValue = context.ActionArguments[parameterName]?.ToString();
                if (string.IsNullOrEmpty(inputValue) || inputValue == "0")
                {
                    context.Result = new BadRequestObjectResult($"Input parameter {parameterName} cannot be 0 or empty");
                    return;
                }
                if (decimal.TryParse(inputValue, out decimal number))
                {
                    if (number < 0)
                    {
                        context.Result = new BadRequestObjectResult($"Negative values for input parameter {parameterName} not allowed.");
                        return;
                    }
                }
                else
                {
                    context.Result = new BadRequestObjectResult($"Input parameter {parameterName} only accepts numbers.");
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

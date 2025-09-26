using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace webAPI.Core
{
    public static class ApiExtensions
    {
        public static IActionResult AsClientErrors(this FluentValidation.Results.ValidationResult result)
        {
            var errors = result.Errors.Adapt<List<Core.ClientError>>();
            return new UnprocessableEntityObjectResult(new
            {
                Errors = errors
            });
        }
    }
}

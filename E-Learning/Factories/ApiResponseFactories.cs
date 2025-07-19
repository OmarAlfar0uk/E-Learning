﻿using Microsoft.AspNetCore.Mvc;
using Share.ErrorModels;
namespace E_Learning.Factories
{
    public static class ApiResponseFactories
    {
        public static IActionResult GenerateApiValidationErrorsRespons(ActionContext Context)
        {
            var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidationError()
                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                    });
            var Pesponse = new ValidationErrorToReturn()
            {
                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Pesponse);
        }
    }
}

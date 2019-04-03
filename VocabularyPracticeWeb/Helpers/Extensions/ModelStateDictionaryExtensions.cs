using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Helpers.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
		public static void AddModelIdentityErrors(this ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
		{
			foreach(var error in errors)
			{
				modelState.AddModelError(error.Code, error.Description);
			}
		}
    }
}

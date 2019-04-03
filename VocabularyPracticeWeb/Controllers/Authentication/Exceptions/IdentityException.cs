using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Controllers.Authentication
{
	public class IdentityException : ApplicationException
	{
		public IEnumerable<IdentityError> ErrorCodes { get; }

		public IdentityException(IEnumerable<IdentityError> errorCodes)
		{
			ErrorCodes = errorCodes;
		}
    }
}

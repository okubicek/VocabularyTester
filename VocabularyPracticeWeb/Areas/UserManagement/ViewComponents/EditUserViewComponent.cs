using Microsoft.AspNetCore.Mvc;
using VocabularyPracticeWeb.Models.UserMaganement;

namespace VocabularyPracticeWeb.Areas.UserManagement.ViewComponents
{
    public class EditUserViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(EditUserViewModel model)
		{
			return View("Edit", model);
		}
    }
}

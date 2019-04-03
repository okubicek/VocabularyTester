using Microsoft.AspNetCore.Mvc;
using VocabularyPracticeWeb.Models.UserMaganement;

namespace VocabularyPracticeWeb.Areas.UserManagement.ViewComponents
{
    public class RemoveUserViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(RemoveUserViewModel model)
		{
			return View("Remove", model);
		}
    }
}

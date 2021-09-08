using System.Linq;
using Clean.Core.ViewModels;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using Clean.Core.Services;

namespace Clean.Core.SurfaceControllers
{
    public class ContactController : SurfaceController
    {
        private readonly ISmtpService smtpService;

        public ContactController(ISmtpService smtpService)
        {
            this.smtpService = smtpService;
        }
        [HttpGet]
        public ActionResult RenderForm()
        {
            var model = new ContactViewModel();
            return PartialView("/Views/Partials/Contact/contactForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult RenderForm(ContactViewModel model)
        {
            return PartialView("/Views/Partials/Contact/contactForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult SubmitForm(ContactViewModel model)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                success = smtpService.SendEmail(model);
            }

            var contactPage = Umbraco.ContentAtXPath("//contact").FirstOrDefault();

            var successMessage = contactPage.Value<IHtmlString>("successMessage");
            var errorMessage = contactPage.Value<IHtmlString>("errorMessage");

            return PartialView("/Views/Partials/Contact/result.cshtml", success ? successMessage : errorMessage);
        }


    }
}

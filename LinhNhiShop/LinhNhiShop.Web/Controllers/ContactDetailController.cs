using AutoMapper;
using BotDetect.Web.Mvc;
using LinhNhiShop.Common;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using LinhNhiShop.Web.Infrastructue.Extentions;
using LinhNhiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinhNhiShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;

        public ContactDetailController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactDetailViewModel = GetContactDetailViewModel();

            return View(feedbackViewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ContactDetailCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(feedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ Website", content);

                feedbackViewModel.Name = string.Empty;
                feedbackViewModel.Email = string.Empty;
                feedbackViewModel.Message = string.Empty;
            }
            feedbackViewModel.ContactDetailViewModel = GetContactDetailViewModel();

            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetContactDetailViewModel()
        {
            var contectDetailModel = _contactDetailService.GetDefaultContact();
            return Mapper.Map<ContactDetail, ContactDetailViewModel>(contectDetailModel);
        }
    }
}
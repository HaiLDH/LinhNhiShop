using AutoMapper;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
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
        public ContactDetailController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var contectDetailModel = _contactDetailService.GetDefaultContact();
            var contectDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contectDetailModel);

            return View(contectDetailViewModel);
        }
    }
}
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Contact> contacts;
            try
            {
                ContactsApiController contactsApi = new ContactsApiController();
                contacts = contactsApi.GetContacts();
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex.Message);
                contacts = new List<Contact>();
                throw;
            }

            ViewBag.Contacts = contacts;
            return View();
        }

        public ActionResult AddContact()
        {
            return View();
        }

        public ActionResult EditContact(int Id)
        {
            Contact contacts;
            try
            {
                ContactsApiController contactsApi = new ContactsApiController();
                contacts = contactsApi.GetContact(Id);
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex.Message);
                contacts = new Contact();
                throw;
            }

            return View(contacts);
        }

    }
}

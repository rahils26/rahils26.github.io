using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContactManager.Models;
using System.Web.Http.Results;

namespace ContactManager.Controllers
{
    [RoutePrefix("Contacts")]
    public class ContactsApiController : ApiController
    {
        private ContactManagerEntities db = new ContactManagerEntities();

        [Route("GetContacts")]
        [HttpGet]
        public List<Contact> GetContacts()
        {
            return db.Contacts.ToList();
        }

        [Route("GetContact")]
        [HttpGet]
        public Contact GetContact(int Id)
        {
            return db.Contacts.Where(x => x.ID.Equals(Id)).FirstOrDefault();
        }      

        [Route("AddContact")]
        [HttpPost]
        public HttpResponseMessage AddContact(Contact contact)
        {
            var responseMessage = string.Empty;
            var success = true;

            try
            {
                var existingContact = db.Contacts.Where(x => x.Email.Equals(contact.Email) || x.PhoneNumber.Equals(contact.PhoneNumber)).FirstOrDefault();
                if (existingContact == null)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                }
                else
                {
                    responseMessage = "Contact with same Email or Phone Number already exists";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex.Message);
                responseMessage = ex.Message;
                success = false;
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { success = success, message = responseMessage });
        }

        [Route("DeleteContact")]
        [HttpDelete]
        public HttpResponseMessage DeleteContact(int Id)
        {
            var responseMessage = string.Empty;
            var success = true;

            try
            {
                var contact = GetContact(Id);
                if (contact == null)
                {
                    responseMessage = "Contact deletion failed or Contact does not exists.";
                    success = false;
                }
                else
                {
                    db.Contacts.Remove(contact);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex.Message);
                responseMessage = ex.Message;
                success = false;
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { success = success, message = responseMessage });
        }

        [Route("UpdateContact")]
        [HttpPatch]
        public HttpResponseMessage UpdateContact(Contact contact)
        {
            var responseMessage = string.Empty;
            var success = true;

            try
            {
                var existingContact = db.Contacts.Where(x => x.ID.Equals(contact.ID)).FirstOrDefault();
                if (existingContact != null)
                {
                    existingContact.FirstName = contact.FirstName;
                    existingContact.LastName = contact.LastName;
                    existingContact.Email = contact.Email;
                    existingContact.PhoneNumber = contact.PhoneNumber;
                    existingContact.ContactStatus = contact.ContactStatus;
                    db.SaveChanges();
                }
                else
                {
                    responseMessage = "Contact updation failed. please try again";
                    success = false;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex.Message);
                responseMessage = ex.Message;
                success = false;
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { success = success, message = responseMessage });
        }


    }
}
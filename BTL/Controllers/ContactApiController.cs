using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        CsdlwebContext db = new CsdlwebContext();
        [HttpGet]
        public List<Contact> GetAllContact()
        {
            return db.Contacts.ToList();
        }
        [HttpPost]
        public bool AddContact(string yourname, string youremail, string subject, string sendmessage)
        {
            Contact contact = new Contact();
            contact.Yourname = yourname;
            contact.YourEmail = youremail;
            contact.Subject = subject;
            contact.SendMessage = sendmessage;
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
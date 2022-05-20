using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using Web_API.Services;
using WebApi.Models;

namespace Web_API.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private static IContactService ContactService = new ContactService();
        public ContactController()
        {


        }

        [HttpGet("/api/{user_id}/contacts")]
        public IActionResult Get(string user_id)
        {
            return Ok(ContactService.GetContacts(user_id));

        }
        [HttpPost("/api/{user_id}/contacts")]
        public IActionResult CreateNewConversion(string id, string name, string server, string user_id)
        {
            ContactService.CreateNewContact(user_id, id, name, server);
            return Ok();
        }

        [HttpGet("api/contacts/{id}")]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = ContactService.Get(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }


        [HttpPut("api/contacts/{id}")]
        public IActionResult Edit(string id, string user_name, string password, string server)
        {
            ContactService.Edit(id, user_name, password, server);
            return Ok();
        }

        [HttpDelete("api/{user_name}/contacts/{id}")]
        public IActionResult Delete(string user_name, string id)
        {
            ContactService.Delete(user_name, id);
            return Ok();
        }

        [HttpGet("/api/{user_name}/contacts/{id}/messages")]
        public IActionResult GetMessagesFromUser(string user_name, string id)
        {
            List<Message> messages = ContactService.GetMessagesBetweenUsers(user_name, id);
            return Ok(ContactService.GetMessagesBetweenUsers(user_name, id));
        }

        [HttpPost("/api/{user_name}/contacts/{id}/messages")]
        public IActionResult SendMessage(string user_name, string id, string content)
        {
            ContactService.SendMessageToOther(user_name, id, content);
            ContactService.SendMessageToMe(user_name, id, content);
            return Ok();
        }

        [HttpGet("/api/{user_name}/contacts/{other_user_id}/messages/{message_id}")]
        public IActionResult GetSpecificMessage(string user_name, string other_user_id, string message_id)
        {
            return Ok(ContactService.GetSpecificMessage(user_name, other_user_id, message_id));
        }

        [HttpPut("/api/{user_name}/contacts/{other_user_id}/messages/{message_id}")]
        public IActionResult EditSpecificMessage(string user_name, string other_user_id, string message_id, string content)
        {
            ContactService.EditSpecificMessage(user_name, other_user_id, message_id, content);
            return Ok();
        }

        [HttpDelete("/api/{user_name}/contacts/{other_user_id}/messages/{message_id}")]
        public IActionResult DeleteSpecificMessage(string user_name, string other_user_id, string message_id)
        {
            ContactService.DeleteSpecificMessage(user_name, other_user_id, message_id);
            return Ok();
        }

        [HttpGet("/api/{user_name}/contacts/{password}")]
        public IActionResult CheckUserInDB(string user_name, string password)
        {
            if (ContactService.CheckUserInDB(user_name, password) == true)
                return Ok();
            return NotFound();
        }

        [HttpPost("/api/contacts/")]
        public IActionResult AddNewUserInDB(string user_id, string nick_name, string password, string server)
        {
            if (ContactService.CheckUserInDB(user_id, password) == false)
            {
                Contact new_contact = new Contact(user_id, nick_name, password, server);
                ContactService.GetAllContacts().Add(new_contact);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("/api/contacts")]
        public IActionResult getAllContactsInJson()
        {
            return Ok(ContactService.GetAllContacts());
        }


        [HttpPost("/api/transfer/")]
        public IActionResult Transfer([FromBody] TransferOBJ transfer)
        {
            string from = transfer.from;
            string to = transfer.to;
            string content = transfer.content;
            SendMessage(from, to, content);

            return Ok();
        }

        [HttpPost("/api/invitations/")]
        public IActionResult Inivitation([FromBody] Inivitation invite)
        {
            string from = invite.from;
            string to = invite.to;
            string server = invite.server;
            ContactService.CreateNewContact(from, to, to, server);

            return Ok();
        }
        [HttpGet("/api/contacts/servername/{user_name}")]
        public IActionResult GetUserServer(string user_name)
        {
            return Ok(ContactService.GetUserServer(user_name));
        }

    }
}


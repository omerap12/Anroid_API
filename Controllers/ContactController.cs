using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using Web_API.Services;


namespace Web_API.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private static IContactService ContactService = new ContactService();
        public ContactController()
        {
            

        }

        [HttpGet("/api/contacts")]
        public IActionResult Get()
        {
            return Ok(ContactService.GetAll());

        }

        [HttpGet("api/contacts/{id}")]
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = ContactService.Get((string)id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost("/api/contacts")]
        public IActionResult Create(string id, string name, string server)
        {
            Contact contact = new Contact(id, name);
            contact.Server = server;
            ContactService.Add(contact);
            return Ok();
        }

        [HttpDelete("/api/contacts/{id}")]
        public IActionResult Delete(string id)
        {
            ContactService.Delete(id);

            return NoContent();
        }

        [HttpPut("/api/contacts/{id}")]
        public IActionResult Update(string id,string server)
        {
            ContactService.Update(id,server);
            return NoContent();
        }

        [HttpGet("/api/contacts/{id}/messages")]
        public IActionResult Messages(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = ContactService.Get((string)id);
            if (contact == null)
            {
                return NotFound();
            }
            List<Message> messages = ContactService.GetMessages(id);
            return Ok(messages);
        }
        [HttpPost("/api/contacts/{id}/messages")]
        public IActionResult PostMessage(string? content,string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = ContactService.Get((string)id);
            if (contact == null)
            {
                return NotFound();
            }
            ContactService.AddMessage(id, content);
            return Ok();
        }

        [HttpGet("/api/contacts/{user_id}/messages/{message_id}")]
        public IActionResult GetMessageWithId_2FromId_1(string user_id, string message_id)
        {
            Message message = ContactService.GetMessageWithId_2FromId_1(user_id, message_id);
            return Ok(message);
        }

        [HttpPut("/api/contacts/{user_id}/messages/{message_id}")]
        public IActionResult UpdateMessageWithId_2FromId_1(string user_id, string message_id, string? content)
        {
            Message message = ContactService.GetMessageWithId_2FromId_1(user_id, message_id);
            if (message == null)
            {
                return NotFound();
            }
            if (content != null)
            {
                message.Content = content;
                return Ok(message);
            }
            return Ok(message);
        }
        [HttpDelete("/api/contacts/{user_id}/messages/{message_id}")]
        public IActionResult DeleteMessageWithIdFromID(string user_id, string message_id)
        {
            ContactService.DeleteMessage(user_id, message_id);
            return Ok();
        }






    }
    [HttpDelete("/api/contacts/{user_id}/messages/{message_id}")]
    public IActionResult DeleteMessageWithIdFromID(string user_id, string message_id)
    {
        ContactService.DeleteMessage(user_id, message_id);
        return Ok();
    }
}


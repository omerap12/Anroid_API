using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using Web_API.Services;
using WebApi.Models;
using WebApi.View;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace Web_API.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private static IContactService ContactService = new ContactService();
        private IHubContext<MyHub> hub;
        public ContactController(IHubContext<MyHub> hub)
        {
            this.hub = hub;

        }

        [HttpGet("/api/{user_id}/contacts")]
        public IActionResult Get(string user_id)
        {
            List< ContactFirstAPI > contacts = ContactService.GetContacts(user_id);
            return Ok(contacts);

        }

        //response need to be empty
        [HttpPost("/api/{user_id}/contacts")]
        public IActionResult CreateNewConversion(string id, string name, string server, string user_id)
        {
            ContactService.CreateNewContact(user_id, id, name, server);
            hub.Clients.All.SendAsync("InviteUpdate", id);

            //return Created("/api/{user_id}/contacts", new LessInfoContactAPI(id, name, server));
            return Created("/api/{user_id}/contacts", "");
        }

        [HttpGet("api/contacts/{user_name}/{id}")]
        public IActionResult Details(string user_name, string id)
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
            return Ok(ContactService.GetApiContact(user_name, id));
        }

        // remove the password according to the hemi's request in the exercise.
        [HttpPut("api/contacts/{id}")]
        public IActionResult Edit(string id, string user_name, string server)
        {
            ContactService.Edit(id, user_name, server);
            return NoContent();
        }

        /*[HttpPut("api/contacts/{id}")]
        public IActionResult Edit(string id, string user_name, string password, string server)
        {
            ContactService.Edit(id, user_name, password, server);
            return Ok();
        }*/

        [HttpDelete("api/{user_name}/contacts/{id}")]
        public IActionResult Delete(string user_name, string id)
        {
            ContactService.Delete(user_name, id);
            return NoContent();
        }

        // api fields are ok and no need to change them
        [HttpGet("/api/{user_name}/contacts/{id}/messages")]
        public IActionResult GetMessagesFromUser(string user_name, string id)
        {
            List<Message> messages = ContactService.GetMessagesBetweenUsers(user_name, id);
            return Ok(ContactService.GetMessagesBetweenUsers(user_name, id));
        }

        //response need to be empty
        [HttpPost("/api/{user_name}/contacts/{id}/messages")]
        public IActionResult SendMessage(string user_name, string id, string content)
        {
            ContactService.SendMessageToOther(user_name, id, content);
            ContactService.SendMessageToMe(user_name, id, content);
            hub.Clients.All.SendAsync("MessageUpdate", id);
            hub.Clients.All.SendAsync("MessageUpdate", user_name);

            return Created("/api/{user_name}/contacts/{id}/messages","");
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
            return NoContent();
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
                return Created("/api/contacts/","");
            }
            return BadRequest();
        }


        [HttpGet("/api/contacts")]
        public IActionResult getAllContactsInJson()
        {
            return Ok(ContactService.GetAllContactsAPI());
        }


        [HttpPost("/api/transfer/")]
        public IActionResult Transfer([FromBody] TransferOBJ transfer)
        {
            string from = transfer.from;
            string to = transfer.to;
            string content = transfer.content;
            SendMessage(from, to, content);
            hub.Clients.All.SendAsync("MessageUpdate",from, to);
            return Created("/api/transfer/","");
        }

        [HttpPost("/api/invitations/")]
        public IActionResult Inivitation([FromBody] Inivitation invite)
        {
            string from = invite.from;
            string to = invite.to;
            string server = invite.server;
            ContactService.AddNewFromOtherServer(from, to, server);
            hub.Clients.All.SendAsync("InviteUpdate", to);

            return Created("/api/invitations/", "");
        }


        [HttpGet("/api/contacts/servername/{user_name}")]
        public IActionResult GetUserServer(string user_name)
        {
            return Ok(ContactService.GetUserServer(user_name));
        }

    }
}


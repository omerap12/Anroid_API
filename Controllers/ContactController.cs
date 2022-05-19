﻿using Microsoft.AspNetCore.Http;
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



        /* 

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


         [HttpPost("/api/transfer")]
         public IActionResult SendNewMessage(string user_id_from, string user_id_to, string content)
         {
             ContactService.SendNewMessage(user_id_from, user_id_to, content);
             return Ok();
         }

     }*/
    }
}


﻿using System.ComponentModel.DataAnnotations.Schema;
using Web_API.Models;
namespace WebApi.Models
{
    public class Conversation
    {
        public List<Message> Messages;
        [ForeignKey("Id")]
        public string from;
        [ForeignKey("Id")]
        public string to;
        public string last;
        public string id;
        public string lastdate;
        

        public Conversation(string from, string to)
        {
            this.from = from;
            this.to = to;
            Messages = new List<Message>();
        }
        public Conversation()
        {
        }
        public Message Last { get; set; }

        public List<Message> GetAllMessages()
        {
            return Messages;
        }

        public Message Get(string id)
        {
            Message message = Messages.Find(x => x.Id == id);
            return message;
        }


        public int GetMessageLength()
        {
            return Messages.Count;
        }

        public void AddMessage(Message message)
        {
            this.last = message.Content;
            message.Id =( GetMessageLength()+1 ).ToString();
            this.Messages.Add(message);
            this.lastdate = DateTime.Now.ToString();
        }
        public bool IsMe(string name)
        {
            if (this.from == name || this.to == name)
                return true;
            return false;
        }

    }
}

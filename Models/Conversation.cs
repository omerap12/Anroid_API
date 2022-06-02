using System.ComponentModel.DataAnnotations.Schema;
using Web_API.Models;
namespace WebApi.Models
{
    public class Conversation
    {
        public List<Message> Messages { get; set; }
        //[ForeignKey("Id")]
        //public string from;
        //[ForeignKey("Id")];

        public List<Contact> Contacts { get; set; }
        //public string to;
        public string last;
        public string Id;
        public string lastdate;
        

        public Conversation(Contact from, Contact to)
        {
            Contacts = new List<Contact>() { from, to};
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
            
            if (this.Contacts[0].Id == name || this.Contacts[1].Id == name)
                return true;
            return false;
        }

    }
}

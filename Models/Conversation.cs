using Web_API.Models;
namespace WebApi.Models
{
    public class Conversation
    {
        private List<Message> Messages;
        private string from;
        private string to;
        private string last;
        

        public Conversation(string from, string to)
        {
            this.from = from;
            this.to = to;
            Messages = new List<Message>();
        }

        public List<Message> GetAllMessages()
        {
            return Messages;
        }

        public Message Get(string id)
        {
            Message message = Messages.Find(x => x.Id == id);
            return message;
        }

        public Message Last { get; set; }

        public int GetMessageLength()
        {
            return Messages.Count;
        }

        public void AddMessage(Message message)
        {
            this.last = message.Content;
            this.Messages.Add(message);
        }
        public bool IsMe(string name)
        {
            if (this.from == name || this.to == name)
                return true;
            return false;
        }

    }
}

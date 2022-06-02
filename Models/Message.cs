using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace Web_API.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public Conversation RefConversation { get; set; }
        public string ConversationId { get; set; }
        public bool Sent { get; set; }

        public Message()
        {
        }

        public Message(string value, bool sent)
        {
            Content = value;
            Created = DateTime.Now.ToString();
            Sent = sent;
        }
    }
}

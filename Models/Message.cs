using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        
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

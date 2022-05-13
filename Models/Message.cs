namespace Web_API.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        
        public bool Sent { get; set; }
        public Message(string value)
        {
            Content = value;
            Created = DateTime.Now.ToString();

        }
        public Message(string value, bool sent)
        {
            Content = value;
            Created = DateTime.Now.ToString();
            Sent = sent;

        }
    }
}

namespace WebApi.View
{
    public class MessageAPI
    {
        public string id { get; set; }
        public string content { get; set; }
        public string created { get; set; }

        public bool sent { get; set; }
        public MessageAPI(string Id, string Content, string Created, bool Sent) 
        {
            this.id = Id;
            this.content = Content;
            this.created = Created;
            this.sent = Sent;
        }
    }
}

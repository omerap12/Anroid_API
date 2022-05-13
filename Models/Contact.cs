namespace Web_API.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int ContactId { get; set; }

        public string CreatedDate { get; set; }

        public string Server { get; set; }


        public List<Message> Messages { get; set; }

        public int getMessageLength()
        {
            return this.Messages.Count;
        }

        public Message Last { get; set; }

        public Contact(string id, string name,string server, string last,string last_date)
        {
            this.Id = Id;
            this.Name = name;
            this.Messages = new List<Message>();

        }
        public Contact(string id, string name)
        {
            this.Id=id;
            this.Name=name;
            this.Messages = new List<Message>();
        }


    }
}

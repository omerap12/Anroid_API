using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace Web_API.Models
{
    public class Contact
    {
        private List<Contact> Contacts { get; set; }
        private List<Conversation> Conversations { get; set; }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public int ContactId { get; set; }

        public string CreatedDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Server { get; set; }

        public Contact(string id, string name, string password, string server)
        {
            this.Id = id;
            this.Name = name;
            this.Contacts = new List<Contact>();
            this.Password = password;
            this.Server = server;
            this.Conversations = new List<Conversation>();
        }

        public void AddContacts(Contact contact)
        {
            this.Contacts.Add(contact);
        }
        public void AddConversation(Conversation conversation)
        {
            this.Conversations.Add(conversation);
        }
        public List<Contact> GetContactsList()
        {
            return this.Contacts;
        }
        public List<Conversation> GetConversations ()
        {
            return this.Conversations;
        }
        public void Delete_user(string id)
        {
            Contacts.Remove(Contacts.Find(x => x.Id == id));
        }
        public Conversation GetSpecificConversion(string id)
        {
            Conversation conversation = Conversations.Find(x=>x.)
        }
    }
}

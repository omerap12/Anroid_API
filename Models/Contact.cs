using Web_API.Models;
using WebShop;

namespace WebApi.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Last { get; set; }
        public string Server { get; set; }
        public string Image { get; set; }
        public DateTime? LastDate { get; set; }
        public string IsContactOf { get; set; }
        public User RefUser { get; set; }

        public int GetId()
        {
            using(var d = new ItemsContext())
            {
                var maxId = d.Contacts.ToList().Max(p => p.Id);
                return maxId + 1;
            }
        }
        public Contact()
        {

        }
        public Contact(string userId, string name, string server, string last, DateTime? lastDate, string isContactOf)
        {
            Id = GetId();
            ContactId = userId;
            Name = name;
            Last = last;
            Server = server;
            Image = String.Empty;
            LastDate = lastDate;
            IsContactOf = isContactOf;
            using (var d = new ItemsContext())
            {
                RefUser = d.Users.ToList().Find(p => p.Id == userId);
            }
        }
    }
}

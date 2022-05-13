using Web_API.Models;
namespace Web_API.Services;

public class ContactService : IContactService
{

    private static List<Contact> Contacts = new List<Contact>();

    public ContactService()
    {
        Contact contact = new Contact("omer","Omer");
        Message message = new Message("Hi, how are u?");
        message.Sent = true;
        message.Id = "1";
        contact.Messages.Add(message);
        Contacts.Add(contact);

        Contact contact_two = new Contact("avital","Avital");
        Message message_two = new Message("I'm mad at you");
        message_two.Sent = false;
        message_two.Id = "2";
        contact_two.Messages.Add(message_two);
        Contacts.Add(contact_two);
    }

    /*public void Create(string user_name)
    {
        return;
    }*/

    public void Delete(int id)
    {
        return;
    }

    public void Add(Contact contact)
    {
        Contacts.Add(contact);
    }

    public void Edit(int id, string user_name, string description, int grade)
    {
        return;
    }

    public Contact Get(string id)
    {
        Contact contact = Contacts.Find(x=>x.Id == id);
        return contact;
    }

    public List<Contact> GetAll()
    {
        return Contacts;
    }

    public void Create(string name, string server)
    {
        Contact contact = new Contact(name, name);
        contact.Server = server;
        Contacts.Add(contact);

    }

    public void Delete(string id)
    {
        Contacts.Remove(Get(id));
    }

    public void Update(string name, string server)
    {
        Contact contact = Get(name);
        contact.Server = server;
        Delete(contact.Id);
        Create(contact.Name, contact.Server);
    }
    public List<Message> GetMessages(string id)
    {
        Contact contact = Get(id);
        List<Message> messages = contact.Messages;
        return messages;
    }
    public void AddMessage(string id, string content)
    {
        Contact contact = Get(id);
        Message message = new Message(content);
        int new_id = contact.Messages.Count + 1;
        message.Id = new_id.ToString();

        contact.Messages.Add(new Message(content));
        Delete(id);
        Contacts.Add(contact);

    }

    public Message GetMessageWithId_2FromId_1(string user_id, string message_id)
    {
        Contact contact = Get(user_id);
        Message message = contact.Messages.Find(x => x.Id == message_id);
        return message;
    }
    public void DeleteMessage(string user_id, string message_id)
    {
        Contact contact = Contacts.Find(x => x.Id == user_id);
        Contacts.Remove(contact);
        Message message_to_delete = contact.Messages.Find(x => x.Id == message_id);
        contact.Messages.Remove(message_to_delete);
        Contacts.Add(contact);

    }
}

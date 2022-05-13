using Web_API.Models;
using WebApi.Models;

namespace Web_API.Services;

public class ContactService : IContactService
{

    private static List<Contact> Contacts = new List<Contact>();

    public ContactService()
    {
        Contact contact = new Contact("TSM_Omer", "Omer", "12345", "TalkToMe");
        Contact contact_two = new Contact("Avitalos", "Avital", "12345", "TalkToMe");

        contact.AddContacts(contact_two);
        contact_two.AddContacts(contact);


        Conversation Conversation_one = new Conversation("TSM_Omer", "Avitalos");
        Conversation_one.AddMessage(new Message("Hi avital", true));

        Conversation Conversation_two = new Conversation("TSM_Omer", "Avitalos");
        Conversation_one.AddMessage(new Message("Hi avital", false));

        contact.AddConversation(Conversation_one);
        contact_two.AddConversation(Conversation_two);

        Contacts.Add(contact);
        Contacts.Add(contact_two);


    }

    /*public void Create(string user_name)
    {
        return;
    }*/


    public List<Contact> GetAllContacts()
    {
        return Contacts;
    }

    public void CreateNewContact(string add_to, string id, string name, string server)
    {
        Contact new_one = new Contact(id, name, "password", server);
        Contact addTo = Contacts.Find(x => x.Id == add_to);
        Conversation conversation_one = new Conversation(id, add_to);
        Conversation conversation_two = new Conversation(add_to, id);
        addTo.AddContacts(new_one);
        new_one.AddContacts(addTo);

        addTo.AddConversation(conversation_one);
        new_one.AddConversation(conversation_two);
    }

    public List<Contact> GetContacts(string user_id)
    {
        Contact contact = Contacts.Find(x => x.Id == user_id);
        return contact.GetContactsList();
    }

    public void Delete(string user_name, string id)
    {
        Contact contact = Contacts.Find(x => x.Id == user_name);
        contact.Delete_user(id);

    }

    public void Edit(string user_name, string id, string password, string server)
    {
        Contact contact = Contacts.Find(x => x.Id == user_name);
        Delete(user_name, id);
        contact.Name = id;
        contact.Password = password;
        contact.Server = server;
        Contacts.Add(contact);

    }
    public Contact Get(string id)
    {
        Contact contact = Contacts.Find(x => x.Id == id);
        return contact;
    }

    public void SendMessage(string from, string to, string content)
    {

    }

}

        /*

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

            public void AddMessage(string id, string content, bool sent)
            {
                Contact contact = Get(id);
                Message message = new Message(content, sent);

                int new_id = contact.Messages.Count + 1;
                message.Id = new_id.ToString();

                contact.Messages.Add(message);
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

            public void SendNewMessage(string user_id_from, string user_id_to, string content)
            {
                AddMessage(user_id_from, content, true);
                AddMessage(user_id_to, content, false);
            }*/
    

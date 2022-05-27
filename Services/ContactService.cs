using Web_API.Models;
using WebApi.Models;
using WebApi.View;
namespace Web_API.Services;

public class ContactService : IContactService
{

    private static List<Contact> Contacts = new List<Contact>();

    public ContactService()
    {
        Contact contact = new Contact("TSM_Omer", "Omer", "12345", "localhost:7030");
        Contact contact_two = new Contact("Avitalos", "Avital", "12345", "localhost:7030");
        Contact contact_three = new Contact("Maron", "MaronChok", "666", "localhost:7030");
        Contacts.Add(contact_three);

        contact.Last = "Hi avital";
        contact_two.Last = "Hi avital";


        contact.AddContacts(contact_two);
        contact_two.AddContacts(contact);


        Conversation Conversation_one = new Conversation("TSM_Omer", "Avitalos");
        Conversation_one.AddMessage(new Message("Hi avital", true));

        Conversation Conversation_two = new Conversation("TSM_Omer", "Avitalos");
        Conversation_two.AddMessage(new Message("Hi avital", false));

        contact.AddConversation(Conversation_one);
        contact_two.AddConversation(Conversation_two);

        Contacts.Add(contact);
        Contacts.Add(contact_two);


    }


    public List<Contact> GetAllContacts()
    {
        return Contacts;
    }

    public List<ContactFirstAPI> GetAllContactsAPI()
    {
        List<ContactFirstAPI> contacts = new List<ContactFirstAPI>();
        List <Contact> all_contacts = GetAllContacts();
        for(int i=0; i < all_contacts.Count ; i++)
        {
            contacts.Add(new ContactFirstAPI(all_contacts[i].Id, all_contacts[i].Name, all_contacts[i].Server, all_contacts[i].Last, all_contacts[i].LastDate.ToString()));
        }
        return contacts;
    }

    public void CreateNewContact(string add_to, string id, string name, string server)
    {
        //check if contact is in db already
        Contact contcat = Get(id);
        Contact request = Get(add_to);

        //need to add ne user
        if (contcat == null)
        {
            Contact new_one = new Contact(id, name, "password", server);
            Contact addTo = Contacts.Find(x => x.Id == add_to);
            Contacts.Add(new_one);
            Conversation conversation_one = new Conversation(id, add_to);
            Conversation conversation_two = new Conversation(add_to, id);
            addTo.AddContacts(new_one);
            new_one.AddContacts(addTo);
            addTo.AddConversation(conversation_one);
            new_one.AddConversation(conversation_two);
        }
        else
        {
            if (request.IsFriendOfMe(id))
            {
                return;
            }
            else
            {
                contcat.AddContacts(request);
                request.AddContacts(contcat);
                Conversation conversation_one = new Conversation(id, add_to);
                Conversation conversation_two = new Conversation(add_to, id);

                contcat.AddConversation(conversation_one);
                request.AddConversation(conversation_two);

            }

        }


    }
    public bool Check_if_friends(Contact user_name, string other_user_name)
    {
        return user_name.IsFriendOfMe(other_user_name);

    }

    public List<ContactFirstAPI> GetContacts(string user_id)
    {
        Contact contact = Contacts.Find(x => x.Id == user_id);
        List<ContactFirstAPI> contacts = contact.GetContacts();
        for (int i = 0; i < contacts.Count; i++)
        {
            contacts[i].server = GetUserServer(contacts[i].id);
        }
        return contacts;
    }


    public void Delete(string user_name, string id)
    {
        Contact contact = Contacts.Find(x => x.Id == user_name);
        contact.Delete_user(id);

    }

    public void Edit(string user_name, string id, string server)
    {
        Contact contact = Contacts.Find(x => x.Id == user_name);
        Delete(user_name, id);
        contact.Name = id;
        contact.Server = server;
        Contacts.Add(contact);

    }
    public Contact Get(string id)
    {
        Contact contact = Contacts.Find(x => x.Id == id);
        return contact;
    }
    public List<Message> GetMessagesBetweenUsers(string user_name, string id)
    {
        Contact contact = Get(user_name);
        return contact.GetMessagesFromUser(id);
    }

    public void SendMessageToOther(string from, string to, string content)
    {
        Contact contact = Get(from);
        contact.setMessage(to, content, true);
    }

    public void SendMessageToMe(string from, string to, string content)
    {
        Contact contact = Get(to);
        contact.setMessage(from, content, false);
    }
    public Message GetSpecificMessage(string user_name, string other_user_id, string message_id)
    {
        Contact contact = Get(user_name);
        return contact.GetSpecificMessageFromUser(other_user_id, message_id);

    }
    public void EditSpecificMessage(string user_name, string other_user_id, string message_id, string content)
    {
        Contact contact = Get(user_name);
        Contact contact_two = Get(other_user_id);
        contact.EditSpecificMessage(other_user_id, message_id, content);
        contact_two.EditSpecificMessage(user_name, message_id, content);
    }
    public void DeleteSpecificMessage(string user_name, string other_user_id, string message_id)
    {
        Contact contact = Get(user_name);
        Contact contact_two = Get(other_user_id);
        contact.DeleteSpecificMessage(other_user_id, message_id);
        contact_two.DeleteSpecificMessage(user_name, message_id);
    }

    public bool CheckUserInDB(string user_name, string password)
    {
        Contact contact = Get(user_name);
        if (contact == null)
            return false;
        return true;
    }

    public string GetUserServer(string user_name)
    {
        Contact contact = Get(user_name);
        return contact.Server;
    }

    public ContactFirstAPI GetApiContact(string username, string id)
    {
        Contact contact = Get(username);
        return new ContactFirstAPI(contact.Id, contact.Name, contact.Server, contact.GetLast(id), contact.GetLastDate(id));
    }
    public void AddNewFromOtherServer(string from, string to, string server)
    {
        Contact contcat = Get(to);
        Contact request = Get(from);

        //need to add ne user
        if (request == null)
        {
            Contact new_one = new Contact(from, from, "password", server);
            Contacts.Add(new_one);
            Conversation conversation_one = new Conversation(from, to);
            Conversation conversation_two = new Conversation(to,from);
            contcat.AddContacts(new_one);
            new_one.AddContacts(contcat);
            contcat.AddConversation(conversation_one);
            new_one.AddConversation(conversation_two);
        }
    }
}


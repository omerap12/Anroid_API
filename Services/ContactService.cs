using Web_API.Models;
using WebApi.Models;
using WebApi.View;
using WebShop;


namespace Web_API.Services;

public class ContactService : IContactService
{

    private static List<Contact> Contacts = new List<Contact>();

    public ContactService()
    {
    }



    public List<Contact> GetAllContacts()
    {
        return Contacts;
    }

    public List<ContactFirstAPI> GetAllContactsAPI()
    { 
        using (var database = new ItemsContext())
        {
            var contacts = database.Contacts.ToList();
            List<ContactFirstAPI> contactsAPI = new List<ContactFirstAPI>();
            for (int i = 0; i < contacts.Count; i++)
            {
                contactsAPI.Add(new ContactFirstAPI(contacts[i].Id, contacts[i].Name, contacts[i].Server, contacts[i].Last, contacts[i].LastDate.ToString()));
            }
            return contactsAPI;
        }
        
    }

    public void CreateNewContact(string add_to, string id, string name, string server)
    {
        //check if contact is in db already
        Contact fromContact = Get(id);
        Contact toContact = Get(add_to);

        //need to add ne user
        if (fromContact == null)
        {
            Contact new_one = new Contact(id, name, "password", server);
            Contact addTo = Contacts.Find(x => x.Id == add_to);
            Contacts.Add(new_one);
            Conversation conversation_one = new Conversation(fromContact, toContact);
            Conversation conversation_two = new Conversation(toContact, fromContact);
            addTo.AddContacts(new_one);
            new_one.AddContacts(addTo);
            addTo.AddConversation(conversation_one);
            new_one.AddConversation(conversation_two);
        }
        else
        {
            if (toContact.IsFriendOfMe(id))
            {
                return;
            }
            else
            {
                fromContact.AddContacts(toContact);
                toContact.AddContacts(fromContact);
                Conversation conversation_one = new Conversation(fromContact, toContact);
                Conversation conversation_two = new Conversation(toContact, fromContact);

                fromContact.AddConversation(conversation_one);
                toContact.AddConversation(conversation_two);

            }

        }


    }
    public bool Check_if_friends(Contact user_name, string other_user_name)
    {
        return user_name.IsFriendOfMe(other_user_name);

    }

    /*public List<Contact> GetContacts()
   {
       using (var database = new MainContext())
       {
           var contacts = database.Contacts.ToList();
       }
   }*/

    public List<ContactFirstAPI> GetContacts(string user_id)
    {
        using (var database = new ItemsContext())
        {
            var contacts = database.Contacts.ToList();
            Contact contact = contacts.Find(x => x.Id == user_id);
            List<ContactFirstAPI> c = contact.GetContacts();
            for (int i = 0; i < c.Count; i++)
            {
                c[i].server = GetUserServer(contacts[i].Id);
            }
            return c;
        }
        
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
        //user does not exist
        if (contact == null)
            return false;
        if (contact.Password == password && contact.Id == user_name)
        {
            return true;
        }
        return false;
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
        Contact toContact = Get(to);
        Contact fromCotnact = Get(from);

        //need to add ne user
        if (fromCotnact == null)
        {
            Contact new_one = new Contact(from, from, "password", server);
            Contacts.Add(new_one);
            Conversation conversation_one = new Conversation(fromCotnact, toContact);
            Conversation conversation_two = new Conversation(toContact, fromCotnact);
            toContact.AddContacts(new_one);
            new_one.AddContacts(toContact);
            toContact.AddConversation(conversation_one);
            new_one.AddConversation(conversation_two);
        }
    }
}


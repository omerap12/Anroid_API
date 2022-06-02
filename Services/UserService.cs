using Web_API.Models;
using WebApi.Models;
using WebApi.View;
using WebShop;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Services;

public class UserService
{


    public UserService()
    {
    }



    public List<User> GetAllContacts()
    {
        using (var d = new ItemsContext())
        {
            return d.Users.ToList();
        }
    }

    public void Add(User u)
    {
        using(var d = new ItemsContext())
        {
            d.Users.Add(u);
            d.SaveChanges();
        }
    }

    public List<ContactFirstAPI> GetAllContactsAPI()
    { 
        using (var database = new ItemsContext())
        {
            var contacts = database.Contacts.ToList();
            List<ContactFirstAPI> contactsAPI = new List<ContactFirstAPI>();
            for (int i = 0; i < contacts.Count; i++)
            {
                contactsAPI.Add(new ContactFirstAPI(contacts[i].ContactId, contacts[i].Name, contacts[i].Server, contacts[i].Last, contacts[i].LastDate.ToString()));
            }
            return contactsAPI;
        }
        
    }

    public void CreateNewContact(string add_to, string id, string name, string server)
    {
        //check if contact is in db already
        User fromUser = Get(id);
        User toUser = Get(add_to);

        using (var d = new ItemsContext())
        {
            User new_one = new User(id, name, "password", server);
            User addTo = d.Users.ToList().Find(x => x.Id == add_to);

            Contact newOneContact = new Contact(id, name, server, null, null, add_to);
            Contact secondContact = new Contact(id, addTo.Name, addTo.Server, null, null, add_to);
            //need to add ne user
            if (fromUser == null)
            {
                var addToContact = addTo.Contacts;
                d.Users.Add(new_one);
                Conversation conversation_one = new Conversation(fromUser, toUser);
                Conversation conversation_two = new Conversation(toUser, fromUser);
                addTo.AddContacts(newOneContact);
                new_one.AddContacts(secondContact);
                addTo.AddConversation(conversation_one);
                new_one.AddConversation(conversation_two);
                d.SaveChanges();
            }
            else
            {
                if (toUser.IsFriendOfMe(id))
                {
                    return;
                }
                else
                {
                    fromUser.AddContacts(newOneContact);
                    toUser.AddContacts(secondContact);
                    Conversation conversation_one = new Conversation(fromUser, toUser);
                    Conversation conversation_two = new Conversation(toUser, fromUser);

                    fromUser.AddConversation(conversation_one);
                    toUser.AddConversation(conversation_two);

                }

            }
        }


    }
    public bool Check_if_friends(User user_name, string other_user_name)
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
            var users = database.Users.Include(p=>p.Contacts).ToList();
            User user = users.Find(x => x.Id == user_id);
            List<ContactFirstAPI> c = user.GetContacts();
            for (int i = 0; i < c.Count; i++)
            {
                c[i].server = GetUserServer(users[i].Id);
            }
            return c;
        }
        
    }


    public void Delete(string user_name, string id)
    {
        using (var d = new ItemsContext())
        {
            User user = d.Users.ToList().Find(x => x.Id == user_name);
            var contactToRemove = user.Contacts.Find(t => t.ContactId == id);
            d.Users.Remove(user);
            d.SaveChanges();
        }

    }

    public void Edit(string user_name, string id, string server)
    {
        using (var d = new ItemsContext())
        {
            User user = d.Users.ToList().Find(x => x.Id == user_name);
            Delete(user_name, id);
            d.Users.Add(user);
            d.SaveChanges();
        }

    }
    public User Get(string id)
    {
        using (var d = new ItemsContext())
        {
            User contact = d.Users.ToList().Find(x => x.Id == id);
            return contact;
        }
    }
    public List<Message> GetMessagesBetweenUsers(string user_name, string id)
    {
        User contact = Get(user_name);
        return contact.GetMessagesFromUser(id);
    }

    public void SendMessageToOther(string from, string to, string content)
    {
        User contact = Get(from);
        contact.setMessage(to, content, true);
    }

    public void SendMessageToMe(string from, string to, string content)
    {
        User contact = Get(to);
        contact.setMessage(from, content, false);
    }
    public Message GetSpecificMessage(string user_name, string other_user_id, string message_id)
    {
        User contact = Get(user_name);
        return contact.GetSpecificMessageFromUser(other_user_id, message_id);

    }
    public void EditSpecificMessage(string user_name, string other_user_id, string message_id, string content)
    {
        User contact = Get(user_name);
        User contact_two = Get(other_user_id);
        contact.EditSpecificMessage(other_user_id, message_id, content);
        contact_two.EditSpecificMessage(user_name, message_id, content);
    }
    public void DeleteSpecificMessage(string user_name, string other_user_id, string message_id)
    {
        User contact = Get(user_name);
        User contact_two = Get(other_user_id);
        contact.DeleteSpecificMessage(other_user_id, message_id);
        contact_two.DeleteSpecificMessage(user_name, message_id);
    }

    public bool CheckUserInDB(string user_name, string password)
    {
        User contact = Get(user_name);
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
        User contact = Get(user_name);
        return contact.Server;
    }

    public ContactFirstAPI GetApiContact(string username, string id)
    {
        User contact = Get(username);
        return new ContactFirstAPI(contact.Id, contact.Name, contact.Server, contact.GetLast(id), contact.GetLastDate(id));
    }
    public void AddNewFromOtherServer(string from, string to, string server)
    {
        using (var d = new ItemsContext())
        {
            var toUser = d.Users.ToList().Find(x => x.Id == to);
            User toContact = Get(to);
            User fromCotnact = Get(from);

            //need to add ne user
            if (fromCotnact == null)
            {
                User new_one = new User(from, from, "password", server);
                d.Users.Add(new_one);
                Conversation conversation_one = new Conversation(fromCotnact, toContact);
                Conversation conversation_two = new Conversation(toContact, fromCotnact);
                toContact.AddContacts(new Contact(from, from, server, null, null, to));
                new_one.AddContacts(new Contact(to, to, "localhost:7030", null, null, from));
                toContact.AddConversation(conversation_one);
                new_one.AddConversation(conversation_two);
            }
        }
    }
}


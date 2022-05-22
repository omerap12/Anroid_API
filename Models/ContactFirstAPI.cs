namespace WebApi.Models
{
    public class ContactFirstAPI
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
        public string last { get; set; }
        public string lastdate { get; set; }
       
        public ContactFirstAPI(string id, string name, string server, string last, string lastdate)
        {
            this.id = id;
            this.name = name;
            this.server = server;
            this.last = last;
            this.lastdate = lastdate;

        }
        
    }
}

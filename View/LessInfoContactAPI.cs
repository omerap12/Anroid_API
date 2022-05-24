
namespace WebApi.View
{
    public class LessInfoContactAPI
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }

        public LessInfoContactAPI(string id, string name, string server)
        {
            this.id = id;
            this.name = name;
            this.server = server;
        }
    }
}
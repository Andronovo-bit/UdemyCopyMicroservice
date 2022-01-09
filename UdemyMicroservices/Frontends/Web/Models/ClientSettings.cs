namespace Web.Models
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientForUser { get; set; }
    }

    public class Client
    {
        public string CliendId { get; set; }
        public string ClientSecret { get; set; }
    }
}

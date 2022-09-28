namespace AuthAPI.Services.DB
{
    public class DbConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string GetConnectionString()
        {
            return $"server={Host}; port={Port}; database={Name}; user={Username}; password={Password}";
        }
    }
}

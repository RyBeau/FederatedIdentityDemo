namespace AuthAPI.Services.Redis
{
    public class RedisConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public bool Ssl { get; set; }
        
        public string GetConnectionString()
        {
            return $"{Host}:{Port},ssl={Ssl},name={Name}";
        }
    }
}

using System;

namespace Framework
{
    public class ServiceProviderSettings : IServiceProviderSettings
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }


        public ServiceProviderSettings()
        {
        }

        public ServiceProviderSettings(string hostname, UInt16 port = 80)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            
            Hostname = hostname;
            Port = port;
        }

        public Uri BuildUri()
        {
            var uri = new UriBuilder(Hostname)
            {
                Port = Port
            };

            if (string.IsNullOrWhiteSpace(Username))
                uri.UserName = Username;
            if (string.IsNullOrWhiteSpace(Password))
                uri.Password = Password;

            return uri.Uri;
        }
    }
}

using System;

namespace Framework
{
    public interface IServiceProviderSettings
    {
        string Hostname { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Prefix { get; set; }

        Uri BuildUri();
    }
}
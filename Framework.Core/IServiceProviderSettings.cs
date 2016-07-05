using System;

namespace Framework.Core
{
    public interface IServiceProviderSettings
    {
        string Hostname { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Protocol { get; set; }

        Uri BuildUri(string path = null);
    }
}
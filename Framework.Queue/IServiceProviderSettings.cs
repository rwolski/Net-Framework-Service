using System;

namespace Framework
{
    public interface IServiceProviderSettings
    {
        string Hostname { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }

        Uri BuildUri();
    }
}
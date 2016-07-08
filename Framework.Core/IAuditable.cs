using System;

namespace Framework.Core
{
    public interface IEquals
    {
        bool Equals(object obj);

        int GetHashCode();
    }

    public interface IAuditable
    {
        DateTime? CreatedDateTime { get; }

        DateTime? UpdatedDateTime { get; }

        int Version { get; }
    }
}

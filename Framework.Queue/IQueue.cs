using System;

namespace Framework.Queue
{
    public interface IQueue<T> : IDisposable
    {
        void Send(T message);

        void Publish(T message);

        T Receive();

        void Consume();
    }
}

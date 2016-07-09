using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    //public abstract class MessageRequestHandler<TArgs> : IMessageRequestHandler<TArgs>
    //{
    //    public TArgs Arguments { get; protected set; }

    //    public MessageRequestHandler(TArgs args)
    //    {
    //        Arguments = args;
    //    }

    //    public abstract Task<IMessageContract> Respond(TArgs args);
    //}

    public interface ITestEntity : IMessageContract
    {
        int Id { get; }
        string Name { get; }
    }

    public class TestEntity : ITestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //public interface ITestRequestById
    //{
    //    int Id { get; }
    //}

    //public interface ITestRequestByName
    //{
    //    string Name { get; }
    //}

    //public class TestRequestById : ITestRequestById
    //{
    //    public int Id { get; set; }
    //}

    //public class TestRequestByName : ITestRequestByName
    //{
    //    public string Name { get; set; }
    //}



    //public interface ITestRequest : IMessageRequest<ITestEntity>
    //{
    //    int Id { get; }
    //}

    //public class TestRequest : ITestRequest
    //{
    //    public int Id { get; set; }
    //}

    //public interface ITestRequestHandler : IMessageRequestHandler<ITestRequest>
    //{
    //}

    //public class TestRequestHandler : ITestRequestHandler
    //{
    //    public Task<object> Request(ITestRequest request)
    //    {
    //        ITestEntity entity = new TestEntity()
    //        {
    //            Id = request.Id,
    //            Name = "Ryan"
    //        };
    //        return Task.FromResult((object)entity);
    //    }
    //}

    public interface ITestRequest : IMessageRequest<ITestEntity>
    {
        int Id { get; }
    }

    public class TestRequest : ITestRequest
    {
        public int Id { get; set; }
    }

    public interface ITestRequest1 : IMessageRequest<ITestEntity>
    {
        string Name { get; }
    }

    public class TestRequest1 : ITestRequest1
    {
        public string Name { get; set; }
    }

    public interface ITestRequestHandler<TReq> : IMessageRequestHandler<TReq>
    {
    }

    public class TestRequestHandler : ITestRequestHandler<ITestRequest>, ITestRequestHandler<ITestRequest1>
    {
        public Task<object> Request(ITestRequest request)
        {
            ITestEntity entity = new TestEntity()
            {
                Id = request.Id,
                Name = "Ryan"
            };
            return Task.FromResult((object)entity);
        }

        public Task<object> Request(ITestRequest1 request)
        {
            ITestEntity entity = new TestEntity()
            {
                Id = 14,
                Name = request.Name
            };
            return Task.FromResult((object)entity);
        }
    }



    //public interface ITestRequestHandler<TArgs> : IMessageRequestHandler<TArgs, ITestEntity>
    //{
    //}

    //public class TestMessageRequestHandler : ITestRequestHandler<ITestRequestById>, ITestRequestHandler<ITestRequestByName>
    //{
    //    public TestMessageRequestHandler()
    //    {
    //    }

    //    public Task<ITestEntity> Request(ITestRequestById args)
    //    {
    //        ITestEntity entity = new TestEntity()
    //        {
    //            Id = 1,
    //            Name = "Ryan"
    //        };
    //        return Task.FromResult(entity);
    //    }

    //    public Task<ITestEntity> Request(ITestRequestByName args)
    //    {
    //        ITestEntity entity = new TestEntity()
    //        {
    //            Id = 2,
    //            Name = "Chappy"
    //        };
    //        return Task.FromResult(entity);
    //    }
    //}


    //    public TArgs Arguments { get; protected set; }

    //    public MessageRequestHandler(TArgs args)
    //    {
    //        Arguments = args;
    //    }

    //    public abstract Task<IMessageContract> Respond(TArgs args);
    //}
}
using Autofac;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Autofac.Module
    {
        //public interface IDoubleMeRequest : IMessageRequest
        //{
        //    int Val { get; }
        //}

        //public class DoubleMeRequest : IDoubleMeRequest
        //{
        //    public int Val { get; set; }
        //}

        //public interface IDoubleMeResponse : IMessageResponse<IDoubleMeRequest>
        //{
        //    int Val { get; }
        //}

        //public class DoubleMeResponse : IDoubleMeResponse
        //{
        //    public int Val { get; set; }

        //    public DoubleMeResponse()
        //    {
        //    }
        //}


        //public class DoubleMeRequestAction : MessageRequestHandler<IDoubleMeRequest>
        //{
        //    public DoubleMeRequestAction(IDoubleMeRequest contract)
        //        : base(contract)
        //    {
        //    }

        //    public override Task<IMessageResponse<IDoubleMeRequest>> Response()
        //    {
        //        var result = new DoubleMeResponse() { Val = Request.Val * 2 };
        //        return Task.FromResult<IMessageResponse<IDoubleMeRequest>>(result);
        //    }
        //}

        //public class DoubleMeResponseAction : MessageAction<IDoubleMeResponse>
        //{
        //    public DoubleMeResponseAction(IDoubleMeContract contract)
        //        : base(contract)
        //    {
        //    }

        //    public override Task Action()
        //    {
        //        _test1Action = Contract.Val;
        //        return Task.FromResult(0);
        //    }
        //}

        //void RegisterType1(ContainerBuilder builder)
        //{
        //    // Register the generic message consumer
        //    builder.RegisterGeneric(typeof(MessageConsumer<>)).AsSelf();

        //    // Register all message types derived from IServiceData
        //    var consumerTypes = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(s => s.GetTypes())
        //        .Where(p => typeof(IServiceEvent).IsAssignableFrom(p) && !p.IsAbstract);

        //    foreach (var type in consumerTypes)
        //    {
        //        builder.RegisterType(type).AsImplementedInterfaces().InstancePerRequest();
        //        builder.RegisterType(typeof(MessageConsumer<>).MakeGenericType(type));
        //    }
        //}

        void RegisterType2(ContainerBuilder builder)
        {
            var assemblyTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes());

            // Get all the contract types derives from the contract interface
            var contractTypes = assemblyTypes
                .Where(p => typeof(IMessageContract).IsAssignableFrom(p) && p.IsAbstract && p != typeof(IMessageContract));

            foreach (var type in contractTypes)
            {
                // Register a consumer for the contract itself
                builder.RegisterType(typeof(ContractMessageConsumer<>).MakeGenericType(type)).InstancePerLifetimeScope();

                var genericActionType = typeof(IMessageAction<>).MakeGenericType(type);
                var actionType = assemblyTypes
                    .Where(r => genericActionType.IsAssignableFrom(r))
                    .FirstOrDefault();

                if (actionType != null)
                    builder.RegisterType(actionType).As(genericActionType);
                //else
                    //throw new InvalidOperationException("Message contract provided without consuming action");
            }


            builder.RegisterType<RequestMessageConsumer<ITestRequest>>().InstancePerLifetimeScope();
            builder.RegisterType<RequestMessageConsumer<ITestRequest1>>().InstancePerLifetimeScope();
            builder.RegisterType<TestRequestHandler>().As<IMessageRequestHandler<ITestRequest>>();
            builder.RegisterType<TestRequestHandler>().As<IMessageRequestHandler<ITestRequest1>>();

            //builder.RegisterType<RequestMessageConsumer<ITestRequestById>>().InstancePerLifetimeScope();
            //builder.RegisterType<RequestMessageConsumer<ITestRequestByName>>().InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(IMessageRequestHandler<,>));

            //var t1 = typeof(IMessageRequestHandler<,>).MakeGenericType(typeof(ITestRequestById), typeof(ITestEntity));
            //builder.RegisterType<TestMessageRequestHandler>().As(t1);
            //builder.RegisterType<TestMessageRequestHandler>().As<IMessageRequestResponse<ITestEntity>>();
            //builder.RegisterType<TestMessageRequestHandler>().As<IMessageRequestArgs<ITestRequestById>>();



            //builder.RegisterType<TestMessageRequestHandler>().As<IMessageRequestHandler<ITestRequestById, IMessageContract>>();

            //var requestTypes = assemblyTypes
            //    .Where(p => typeof(IMessageRequest).IsAssignableFrom(p) && p.IsAbstract && p != typeof(IMessageRequest));

            //foreach (var type in requestTypes)
            //{
            //    // Register a consumer for the contract itself
            //    builder.RegisterType(typeof(RequestMessageConsumer<>).MakeGenericType(type)).InstancePerLifetimeScope();

            //    var genericActionType = typeof(IMessageRequestHandler<>).MakeGenericType(type);
            //    var actionType = assemblyTypes
            //        .Where(r => genericActionType.IsAssignableFrom(r))
            //        .FirstOrDefault();

            //    if (actionType != null)
            //        builder.RegisterType(actionType).As(genericActionType);
            //    //else
            //    //throw new InvalidOperationException("Message request provided without consuming response");
            //}

            //builder.RegisterType<DoubleMeResponse>().As<IMessageResponse<IDoubleMeRequest>>();


            //var contractTypes = assemblyTypes
            //    .Where(p => typeof(IMessageContract).IsAssignableFrom(p) && p.IsAbstract && p != typeof(IMessageContract));
            //var requestTypes = assemblyTypes
            //    .Where(p => typeof(IMessageRequest).IsAssignableFrom(p) && p.IsAbstract && p != typeof(IMessageRequest));
            //var types = contractTypes.Concat(requestTypes);

            //foreach (var type in types)
            //{
            //    // Register a consumer for the contract itself
            //    builder.RegisterType(typeof(ContractMessageConsumer<>).MakeGenericType(type)).InstancePerLifetimeScope();

            //    var genericActionType = typeof(IMessageAction<>).MakeGenericType(type);
            //    var actionType = assemblyTypes
            //        .Where(r => genericActionType.IsAssignableFrom(r))
            //        .FirstOrDefault();

            //    if (actionType != null)
            //        builder.RegisterType(actionType).As(genericActionType);
            //}

            // OR register the explicity types within the API level

            // Consumers
            //builder.RegisterType<ContractMessageConsumer<ITestContract1>>();
            //builder.RegisterType<ContractMessageConsumer<ITestContract2>>();

            // Contract actions
            //builder.RegisterType<ServiceContractAction1>().As<IMessageAction<ITestContract1>>();
            //builder.RegisterType<ServiceContractAction2>().As<IMessageAction<ITestContract1>>();
        }

        protected override void Load(ContainerBuilder builder)
        {
            //RegisterType1(builder);

            RegisterType2(builder);

        }
    }
}
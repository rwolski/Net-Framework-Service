using Autofac;
using MassTransit;
using System;

namespace Framework.Queue
{
    public class QueueModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(context =>
            //{
            //    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            //    {
            //        var host = cfg.Host(new Uri("localhost"), h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });
            //    });

            //    return busControl.GetSendEndpoint(new Uri("blah"));
            //}).SingleInstance().As<IBusControl>().As<IBus>();

            builder.RegisterConsumers(typeof(QueueModule).Assembly);
        }
    }
}
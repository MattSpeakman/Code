using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Device.Gpio;

namespace DotNetRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Hello World!");
            using(var robot = host.Services.GetRequiredService<IRobot>())
            {
                robot.Forwards();
                Thread.Sleep(1000);

                robot.Backwards();
                Thread.Sleep(1000);
                
                robot.Right();
                Thread.Sleep(1000);
                
                robot.Left();
                Thread.Sleep(1000);
                
                robot.Stop();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb => 
                {
                    cb.Register(context => new GpioController(PinNumberingScheme.Logical)).InstancePerDependency();
                    cb.RegisterType<GpioControllerWrapper>().As<IGpioControllerWrapper>().InstancePerDependency();
                    cb.RegisterType<Robot>().As<IRobot>().InstancePerLifetimeScope();
                }));
        }
    }
}

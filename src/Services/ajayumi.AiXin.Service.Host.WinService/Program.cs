using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Nancy;

namespace ajayumi.AiXin.Service.Host.WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(x =>
            {
                //x.UseNLog();
                x.Service<SampleService>(s =>
                {
                    s.ConstructUsing(settings => new SampleService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.WithNancyEndpoint(x, c =>
                    {
                        c.AddHost(port: 3333);
                        c.CreateUrlReservationsOnInstall();
                        c.OpenFirewallPortsOnInstall(firewallRuleName: "AiXin.Service");
                    });
                });

                x.StartAutomatically();
                x.SetServiceName("AiXin.Service");
                x.SetDisplayName("AiXin.Service");
                x.SetDescription("AiXin Windows Service");
                x.RunAsLocalSystem();
            });

            host.Run();
        }
    }
}

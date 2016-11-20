using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ajayumi.AiXin.Server.Host.WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(x =>
            {
                x.Service<ServiceManager>(s =>
                {
                    s.ConstructUsing(settings => new ServiceManager());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.StartAutomatically();
                x.SetServiceName("AiXin.SuperSocket.Service");
                x.SetDisplayName("AiXin.SuperSocket.Service");
                x.SetDescription("AiXin SuperSocket Windows Service");
                x.RunAsLocalSystem();
            });

            host.Run();
        }
    }
}

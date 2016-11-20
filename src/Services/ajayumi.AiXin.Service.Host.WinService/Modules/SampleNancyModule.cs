using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.Service.Host.WinService.Modules
{
    public class SampleNancyModule : NancyModule
    {
        public SampleNancyModule()
        {
            Get["/"] = _ => "Hello ajayumi";
            Get["/status"] = _ => "I am alive";
        }
    }
}

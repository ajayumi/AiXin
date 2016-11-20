using ajayumi.develop.CSharp.Logger;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.Server.Host.WinService
{
    public class ServiceManager
    {
        private static IBootstrap m_Bootstrap = null;

        public ServiceManager()
        {
            m_Bootstrap = BootstrapFactory.CreateBootstrap();

            if (!m_Bootstrap.Initialize())
            {
                LoggerManager.Log(ELoggerType.Error, "SuperSocket 初始化失败");
                return;
            }
        }

        public bool Start()
        {
            m_Bootstrap.Start();
            return true;
        }

        public bool Stop()
        {
            m_Bootstrap.Stop();
            return true;
        }
    }
}

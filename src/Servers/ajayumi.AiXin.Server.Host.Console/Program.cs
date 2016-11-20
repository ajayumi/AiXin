#region Copyright
//************************************************************************************/
//	创建人员(Creator)    ：ajayumi
//	创建日期(Date)       ：2016-11-20
//  联系方式(Email)      ：aj-ayumi@163.com; gajayumi@gmail.com;
//  描    述(Description)：
//
//	Copyright(C) 2009-2016 ajayumi.All rights reserved.
//************************************************************************************/
#endregion

using ajayumi.AiXin.BizManager;
using ajayumi.develop.CSharp.Logger;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysConsole = System.Console;

namespace ajayumi.AiXin.Server.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bootstrap = BootstrapFactory.CreateBootstrap();

                if (!bootstrap.Initialize())
                {
                    LoggerManager.Log(ELoggerType.Error, "初始化失败");
                    sysConsole.Read();
                    return;
                }

                var result = bootstrap.Start();

                LoggerManager.Log(ELoggerType.Info, "启动状态: {0}!", result);

                if (result == StartResult.Failed)
                {
                    LoggerManager.Log(ELoggerType.Warning, "启动失败");
                    sysConsole.Read();
                    return;
                }

                LoggerManager.Log(ELoggerType.Info, "按 'q' 键停止");

                while (sysConsole.ReadKey().KeyChar != 'q')
                {
                    continue;
                }

                //Stop the appServer
                bootstrap.Stop();

                LoggerManager.Log(ELoggerType.Info, "服务停止");

            }
            catch (Exception ex)
            {
                LoggerManager.Log(ELoggerType.Error, ex.Message);
            }
        }
    }
}

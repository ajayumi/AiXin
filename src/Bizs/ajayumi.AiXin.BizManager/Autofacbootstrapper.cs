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

using ajayumi.Platform.Core.Security;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ajayumi.AiXin.BizManager
{
    public static class AutofacBootstrapper
    {
        private static IContainer m_Container = null;
        static AutofacBootstrapper()
        { Init(); }

        private static void Init()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "autofac.json");

                var config = new ConfigurationBuilder();
                config.AddJsonFile(path);

                // Register the ConfigurationModule with Autofac.
                var module = new ConfigurationModule(config.Build());
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);
                builder.Register<CryptoProvider>(o => ProviderFactory.CreateCryptoProvider());

                m_Container = builder.Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static T Resolve<T>()
        {
            return m_Container.Resolve<T>();
        }

    }
}

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ajayumi.AiXin.Infrastructure
{
    /// <summary>
    /// 应用程序配置管理类
    /// </summary>
    public static class AppConfiger
    {
        private static readonly string m_ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appConfig.xml");

#if DEBUG
        private const bool FORCE_INIT_CONFIG = true;    //强制初始化配置
#else
        private const bool FORCE_INIT_CONFIG = false;    //强制初始化配置
#endif

        public static AppConfig AppCfg { get; private set; }

        static AppConfiger()
        {
            if (!File.Exists(m_ConfigPath) || FORCE_INIT_CONFIG)
            {
                InitConfig();
            }

            LoadConfig();
        }

        private static void InitConfig()
        {
            AppConfig config = new AppConfig()
            {
                AppName = "蕨菜",
                AppTheme = "BaseLight",
                //AppTheme = "BaseDark",
                AppAccent = "Blue"
            };
            config.CC_Name = "SuperSocket02";
            config.CC_Cfgs = new List<CC_Config>() {
                new CC_Config() { Name="SuperSocket01", ServerAddr = "127.0.0.1", ServerPort = 33001, Mode = CC_Config.EMode.SuperSocket },
                new CC_Config() { Name="SuperSocket02", ServerAddr = "ajayumi.JueCai.com", ServerPort = 33001, Mode = CC_Config.EMode.SuperSocket },
            };
            AppCfg = config;

            Save();
        }

        private static void LoadConfig()
        {
            XDocument doc = XDocument.Load(m_ConfigPath);
            AppCfg = doc.Descendants("AppConfig").Select(o => new AppConfig()
            {
                AppName = o.Element("AppName")?.Value,
                AppTheme = o.Element("AppTheme")?.Value,
                AppAccent = o.Element("AppAccent")?.Value,
                CC_Name = o.Element("CC_Name")?.Value
            }).SingleOrDefault();
            AppCfg.CC_Cfgs = doc.Descendants("CC_Config").Select(o => new CC_Config()
            {
                Name = o.Element("Name")?.Value,
                ServerAddr = o.Element("ServerAddr")?.Value,
                ServerPort = int.Parse(o.Element("ServerPort")?.Value),
                Mode = (CC_Config.EMode)Enum.Parse(typeof(CC_Config.EMode), o.Element("Mode")?.Value),
                MaxRequestLength = int.Parse(o.Element("MaxRequestLength")?.Value)
            }).ToList();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
            using (StreamWriter sw = new StreamWriter(m_ConfigPath, false))
            {
                serializer.Serialize(sw, AppCfg);
            }
        }

    }

    /// <summary>
    /// 应用程序配置类
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string AppTheme { get; set; }

        /// <summary>
        /// 风格
        /// </summary>
        public string AppAccent { get; set; }

        public string CC_Name { get; set; }

        public List<CC_Config> CC_Cfgs { get; set; }

        public CC_Config this[string name]
        {
            get { return this.CC_Cfgs.FirstOrDefault(o => o.Name.Equals(CC_Name)); }
        }

        public CC_Config CurrCC_Cfg
        {
            get
            {
                return this[this.CC_Name];
            }
        }

        public CC_Config.EMode CurrCCCfgMode
        {
            get
            {
                return this.CurrCC_Cfg.Mode;
            }
        }
    }

    /// <summary>
    /// CC配置类
    /// </summary>
    public class CC_Config
    {
        public string Name { get; set; }

        /// <summary>
        /// 通信服务器地址
        /// </summary>
        public string ServerAddr { get; set; }

        /// <summary>
        /// 通信服务器端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 通信模式
        /// </summary>
        public EMode Mode { get; set; }

        /// <summary>
        /// 最大接收长度
        /// </summary>
        public int MaxRequestLength { get; set; }

        public CC_Config()
        {
            this.MaxRequestLength = 1024 * 1024 * 10; //10MB=10485760Byte
        }

        /// <summary>
        /// 通信模式枚举
        /// </summary>
        public enum EMode
        {
            SuperSocket = 0
        }

    }



}

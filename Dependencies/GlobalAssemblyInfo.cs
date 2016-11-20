/* ============================== 
 * 全局程序集信息 
 * GlobalAssemblyInfo.cs 
 *  
 * 请把此文件引用到其他的项目中 
 ==============================*/

using System.Reflection;
using System.Runtime.InteropServices;

// 将 ComVisible 设置为 false 将使此程序集中的类型
// 对 COM 组件不可见。如果需要
// 从 COM 访问此程序集中的某个类型，请针对该类型将 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]
[assembly: AssemblyDescription("ajayumi.AiXin")]
[assembly: AssemblyProduct("ajayumi.AiXin")]
[assembly: AssemblyCompany("ajayumi")]
[assembly: AssemblyVersion(RevisionClass.FullVersion)]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCopyright("Copyright \x00a9 2016 ajayumi. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

internal static class RevisionClass
{
    public const string Major = "4";
    public const string Minor = "6";
    public const string Build = "0";
    public const string Revision = "*";

    public const string MainVersion = Major + "." + Minor + "." + Build;
    public const string FullVersion = Major + "." + Minor + "." + Build + "." + Revision;
}


/* 
 
其他程序集的AssemblyInfo.cs简化如下内容 
所有信息数据单独填写 
 
using System; 
using System.Reflection; 
using System.Runtime.CompilerServices; 
using System.Runtime.InteropServices; 
 
[assembly: AssemblyTitle("程序集标题")] 
[assembly: AssemblyDescription("程序集描述")] 
 
// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID 
[assembly: Guid("6a6263f2-35d2-4077-a1aa-cc775ca7cf84")] 
 
[assembly: System.CLSCompliant(true)]
[assembly: StringFreezing()] 
*/
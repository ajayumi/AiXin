/* ============================== 
 * ȫ�ֳ�����Ϣ 
 * GlobalAssemblyInfo.cs 
 *  
 * ��Ѵ��ļ����õ���������Ŀ�� 
 ==============================*/

using System.Reflection;
using System.Runtime.InteropServices;

// �� ComVisible ����Ϊ false ��ʹ�˳����е�����
// �� COM ������ɼ��������Ҫ
// �� COM ���ʴ˳����е�ĳ�����ͣ�����Ը����ͽ� ComVisible ��������Ϊ true��
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
 
�������򼯵�AssemblyInfo.cs���������� 
������Ϣ���ݵ�����д 
 
using System; 
using System.Reflection; 
using System.Runtime.CompilerServices; 
using System.Runtime.InteropServices; 
 
[assembly: AssemblyTitle("���򼯱���")] 
[assembly: AssemblyDescription("��������")] 
 
// �������Ŀ�� COM ������������ GUID �������Ϳ�� ID 
[assembly: Guid("6a6263f2-35d2-4077-a1aa-cc775ca7cf84")] 
 
[assembly: System.CLSCompliant(true)]
[assembly: StringFreezing()] 
*/
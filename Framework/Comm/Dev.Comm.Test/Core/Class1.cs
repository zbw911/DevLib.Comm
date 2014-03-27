using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.InteropServices;

using System.Runtime.CompilerServices;

namespace Dev.Comm.Test.Core
{
    class 注音的一个思路没调试通过
    {



        [DllImport("ole32.dll")]

        public static extern int CLSIDFromString([MarshalAs(UnmanagedType.LPWStr)] string lpsz, out Guid clsid);



        [DllImport("ole32.dll")]

        public static extern int CoCreateInstance(

            [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsid,

            IntPtr pUnkOuter, uint dwClsContext,

            [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,

            out IntPtr pv);



        [DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall)]

        public static extern int CoInitialize(IntPtr pvReserved);



        public const int FELANG_REQ_REV = 0x00030000;

        public const int FELANG_CMODE_PINYIN = 0x00000100;

        public const int FELANG_CMODE_NOINVISIBLECHAR = 0x40000000;



        [ComImport]

        [Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E")]

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]

        public interface IFELanguage
        {

            [MethodImpl(MethodImplOptions.InternalCall,

                MethodCodeType = MethodCodeType.Runtime)]

            int Open();

            [MethodImpl(MethodImplOptions.InternalCall,

                MethodCodeType = MethodCodeType.Runtime)]

            int Close();

            [MethodImpl(MethodImplOptions.InternalCall,

                MethodCodeType = MethodCodeType.Runtime)]

            int GetJMorphResult(

              [In] uint dwRequest,

              [In] uint dwCMode,

              [In] int cwchInput,

              [In, MarshalAs(UnmanagedType.LPWStr)] string pwchInput,

              [In] IntPtr pfCInfo,

              [Out] out IntPtr ppResult

            );

        }



        public const int CLSCTX_INPROC_SERVER = 1;

        public const int CLSCTX_INPROC_HANDLER = 2;

        public const int CLSCTX_LOCAL_SERVER = 4;

        public const int CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER;



        [DllImport("kernel32.dll")]

        public static extern int FormatMessage(int dwFlags, IntPtr lpSource,

            int dwMessageId, int dwLanguageId,

            StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);

        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;

        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

        public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;



        [DllImport("kernel32.dll")]

        public static extern int GetLastError();



        [DllImport("ole32.dll")]

        public static extern void CoTaskMemFree(IntPtr ptr);



        public const int S_OK = 0x00000000;



        public static string GetMessage(int errorCode)
        {

            StringBuilder lpBuffer = new StringBuilder(0x200);

            if (FormatMessage(FORMAT_MESSAGE_IGNORE_INSERTS | FORMAT_MESSAGE_FROM_SYSTEM |

                FORMAT_MESSAGE_ARGUMENT_ARRAY,

                IntPtr.Zero, errorCode, 0, lpBuffer, lpBuffer.Capacity, IntPtr.Zero) != 0)
            {

                return lpBuffer.ToString();

            }

            return "Unknown";

        }






        private bool coInitialized = false;

        public void ZhunYin(string input)
        {

            if (!coInitialized)
            {

                CoInitialize(IntPtr.Zero);

                coInitialized = true;

            }



            //textBox2.Clear();

            Guid vGuidIme;

            int vError;

            vError = CLSIDFromString("MSIME.China", out vGuidIme);

            if (vError != S_OK)
            {

                throw new Exception(GetMessage(vError));

                return;

            }

            Guid vGuidLanguage = new Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E");

            IntPtr vPPV;

            vError = CoCreateInstance(vGuidIme, IntPtr.Zero, CLSCTX_SERVER,

                vGuidLanguage, out vPPV);

            if (vError != S_OK)
            {

                //MessageBox.Show(GetMessage(vError));

                return;

            }

            IFELanguage vLanguage =

                Marshal.GetTypedObjectForIUnknown(vPPV, typeof(IFELanguage)) as IFELanguage;

            vError = vLanguage.Open();

            if (vError != S_OK)
            {

                //MessageBox.Show(GetMessage(vError));

                return;

            }

            IntPtr vMorrslt;

            string vInput = "中文";//textBox1.Text;

            vError = vLanguage.GetJMorphResult(FELANG_REQ_REV,

                FELANG_CMODE_PINYIN | FELANG_CMODE_NOINVISIBLECHAR,

                vInput.Length, vInput, IntPtr.Zero, out vMorrslt);

            if (vError != S_OK)
            {

                //MessageBox.Show(GetMessage(vError));

                return;

            }

            string vPinYin = Marshal.PtrToStringUni(Marshal.ReadIntPtr(vMorrslt, 4),

                Marshal.ReadInt16(vMorrslt, 8));

            //textBox2.AppendText("=" + vPinYin  + "\r\n");

            IntPtr vMonoRubyPos = Marshal.ReadIntPtr(vMorrslt, 28);

            IntPtr vReadIdxWDD = Marshal.ReadIntPtr(vMorrslt, 24);

            int iReadIdxWDD = 0;

            int iMonoRubyPos = Marshal.ReadInt16(vMonoRubyPos);

            vMonoRubyPos = (IntPtr)((int)vMonoRubyPos + 2);

            int i = 0;

            while (i < vInput.Length)
            {

                while (i < Marshal.ReadInt16(vReadIdxWDD))
                {

                    i++;

                    if (i >= Marshal.ReadInt16(vReadIdxWDD))
                    {

                        Console.WriteLine(Marshal.ReadInt16(vMonoRubyPos));

                        string s = vPinYin.Substring(iMonoRubyPos,

                              Marshal.ReadInt16(vMonoRubyPos) - iMonoRubyPos);

                        if (s != string.Empty)

                            s = vInput.Substring(iReadIdxWDD, i - iReadIdxWDD) + "(" + s + ")";

                        else s = vInput.Substring(iReadIdxWDD, i - iReadIdxWDD);

                        //textBox2.AppendText(s + "\r\n");

                        iReadIdxWDD = i;

                        iMonoRubyPos = Marshal.ReadInt16(vMonoRubyPos);

                        break;

                    }

                    vMonoRubyPos = (IntPtr)((int)vMonoRubyPos + 2);

                    vReadIdxWDD = (IntPtr)((int)vReadIdxWDD + 2);

                }

                vMonoRubyPos = (IntPtr)((int)vMonoRubyPos + 2);

                vReadIdxWDD = (IntPtr)((int)vReadIdxWDD + 2);

            }

            CoTaskMemFree(vMorrslt);

            vLanguage.Close();

        }

    }
}

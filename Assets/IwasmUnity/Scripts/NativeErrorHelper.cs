#nullable enable
using System;
using System.Text;

namespace IwasmUnity
{
    internal static class NativeErrorHelper
    {
        [ThreadStatic]
        private static byte[]? _errBufPool = null;

        public static byte[] GetErrorBuf()
        {
            const int size = 512;
            return (_errBufPool ??= new byte[size]);
        }

        public static void ReturnErrorBuf(byte[] buffer)
        {
            Array.Clear(buffer, 0, buffer.Length);
            _errBufPool = buffer;
        }

        public static string GetErrorString(byte[] buffer)
        {
            var len = buffer.Length;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == (byte)'\0')
                {
                    len = i;
                    break;
                }
            }
            return Encoding.UTF8.GetString(buffer, 0, len);
        }
    }
}

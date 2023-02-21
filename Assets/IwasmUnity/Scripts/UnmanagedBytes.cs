#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IwasmUnity
{
    internal unsafe struct UnmanagedBytes : IDisposable
    {
        private IntPtr _ptr;
        private int _length;

        public readonly IntPtr Ptr => _ptr;
        public readonly int Length => _length;

        public static UnmanagedBytes Empty => default;

        public UnmanagedBytes(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (size == 0)
            {
                _ptr = IntPtr.Zero;
                _length = 0;
                return;
            }
            _ptr = Marshal.AllocHGlobal(size);
            _length = size;
        }

        public static UnmanagedBytes CreateAsciiNullTerminated(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Empty;
            }
            var enc = Encoding.ASCII;
            var bytelen = enc.GetByteCount(str);
            var dest = new UnmanagedBytes(bytelen + 1);
            try
            {
                byte* destPtr = dest.AsPointer();
                fixed (char* src = str)
                {
                    enc.GetBytes(src, str.Length, destPtr, bytelen);
                }
                destPtr[bytelen] = (byte)'\0';
                return dest;
            }
            catch
            {
                dest.Dispose();
                throw;
            }
        }

        public static UnmanagedBytes CopyFrom(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            var bytes = new UnmanagedBytes(array.Length);
            try
            {
                fixed (byte* source = array)
                {
                    Buffer.MemoryCopy(source, bytes.AsPointer(), bytes.Length, array.Length);
                }
            }
            catch
            {
                bytes.Dispose();
                throw;
            }
            return bytes;
        }

        public readonly byte* AsPointer() => (byte*)_ptr;

        public void Dispose()
        {
            Marshal.FreeHGlobal(_ptr);
            this = default;
        }
    }

    /// <summary>
    /// Wrapper of `byte**`
    /// </summary>
    internal unsafe struct RawStringArray : IDisposable
    {
        private IntPtr _ptr;    // byte**
        private int _length;

        public IntPtr Ptr => _ptr;
        public int Length => _length;

        public RawStringArray(string[] array)
        {
            if (array == null || array.Length == 0)
            {
                this = default;
                return;
            }

            byte** p = (byte**)Alloc(array.Length * sizeof(byte*));
            for (int i = 0; i < array.Length; i++)
            {
                p[i] = CreateAsciiNullTerminated(array[i]);
            }
            _ptr = (IntPtr)p;
            _length = array.Length;
        }

        private static void* Alloc(int size) => (void*)Marshal.AllocHGlobal(size);

        private static void Free(void* ptr) => Marshal.FreeHGlobal((IntPtr)ptr);

        private static byte* CreateAsciiNullTerminated(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            var enc = Encoding.ASCII;
            var bytelen = enc.GetByteCount(str);
            byte* dest = (byte*)Alloc(bytelen + 1);
            fixed (char* src = str)
            {
                enc.GetBytes(src, str.Length, dest, bytelen);
            }
            dest[bytelen] = (byte)'\0';
            return dest;
        }

        public void Dispose()
        {
            var p = (byte**)_ptr;
            for (int i = 0; i < _length; i++)
            {
                Free(p[i]);
            }
            Free(p);
            this = default;
        }
    }
}

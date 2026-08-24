using System;
using System.Runtime.InteropServices;

namespace LibGit2Sharp.Core.Handles
{
    [StructLayout(LayoutKind.Sequential)]
    [System.Runtime.InteropServices.Marshalling.NativeMarshalling(typeof(LibGit2Sharp.Core.GitBufMarshaller))]
    internal class GitBuf : IDisposable
    {
        public IntPtr ptr;
        public UIntPtr asize;
        public UIntPtr size;

        public void Dispose()
        {
            Proxy.git_buf_dispose(this);
        }
    }
}

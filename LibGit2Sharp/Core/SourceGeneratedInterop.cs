using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using LibGit2Sharp.Core.Handles;

namespace LibGit2Sharp.Core
{
    [CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedIn, typeof(StrictUtf8Marshaller))]
    internal static unsafe class StrictUtf8Marshaller
    {
        public static byte* ConvertToUnmanaged(string managed)
        {
            return (byte*)StrictUtf8Marshaler.FromManaged(managed);
        }

        public static void Free(byte* unmanaged)
        {
            EncodingMarshaler.Cleanup((IntPtr)unmanaged);
        }
    }

    [CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(LaxUtf8NoCleanupMarshaller))]
    internal static unsafe class LaxUtf8NoCleanupMarshaller
    {
        public static string ConvertToManaged(byte* unmanaged)
        {
            return LaxUtf8NoCleanupMarshaler.FromNative((IntPtr)unmanaged);
        }
    }

    [CustomMarshaller(typeof(GitBuf), MarshalMode.ManagedToUnmanagedIn, typeof(GitBufMarshaller.ManagedToUnmanagedIn))]
    internal static unsafe class GitBufMarshaller
    {
        internal struct Unmanaged
        {
            public IntPtr ptr;
            public UIntPtr asize;
            public UIntPtr size;
        }

        internal ref struct ManagedToUnmanagedIn
        {
            private GitBuf managed;
            private Unmanaged* unmanaged;

            public void FromManaged(GitBuf managed)
            {
                this.managed = managed;

                if (managed is null)
                {
                    unmanaged = null;
                    return;
                }

                unmanaged = (Unmanaged*)NativeMemory.Alloc((nuint)sizeof(Unmanaged));
                *unmanaged = new Unmanaged
                {
                    ptr = managed.ptr,
                    asize = managed.asize,
                    size = managed.size,
                };
            }

            public Unmanaged* ToUnmanaged() => unmanaged;

            public void Free()
            {
                if (unmanaged is null)
                {
                    return;
                }

                managed.ptr = unmanaged->ptr;
                managed.asize = unmanaged->asize;
                managed.size = unmanaged->size;
                NativeMemory.Free(unmanaged);
            }
        }
    }

    internal static unsafe class StructMarshaller
    {
        public static IntPtr ConvertToUnmanaged<T>(T managed)
        {
            if (managed is null)
            {
                return IntPtr.Zero;
            }

            int size = Marshal.SizeOf<T>();
            IntPtr unmanaged = Marshal.AllocHGlobal(size);
            new Span<byte>((void*)unmanaged, size).Clear();

            try
            {
                Marshal.StructureToPtr(managed, unmanaged, false);
                return unmanaged;
            }
            catch
            {
                Marshal.FreeHGlobal(unmanaged);
                throw;
            }
        }

        public static void Free<T>(IntPtr unmanaged)
        {
            if (unmanaged == IntPtr.Zero)
            {
                return;
            }

            Marshal.DestroyStructure<T>(unmanaged);
            Marshal.FreeHGlobal(unmanaged);
        }
    }

    [CustomMarshaller(typeof(git_blame_options), MarshalMode.ManagedToUnmanagedIn, typeof(GitBlameOptionsMarshaller))]
    internal static class GitBlameOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(git_blame_options managed) => StructMarshaller.ConvertToUnmanaged(managed);

        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<git_blame_options>(unmanaged);
    }

    [CustomMarshaller(typeof(GitRebaseOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitRebaseOptionsMarshaller))]
    internal static class GitRebaseOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitRebaseOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitRebaseOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitCheckoutOpts), MarshalMode.ManagedToUnmanagedIn, typeof(GitCheckoutOptsMarshaller))]
    internal static class GitCheckoutOptsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitCheckoutOpts managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitCheckoutOpts>(unmanaged);
    }

    [CustomMarshaller(typeof(GitCloneOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitCloneOptionsMarshaller))]
    internal static class GitCloneOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitCloneOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitCloneOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitDescribeOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitDescribeOptionsMarshaller))]
    internal static class GitDescribeOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitDescribeOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitDescribeOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitDescribeFormatOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitDescribeFormatOptionsMarshaller))]
    internal static class GitDescribeFormatOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitDescribeFormatOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitDescribeFormatOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitDiffOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitDiffOptionsMarshaller))]
    internal static class GitDiffOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitDiffOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitDiffOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitDiffFindOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitDiffFindOptionsMarshaller))]
    internal static class GitDiffFindOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitDiffFindOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitDiffFindOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitMergeOpts), MarshalMode.ManagedToUnmanagedIn, typeof(GitMergeOptsMarshaller))]
    internal static class GitMergeOptsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitMergeOpts managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitMergeOpts>(unmanaged);
    }

    [CustomMarshaller(typeof(GitRemoteCallbacks), MarshalMode.ManagedToUnmanagedIn, typeof(GitRemoteCallbacksMarshaller))]
    internal static class GitRemoteCallbacksMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitRemoteCallbacks managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitRemoteCallbacks>(unmanaged);
    }

    [CustomMarshaller(typeof(GitProxyOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitProxyOptionsMarshaller))]
    internal static class GitProxyOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitProxyOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitProxyOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitFetchOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitFetchOptionsMarshaller))]
    internal static class GitFetchOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitFetchOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitFetchOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitPushOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitPushOptionsMarshaller))]
    internal static class GitPushOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitPushOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitPushOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitRepositoryInitOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitRepositoryInitOptionsMarshaller))]
    internal static class GitRepositoryInitOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitRepositoryInitOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitRepositoryInitOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitRevertOpts), MarshalMode.ManagedToUnmanagedIn, typeof(GitRevertOptsMarshaller))]
    internal static class GitRevertOptsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitRevertOpts managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitRevertOpts>(unmanaged);
    }

    [CustomMarshaller(typeof(GitStashApplyOpts), MarshalMode.ManagedToUnmanagedIn, typeof(GitStashApplyOptsMarshaller))]
    internal static class GitStashApplyOptsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitStashApplyOpts managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitStashApplyOpts>(unmanaged);
    }

    [CustomMarshaller(typeof(GitStatusOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitStatusOptionsMarshaller))]
    internal static class GitStatusOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitStatusOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitStatusOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitSubmoduleUpdateOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitSubmoduleUpdateOptionsMarshaller))]
    internal static class GitSubmoduleUpdateOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitSubmoduleUpdateOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitSubmoduleUpdateOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(GitCherryPickOptions), MarshalMode.ManagedToUnmanagedIn, typeof(GitCherryPickOptionsMarshaller))]
    internal static class GitCherryPickOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(GitCherryPickOptions managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<GitCherryPickOptions>(unmanaged);
    }

    [CustomMarshaller(typeof(git_worktree_add_options), MarshalMode.ManagedToUnmanagedIn, typeof(GitWorktreeAddOptionsMarshaller))]
    internal static class GitWorktreeAddOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(git_worktree_add_options managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<git_worktree_add_options>(unmanaged);
    }

    [CustomMarshaller(typeof(git_worktree_prune_options), MarshalMode.ManagedToUnmanagedIn, typeof(GitWorktreePruneOptionsMarshaller))]
    internal static class GitWorktreePruneOptionsMarshaller
    {
        public static IntPtr ConvertToUnmanaged(git_worktree_prune_options managed) => StructMarshaller.ConvertToUnmanaged(managed);
        public static void Free(IntPtr unmanaged) => StructMarshaller.Free<git_worktree_prune_options>(unmanaged);
    }
}

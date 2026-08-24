using System;
using System.Runtime.InteropServices;
using Xunit;

namespace LibGit2Sharp.Tests
{
    public class InteropLayoutFixture
    {
        [Fact]
        public void InteropStructLayoutsMatchNativeAbi()
        {
            Assert.True(IntPtr.Size == 4 || IntPtr.Size == 8);

            AssertSize("git_blame_options", 60, 72);
            AssertSize("git_blame_hunk", 80, 128);
            AssertOffset("git_blame_hunk", "final_committer", 32, 48);
            AssertOffset("git_blame_hunk", "orig_committer", 68, 104);
            AssertOffset("git_blame_hunk", "summary", 72, 112);
            AssertOffset("git_blame_hunk", "boundary", 76, 120);

            AssertSize("GitConfigEntry", 24, 40);

            AssertSize("GitDiffOptions", 64, 96);
            AssertOffset("GitDiffOptions", "OidType", 40, 64);
            AssertOffset("GitDiffOptions", "IdAbbrev", 44, 68);

            AssertSize("GitRemoteCallbacks", 64, 128);
            AssertOffset("GitRemoteCallbacks", "update_refs", 60, 120);

            AssertSize("GitFetchOptions", 120, 216);
            AssertSize("GitPushOptions", 116, 216);
            AssertSize("GitCloneOptions", 236, 416);
            AssertSize("GitSubmoduleUpdateOptions", 212, 376);
        }

        private static void AssertSize(string typeName, int expected32, int expected64)
        {
            Assert.Equal(IntPtr.Size == 4 ? expected32 : expected64, Marshal.SizeOf(CoreType(typeName)));
        }

        private static void AssertOffset(string typeName, string fieldName, int expected32, int expected64)
        {
            Assert.Equal(IntPtr.Size == 4 ? expected32 : expected64, Marshal.OffsetOf(CoreType(typeName), fieldName).ToInt32());
        }

        private static Type CoreType(string typeName)
        {
            return typeof(Repository).Assembly.GetType($"LibGit2Sharp.Core.{typeName}", throwOnError: true);
        }
    }
}

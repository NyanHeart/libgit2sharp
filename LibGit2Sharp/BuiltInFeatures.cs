using System;

namespace LibGit2Sharp
{
    /// <summary>
    /// Flags to identify libgit2 compiled features.
    /// </summary>
    [Flags]
    public enum BuiltInFeatures
    {
        /// <summary>
        /// No optional features are compiled into libgit2.
        /// </summary>
        None = 0,

        /// <summary>
        /// Threading support is compiled into libgit2.
        /// </summary>
        Threads = (1 << 0),

        /// <summary>
        /// Support for remotes over the HTTPS protocol is compiled into
        /// libgit2.
        /// </summary>
        Https = (1 << 1),

        /// <summary>
        /// Support for remotes over the SSH protocol is compiled into
        /// libgit2.
        /// </summary>
        Ssh = (1 << 2),

        /// <summary>
        /// Support for sub-second resolution in file modification times
        /// is compiled into libgit2.
        /// </summary>
        NSec = (1 << 3),

        /// <summary>
        /// Support for HTTP parsing is compiled into libgit2.
        /// </summary>
        HttpParser = (1 << 4),

        /// <summary>
        /// Support for regular expressions is compiled into libgit2.
        /// </summary>
        Regex = (1 << 5),

        /// <summary>
        /// Support for internationalized messages is compiled into libgit2.
        /// </summary>
        I18N = (1 << 6),

        /// <summary>
        /// Support for NTLM authentication is compiled into libgit2.
        /// </summary>
        AuthNtlm = (1 << 7),

        /// <summary>
        /// Support for Negotiate authentication is compiled into libgit2.
        /// </summary>
        AuthNegotiate = (1 << 8),

        /// <summary>
        /// Support for compression is compiled into libgit2.
        /// </summary>
        Compression = (1 << 9),

        /// <summary>
        /// Support for SHA-1 object identifiers is compiled into libgit2.
        /// </summary>
        Sha1 = (1 << 10),

        /// <summary>
        /// Support for SHA-256 object identifiers is compiled into libgit2.
        /// </summary>
        Sha256 = (1 << 11),
    }
}

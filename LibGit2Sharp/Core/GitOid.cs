using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibGit2Sharp.Core
{
    internal struct git_oid
    {
        public const int Size = 20;
        public unsafe fixed byte Id[20];
    }

    [InlineArray(20)]
    internal struct InlineArray20<T>
    {
        private T elem;

        public int Length => 20;

        public static implicit operator T[](InlineArray20<T> value)
        {
            var result = new T[20];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = value[i];
            }

            return result;
        }

        public static implicit operator InlineArray20<T>(T[] value)
        {
            var result = new InlineArray20<T>();

            if (value is null)
            {
                return result;
            }

            if (value.Length != 20)
            {
                throw new ArgumentException("An array of 20 elements is required.", nameof(value));
            }

            for (int i = 0; i < value.Length; i++)
            {
                result[i] = value[i];
            }

            return result;
        }
    }

    /// <summary>
    /// Represents a unique id in git which is the sha1 hash of this id's content.
    /// </summary>
    internal struct GitOid
    {
        /// <summary>
        /// Number of bytes in the Id.
        /// </summary>
        public const int Size = 20;

        /// <summary>
        /// The raw binary 20 byte Id.
        /// </summary>
        public InlineArray20<byte> Id;

        public static implicit operator ObjectId(GitOid oid)
        {
            return new ObjectId(oid);
        }

        public static implicit operator ObjectId(GitOid? oid)
        {
            return oid == null ? null : new ObjectId(oid.Value);
        }

        /// <summary>
        /// Static convenience property to return an id (all zeros).
        /// </summary>
        public static GitOid Empty
        {
            get { return new GitOid(); }
        }
    }
}

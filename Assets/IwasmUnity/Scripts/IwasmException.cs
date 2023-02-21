#nullable enable
using System;

namespace IwasmUnity
{
    public sealed class IwasmException : Exception
    {
        public IwasmException(string message) : base(message)
        {
        }
    }
}

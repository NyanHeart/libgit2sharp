using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LibGit2Sharp.Tests
{
    public partial class TestApp
    {
        [LibraryImport("kernel32", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial IntPtr GetModuleHandle(string path);

        [LibraryImport("kernel32", EntryPoint = "GetModuleFileNameW")]
        private static unsafe partial int GetModuleFileName(IntPtr handle, char* path, int size);

        static unsafe int Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 2)
            {
                Console.Error.WriteLine("Usage: <module-name> <directory>");
                return -1;
            }

            var moduleName = args[0];
            var loadFromDirectory = args[1];
            var expectedPath = Path.Combine(loadFromDirectory, moduleName + ".dll");

            GlobalSettings.NativeLibraryPath = loadFromDirectory;
            var isValid = Repository.IsValid(Path.GetTempPath());

            var capacity = ushort.MaxValue;
            var moduleHandle = GetModuleHandle(moduleName);
            var buffer = new char[capacity];
            int actualLength;

            fixed (char* path = buffer)
            {
                actualLength = GetModuleFileName(moduleHandle, path, capacity);
            }

            var actualPath = new string(buffer, 0, actualLength);

            if (expectedPath != actualPath)
            {
                Console.WriteLine(actualPath);
                return 1;
            }

            return 0;
        }
    }
}

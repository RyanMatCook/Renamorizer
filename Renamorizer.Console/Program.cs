using System;
using System.Linq;
using System.IO;

namespace Renamorizer.Console
{
    class Program
    {
        static void Main()
        {
            var basePath = @"C:\SourceControl\rename-go";
            Renamify(basePath, "GraphiteOrchestration", "Mercury");
        }

        static void Renamify(string basePath, string renameFrom, string renameTo)
        {
            var directories = Directory.GetDirectories(basePath);
            if (directories.Length > 0)
                foreach (var d in directories)
                {
                    Renamify(d, renameFrom, renameTo);
                    //Directory.Delete(d);
                    MoveFiles(basePath, renameFrom, renameTo);
                }
            else if (basePath.Contains(renameFrom))
            {
                Directory.CreateDirectory(basePath.Replace(renameFrom, renameTo));
                MoveFiles(basePath, renameFrom, renameTo);
                Directory.Delete(basePath);
            }
        }

        private static void MoveFiles(string basePath, string renameFrom, string renameTo)
        {
            foreach (var file in Directory.GetFiles(basePath))
            {
                if(!File.Exists(file.Replace(renameFrom, renameTo)))
                {
                    File.Copy(file, file.Replace(renameFrom, renameTo));
                    File.Delete(file);
                }
            }
        }
    }
}

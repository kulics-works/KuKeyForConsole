using KuKey.Core;
using Sharprompt;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KuKeyForConsole
{
    public class DatastoreService
    {
        public async Task Import(IKuKey Core)
        {
            var filePath = Prompt.Input<string>("Please enter the path of the source data");
            var text = ReadFile(filePath);
            Console.WriteLine(text);
            await Core.Import(text);
        }

        public string ReadFile(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("file not found");
                return "";
            }
            else
            {
                using var fsRead = new FileStream(filePath, FileMode.Open);
                try
                {
                    var ByteBlock = new byte[fsRead.Length];
                    var r = fsRead.Read(ByteBlock, 0, ByteBlock.Length);
                    return Encoding.UTF8.GetString(ByteBlock);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    return "";
                }
            }
        }

        public async Task Export(IKuKey Core)
        {
            var os = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                os = "Linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                os = "mac OS";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                os = "windows";
            }
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            var export = await Core.Export();
            var filePath = "kukey-" + os + "-" + time + ".txt";
            WriteTextAsync(filePath, export);
        }

        public void WriteTextAsync(string filePath, string text)
        {
            var ByteResult = Encoding.UTF8.GetBytes(text.ToString());
            using var fsWrite = new FileStream(filePath, FileMode.Create);
            fsWrite.Write(ByteResult, 0, ByteResult.Length);
        }
    }
}

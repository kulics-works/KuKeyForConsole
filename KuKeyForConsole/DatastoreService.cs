using Kulics.KuKey.Core;
using Sharprompt;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KuKeyForConsole
{
    class DatastoreService
    {
        public static async Task Import(DefaultKuKey Core)
        {
            string FilePath = Prompt.Input<string>("Please enter the path of the source data");
            //text是文件内容
            string text = await ProcessReadAsync(FilePath);
            Console.WriteLine(text);
            await Core.Import(text);
        }
        //下面两个是读文件
        public static async Task<string> ProcessReadAsync(string filePath)
        {

            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("file not found");
                return "";
            }
            else
            {
                try
                {
                    string text = await ReadTextAsync(filePath);
                    return text;

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        private static async Task<string> ReadTextAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
        public static async Task Export(DefaultKuKey Core)
        {
            string os = "";
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
            string time = DateTime.Now.ToString("yyyy-MM-dd");
           
            string export = await Core.Export();
            string filePath = "kukey-" + os + "-" + time + ".txt";
            await WriteTextAsync(filePath, export);
        }
        //写文件
        private static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.UTF8.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}

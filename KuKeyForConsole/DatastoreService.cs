using Library;
using static Library.Lib;
using KuKey.Core;
using Sharprompt;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KuKeyForConsole
{
public partial class DatastoreService{
public  async  virtual  System.Threading.Tasks.Task Import( DefaultKuKey Core ){
var filePath = Prompt.Input<string>("Please enter the path of the source data");
var text = ReadFile(filePath);
print(text);
await Core.Import(text);
}
public  virtual  string ReadFile( string filePath ){
if ( File.Exists(filePath)==false ) {
print("file not found");
return "";
}
else {
using (var fsRead = (new FileStream(filePath, FileMode.Open))) {
try {
var FSLength = (int)(fsRead.Length);
var ByteBlock = array<byte>(FSLength);
var r = fsRead.Read(ByteBlock, 0, ByteBlock.Length);
return Encoding.UTF8.GetString(ByteBlock);
}catch( Exception err )
{
print(err);
return "";
}
}}
}
public  async  virtual  System.Threading.Tasks.Task Export( DefaultKuKey Core ){
var os = "";
if ( RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ) {
os="Linux";
}
else if ( RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ) {
os="mac OS";
}
else if ( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ) {
os="windows";
}
var time = DateTime.Now.ToString("yyyy-MM-dd");
var export = await Core.Export();
var filePath = "kukey-"+os+"-"+time+".txt";
WriteTextAsync(filePath, export);
}
public  virtual  void WriteTextAsync( string filePath ,  string text ){
var ByteResult = Encoding.UTF8.GetBytes(text.to_str());
using (var fsWrite = (new FileStream(filePath, FileMode.Create))) {
fsWrite.Write(ByteResult, 0, ByteResult.Length);
}}
}
}

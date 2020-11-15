using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KuKey.Core;
using Sharprompt;

namespace KuKeyForConsole
{
    public static class KuKeyForConsole
    {
        public static async Task Main()
        {
            var MainKey = Prompt.Password("Please input master password");
            var Core = new DefaultKuKey("./kukey.sql", MainKey);
            var KeySvc = new KeyService();
            var DataSvc = new DatastoreService();
            var commands = new Dictionary<string, Func<IKuKey, Task>>
            {
                ["View key"] = KeySvc.QueryKey,
                ["Add key"] = KeySvc.CreateKey,
                ["Modify key"] = KeySvc.UpdateKey,
                ["Delete key"] = KeySvc.DeleteKey,
                ["Import"] = DataSvc.Import,
                ["Export"] = DataSvc.Export
            };
            var behavior = Prompt.Select("What do you want to do?", commands.Keys);
            await commands[behavior](Core);
            Console.ReadLine();
        }
    }
}

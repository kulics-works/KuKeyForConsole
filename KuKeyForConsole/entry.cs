using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Kulics.KuKey.Services;
using Sharprompt;
using System;
using System.Threading.Tasks;
using static Library.Lib;

namespace KuKeyForConsole
{
    class entry
    {
        static async Task Main(string[] args)
        {
            var MainKey = Prompt.Password("Please input master password");
            var Core = new DefaultKuKey("./kukey.sql", MainKey);
            var behavior = Prompt.Select("What do you want to do?", new[] { "View key", "Add key", "Modify key", "Delete key", "Import", "Export" });
            switch (behavior)
            {
                case "View key":
                    KeyService.QueryKey(MainKey);
                    break;
                case "Add key":
                    KeyService.CreateKey(MainKey);
                    break;
                case "Modify key":
                    KeyService.UpdateKey(MainKey);
                    break;
                case "Delete key":
                    KeyService.DeleteKey(MainKey);
                    break;
                case "Import":
                    await DatastoreService.Import(Core);
                    break;
                case "Export":
                    await DatastoreService.Export(Core);
                    break;
            }
        }
    }
}

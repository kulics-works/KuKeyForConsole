using Sharprompt;
using System;

namespace KuKeyForConsole
{
    class entry
    {
        static void Main(string[] args)
        {
            var MainKey = Prompt.Password("Please input master password");
            var behavior = Prompt.Select("What do you want to do?", new[] { "View key", "Add key", "Modify key", "Delete key" });
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
                    DatastoreService.Import(MainKey);
                    break;
                case "Export":
                    DatastoreService.Export(MainKey);
                    break;
            }
        }
    }
}

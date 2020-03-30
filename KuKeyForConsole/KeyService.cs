using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Kulics.KuKey.Services;
using Library;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Library.Lib;


namespace KuKeyForConsole
{
    class KeyService
    {
        static void PrintKey(KeyModel item)
        {
            print("ID:", item.Id, " Name:", item.Name, " Password:", item.Password);
        }
        public static async Task CreateKeyAsync(DefaultKuKey Core)
        {
            var Name = Prompt.Input<string>("Please enter the Name") ?? "";
            var Account = Prompt.Input<string>("Please enter the Account") ?? "";
            var Password = Prompt.Input<string>("Please enter the Password") ?? "";
            var URL = Prompt.Input<string>("Please enter the URL") ?? "";
            var Note = Prompt.Input<string>("Please enter the Note") ?? "";
            var key = new KeyModel { Name = Name, Account = Account,Password = Password, URL = URL, Note = Note};
            await Core.Create(key);
        }
        public static async Task DeleteKey(DefaultKuKey Core)
        {

        }
        public static async Task QueryKey(DefaultKuKey Core)
        {

            var want = Prompt.Input<string>("Please enter the name of the key you want to find");
            var keys = await Core.QueryAll<KeyModel>(i => i.Name.ToLower().Contains(want.ToLower()));
            var selectedKey = Prompt.Select("Please select a key", keys, valueSelector : i => i.Name);
            print($"Key: {selectedKey.Name}\nAccount: {selectedKey.Account}\nPassword: {selectedKey.Password}");
        }
        public static async Task UpdateKey(DefaultKuKey Core)
        {

        }
    }
}

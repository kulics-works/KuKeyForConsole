using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Kulics.KuKey.Services;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace KuKeyForConsole
{
    class KeyService
    {
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

        }
        public static async Task UpdateKey(DefaultKuKey Core)
        {

        }
    }
}

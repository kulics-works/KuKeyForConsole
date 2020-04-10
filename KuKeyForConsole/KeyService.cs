using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Sharprompt;
using System;
using System.Threading.Tasks;
using System.Linq;


namespace KuKeyForConsole
{
    class KeyService
    {
        public static async Task CreateKey(DefaultKuKey Core)
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
            var want = Prompt.Input<string>("Please enter the name of the key you want to delete");
            var selectedKey = await Query(want, Core);
            await Core.Create(new DeleteRecordModel(selectedKey.Id));
            await Core.Delete<KeyModel>(selectedKey.Id);
            Console.WriteLine("successfully deleted");
        }
        public static async Task QueryKey(DefaultKuKey Core)
        {

            var want = Prompt.Input<string>("Please enter the name of the key you want to find");
            var selectedKey = await Query(want, Core);
            Console.WriteLine($"Key: {selectedKey.Name}\nAccount: {selectedKey.Account}\nPassword: {selectedKey.Password}\nURL: {selectedKey.URL}\nNote: {selectedKey.Note}");
            Console.ReadKey();
        }
        public static async Task UpdateKey(DefaultKuKey Core)
        {
            var want = Prompt.Input<string>("Please enter the name of the key you want to update");
            var selectedKey = await Query(want, Core);
            selectedKey.Name = Prompt.Input<string>("Please enter new Name, if you do not modify this item, press enter") ?? selectedKey.Name;
            selectedKey.Account = Prompt.Input<string>("Please enter new Account, if you do not modify this item, press enter") ?? selectedKey.Account;
            selectedKey.Password = Prompt.Input<string>("Please enter new Password, if you do not modify this item, press enter") ?? selectedKey.Password;
            selectedKey.URL = Prompt.Input<string>("Please enter new URL, if you do not modify this item, press enter") ?? selectedKey.URL;
            selectedKey.Note = Prompt.Input<string>("Please enter new Note, if you do not modify this item, press enter") ?? selectedKey.Note;
            await Core.Update(selectedKey);
        }
        protected static async Task<KeyModel> Query(string want, DefaultKuKey Core)
        {
            return Core.LINQ(i => {
                var origins = (from originItem in i.Set<KeyModel>()
                               select i.Decrypt(originItem)).ToList();
                return Prompt.Select("Please select a key",
                       (from item in origins
                        where item.b && item.r.Name.ToLower().Contains(want.ToLower())
                        select item.r).ToList(), valueSelector: i => i.Name);
            });
        }
    }
}

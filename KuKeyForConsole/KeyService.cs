using KuKey.Core;
using KuKey.Models;
using Sharprompt;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace KuKeyForConsole
{
    public class KeyService
    {
        public async Task CreateKey(IKuKey Core)
        {
            var name = Prompt.Input<string>("Please enter the Name", "");
            var account = Prompt.Input<string>("Please enter the Account", "");
            var password = Prompt.Input<string>("Please enter the Password", "");
            var url = Prompt.Input<string>("Please enter the URL", "");
            var note = Prompt.Input<string>("Please enter the Note", "");
            var key = new KeyModel
            {
                Name = name,
                Account = account,
                Password = password,
                URL = url,
                Note = note
            };
            await Core.SaveAsync(ctx => ctx.Create(key));
        }

        public async Task DeleteKey(IKuKey Core)
        {
            var want = Prompt.Input<string>("Please enter the name of the key you want to delete", "");
            var selectedKey = await Query(want, Core);
            if (selectedKey is not null)
            {
                await Core.SaveAsync(ctx => ctx.Delete(selectedKey));
                Console.WriteLine("successfully deleted");
            }
        }

        public async Task QueryKey(IKuKey Core)
        {
            var want = Prompt.Input<string>("Please enter the name of the key you want to find", "");
            var key = await Query(want, Core);
            if (key is not null)
            {
                Console.WriteLine($"Key: {key.Name}\nAccount: {key.Account}\nPassword: {key.Password}\nURL: {key.URL}\nNote: {key.Note}");
            }
        }

        public async Task UpdateKey(IKuKey Core)
        {
            var want = Prompt.Input<string>("Please enter the name of the key you want to update", "");
            var key = await Query(want, Core);
            if (key is not null)
            {
                key.Name = Prompt.Input<string>("Please enter new Name, if you do not modify this item, press enter", key.Name);
                key.Account = Prompt.Input<string>("Please enter new Account, if you do not modify this item, press enter", key.Account);
                key.Password = Prompt.Input<string>("Please enter new Password, if you do not modify this item, press enter", key.Password);
                key.URL = Prompt.Input<string>("Please enter new URL, if you do not modify this item, press enter", key.URL);
                key.Note = Prompt.Input<string>("Please enter new Note, if you do not modify this item, press enter", key.Note);
                await Core.SaveAsync(ctx => ctx.Update(key));
            }
        }

        public async Task<KeyModel?> Query(string want, IKuKey Core)
        {
            KeyModel? result = null;
            await Core.QueryAsync(i =>
            {
                var origins = (from originItem in i.Set<KeyModel>() select i.Decrypt(originItem)).ToList();
                var items = new List<KeyModel>();
                foreach (var item in origins)
                {
                    if (item is not null && item.Name.ToLower().Contains(want.ToLower()))
                    {
                        items.Add(item);
                    }
                }
                if (items.Count == 0)
                {
                    Console.WriteLine("NO KEYS");
                    return;
                }
                result = Prompt.Select("Please select a key", items, null, 10, (i) => i.Name);
            });
            return result;
        }
    }
}

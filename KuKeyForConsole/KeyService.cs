using Library;
using static Library.Lib;
using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Sharprompt;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace KuKeyForConsole
{
public partial class KeyService{
public  async  virtual  System.Threading.Tasks.Task CreateKey( DefaultKuKey Core ){
var Name = (Prompt.Input<string>("Please enter the Name")??"");
var Account = (Prompt.Input<string>("Please enter the Account")??"");
var Password = (Prompt.Input<string>("Please enter the Password")??"");
var URL = (Prompt.Input<string>("Please enter the URL")??"");
var Note = (Prompt.Input<string>("Please enter the Note")??"");
var key = (new KeyModel(){Name = Name,Account = Account,Password = Password,URL = URL,Note = Note});
await Core.SaveAsync( async (ctx)=>ctx.Create(key));
}
public  async  virtual  System.Threading.Tasks.Task DeleteKey( DefaultKuKey Core ){
var want = Prompt.Input<string>("Please enter the name of the key you want to delete");
var selectedKey = await Query(want, Core);
await Core.SaveAsync( async (ctx)=>ctx.Delete<KeyModel>(selectedKey.Id));
print("successfully deleted");
}
public  async  virtual  System.Threading.Tasks.Task QueryKey( DefaultKuKey Core ){
var want = Prompt.Input<string>("Please enter the name of the key you want to find");
var selectedKey = await Query(want, Core);
print((new System.Text.StringBuilder().Append("Key: ").Append(selectedKey.Name).Append("\n").Append("Account: ").Append(selectedKey.Account).Append("\n").Append("Password: ").Append(selectedKey.Password).Append("\n").Append("URL: ").Append(selectedKey.URL).Append("\n").Append("Note: ").Append(selectedKey.Note)).to_str());
}
public  async  virtual  System.Threading.Tasks.Task UpdateKey( DefaultKuKey Core ){
var want = Prompt.Input<string>("Please enter the name of the key you want to update");
var selectedKey = await Query(want, Core);
selectedKey.Name=(Prompt.Input<string>("Please enter new Name, if you do not modify this item, press enter")??selectedKey.Name);
selectedKey.Account=(Prompt.Input<string>("Please enter new Account, if you do not modify this item, press enter")??selectedKey.Account);
selectedKey.Password=(Prompt.Input<string>("Please enter new Password, if you do not modify this item, press enter")??selectedKey.Password);
selectedKey.URL=(Prompt.Input<string>("Please enter new URL, if you do not modify this item, press enter")??selectedKey.URL);
selectedKey.Note=(Prompt.Input<string>("Please enter new Note, if you do not modify this item, press enter")??selectedKey.Note);
await Core.SaveAsync( async (ctx)=>ctx.Update(selectedKey));
}
public  async  virtual  System.Threading.Tasks.Task<KeyModel> Query( string want ,  DefaultKuKey Core ){
KeyModel result = null;
await Core.QueryAsync( async (i)=>{var origins = (from originItem in i.Set<KeyModel>() select i.Decrypt(originItem)).ToList();
result=Prompt.Select("Please select a key", (from item in origins where item.b&&item.r.Name.ToLower().Contains(want.ToLower()) select item.r).ToList(), null, 10, (i)=>i.Name);
});
return result;
}
}
}

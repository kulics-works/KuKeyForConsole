using Library;
using static Library.Lib;
using Kulics.KuKey.Core;
using Sharprompt;
using System.Threading.Tasks;
using System;
using System.IO;

namespace KuKeyForConsole
{
public partial class KuKeyForConsole_static {
public static  async System.Threading.Tasks.Task Main(){
var MainKey = Prompt.Password("Please input master password");
var Core = (new DefaultKuKey("./kukey.sql", MainKey));
var KeySvc = (new KeyService());
var DataSvc = (new DatastoreService());
var behavior = Prompt.Select("What do you want to do?", array_of("View key", "Add key", "Modify key", "Delete key", "Import", "Export"));
switch (behavior) {
case "View key" :
{ await KeySvc.QueryKey(Core);
} break;
case "Add key" :
{ await KeySvc.CreateKey(Core);
} break;
case "Modify key" :
{ await KeySvc.UpdateKey(Core);
} break;
case "Delete key" :
{ await KeySvc.DeleteKey(Core);
} break;
case "Import" :
{ await DataSvc.Import(Core);
} break;
case "Export" :
{ await DataSvc.Export(Core);
} break;
}
read();
}
}
}

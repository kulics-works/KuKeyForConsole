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
var behavior = Prompt.Select("What do you want to do?", array_of("View key", "Add key", "Modify key", "Delete key", "Import", "Export"));
switch (behavior) {
case "View key" :
{ await KeyService.QueryKey(Core);
} break;
case "Add key" :
{ await KeyService.CreateKey(Core);
} break;
case "Modify key" :
{ await KeyService.UpdateKey(Core);
} break;
case "Delete key" :
{ await KeyService.DeleteKey(Core);
} break;
case "Import" :
{ await DatastoreService.Import(Core);
} break;
case "Export" :
{ await DatastoreService.Export(Core);
} break;
}
read();
}
}
}

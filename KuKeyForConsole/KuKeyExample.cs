using System;
using System.Threading.Tasks;
using Kulics.KuKey.Core;
using Kulics.KuKey.Models;
using Kulics.KuKey.Services;
using static Library.Lib;

namespace KuKeyForConsole {
    // KuKey 核心库使用示例
    public class KuKeyExample {
        public static async Task ExampleAsync(string masterPassword) {
            // 打印key方法
            static void printKey(KeyModel item) {
                print("ID:", item.Id, " Name:", item.Name, " Password:", item.Password);
            }
            // 清空数据辅助函数
            static async Task ClearDatabase<T>(IKuKey app) where T : BaseModel {
                foreach (var item in await app.QueryAll<T>(i => true)) {
                    await app.Delete<T>(item.Id);
                }
            }

            // 构建核心程序，使用sqlite持久化
            // var Core = new DefaultKuKey("./kukey.sql", masterPassword);
            // 测试时可使用内存版本，不持久化
            var Core = new DefaultKuKey(new MemoryDataStoreService(), new DefaultEncryptService(masterPassword));

            // 清空key和删除记录
            await ClearDatabase<KeyModel>(Core);
            await ClearDatabase<DeleteRecordModel>(Core);

            // 新建一个key
            var key1 = new KeyModel { Name = "test1", Password = "password" };

            // 存储到核心
            await Core.Create(key1);

            // 查询并打印是否插入成功
            var keys = await Core.QueryAll<KeyModel>(i => true);
            foreach (var item in keys) {
                printKey(item);
            }

            // 测试第二条数据
            var key2 = new KeyModel { Name = "test2", Password = "account" };
            await Core.Create(key2);
            
            // 重新打印key，看数据是否有变化
            keys = await Core.QueryAll<KeyModel>(i => true);
            foreach (var item in keys) {
                printKey(item);
            }

            // 示范导出功能
            var export = await Core.Export();
            print(export);

            // 删除一个key时，需要开发者自行创建删除记录
            await Core.Create(new DeleteRecordModel(key1.Id));
            await Core.Delete<KeyModel>(key1.Id);

            // 查看是否删除成功
            keys = await Core.QueryAll<KeyModel>(i => true);
            foreach (var item in keys) {
                printKey(item);
            }

            // 示范修改数据
            key2.Password = "123456";

            // 更新
            await Core.Update(key2);

            // 查看是否更新成功
            keys = await Core.QueryAll<KeyModel>(i => true);
            foreach (var item in keys) {
                printKey(item);
            }

            // 重新导出，查看是否更新成功，是否有删除记录
            export = await Core.Export();
            print(export);

            // 示范导入
            await Core.Import(export);

            // 重新导出，查看数据是否有变化
            export = await Core.Export();
            print(export);

            // 示范生成高强度密码函数
            print(Core.GeneratePassword(PasswordLevel.Mix, 24));
        }
    }
}

using Nadmin.Common;
using Nadmin.Model.Models;
using SqlSugar;
using System;
using System.Linq;
using System.Reflection;

namespace Nadmin.Model
{
    public class DataBaseManager
    {
        public static void CreateOrUpdateTableSchemas(SqlSugarClient dbClient, bool isBackup = false)
        {
            var entitys = ScanEntitys(typeof(BaseEntity).Assembly);
            if (isBackup)
                dbClient.CodeFirst.SetStringDefaultLength(50).BackupTable().InitTables(entitys);
            else
                dbClient.CodeFirst.SetStringDefaultLength(50).InitTables(entitys);

            AfterCreateOrUpdateTableSchemas(dbClient);
        }

        private static Type[] ScanEntitys(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsDefined(typeof(SugarTable), true)).ToArray();
        }

        private static void AfterCreateOrUpdateTableSchemas(SqlSugarClient dbClient)
        {
            var userContext = new SimpleClient<User>(dbClient);
            var user = userContext.GetSingle(o => o.UserName == "admin");
            if (user == null)
            {
                var password = EncryptHelper.Sha1("123456");
                user = new User("admin", password)
                {
                    Remark = "系统管理员"
                };

                userContext.Insert(user);
            }
        }
    }
}
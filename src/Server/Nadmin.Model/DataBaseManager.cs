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
                dbClient.CodeFirst.SetStringDefaultLength(255).BackupTable().InitTables(entitys);
            else
                dbClient.CodeFirst.SetStringDefaultLength(255).InitTables(entitys);
        }

        private static Type[] ScanEntitys(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsDefined(typeof(SugarTable), true)).ToArray();
        }
    }
}
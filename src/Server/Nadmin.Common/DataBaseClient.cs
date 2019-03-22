using Autofac;
using Microsoft.Extensions.Options;
using Nadmin.Common.AppSetting;
using SqlSugar;

namespace Nadmin.Common
{
    public static class DataBaseClient
    {
        public static SqlSugarClient Create()
        {
            var appSettings = Global.Instance.Container.Resolve<IOptions<AppSettings>>().Value;

            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = appSettings.DataBase.ConnectionString, //必填, 数据库连接字符串
                DbType = DbType.MySql, //必填, 数据库类型
                IsAutoCloseConnection = true, //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                InitKeyType = InitKeyType.Attribute, //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });
        }
    }
}
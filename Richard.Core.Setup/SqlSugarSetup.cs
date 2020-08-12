using Microsoft.Extensions.DependencyInjection;
using Richard.Core.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.Setup
{
    public static class SqlSugarSetup
    {
        public static void ConfigSqlSugarSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            services.AddScoped<ISqlSugarClient>(item =>
            {
                List<ConnectionConfig> connectionConfigs = new List<ConnectionConfig>();
                List<SlaveConnectionConfig> slaveConnectionConfigs = new List<SlaveConnectionConfig>();
                BaseDBConfig.MutiConnectionString.Item1.ForEach(m =>
                {
                    connectionConfigs.Add(new ConnectionConfig
                    {
                        ConfigId = m.ConnId,
                        ConnectionString = m.Connection,
                        DbType = (DbType)m.DbType,
                        IsAutoCloseConnection = true,
                        IsShardSameThread = false,
                        MoreSettings = new ConnMoreSettings
                        {
                            IsAutoRemoveDataCache = true
                        },
                        SlaveConnectionConfigs = slaveConnectionConfigs
                    });
                });
                BaseDBConfig.MutiConnectionString.Item2.ForEach(s =>
                {
                    slaveConnectionConfigs.Add(new SlaveConnectionConfig
                    {
                        HitRate = s.HitRate,
                        ConnectionString = s.Connection
                    });
                });
                return new SqlSugarClient(connectionConfigs);
            });
        }
    }
}

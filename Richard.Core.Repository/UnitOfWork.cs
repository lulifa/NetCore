using Richard.Core.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ISqlSugarClient sqlSugarClient;
        public UnitOfWork(ISqlSugarClient client)
        {
            sqlSugarClient = client;
        }

        public SqlSugarClient Client => GetSugarClient();

        public SqlSugarClient GetSugarClient()
        {
            SqlSugarClient client = sqlSugarClient as SqlSugarClient;
            return client;
        }

        public void BeginTransition()
        {
            Client.BeginTran();
        }

        public void CommitTransition()
        {
            try
            {
                Client.CommitTran();
            }
            catch (Exception ex)
            {
                Client.RollbackTran();
                throw ex;
            }
        }

        public void RollbackTransition()
        {
            Client.RollbackTran();
        }
    }
}

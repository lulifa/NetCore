using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.IRepository
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetSugarClient();
        void BeginTransition();
        void CommitTransition();
        void RollbackTransition();
    }
}

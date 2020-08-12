using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richard.Core.Common
{
    public class BaseDBConfig
    {
        public static string CurrentDbConnId => CurrentDbConn();

        private static string CurrentDbConn()
        {
           return  AppSettingHelper.GetSection(SystemHelper.MainDB).ObjToString();
        }

        public static Tuple<List<MutiDbOperate>, List<MutiDbOperate>> MutiConnectionString => MutiInitConn();

        private static Tuple<List<MutiDbOperate>, List<MutiDbOperate>> MutiInitConn()
        {
            List<MutiDbOperate> dbOperates = AppSettingHelper.GetSection<MutiDbOperate>(SystemHelper.DBS).Where(item => item.Enabled).ToList();
            List<MutiDbOperate> operateFirst = new List<MutiDbOperate>();
            List<MutiDbOperate> operateSecond = new List<MutiDbOperate>();
            bool isMutiFlag = AppSettingHelper.GetSection(SystemHelper.MutiDBEnabled).ObjToBool();
            bool isCQRSFlag = AppSettingHelper.GetSection(SystemHelper.CQRSEnabled).ObjToBool();
            if (isMutiFlag)
            {
                operateFirst = dbOperates;
            }
            else
            {
                if (isCQRSFlag)
                {
                    operateSecond = dbOperates.Where(item => item.ConnId != CurrentDbConnId).ToList();
                }
                else
                {
                    MutiDbOperate dbOperate = dbOperates.FirstOrDefault(item => item.ConnId == CurrentDbConnId);
                    if (dbOperate == null)
                    {
                        dbOperate = dbOperates.FirstOrDefault();
                    }
                    operateFirst.Add(dbOperate);
                }
            }
            return new Tuple<List<MutiDbOperate>, List<MutiDbOperate>>(operateFirst, operateSecond);
        }
    }
}

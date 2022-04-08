using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreNET.Common.BO;
using CoreNET.Common.Base;

namespace Usadi.Valid49.BO
{
  public class BaseDataControlAsetMAT : BaseDataControlExt, IDataControlUIEntry, IExtLoadCsv, IHasJSScript
  {
    public BaseDataControlAsetMAT()
    {
      ConnectionString = GetConnectionString();
      ModeDB = SQLDataSource.MODE_DB_OPERATIONAL;
    }

    public static string GetConnectionString()
    {
      string SQLInstance = SQLDataSource.GetSQLInstance();
      string db = ((Ss10userLoginAsetControl)GlobalAsp.GetSessionUser()).GetDBName();
      string user = SQLDataSource.GetUserDB();
      string pwd = SQLDataSource.GetPwdDB();
      //    string ConnectionString = string.Format("data source={0};initial catalog=V@LID49V7_2019;user id={1};password={2};Asynchronous Processing=true",
      //SQLInstance, user, pwd);
      string ConnectionString = string.Format("data source={0};initial catalog=V@LID49ASETV5;user id={2};password={3};Asynchronous Processing=true",
        SQLInstance, db, user, pwd);
      return ConnectionString;
    }
  }
}

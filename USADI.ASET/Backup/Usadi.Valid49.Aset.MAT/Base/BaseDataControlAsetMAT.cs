using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreNET.Common.BO;
using CoreNET.Common.Base;

namespace Usadi.Valid49.BO
{
  public class BaseDataControlAsetMAT : BaseDataControlAsetDM, IDataControlUIEntry, IExtLoadCsv, IHasJSScript
  {
    public BaseDataControlAsetMAT()
    {
      ConnectionString = GetConnectionString();
      ModeDB = SQLDataSource.MODE_DB_OPERATIONAL;
    }
  }
}

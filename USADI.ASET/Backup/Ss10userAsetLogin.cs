using CoreNET.Common.Base;
using CoreNET.Common.BO;
using System;
using System.Collections.Generic;


namespace Usadi.Valid49.BO
{
  #region Usadi.Valid49.BO.PmuserLoginAdminControl, Usadi.Valid49.BO;
  [Serializable]
  public class Ss10userLoginAsetControl : Ss10userLoginControl, IDataControlAppuserUI
  {
    private static Ss10userLoginAsetControl _Instance = null;
    public static Ss10userLoginAsetControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new Ss10userLoginAsetControl();
        }
        return _Instance;
      }
    }
    public Ss10userLoginAsetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLWEBUSER;
    }
    public string GetDBName()
    {
      return DBName;
    }

    public new BaseBO Load()
    {
      string sql = $@"
      select
      RTRIM(A.USERID) AS USERID, RTRIM(A.KDPEMDA) as KDPEMDA,D.NMPEMDA
      ,USERTYPE,USERNAMA,USERPWD,USERBLOCK,USERNIP,USERHP,USEREMAIL,USERNO,USERURAIAN,USERSTATUS,USERKET,IDROLE
      , RTRIM(B.UNITKEY) AS UNITKEY
      , RTRIM(B.KDTAHAP) AS KDTAHAP
      , RTRIM(C.KDUNIT) AS KDUNIT
      , RTRIM(C.NMUNIT) AS NMUNIT
      from SMARTSYS..SS10USER A
      LEFT JOIN V@LID49ASETV5.dbo.WEBUSER B ON A.USERID=B.USERID COLLATE DATABASE_DEFAULT
      LEFT JOIN V@LID49ASETV5.dbo.DAFTUNIT C ON B.UNITKEY=C.UNITKEY COLLATE DATABASE_DEFAULT
      LEFT JOIN DMPEMDA D ON A.KDPEMDA=D.KDPEMDA
      where RTRIM(A.USERID)='{Userid}'
      ";
      string[] fields = new string[] { "Userid","Kdpemda","Nmpemda","Usertype", "Usernama", "Userpwd", "Userblock", "Usernip", "Userhp", "Useremail", "Userno", "Useruraian", "Userstatus"
                                      , "Userket", "Idrole","Unitkey","Kdtahap" ,"Kdunit","Nmunit"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<Ss10userLoginAsetControl> ListData = list.ConvertAll(new Converter<IDataControl, Ss10userLoginAsetControl>(delegate (IDataControl par) { return (Ss10userLoginAsetControl)par; }));

      if (ListData.Count > 0)
      {
        this.CopyPropertyBOFrom(ListData[0]);
        this.Unitkey = "4";
        this.Kdunit = "1.01.XX.";
        this.Nmunit = "Dinas UU";
        return this;
      }
      else
      {
        return null;
      }
    }


  }
  #endregion SsuserPmuserLogin
}


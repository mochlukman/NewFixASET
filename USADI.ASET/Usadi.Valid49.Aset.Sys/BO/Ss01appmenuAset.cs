using CoreNET.Common.Base;
using CoreNET.Common.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Usadi.Valid49.BO
{
  #region CoreNET.Common.BO.Ss01appmenuAsetControl, CoreNET.Common.BO
  [Serializable]
  public class Ss01appmenuAsetControl : Ss01appmenuControl, IDataControlMenu
  {
    public Ss01appmenuAsetControl()
    {
      XMLName = ConstantTablesSys.XMLSS01APPMENU;
    }

    private ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
      }
      return cViewListProperties;
    }

    public new IList View()
    {
      return base.View();
    }

    public new void SetOtorisasiMenu(Page page, string idapp)
    {
      Idapp = GlobalAsp.GetSessionApp();

      Ss10userLoginAsetControl user = (Ss10userLoginAsetControl)GlobalAsp.GetSessionUser();

      SetAboutValue(page, "userid", string.Format("Userid = {0}", user.Userid));
      SetAboutValue(page, "nama", string.Format("Nama = {0}", user.Usernama));
      SetAboutValue(page, "nip", string.Format("NIP = {0}", user.Usernip));
      SetAboutValue(page, "email", string.Format("Email = {0}", user.Useremail));
      SetAboutValue(page, "nohp", string.Format("Mobile No.={0}", user.Userhp));
      SetAboutValue(page, "role", string.Format("Jabatan = {0}", user.Uturaian));
      SetAboutValue(page, "uraian", string.Format("{0}", user.Uraian + " " + user.Nmpemda));
      SetAboutValue(page, "ipaddr", string.Format("IP Adress = {0}", UtilityUI.GetIPAddress())); //UtilityUI.GetClientCompIP();
    }
  }
  #endregion Ss01appmenuAset
}


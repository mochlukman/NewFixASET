using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Usadi.Valid49.BO;
using NLog;

public partial class LoginAset : System.Web.UI.Page
{
  private static ILogger logger = LogManager.GetCurrentClassLogger();
  protected void Page_Load(object sender, EventArgs e)
  {
    CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();

    GlobalExt.InitHome(Page, null);
    if (!Page.IsPostBack)
    {
      MasterAppConstants.Instance.ShowContextMenu = true;
      MasterAppConstants.Instance.ShowTranslatedLabel = true;
      string sufix = string.Empty;

      string baseurl = GlobalAsp.GetBaseURL();
      /*
       * Kasus 1: server ngga bisa akses url selain localhost
       * Kasus 2: portal
       * 
       */
      string idapp = GlobalAsp.GetIdApp();
      if (MasterAppConstants.Instance.StatusTesting)
      {
        MasterAppConstants.Instance.ShowContextMenu = (ConfigurationManager.AppSettings["ShowContextMenu"] == "1");
        MasterAppConstants.Instance.ShowTranslatedLabel = (ConfigurationManager.AppSettings["ShowTranslatedLabel"] == "1");


        //mainDiv.Style.Add("background-image", baseurl + "App_Portal/Img/bg2.png");
        //mainDiv.Style.Add("width", "1366px");
        //mainDiv.Style.Add("height", "600px");
        //mainDiv.Style.Add("background-image", baseurl + "App_Portal/Img/bg.jpg");
        //mainDiv.Style.Add("width", "1366px");
        //mainDiv.Style.Add("height", "600px");
      }
      if (!string.IsNullOrEmpty(idapp))
      {
        MasterAppConstants.Instance.StatusAdmin = false;
        GlobalAsp.SetConfiguration(idapp, false);

        string dcdict = (string)SsappconfigLookupControl.Instance.Params["DCDictionary"];
        if (!string.IsNullOrEmpty(dcdict) && !MasterAppConstants.Instance.DictionaryDC.Equals(dcdict))
        {
          MasterAppConstants.Instance.SetValue(MasterAppConstants.DICTIONARYDC, dcdict);
          ConstantDict.SetInstanceNullForReloadDict();
        }

        if (ConfigurationManager.AppSettings["ShowKodeApp"] != "1")
        {
          Title = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.APPTITLE);
          if (MasterAppConstants.Instance.StatusTesting)
          {
            Title += " ";
          }
          Window1.Title = "Login " + Title;
        }
        else
        {
          string idappfromreq = "0";
          string idappcosfail = "0";
          CoreNET.Common.Base.SQLDataSource.Instance.SetDataSourceConfig(CoreNET.Common.Base.GlobalAsp.GetDataSourceConfig());
          string sesname = GlobalAsp.SESSION_APP_PARAMS;
          string sub = GlobalAsp.GetRequestSub();
          string cname = MasterAppConstants.Instance.AppDC;
          string db = SQLDataSource.Instance.CS_Config;
          string publickey = GlobalLib.GetPublishedKey();
          string info = string.Format("(Kdapp='{0}';Idapp={1};Sesname={2};ByReq={3};" +
            "ByFail={4};Sub={5};Cname={6};DBName={7};Key={8})",
            GlobalAsp.GetRequestKdapp(), idapp, sesname, idappfromreq, idappcosfail, sub, cname, db, publickey);
          WindowDebug.ShowMessage(Page, info);
        }
      }
      else
      {
        string msg = ConstantDict.Translate("LBL_CEK_APP_CONFIG=Cek App config, global.asax dan file config");
        X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
        return;
      }
    }

    CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }
  [DirectMethod(Timeout = 3600000)]
  public void ProcessLogin() { }
  protected void btnLogin_Click(object sender, DirectEventArgs e)
  {
    bool ok = false;
    if (AssemblyUtils.ProfilingActive)
    {
      logger.Info("S1");
    }
    string key = utxt_Code.Value;
    try
    {
      if (string.IsNullOrEmpty(txtUser.Text))


      {
        X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), ConstantDictExt.Translate("LBL_EMPTY_USERID")).Show();
        return;
      }
      /*User Key ini di log di GlobalAsp*/
      #region Authentication

      Ss10userLoginAsetControl dcuser = new Ss10userLoginAsetControl();
      if (AssemblyUtils.ProfilingActive)
      {
        logger.Info("S1.1");
      }
      dcuser.Userid = txtUser.Text;
      GlobalAsp.SetSessionUser(dcuser);
      if (AssemblyUtils.ProfilingActive)
      {
        logger.Info("userid=" + dcuser.Userid);
      }
      GlobalExt.SetSessionUser(GlobalAsp.GetSessionApp(), key);
      logger.Info("key=" + key);

      #endregion
      if (AssemblyUtils.ProfilingActive)
      {
        logger.Info("S1.3");
      }

      if (MasterAppConstants.Instance.StatusTesting)
      {
        //ValidateLibrary();
      }

      ok = true;
    }
    catch (Exception ex)
    {
      if (AssemblyUtils.ProfilingActive)
      {
        logger.Info("ex21" + ex.Message);
      }
      try
      {
        string msg = string.Empty;
        if (MasterAppConstants.Instance.StatusTesting)
        {
          msg = string.Format(@"Testing: Error executing '{0}'. Cause: {1}; In: {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
          WindowDebug.ShowMessage(Page, msg);
        }
        else
        {
          msg = ConstantDictExt.Translate(ex.Message);
          X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
        }
      }
      catch (Exception)
      {
        if (ex.Message.Contains("Unable to open connection"))
        {
          Response.Redirect(GlobalExt.GetHomeURL());
        }
      }
    }
    if (AssemblyUtils.ProfilingActive)
    {
      logger.Info("S2");
    }
    if (ok)
    {
      string app = GlobalAsp.GetRequestApp();
      if (string.IsNullOrEmpty(app))
      {
        app = GlobalAsp.GetSessionApp();
      }

      string url = string.Empty;
      string sub = ConfigurationManager.AppSettings["IsEtalase"];
      url = GlobalAsp.GetMenuURL() +
        string.Format("?app={0}&key={1}&sub={2}&kdapp={3}", app, key,
        sub, GlobalAsp.GetRequestKdapp());//Modul Admin
      if (AssemblyUtils.ProfilingActive)
      {
        logger.Info("S3=" + url);
      }
      Response.Redirect(url);
    }
  }

  protected void btnCancel_Click(object sender, DirectEventArgs e)
  {
    string sub = ConfigurationManager.AppSettings["IsEtalase"];
    string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
      + "&sub=" + sub;
    Response.Redirect(urlout);
  }
}
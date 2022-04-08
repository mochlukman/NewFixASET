using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

public partial class Portal : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    //CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();

    GlobalExt.InitHome(Page, null);
    if (true)
    {
      #region StatusBar
      if (MasterAppConstants.Instance.StatusTesting)
      {
        Ext.Net.Button btnSign = new Ext.Net.Button() { ID = "btnSign" };
        btnSign.OnClientClick = string.Format(@"
          Ext.Msg.alert('Current User Key','{0}');
          ", GlobalExt.GetSessionKey());
        //StatusBar2.Items.Add(btnSign);

        Ext.Net.Button btnConfig = new Ext.Net.Button() { ID = "btnConfig" };
        string url = string.Format("Page/PageTabular.aspx?app={0}&id={1}", Request["app"], "0A992D53-DB54-4303-A3B9-F575AAF7EC1A");
        btnConfig.OnClientClick = string.Format(@"
          loadPageByID('Pages','Config','Config','{0}');
        ", url);
        //StatusBar2.Items.Add(btnConfig);
      }
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        try
        {
          string kdapp = GlobalAsp.GetRequestUrlKdapp();
          if (MasterAppConstants.Instance.StatusTesting)
          {
            if (string.IsNullOrEmpty(kdapp))
            {
              kdapp = GlobalAsp.GetRequestKdapp();
            }
          }

          if (string.IsNullOrEmpty(kdapp))
          {
            if (!string.IsNullOrEmpty(GlobalAsp.GetConfigPrefixPortal()))
            {
              kdapp = GlobalAsp.GetConfigPrefixPortal();
            }
          }
          if (string.IsNullOrEmpty(kdapp))
          {
            kdapp = "01.";
          }

          SsappControl dc = new SsappControl() { Kdapp = kdapp };
          if (MasterAppConstants.Instance.StatusTesting)
          {
            try
            {
              ConstantDict.SetInstanceNullForReloadDict();
            }
            catch (Exception ex)
            {
              LogUtils.Log(ex);
            }
          }


          if (string.IsNullOrEmpty(GlobalAsp.GetConfigTitlePortal()))
          {
            if (string.IsNullOrEmpty(kdapp))
            {
              Title = "CoreNET Framework";
            }
            else
            {
              SsappLookupControl.FindAndSetValuesIntoByKdapp(dc);
              Title = dc.Nmapp;
            }
          }
          else
          {
            Title = GlobalAsp.GetConfigTitlePortal();
          }
          Panel1.Html = @"<div id='Div1' class='header' style='height:32px;border:0;width:100%'>
										<a style='float:right;margin-right:10px' href='http://www.usadi.co,id' target='_blank'></a>
										<div class='api-title' style='font-size:16px;font-weight:bold'>" + Title + "</div></div>";

          bool isEtalase = string.IsNullOrEmpty(kdapp);

          if (!string.IsNullOrEmpty(kdapp))
          {
            isEtalase = (kdapp.Equals("00.") || kdapp.Equals("01.") || kdapp.Equals("02."));
          }

          if (isEtalase || (ConfigurationManager.AppSettings["IsEtalase"] == "1"))
          {
            //Etalase
            //TopPanel.Visible = ConfigurationManager.AppSettings["ShowToolbar"] == "1";
            toolBar.Visible = ((string.IsNullOrEmpty(ConfigurationManager.AppSettings["ShowToolbarPortal"]) ||
              ConfigurationManager.AppSettings["ShowToolbarPortal"] == "1"));
            //StatusBar2.Visible = TopPanel.Visible;
            if (TopPanel.Visible)
            {
              CreateToolbar();
            }
            UtilityExt.LoadUrl(PanelCenter, ConfigurationManager.AppSettings["URLHomePortal"]);
          }
          else
          {
            //Portal
            toolBar.Visible = false;
            TopPanel.Visible = false;
            TopPanel.Layout = "Fit";
            TopPanel.Height = new System.Web.UI.WebControls.Unit(32, System.Web.UI.WebControls.UnitType.Pixel);

            UtilityExt.LoadUrl(PanelCenter, "App_Portal/PageMenu.aspx?mode=" + GlobalAsp.GetRequestMode()
              + "&kdapp=" + kdapp + "&sub=" + GlobalAsp.GetRequestSub());
          }
        }
        catch (Exception ex)
        {
          WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
          return;
        }
      }
      #endregion
    }

    CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CreateToolbar()
  {
    string kdapp = GlobalAsp.GetRequestUrlKdapp();

    if (MasterAppConstants.Instance.StatusTesting)
    {
      if (string.IsNullOrEmpty(kdapp))
      {
        kdapp = GlobalAsp.GetRequestKdapp();
      }
    }
    if (string.IsNullOrEmpty(kdapp))
    {
      kdapp = "01.";
    }
    string cname = MasterAppConstants.Instance.AppDC;
    IDataControl dc = UtilityBO.Create(cname);
    Button btnMenu = null;
    btnMenu = new Button(ConstantDict.Translate("HOME"));
    string basehomeURL = GlobalAsp.GetHomePortalURL();
    btnMenu.Listeners.Click.Handler = string.Format("CoreNET.LoadPage('{0}');", basehomeURL);
    toolBar.Items.Add(btnMenu);
    toolBar.Items.Add(new ToolbarSeparator());

    //if (typeof(Ss00appLinkControl).IsInstanceOfType(dcApp))
    //{
    //Ss00appLinkControl dc = new Ss00appLinkControl
    //{
    //  QSSub = "1"
    //};
    dc.SetValue("QSSub", "1");
    List<SsappControl> list = (List<SsappControl>)dc.View();
    List<SsappControl> roots = list.FindAll(o => o.Kdapp.StartsWith(kdapp) && o.Kdlevel.Equals(2));

    #region Toolbar
    for (int i = 0; i < roots.Count; i++)
    {
      SsappControl ctrl = roots[i];
      btnMenu = new Button(ctrl.Nmapp);
      btnMenu.Menu.Add(GetMenu(list, ctrl.Kdapp, 2));
      toolBar.Items.Add(btnMenu);
      toolBar.Items.Add(new ToolbarSeparator());
    }
    //}

    #region Settting dan Tentang
    string baseurl = MasterAppConstants.Instance.URLBase;

    btnMenu = new Button("Setting");
    Menu menu = new Menu();
    MenuItem cmenuitem = new MenuItem("Administrator");

    string url = ConfigurationManager.AppSettings["URLHomeAdmin"];
    if (!string.IsNullOrEmpty(url))
    {
      cmenuitem.Listeners.Click.Handler = string.Format("CoreNET.LoadPage('{0}');", url);
      menu.Items.Add(cmenuitem);
    }

    url = ConfigurationManager.AppSettings["URLHomeStatus"];
    if (!string.IsNullOrEmpty(url))
    {
      cmenuitem = new MenuItem("Status Aplikasi");
      cmenuitem.Listeners.Click.Handler = string.Format("CoreNET.LoadPage('{0}');", url);
      menu.Items.Add(cmenuitem);
    }

    btnMenu.Menu.Add(menu);

    toolBar.Items.Add(new ToolbarFill());
    toolBar.Items.Add(new ToolbarSeparator());
    toolBar.Items.Add(btnMenu);
    btnMenu = new Button(ConstantDict.Translate("BTN_ABOUT"));
    btnMenu.Listeners.Click.Handler = string.Format("CoreNET.LoadPage('{0}');", "App_Portal/About.aspx");

    toolBar.Items.Add(new ToolbarSeparator());
    toolBar.Items.Add(btnMenu);
    #endregion
    #endregion Toolbar
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void LoadPage(string url)
  {
    Session.Abandon();
    Session.RemoveAll();
    Application[GlobalAsp.DAO] = null;
    UtilityExt.LoadUrl(PanelCenter, url);
    PanelCenter.LoadContent();
  }
  public Ext.Net.Menu GetMenu(IList list, string kdapp, int kdlevel)
  {
    //Dari query ini, baris 145:   Ss00appLinkControl dc = new Ss00appLinkControl();
    List<SsappControl> localList = (List<SsappControl>)list;
    List<SsappControl> roots = localList.FindAll(i => (i.Kdapp.StartsWith(kdapp)) && (i.Kdlevel == kdlevel + 1));
    roots.Sort((emp1, emp2) => emp1.Kdapp.CompareTo(emp2.Kdapp));
    Ext.Net.Menu menu = new Ext.Net.Menu();
    foreach (SsappControl ctrl in roots)
    {
      menu.Items.Add(CreateMenuWithChildren((List<SsappControl>)list, ctrl));
    }
    return menu;
  }
  private Ext.Net.MenuItem CreateMenuWithChildren(List<SsappControl> list, SsappControl parent)
  {
    Ext.Net.MenuItem menuNode = new Ext.Net.MenuItem(parent.Kdapp + parent.Nmapp);
    string baseurl = GlobalAsp.GetBaseURL();
    string prefix = baseurl + System.Configuration.ConfigurationManager.AppSettings["URLPrefix"];
    menuNode.Listeners.Click.Handler = string.Format("CoreNET.LoadPage('{0}');", prefix + parent.UrlFull);
    if (MasterAppConstants.Instance.StatusTesting)
    {
      if (parent.Status == -1)
      {
        menuNode.Icon = Icon.Cancel;
      }
    }

    List<SsappControl> children = GetChildren(list, parent);
    Ext.Net.Menu menu = new Ext.Net.Menu();
    if (children.Count > 0)
    {
      menuNode.Menu.Add(menu);
    }
    for (int i = 0; i < children.Count; i++)
    {
      SsappControl child = children[i];
      menu.Items.Add(CreateMenuWithChildren(list, child));
    }
    return menuNode;
  }
  public static List<SsappControl> GetChildren(List<SsappControl> domainset, SsappControl parent)
  {
    List<SsappControl> children = domainset.FindAll(o => o.Kdapp.StartsWith(parent.Kdapp) && (o.Kdlevel == parent.Kdlevel + 1));
    children.Sort((emp1, emp2) => emp1.Kdapp.CompareTo(emp2.Kdapp));
    return children;
  }
  public void Home()
  {
    if (MasterAppConstants.Instance.StatusTesting)
    {
      string msg = string.Format("Error in class {0}, method {1}", GetType().Name,
        MethodBase.GetCurrentMethod().Name);
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), msg).Show();

    }
    else
    {
      string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
        + "&sub=" + GlobalAsp.GetRequestSub();
      Response.Redirect(urlout);
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SaveSessionPage(string url, string roleid, string path)
  {
    UtilityUI.SetSessionExtPage(url, roleid);
  }
}

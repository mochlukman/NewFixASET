using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;
using Ext.Net;
using Ext.Net.Utilities;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

public partial class PageMenu : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionMenu(Page))
    {
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        Title = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.APPTITLE);
        TreePanel1.Title = Title;
        try
        {
          CreateNode(TreePanel1);
        }
        catch (Exception ex)
        {
          TreePanel1.Hidden = true;
          WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
          return;
        }
      }
      #endregion
    }
    else
    {
      X.Js.Call("home");
      string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
        + "&sub=" + GlobalAsp.GetRequestSub();
      Response.Redirect(urlout);
    }
  }
  private void CreateNode(TreePanel tree)
  {
    tree.Icon = Icon.BookOpen;
    tree.Title = ConstantDictExt.Translate("LIST_MENU");
    tree.AutoScroll = true;

    StatusBar statusBar = new StatusBar();
    statusBar.AutoClear = 1000;
    tree.BottomBar.Add(statusBar);

    tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
    tree.Listeners.ExpandNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + node.text + '</b>', clear: true});";
    tree.Listeners.ExpandNode.Delay = 30;
    tree.Listeners.CollapseNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + node.text + '</b>', clear: true});";

    IList list = null;
    Ext.Net.TreeNode root = null;
    IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
    IDataControlMenu cMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
    try
    {
      //Beda dengan MainMenu.aspx, ada query string qs menu
      list = (IList)GlobalExt.GetSessionListSubMenu();
      if (list == null)
      {
        cMenu.SetPageKey();
        list = (IList)cMenu.View();//polimorphisme using qs menu=
      }
      if (list != null && list.Count > 0)
      {
        GlobalExt.SetSessionListSubMenu(list);

        root = ((IDataControlMenu)cMenu).CreateMenu(list, ExtTreePanelUtil.TYPE_TREEMENU, ((ViewListProperties)cMenu.GetProperties()).HasTreeRoot);
      }
      else
      {
        root = new Ext.Net.TreeNode();
      }
      tree.Root.Add(root);
      tree.Listeners.Click.Handler = "if (node.attributes.href) { CoreNET.SaveSessionPage(node.attributes.href,node.attributes.id,node.getPath('id','/'));e.stopEvent(); loadNode(#{Pages}, node,node.getPath('id','/')); }";
    }
    catch (Exception ex)
    {
      if (MasterAppConstants.Instance.StatusTesting)
      {
        throw new Exception(ex.Message + " at " + ex.StackTrace);
      }
      else
      {
        TreePanel1.Hidden = true;
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        return;
      }
    }

  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandTreePanel(string tree, string path)
  {
    //Ext.Net.TreePanel TreePanel1 = Ext.Net.Utilities.ControlUtils.FindControl<Ext.Net.TreePanel>(PnlWest, tree);
    TreePanel1.SelectPath(path);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SaveSessionPage(string url, string roleid, string path)
  {
    UtilityUI.SetSessionMenuPage(url, roleid, false);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandRightFrame()
  {
    TreePanel1.Collapse();
  }
}

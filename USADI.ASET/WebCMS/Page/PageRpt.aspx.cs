﻿using System;
using CoreNET.Common.Base;

public partial class PageRpt : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      int id = GlobalExt.GetRequestI();
      IPrintable dc = (IPrintable)UtilityUI.GetDataControl(id);

      Response.Redirect(dc.GetURLReport(int.Parse(GlobalAsp.GetRequestVal())));
    }
  }
}
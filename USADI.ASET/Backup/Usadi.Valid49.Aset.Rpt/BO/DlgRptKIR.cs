using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using Ext.Net;
using Ext.Net.Utilities;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

namespace Usadi.Valid49.BO
{
  #region Usadi.Valid49.BO.DlgRptKIRControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptKIRControl : DaftunitControl, IDataControlUIEntry, IPrintable
  {
    #region Property Kdtahun
    private string _Kdtahun;
    public string Kdtahun
    {
      get { return _Kdtahun; }
      set { _Kdtahun = value; }
    }
    #endregion


    #region Property Ruangkey
    private string _Ruangkey;
    public string Ruangkey
    {
      get { return _Ruangkey; }
      set { _Ruangkey = value; }
    }
    #endregion


    #region Property Kdruang
    private string _Kdruang;
    public string Kdruang
    {
      get { return _Kdruang; }
      set { _Kdruang = value; }
    }
    #endregion


    #region Property Nmruang
    private string _Nmruang;
    public string Nmruang
    {
      get { return _Nmruang; }
      set { _Nmruang = value; }
    }
    #endregion



    public DlgRptKIRControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      Kdtahun = DateTime.Now.Year.ToString();
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        cViewListProperties.ReadOnlyFields = new string[] { };
        cViewListProperties.ModeToolbar = ViewListProperties.MODE_TOOLBAR_PRINT;
      }
      return cViewListProperties;
    }

    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "LinkPdf",
          Icon = Icon.Printer,
          ToolTip =
          {
            Text = "Klik tombol ini untuk mencetak data"
          }
        };
        return new ImageCommand[] { cmd1 };
      }
    }

    public override HashTableofParameterRow GetEntries()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true));
      hpars.Add(DaftruangLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahun=Tahun"),
        GetList(new TahunLookupControl()), "Kdtahun=Nmtahun", 20).SetAllowRefresh(true).SetEnable(enableFilter));

      return hpars;
    }    

    public new void SetFilterKey(BaseBO bo)
    {
      //if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      //{
      //  Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
      //}
      //else if (typeof(DaftruangControl).IsInstanceOfType(bo))
      //{
      //  Unitkey = ((DaftruangControl)bo).Unitkey;
      //}
    }

    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }


    public string LinkPdf
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("Page/PageRpt.aspx?passdc=1&app={0}&i=1&id={1}&idprev={2}&kode={3}&idx={4}&val=0" + strenable, app, id, idprev, kode, idx);
        return ConstantDict.Translate(XMLName) + " " + Kdunit + "|" + url;
      }
    }

    public string GetURLReport(int n)
    {
      Hashtable CS = new Hashtable();
      CS[ReportUtils.SQLINSTANCE] = GlobalAsp.GetDataSource();
      CS[ReportUtils.DBNAME] = (string)((IDataControlAppuser)GlobalAsp.GetSessionUser()).GetValue("DBName");
      CS[ReportUtils.DBUSER] = SQLDataSource.GetUserDB();
      CS[ReportUtils.DBPASSWORD] = SQLDataSource.GetPwdDB();
      CS[ReportUtils.PATH] = "Report/Aset/";
      CS[ReportUtils.RPTLIB] = "Usadi.Valid49.Aset.Rpt.dll";
      CS[ReportUtils.RPTNAME] = "KIR.rpt";
      CS[ReportUtils.PDFNAME] = "KIR.pdf";


      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@kdtahun"] = Kdtahun;



      string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.KIR.rpt", CS, Params);
      return pdfurl;
    }


    public new void SetPageKey()
    {
    }

    public Hashtable GetDocProperties()
    {
      return new Hashtable();
    }

    public List<RptPars> GetReports()
    {
      RptPars rptPar = new RptPars() { Title = "Daftar KIR", RptClass = LinkPdf };
      return new List<RptPars>(new RptPars[] { rptPar });
    }

    public void Print(int n)
    {
    }
  }
  #endregion RptKIR
}


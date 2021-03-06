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
  #region Usadi.Valid49.BO.DlgRptRekapBukuInvControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptRekapBukuInvControl : DaftunitControl, IDataControlUIEntry, IPrintable
  {

    #region Property Subunit
    private string _Subunit;
    public string Subunit
    {
      get { return _Subunit; }
      set { _Subunit = value; }
    }
    #endregion


    #region Property Kdpar
    private string _Kdpar;
    public string Kdpar
    {
      get { return _Kdpar; }
      set { _Kdpar = value; }
    }
    #endregion


    #region Property Kdklas
    private string _Kdklas;
    public string Kdklas
    {
      get { return _Kdklas; }
      set { _Kdklas = value; }
    }
    #endregion


    public DlgRptRekapBukuInvControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      Subunit = "1";
      Kdklas = "01";
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
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdklas=Kelas Aset"),
        GetList(new JklasRptLookupControl()), "Kdklas=Uraiklas", 54).SetAllowRefresh(true).SetEnable(enableFilter));

      hpars.Add(new ParameterRowDate(this, ParameterRow.MODE_DATE_RANGE).SetAllowRefresh(true).SetEnable(enableFilter));

      ArrayList list = new ArrayList(new ParamControl[] {
          new ParamControl() {  Kdpar="1",Nmpar="Ya"}
          ,new ParamControl() { Kdpar="0",Nmpar="Tidak"}
        });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Subunit=Subunit"), ParameterRow.MODE_TYPE,
        list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enableFilter).SetAllowRefresh(true));

      return hpars;
    }




    public new void SetFilterKey(BaseBO bo)
    {
      //Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
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
      CS[ReportUtils.RPTNAME] = "RptRekapBukuInv.rpt";
      CS[ReportUtils.PDFNAME] = "RptRekapBukuInv.pdf";


      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@tglawal"] = Tgl1;
      Params["@tglakhir"] = Tgl2;
      Params["@kdklas"] = Id;
      Params["@subunit"] = Kdpar;


      string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.RptRekapBukuInv.rpt", CS, Params);
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
      RptPars rptPar = new RptPars() { Title = "Rekap Buku Inventaris", RptClass = LinkPdf };
      return new List<RptPars>(new RptPars[] { rptPar });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptRegRekapBukuInv
}


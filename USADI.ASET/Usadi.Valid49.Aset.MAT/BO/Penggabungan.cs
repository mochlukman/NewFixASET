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
  #region Usadi.Valid49.BO.PenggabunganControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenggabunganControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglbagabung { get; set; }
    public string Nobagabung { get; set; }
    public long Idbrg { get; set; }
    public string Kdtans { get; set; }
    public string Nmtrans { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public decimal Nilai { get; set; }
    public string Blokid
    {
      get
      {
        WebuserControl cWebuserGetid = new WebuserControl();
        cWebuserGetid.Userid = GlobalAsp.GetSessionUser().GetUserID();
        cWebuserGetid.Load("PK");

        return cWebuserGetid.Blokid;
      }
    }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewPenggabungandet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewPenggabungandet
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i={1}&id={2}&idprev={3}&kode={4}&idx={5}" + strenable, app, 11, id, idprev, kode, idx);
        return "Rincian Rekening; " + Nobagabung + "- Nilai Rp. " + Nilai.ToString("#,##0") + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public PenggabunganControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPENGGABUNGAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobagabung" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.SortFields = new String[] { "Tglbagabung", "Nobagabung" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      bool enable = true;

      if (Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobagabung=No Dokumen"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbagabung=Tanggal"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
        Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
        Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
      }
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PenggabunganControl> ListData = new List<PenggabunganControl>();
      foreach (PenggabunganControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    //public new void SetPrimaryKey()
    //{
    //  PemdaControl cPemda = new PemdaControl();
    //  cPemda.Configid = "cur_thang";
    //  cPemda.Load("PK");

    //  KegunitControl cKegunit = new KegunitControl();
    //  cKegunit.Unitkey = Unitkey;
    //  cKegunit.Kdkegunit = Kdkegunit;
    //  cKegunit.Kdtahap = Kdtahap;
    //  cKegunit.Thang = cPemda.Configval.Trim();
    //  cKegunit.Load("PK");

    //  Idprgrm = cKegunit.Idprgrm.Trim();
    //  Tahunsa = (Int32.Parse(cPemda.Configval));
    //  Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
    //  Tglbarenov = Tglsa;
    //}
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
      //GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      //hpars.Add(KegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(false).SetAllowEmpty(false));
      return hpars;
    }   

    #endregion Methods 
  }
  #endregion Penggabungan  

  //#region Penggabungan104
  //[Serializable]
  //public class Penggabungan104Control : PenggabunganControl, IDataControlUIEntry, IHasJSScript
  //{
  //  #region Methods 
  //  public new void SetPageKey()
  //  {
  //    Kdtans = "104";
  //  }
  //  #endregion Methods 
  //}
  //#endregion Penggabungan104

  //#region Penggabungan109
  //[Serializable]
  //public class Penggabungan109Control : PenggabunganControl, IDataControlUIEntry, IHasJSScript
  //{
  //  #region Methods 
  //  public new void SetPageKey()
  //  {
  //    Kdtans = "109";
  //  }
  //  #endregion Methods 
  //}
  //#endregion Penggabungan109
}


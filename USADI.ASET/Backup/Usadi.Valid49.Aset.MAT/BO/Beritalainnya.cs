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
  #region Usadi.Valid49.BO.BeritalainnyaControl, Usadi.Valid49.Aset.MAT
  [Serializable] 
  public class BeritalainnyaControl : BeritaControl, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public new ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewBeritadetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewBeritadetbrg
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
        return "No Berita " + Noba + "; " + "Jenis Transaksi " + Nmtrans + ":" + url;
      }
    }
    #endregion Properties
    #region Methods    
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No BAST"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public new void SetPrimaryKey()
    {
      Kdkegunit = null;
      Kdtahap = null;
      Idprgrm = null;
      Nokontrak = null;
      Kddana = null;
      Kdbukti = "01";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

      return hpars;
    }

    public new IList View()
    {
      IList list = this.View("Beritalainnya");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<BeritaControl> ListData = new List<BeritaControl>();
      foreach (BeritaControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }    
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = false;
      bool enable = !Valid;

      BeritalainnyaControl cBeritaCekjmlbast = new BeritalainnyaControl();
      cBeritaCekjmlbast.Unitkey = Unitkey;
      cBeritaCekjmlbast.Noba = Noba;
      cBeritaCekjmlbast.Load("Jmlbast");
      Jmlbast = cBeritaCekjmlbast.Jmlbast;

      BeritalainnyaControl cBeritaCekjmlbrg = new BeritalainnyaControl();
      cBeritaCekjmlbrg.Unitkey = Unitkey;
      cBeritaCekjmlbrg.Noba = Noba;
      cBeritaCekjmlbrg.Load("Jmlbrg");
      Jmlbrg = cBeritaCekjmlbrg.Jmlbrg;

      BeritalainnyaControl cBeritaCekjmlgenerated = new BeritalainnyaControl();
      cBeritaCekjmlgenerated.Unitkey = Unitkey;
      cBeritaCekjmlgenerated.Noba = Noba;
      cBeritaCekjmlgenerated.Load("Jmlgenerated");
      Jmlgenerated = cBeritaCekjmlgenerated.Jmlgenerated;

      if ((Jmlbrg - Jmlgenerated) == 0 && Jmlbast != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Transaksi"),
        GetList(new JtransBastLookupControl()), "Kdtans=Nmtrans", 50).SetEnable(enable).SetEditable(enableValid).SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No NPH"), true, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal NPH"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Beritalainnya
}


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
  #region Usadi.Valid49.BO.ReklasdetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ReklasdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdaset2 { get; set; }
    public string Nmaset2 { get; set; }
    public string Asetkey2 { get; set; }
    public string Noreg2 { get; set; }
    public string Idbrg { get; set; }
    public string Statusreklas { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Unitkey { get; set; }
    public string Noreklas { get; set; }
    public string Kdtans { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    #endregion Properties 

    #region Methods 
    public ReklasdetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLREKLASDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noreklas", "Kdtans", "Noreg", "Asetkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Noreklas", "Kdtans" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Tglvalid != new DateTime())
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetPageKey()
    {
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(ReklasControl).IsInstanceOfType(bo))
      {
        Unitkey = ((ReklasControl)bo).Unitkey;
        Noreklas = ((ReklasControl)bo).Noreklas;
        Kdtans = ((ReklasControl)bo).Kdtans;
        Tglvalid = ((ReklasControl)bo).Tglvalid;
      }
      else if (typeof(ViewasetReklasControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetReklasControl)bo).Asetkey;
        Tahun = ((ViewasetReklasControl)bo).Tahun;
        Noreg = ((ViewasetReklasControl)bo).Noreg;
        Idbrg = ((ViewasetReklasControl)bo).Idbrg;
      }
    }
    public new void SetPrimaryKey()
    {
      Unitkey = Unitkey;
      Noreklas = Noreklas;
      Kdtans = Kdtans;
      Noreg2 = "";
      Statusreklas = "0";
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<ReklasdetControl> ListData = new List<ReklasdetControl>();
      foreach (ReklasdetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(ViewasetReklasControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(DaftasetReklasLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Reklasdet
}


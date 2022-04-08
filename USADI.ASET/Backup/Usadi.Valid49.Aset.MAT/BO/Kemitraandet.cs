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
  #region Usadi.Valid49.BO.KemitraandetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KemitraandetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public decimal Nilai { get; set; }
    public string Nopenilaian { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Nminst { get; set; }
    public string Nmtrans { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    public string Kdtans { get; set; }
    public string Kdp3 { get; set; }
    public string Nodokumen { get; set; }
    public string Unitkey { get; set; }
    #endregion Properties 

    #region Methods 
    public KemitraandetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKEMITRAANDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokumen", "Kdp3", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetKemitraanControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 20;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopenilaian=No Penilaian"), typeof(string), 25, HorizontalAlign.Left));
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
      else if (typeof(KemitraanControl).IsInstanceOfType(bo))
      {
        Unitkey = ((KemitraanControl)bo).Unitkey;
        Nodokumen = ((KemitraanControl)bo).Nodokumen;
        Kdp3 = ((KemitraanControl)bo).Kdp3;
        Kdtans = ((KemitraanControl)bo).Kdtans;
      }
      else if (typeof(ViewasetKemitraanControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetKemitraanControl)bo).Asetkey;
        Tahun = ((ViewasetKemitraanControl)bo).Tahun;
        Noreg = ((ViewasetKemitraanControl)bo).Noreg;
        Idbrg = ((ViewasetKemitraanControl)bo).Idbrg;
        Nilai = ((ViewasetKemitraanControl)bo).Nilai;
        Nopenilaian = ((ViewasetKemitraanControl)bo).Nopenilaian;
      }
    }
    public new void SetPrimaryKey()
    {
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<KemitraandetControl> ListData = new List<KemitraandetControl>();
      foreach (KemitraandetControl dc in list)
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
    #endregion Methods 
  }
  #endregion Kemitraandet
}


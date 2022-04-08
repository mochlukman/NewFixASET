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
  #region Usadi.Valid49.BO.PenilaiandetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenilaiandetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public decimal Nilai { get; set; }
    public string Kdkon { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nmtrans { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Nmkon { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    public string Kdtans { get; set; }
    public string Nopenilaian { get; set; }
    public string Unitkey { get; set; }
    #endregion Properties 

    #region Methods 
    public PenilaiandetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPENILAIANDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nopenilaian", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetPenilaianControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
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
      bool enableEdit = true;
      if (Tglvalid != new DateTime())
      {
        enableEdit = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Penilaian"), typeof(decimal), 30, HorizontalAlign.Left).SetEditable(enableEdit));
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
      else if (typeof(PenilaianControl).IsInstanceOfType(bo))
      {
        Unitkey = ((PenilaianControl)bo).Unitkey;
        Nopenilaian = ((PenilaianControl)bo).Nopenilaian;
        Kdtans = ((PenilaianControl)bo).Kdtans;
        Tglvalid = ((PenilaianControl)bo).Tglvalid;
      }
      else if (typeof(ViewasetPenilaianControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetPenilaianControl)bo).Asetkey;
        Tahun = ((ViewasetPenilaianControl)bo).Tahun;
        Noreg = ((ViewasetPenilaianControl)bo).Noreg;
        Idbrg = ((ViewasetPenilaianControl)bo).Idbrg;
        Kdkon = ((ViewasetPenilaianControl)bo).Kdkon;
      }
    }
    public new void SetPrimaryKey()
    {
      Unitkey = Unitkey;
      Nopenilaian = Nopenilaian;
      Kdtans = Kdtans;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PenilaiandetControl> ListData = new List<PenilaiandetControl>();
      foreach (PenilaiandetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      if (Kdkon == "")
      {
        Kdkon = null;
      }
      else
      {
        Kdkon = Kdkon;
      }
      base.Insert();
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }


    #endregion Methods 
  }
  #endregion Penilaiandet
}


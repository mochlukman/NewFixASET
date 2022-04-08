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
  #region Usadi.Valid49.BO.ViewasetMutasiControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetMutasiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Idbrg { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public decimal Nilai { get; set; }
    public string Merktype { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Kdklas { get; set; }
    public string Idkey { get { return Idbrg + Asetkey + Tahun + Noreg; } }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Nomutasikel { get; set; }
    #endregion Properties 

    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Unitkey2", "Nomutasikel", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Idkey";
      cViewListProperties.IDProperty = "Idkey";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 15;
      cViewListProperties.RefreshParent = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merktype=Merk/Tipe"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
        Unitkey2 = bo.GetValue("Unitkey2").ToString();
        Nomutasikel = bo.GetValue("Nomutasikel").ToString();
      }
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_KIBMUTASIDET]
		    @UNITKEY = N'{0}',
		    @UNITKEY2 = N'{1}',
		    @NOMUTASIKEL = N'{2}'
      ";

      sql = string.Format(sql, Unitkey, Unitkey2, Nomutasikel);
      string[] fields = new string[] { "Idbrg", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg", "Nilai", "Merktype", "Alamat"
        , "Ket", "Kdkon", "Nmkon", "Kdklas"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetMutasiControl> ListData = new List<ViewasetMutasiControl>();

      foreach (ViewasetMutasiControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion ViewasetMutasi
}


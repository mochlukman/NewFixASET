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
  #region Usadi.Valid49.BO.KiblainnyadetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KiblainnyadetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    public string Keyinv { get; set; }
    public decimal Noreg { get; set; }
    public string Noba { get; set; }
    public string Kdtans { get; set; }
    public string Nmtrans { get; set; }
    public DateTime Tgldokumen { get; set; }
    public decimal Nilai { get; set; }
    public new string Ket { get; set; }
    public string Thang { get; set; }
    public string Uraitrans { get; set; }
    public decimal Nilaitrans { get; set; }
    public string Idkey { get { return Idbrg + Asetkey + Tahun + Noreg; } }
    #endregion Properties 

    #region Methods
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { };
      cViewListProperties.IDKey = "Idkey";
      cViewListProperties.IDProperty = "Idkey";
      cViewListProperties.ReadOnlyFields = new String[] {  };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshParent = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=Nomor BAP"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokumen=Tanggal BAP"), typeof(DateTime), 20, HorizontalAlign.Center).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraitrans=Jenis Transaksi"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaitrans=Nilai"), typeof(decimal), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(KiblainnyaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((KiblainnyaControl)bo).Unitkey;
        Asetkey = ((KiblainnyaControl)bo).Asetkey;
        Keyinv = ((KiblainnyaControl)bo).Keyinv;
      }
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[REGISTER_ASETLAINNYADET]
		    @UNITKEY = N'{0}',
		    @ASETKEY = N'{1}',
		    @KEYINV = N'{2}'
      ";

      sql = string.Format(sql, Unitkey, Asetkey, Keyinv);
      string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Keyinv", "Tahun", "Noreg", "Noba", "Kdtans", "Nmtrans"
        , "Tgldokumen", "Nilai", "Ket", "Thang", "Uraitrans", "Nilaitrans" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<KiblainnyadetControl> ListData = new List<KiblainnyadetControl>();

      foreach (KiblainnyadetControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Kiblainnyadet
}


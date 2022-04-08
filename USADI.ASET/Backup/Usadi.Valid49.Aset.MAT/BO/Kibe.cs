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
  #region Usadi.Valid49.BO.KibeControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibeControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public DateTime Tglperolehan { get; set; }
    public string Noreg { get; set; }
    public string Kdpemilik { get; set; }
    public decimal Nilai { get; set; }
    public decimal Umeko { get; set; }
    public string Asalusul { get; set; }
    public new string Ket { get; set; }
    public string Kdkon { get; set; }
    public string Jdlpenerbit { get; set; }
    public string Bkpencipta { get; set; }
    public string Spesifikasi { get; set; }
    public string Asaldaerah { get; set; }
    public string Pencipta { get; set; }
    public string Bahan { get; set; }
    public string Jenis { get; set; }
    public string Ukuran { get; set; }
    public string Kdlokpo { get; set; }
    public string Kdbrgpo { get; set; }
    public string Kdsatuan { get; set; }
    public string Kdklas { get; set; }
    public string Kdsensus { get; set; }
    public string Kdstatusaset { get; set; }
    public string Kdkib { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Nmpemilik { get; set; }
    public string Nmkon { get; set; }
    public string Nmsatuan { get; set; }
    public string Uraiklas { get; set; }
    public string Kdtahun { get; set; }
    public string Nmtahun { get; set; }
    public string Noba { get; set; }
    public string Kdtans { get; set; }
    public int Jml { get; set; }
    public string Nilinsert { get; set; }
    public int Jmldata { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    public string Keyinv { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewTransaksi",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewTransaksi
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
        return "" + Nmaset + "; Tahun " + Tahun + "; Register " + Noreg + "; " + Jdlpenerbit + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public KibeControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBE;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Asetkey", "Keyinv" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Asetkey", "Kdaset", "Nmaset", "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pencipta"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
      Kdkib = ConstantTablesAsetMAT.KDKIBE;
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
    public new void SetPrimaryKey()
    {
      Kdtans = "000";
      Jml = 1;
      Nilinsert = "2";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<KibeControl> ListData = new List<KibeControl>();
      foreach (KibeControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new void Insert()
    {
      Tahun = Tglperolehan.Year;

      string sql = @"
        exec [dbo].[KIBE_INSERT]
        @Unitkey = N'{0}',
        @Asetkey = N'{1}',
        @Tahun = N'{2}',
        @Kdpemilik = N'{3}',
        @Asalusul = N'{4}',
        @Ket = N'{5}',
        @Kdkon = N'{6}',
        @Jdlpenerbit = N'{7}',
        @Bkpencipta = N'{8}',
        @Spesifikasi = N'{9}',
        @Asaldaerah = N'{10}',
        @Pencipta = N'{11}',
        @Bahan = N'{12}',
        @Jenis = N'{13}',
        @Ukuran = N'{14}',
        @Kdsatuan = N'{15}',
        @Noba = N'{16}',
        @Tglba = N'{17}',
        @Kdtans = N'{18}',
        @Nilai = N'{19}',
        @Jml = N'{20}',
        @Nilinsert = N'{21}'
        ";
      sql = string.Format(sql, Unitkey, Asetkey, Tahun, Kdpemilik, Asalusul, Ket, Kdkon, Jdlpenerbit, Bkpencipta, Spesifikasi
        , Asaldaerah, Pencipta, Bahan, Jenis, Ukuran, Kdsatuan, Noba, Tglperolehan.ToString("yyyy-MM-dd"), Kdtans, Nilai, Jml, Nilinsert);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    public new int Update()
    {
      int n = 0;
      if (IsValid())
      {
        n = base.Update();
      }
      else
      {
        throw new Exception(ConstantDict.Translate("LBL_INVALID_UPDATE"));
      }
      return n;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }

    public const string GROUP_1 = "A. Spesifikasi 1";
    public const string GROUP_2 = "B. Spesifikasi 2";
    public const string GROUP_3 = "C. Nilai";

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      KibeControl cKibeCekdata = new KibeControl();
      cKibeCekdata.Unitkey = Unitkey;
      cKibeCekdata.Asetkey = Asetkey;
      cKibeCekdata.Keyinv = Keyinv;
      cKibeCekdata.Load("Cekjmldata");

      if (cKibeCekdata.Jmldata != 0)
      {
        Jml = 1;
        Nilinsert = "2";
      }

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
      GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), false).SetEnable(enable).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkon=Kondisi"),
      GetList(new KonasetLookupControl()), "Kdkon=Nmkon", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), true, 90).SetEnable(enable)
        .SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Spesifikasi"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pencipta"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bahan"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jenis"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ukuran"), true, 90).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
        .SetGroup(GROUP_2));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jml=Jumlah Barang"), true, 15).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));

      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Nilai Total "}
        ,new ParamControl() { Kdpar="2",Nmpar="Nilai Satuan "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Nilinsert=Tipe Nilai"), ParameterRow.MODE_TYPE,
        list, "Kdpar=Nmpar", 70).SetAllowRefresh(false).SetEnable(enable).SetEditable(false).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 50).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));

      return hpars;
    }

    #endregion Methods 
  }
  #endregion Kibe
}


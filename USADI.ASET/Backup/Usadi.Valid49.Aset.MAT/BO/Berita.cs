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
  #region Usadi.Valid49.BO.BeritaControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class BeritaControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglba { get; set; }
    public string Nobap { get; set; }
    public DateTime Tglbap { get; set; }
    public string Kdkegunit { get; set; }
    public string Kdtahap { get; set; }
    public string Idprgrm { get; set; }
    public string Nokontrak { get; set; }
    public string Uraiba { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdbukti { get; set; }
    public string Kdtans { get; set; }
    public string Kddana { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public string Nmprgrm { get; set; }
    public string Uraian { get; set; }
    public string Kdp3 { get; set; }
    public string Nminst { get; set; }
    public decimal Nilai { get; set; }
    public string Nmtrans { get; set; }
    public string Nmdana { get; set; }
    public string Nmbukti { get; set; }
    public int Jmlbast { get; set; }
    public int Jmlbrg { get; set; }
    public int Jmlgenerated { get; set; }
    public int Jmlkib { get; set; }
    public string Unitkey { get; set; }
    public string Noba { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewBeritadetr",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewBeritadetr
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
        return "Rincian Rekening; " + Noba + "- Nilai Kontrak Rp. " + Nilai.ToString("#,##0") + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public BeritaControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLBERITA;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noba" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdtahap", "Kdkegunit", "Nukeg", "Nmkegunit", "Idprgrm" };
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No BAST"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdana=Sumber Dana"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
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
    public new void SetPrimaryKey()
    {
      Kdtans = "101";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
      GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      hpars.Add(KegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
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

      BeritaControl cBeritaCekjmlbast = new BeritaControl();
      cBeritaCekjmlbast.Unitkey = Unitkey;
      cBeritaCekjmlbast.Noba = Noba;
      cBeritaCekjmlbast.Load("Jmlbast");
      Jmlbast = cBeritaCekjmlbast.Jmlbast;

      BeritaControl cBeritaCekjmlbrg = new BeritaControl();
      cBeritaCekjmlbrg.Unitkey = Unitkey;
      cBeritaCekjmlbrg.Noba = Noba;
      cBeritaCekjmlbrg.Load("Jmlbrg");
      Jmlbrg = cBeritaCekjmlbrg.Jmlbrg;

      BeritaControl cBeritaCekjmlgenerated = new BeritaControl();
      cBeritaCekjmlgenerated.Unitkey = Unitkey;
      cBeritaCekjmlgenerated.Noba = Noba;
      cBeritaCekjmlgenerated.Load("Jmlgenerated");
      Jmlgenerated = cBeritaCekjmlgenerated.Jmlgenerated;

      if ((Jmlbrg-Jmlgenerated) == 0 && Jmlbast != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(KontrakLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(enable).SetAllowRefresh(true)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No BAP"), true, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal BAP"), true).SetEnable(enable));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
      GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetEnable(enable).SetEditable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
      GetList(new JbuktiLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(enable).SetEditable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      KegunitControl cKegunit = new KegunitControl();
      cKegunit.Unitkey = Unitkey;
      cKegunit.Kdkegunit = Kdkegunit;
      cKegunit.Kdtahap = Kdtahap;
      cKegunit.Thang = cPemda.Configval.Trim();
      cKegunit.Load("PK");

      if (Kdtans == "101")
      {
        Idprgrm = cKegunit.Idprgrm.Trim();
      }
      else
      {
        Idprgrm = null;
      }

      
      try
      {
        base.Insert();
      }
      catch (Exception ex)
      {
        if (Tglba.Year.ToString().Trim() != cPemda.Configval.Trim())
        {
          throw new Exception("Gagal menyimpan data : tanggal BAST hanya untuk tahun anggaran berjalan");
        }
        else if (Tglbap.Year.ToString().Trim() != cPemda.Configval.Trim())
        {
          throw new Exception("Gagal menyimpan data : tanggal BAP hanya untuk tahun anggaran berjalan");
        }
        else if (ex.Message.Contains("Violation of PRIMARY KEY"))
        {
          string msg = "Gagal menambah data : nomor BAST sudah digunakan";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
        else if (ex.Message.Contains("FK_ASET_BERITA_KONTRAK"))
        {
          string msg = "Gagal menambah data : entri BAST harus memilih kontrak";
          msg = string.Format(msg, Nmunit);
          throw new Exception(msg);
        }
        else if (ex.Message.Contains("FK_ASET_BERITA_JDANA"))
        {
          string msg = "Gagal menambah data : entri BAST harus memilih sumber dana";
          msg = string.Format(msg, Nmunit);
          throw new Exception(msg);
        }
      }
    }
    public new int Update()
    {
      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          string sql = @"
          exec [dbo].[WSP_VALBERITA]
          @Unitkey = N'{0}',
          @Noba = N'{1}',
          @Tglba = N'{2}',
          @Kdtans = N'{3}'
          ";
          sql = string.Format(sql, Unitkey, Noba, Tglba.ToString("yyyy-MM-dd"), Kdtans);
          BaseDataAdapter.ExecuteCmd(this, sql);
        }
        else
        {
          base.Update();
        }
      }
      return n;
    }
    public new int Delete()
    {
      bool cekjmlkib = false;

      BeritaControl cBeritaCekjmlkib = new BeritaControl();
      cBeritaCekjmlkib.Unitkey = Unitkey;
      cBeritaCekjmlkib.Noba = Noba;
      cBeritaCekjmlkib.Load("Jmlkib");

      Jmlkib = cBeritaCekjmlkib.Jmlkib;
      
      cekjmlkib = (Jmlkib >= 1);

      if (Valid)
      {
        if (cekjmlkib == true)
        {
          throw new Exception("BAST sudah digunakan di KIB, pengesahan tidak dapat dicabut");
        }
        return ((BaseDataControlUI)this).Update("Draft");
      }
      else
      {
        Status = -1;
        int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
        return n;
      }
    }
    #endregion Methods 
  }
  #endregion Berita
}


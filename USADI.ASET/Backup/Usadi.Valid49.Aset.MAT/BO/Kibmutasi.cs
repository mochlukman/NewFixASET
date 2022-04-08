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
  #region Usadi.Valid49.BO.KibmutasiControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibmutasiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglmutasikel { get; set; }
    public string Kdtans { get; set; }
    public new string Ket { get; set; }
    public DateTime Tglmutasiter { get; set; }
    public string Nmtrans { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdunit2 { get; set; }
    public string Nmunit2 { get; set; }
    public string Kdnmunit { get; set; }
    public string Kdnmunit2 { get; set; }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Nomutasikel { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewKibmutasidet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Mutasi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewKibmutasidet
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
        return "No. BAST; " + Nomutasikel + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public KibmutasiControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Unitkey2", "Nomutasikel" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 15;
      cViewListProperties.RefreshFilter = true;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit2=Kode Unit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit2=Unit Tujuan"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasiter=Tanggal Terima"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

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
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
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
      List<KibmutasiControl> ListData = new List<KibmutasiControl>();
      foreach (KibmutasiControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      if (Unitkey2 == Unitkey)
      {
        throw new Exception("Gagal menyimpan data : Unit Tujuan tidak boleh sama dengan Unit Asal.");
      }
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglmutasikel.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Proses mutasi hanya untuk tahun anggaran berjalan.");
      }
      base.Insert();
    }
    public new int Update()
    {
      int n = 0;
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglmutasikel.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal mengubah data : Proses mutasi hanya untuk tahun anggaran berjalan.");
      }

      base.Update();
      return n;
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

      hpars.Add(DaftunitMutasiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Kibmutasi

  #region KibmutasiTerima
  [Serializable]
  public class KibmutasiTerimaControl : KibmutasiControl, IDataControlUIEntry
  {
    public KibmutasiTerimaControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
    }
    public new IList View()
    {
      IList list = this.View("Terima");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<KibmutasiControl> ListData = new List<KibmutasiControl>();
      foreach (KibmutasiControl dc in list)
      {
        dc.Valid = (dc.Tglmutasiter != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
      cViewListProperties.PageSize = 20;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit=Kode Unit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit=Unit Asal"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasiter=Tanggal Terima"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = true;
      bool enable = !Valid;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmunit=Unit Asal"), false, 95).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), false).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), false, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new int Update()
    {
      Tglmutasiter = Tglmutasikel;

      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          string sql = @"
          exec [dbo].[WSPX_MUTASI]
          @Unitkey = N'{0}',
          @Unitkey2 = N'{1}',
          @Nomutasikel = N'{2}',
          @Tglmutasikel = N'{3}',
          @Tglmutasiter = N'{4}'
          ";
          sql = string.Format(sql, Unitkey, Unitkey2, Nomutasikel, Tglmutasikel.ToString("yyyy-MM-dd"), Tglmutasiter.ToString("yyyy-MM-dd"));
          BaseDataAdapter.ExecuteCmd(this, sql);

          base.Update("Terima");
        }
      }
      return n;
    }
    public new int Delete()
    {
      KibmutasiControl cKibmutasicekkib = new KibmutasiControl();
      cKibmutasicekkib.Unitkey = Unitkey;
      cKibmutasicekkib.Nomutasikel = Nomutasikel;

      bool cekjmlkib = false;
      IList list = cKibmutasicekkib.View("Jmlkib");
      cekjmlkib = (list.Count >= 1);

      if (cekjmlkib == true)
      {
        throw new Exception("Aset sudah di mutasi, penerimaan tidak dapat dicabut.");
      }

      return ((BaseDataControlUI)this).Update("Draft");
    }
  }
  #endregion KibmutasiTerima
}


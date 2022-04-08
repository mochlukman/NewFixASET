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
  #region Usadi.Valid49.BO.KemitraanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KemitraanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglawal { get; set; }
    public DateTime Tglakhir { get; set; }
    public Decimal Jangkawaktu { get; set; }
    public decimal Nilai { get; set; }
    public string Iddokumen { get; set; }
    public int Idpengajuan { get; set; }
    public new string Ket { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nmp3 { get; set; }
    public string Nminst { get; set; }
    public string Nmtrans { get; set; }
    public string Iddokumenawal { get; set; }
    public string Nodokumenawal { get; set; }
    public string Kdtans { get; set; }
    public string Kdp3 { get; set; }
    public string Nodokumen { get; set; }
    public string Unitkey { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewKemitraandet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Kemitraan";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewKemitraandet
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
        return "No. Dokumen; " + Nodokumen + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public KemitraanControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKEMITRAAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokumen", "Kdp3", "Kdtans" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=No Dokumen Kontrak"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Pihak Ketiga"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

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
      List<KemitraanControl> ListData = new List<KemitraanControl>();
      foreach (KemitraanControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      KemitraanControl cKemitraancekpengajuan = new KemitraanControl();
      cKemitraancekpengajuan.Unitkey = Unitkey;
      cKemitraancekpengajuan.Iddokumen = Iddokumen;
      cKemitraancekpengajuan.Load("Pengajuan");

      Idpengajuan = cKemitraancekpengajuan.Idpengajuan;

      base.Insert();
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

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokumen=Nomor Dokumen Kontrak"), false, 50).SetEnable(enable));
      hpars.Add(KemitraanLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(Daftphk3LookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), true).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), true).SetEnable(enable));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), true, 26).SetEnable(enable).SetEditable(true));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 50).SetEnable(enable).SetEditable(true));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));

      return hpars;
    }

    #endregion Methods 
  }
  #endregion Kemitraan
  #region Sewa
  [Serializable]
  public class SewaControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Sewa");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "301";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }

  }
  #endregion Sewa
  #region Pinjampakai
  [Serializable]
  public class PinjampakaiControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Pinjampakai");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "302";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }
  }
  #endregion Pinjampakai
  #region KSP
  [Serializable]
  public class KspControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Ksp");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "303";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }
  }
  #endregion KSP
  #region KSPI
  [Serializable]
  public class KspiControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Kspi");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "304";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }
  }
  #endregion KSPI
  #region BGS
  [Serializable]
  public class BgsControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Bgs");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "305";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }
  }
  #endregion BGS
  #region BSG
  [Serializable]
  public class BsgControl : KemitraanControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Bsg");
      return list;
    }
    public new void SetPrimaryKey()
    {
      Kdtans = "306";
      Iddokumen = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Iddokumen", 0, "Dokumen", string.Empty, string.Empty);
    }
  }
  #endregion BSG
}


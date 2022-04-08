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
  #region Usadi.Valid49.BO.PindahtanganControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PindahtanganControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Nopindahtangan { get; set; }
    public string Kdtans { get; set; }
    public string Nmtrans { get; set; }
    public DateTime Tglpindahtangan { get; set; }
    public new string Ket { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public int Jmldata { get; set; }
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
        return "Nomor " + Nopindahtangan + "; Jenis Penghapusan " + Nmtrans + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public PindahtanganControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPINDAHTANGAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nopindahtangan", "Kdtans" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopindahtangan=No Dokumen"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Penghapusan"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglpindahtangan=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));
      
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
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
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = false;
      bool enable = !Valid;

      PindahtanganControl cPindahtanganCekdata = new PindahtanganControl();
      cPindahtanganCekdata.Unitkey = Unitkey;
      cPindahtanganCekdata.Nopindahtangan = Nopindahtangan;
      cPindahtanganCekdata.Kdtans = Kdtans;
      cPindahtanganCekdata.Load("Cekjmldata");

      if (cPindahtanganCekdata.Jmldata != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopindahtangan=Nomor"), false, 90).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglpindahtangan=Tanggal"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 90).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));

      return hpars;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglpindahtangan.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Proses pemindahtanganan hanya untuk tahun anggaran berjalan.");
      }
      base.Insert();
    }

    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new int Update()
    {
      Tglvalid = Tglpindahtangan;

      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          base.Update("Sah");
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
      PindahtanganControl cPindahtanganHapus = new PindahtanganControl();
      cPindahtanganHapus.Unitkey = Unitkey;
      cPindahtanganHapus.Nopindahtangan = Nopindahtangan;
      cPindahtanganHapus.Kdtans = Kdtans;
      cPindahtanganHapus.Load("Hapus");

      if (Valid)
      {
        if (cPindahtanganHapus.Jmldata != 0)
        {
          throw new Exception("Aset sudah di tahap transaksi penghapusan, pengesahan tidak dapat dicabut.");
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
  #endregion Pindahtangan

  #region Usadi.Valid49.BO.PenghibahanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenghibahanControl : PindahtanganControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods     
    public new void SetPrimaryKey()
    {
      Kdtans = "202";
    }
    public new IList View()
    {
      IList list = this.View("Hibah");
      return list;
    }

    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Penghibahan

  #region Usadi.Valid49.BO.DijualControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class DijualControl : PindahtanganControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods     
    public new void SetPrimaryKey()
    {
      Kdtans = "201";
    }
    public new IList View()
    {
      IList list = this.View("Dijual");
      return list;
    }

    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Dijual  

  #region Usadi.Valid49.BO.TukarControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class TukarControl : PindahtanganControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods     
    public new void SetPrimaryKey()
    {
      Kdtans = "203";
    }
    public new IList View()
    {
      IList list = this.View("Tukar");
      return list;
    }

    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Tukar

  #region Usadi.Valid49.BO.PmodalControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PmodalControl : PindahtanganControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods     
    public new void SetPrimaryKey()
    {
      Kdtans = "309";
    }
    public new IList View()
    {
      IList list = this.View("Pmodal");
      return list;
    }

    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Pmodal

  #region Usadi.Valid49.BO.PemusnahanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PemusnahanControl : PindahtanganControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods     
    public new void SetPrimaryKey()
    {
      Kdtans = "204";
    }
    public new IList View()
    {
      IList list = this.View("Musnah");
      return list;
    }

    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtanganControl> ListData = new List<PindahtanganControl>();
      foreach (PindahtanganControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Pemusnahan
}


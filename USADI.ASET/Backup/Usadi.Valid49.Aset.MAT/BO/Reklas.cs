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
  #region Usadi.Valid49.BO.ReklasControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ReklasControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglreklas { get; set; }
    public DateTime Tglvalid { get; set; }
    public new string Ket { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nmtrans { get; set; }
    public int Jmldata { get; set; }
    public string Kdtans { get; set; }
    public string Noreklas { get; set; }
    public string Unitkey { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewReklasdet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Reklas";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewReklasdet
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
        return "No. BA Reklas; " + Noreklas + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public ReklasControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLREKLAS;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noreklas", "Kdtans" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreklas=Nomor BA Reklas"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglreklas=Tanggal BA Reklas"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
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
      List<ReklasControl> ListData = new List<ReklasControl>();
      foreach (ReklasControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglreklas.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Proses reklas hanya untuk tahun anggaran berjalan.");
      }
      base.Insert();
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = false;
      bool enable = !Valid;

      ReklasControl cReklasCekdata = new ReklasControl();
      cReklasCekdata.Unitkey = Unitkey;
      cReklasCekdata.Noreklas = Noreklas;
      cReklasCekdata.Kdtans = Kdtans;
      cReklasCekdata.Load("Cekjmldata");

      if (cReklasCekdata.Jmldata != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noreklas=Nomor BA Reklas"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglreklas=Tanggal BA Reklas"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
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
      Tglvalid = Tglreklas;

      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          string sql = @"
          exec [dbo].[WSPX_REKLAS]
          @Unitkey = N'{0}',
          @Noreklas = N'{1}',
          @Tglreklas = N'{2}',
          @Tglvalid = N'{3}'
          ";
          sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Tglvalid.ToString("yyyy-MM-dd"));
          BaseDataAdapter.ExecuteCmd(this, sql);

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
      ReklasControl cReklascekkib = new ReklasControl();
      cReklascekkib.Unitkey = Unitkey;
      cReklascekkib.Noreklas = Noreklas;

      bool cekjmlkib = false;
      IList list = cReklascekkib.View("Jmlkib");
      cekjmlkib = (list.Count >= 1);

      if (Valid)
      {
        if (cekjmlkib == true)
        {
          throw new Exception("Aset sudah di reklas, pengesahan tidak dapat dicabut.");
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
  #endregion Reklas
}


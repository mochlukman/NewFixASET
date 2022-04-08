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
  #region Usadi.Valid49.BO.BeritadetbrglainnyaControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class BeritadetbrglainnyaControl : BeritadetbrgControl, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public new ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewBeritadetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi Barang";
        return new ImageCommand[] { cmd1 };
      }
    }
    public new string ViewBeritadetbrg
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=12&iprev=11&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
        return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
      }
    }
    #endregion Properties
    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noba", "Asetkey", "Urutbrg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Noba", "Kdtans", "Tglba" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
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
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(BeritalainnyaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((BeritalainnyaControl)bo).Unitkey;
        Noba = ((BeritalainnyaControl)bo).Noba;
        Kdtans = ((BeritalainnyaControl)bo).Kdtans;
        Tglba = ((BeritalainnyaControl)bo).Tglba;
        Tglvalid = ((BeritalainnyaControl)bo).Tglvalid;
      }
      else if (typeof(DaftasetControl).IsInstanceOfType(bo))
      {
        Asetkey = ((DaftasetControl)bo).Asetkey;
      }
      else if (typeof(BeritadetbrglainnyaControl).IsInstanceOfType(bo))
      {
        Asetkey = ((BeritadetbrglainnyaControl)bo).Asetkey;
        Kdkib = ((BeritadetbrglainnyaControl)bo).Kdkib;
      }
    }
    public new void SetPrimaryKey()
    {
      BeritadetbrglainnyaControl cBapNourut = new BeritadetbrglainnyaControl();
      cBapNourut.Unitkey = Unitkey;
      cBapNourut.Noba = Noba;
      cBapNourut.Load("Geturutbrg");

      Urutbrg = cBapNourut.Urutbrg;
      Jumlah = 1;
      Mtgkey = null;
      Tahun = Tglba.Year;
    }
    public new IList View()
    {
      IList list = this.View("Lainnya");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<BeritadetbrgControl> ListData = new List<BeritadetbrgControl>();
      foreach (BeritadetbrgControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftasetTetapLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 15).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 50).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false));
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Beritadetbrglainnya  
  #region BeritadetbrglainnyaEdit
  [Serializable]
  public class BeritadetbrglainnyaEditControl : BeritadetbrgEditControl, IDataControlUIEntry
  {
    public new IList View()
    {
      IList list = this.View("Editlainnya");
      return list;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(BeritadetbrglainnyaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((BeritadetbrgControl)bo).Unitkey;
        Noba = ((BeritadetbrgControl)bo).Noba;
        Asetkey = ((BeritadetbrgControl)bo).Asetkey;
        Urutbrg = ((BeritadetbrgControl)bo).Urutbrg;
        Kdkib = ((BeritadetbrgControl)bo).Kdkib;
        Tglba = ((BeritadetbrgControl)bo).Tglba;
        Tglvalid = ((BeritadetbrgControl)bo).Tglvalid;
      }
    }
    public new void SetPrimaryKey()
    {
      Bertingkat = "0";
      Beton = "0";
    }
  }    
  #endregion BeritadetbrglainnyaEdit
}

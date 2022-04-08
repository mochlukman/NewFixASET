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
  #region ConstantTablesAsetMAT
  [Serializable]
  public class ConstantTablesAsetMAT
  {
    public const string KDKIBA = "01";
    public const string KDKIBB = "02";
    public const string KDKIBC = "03";
    public const string KDKIBD = "04";
    public const string KDKIBE = "05";
    public const string KDKIBF = "06";
    public const string KDKIBG = "07";
    public const string KDKIBKEMITRAAN = "08";
    public const string KDKIBLAINNYA = "09";

    public const string XMLATRIBUSI = "Atribusi";
    public const string XMLATRIBUSIDET = "Atribusidet";
    public const string XMLATRIBUSIDETBRG = "Atribusidetbrg";
    public const string XMLBAPKIR = "Bapkir";
    public const string XMLBAPKIRDET = "Bapkirdet";
    public const string XMLBERITA = "Berita";
    public const string XMLBERITADETBRG = "Beritadetbrg";
    public const string XMLBERITADETR = "Beritadetr";
    public const string XMLHAPUSSK = "Hapussk";
    public const string XMLHAPUSSKDET = "Hapusskdet";
    public const string XMLKEMITRAAN = "Kemitraan";
    public const string XMLKEMITRAANDET = "Kemitraandet";
    public const string XMLKIBA = "Kiba";
    public const string XMLKIBADET = "Kibadet";
    public const string XMLKIBB = "Kibb";
    public const string XMLKIBBDET = "Kibbdet";
    public const string XMLKIBC = "Kibc";
    public const string XMLKIBCDET = "Kibcdet";
    public const string XMLKIBD = "Kibd";
    public const string XMLKIBDDET = "Kibddet";
    public const string XMLKIBE = "Kibe";
    public const string XMLKIBEDET = "Kibedet";
    public const string XMLKIBF = "Kibf";
    public const string XMLKIBFDET = "Kibfdet";
    public const string XMLKIBG = "Kibg";
    public const string XMLKIBGDET = "Kibgdet";
    public const string XMLKIBMUTASI = "Kibmutasi";
    public const string XMLKIBMUTASIDET = "Kibmutasidet";
    public const string XMLPENILAIAN = "Penilaian";
    public const string XMLPENILAIANDET = "Penilaiandet";
    public const string XMLPENYUSUTAN = "Penyusutan";
    public const string XMLPENYUSUTANDET = "Penyusutandet";
    public const string XMLPINDAHTANGAN = "Pindahtangan";
    public const string XMLPINDAHTANGANDET = "Pindahtangandet";
    public const string XMLPOSTING = "Posting";
    public const string XMLPOSTPENYUSUTAN = "Postpenyusutan";
    public const string XMLREKLAS = "Reklas";
    public const string XMLREKLASDET = "Reklasdet";
    public const string XMLSKPENGGUNA = "Skpengguna";
    public const string XMLSKPENGGUNADET= "Skpenggunadet";

  }
  #endregion ConstantTablesAsetMAT
}
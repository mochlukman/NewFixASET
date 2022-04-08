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
  #region Usadi.Valid49.BO.JtransBakfLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JtransBakfLookupControl : JtransControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static JtransBakfLookupControl _Instance = null;
    public static JtransBakfLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new JtransBakfLookupControl();
        }
        return _Instance;
      }
    }

    private static List<JtransControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JtransControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JtransBakfLookupControl dc = new JtransBakfLookupControl();
        dc.SetPageKey();
        _ListData = (List<JtransControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JtransBakfLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJTRANS;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("Bakf");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtans"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdtans"));

      JtransBakfLookupControl dclookup = new JtransBakfLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdtans", "Nmtrans" };
      string[] targets = new String[] { "Kdtans", "Nmtrans" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Jenis Transaksi",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdtans=Nmtrans";
    }
  }
  #endregion JtransBakfLookupControl
}


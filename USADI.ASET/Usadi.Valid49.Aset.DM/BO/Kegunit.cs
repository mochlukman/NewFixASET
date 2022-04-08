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
  #region Usadi.Valid49.BO.KegunitControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class KegunitControl : BaseDataControlAsetDM, IDataControlUIEntry, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Idprgrm { get; set; }
    public int Noprior { get; set; }
    public string Kdsifat { get; set; }
    public string Nip { get; set; }
    public DateTime Tglakhir { get; set; }
    public DateTime Tglawal { get; set; }
    public decimal Targetp { get; set; }
    public string Lokasi { get; set; }
    public decimal Jumlahmin1 { get; set; }
    public decimal Pagu { get; set; }
    public decimal Jumlahpls1 { get; set; }
    public string Sasaran { get; set; }
    public string Ketkeg { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public string Nuprgrm { get; set; }
    public string Nmprgrm { get; set; }
    public string Thang { get; set; }
    public string Unitkey { get; set; }
    public string Kdtahap { get; set; }
    public string Kdkegunit { get; set; }
    public string Kegunit
    {
      get
      {
        return Kdkegunit + Unitkey;
      }
    }
    #endregion Properties 

    #region Methods 
    public KegunitControl()
    {
      XMLName = ConstantTablesAsetDM.XMLKEGUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Thang", "Unitkey", "Kdkegunit", "Kdtahap"};
      cViewListProperties.IDKey = "Kdtahap";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Kegunit";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Idprgrm" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] {"Nukeg"};//
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nukeg=No Kegiatan"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkegunit=Uraian Kegiatan"), typeof(string), 100, HorizontalAlign.Left).SetEditable(false));
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
      else if (typeof(PgrmunitControl).IsInstanceOfType(bo))
      {
        Unitkey = ((PgrmunitControl)bo).Unitkey;
        Kdunit = ((PgrmunitControl)bo).Kdunit;
        Nmunit = ((PgrmunitControl)bo).Nmunit;
        Idprgrm = ((PgrmunitControl)bo).Idprgrm;
        Nuprgrm = ((PgrmunitControl)bo).Nuprgrm;
        Nmprgrm = ((PgrmunitControl)bo).Nmprgrm;
        Kdtahap = ((PgrmunitControl)bo).Kdtahap;

      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
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
      List<KegunitControl> ListData = new List<KegunitControl>();
      foreach (KegunitControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
   
    public new void Insert()
    {
      if (IsValid())
      {
        base.Insert();
      }
      else
      {
        throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
      }
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
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }

    public override HashTableofParameterRow GetEntries()
    {
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }

    #endregion Methods 

    #region IDataControlTreeGrid3
    public string ParentNukeg { get { return ParentKode(Nukeg); } set { } }
    public static bool IsRootCondition(KegunitControl dc)
    {
      return (dc.Kdlevel == 0);
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    /*pastikan GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    public new string[] GetKeys()
    {
      return new string[] { "Id", "Unitkey", "Kdkegunit", "Nukeg", "Nmkegunit", "Kdtahap", "Idprgrm", "Noprior", "Kdsifat", "Nip", "Tglakhir", "Tglawal"
        , "Targetp", "Lokasi", "Jumlahmin1", "Pagu", "Jumlahpls1", "Sasaran", "Ketkeg", "Statusicon", "Url", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nukeg"), Width = 200, DataIndex = "Nukeg", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmkegunit"), Width = 500, DataIndex = "Nmkegunit", Align = Ext.Net.TextAlign.Left });
    }
    public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string delim_menu = GlobalAsp.DELIMITER_MENU;
      List<KegunitControl> localList = (List<KegunitControl>)list;
      List<KegunitControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (KegunitControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<KegunitControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<KegunitControl> list = (List<KegunitControl>)inList;
      if (list != null && list.Count > 0)
      {
        KegunitControl parent = list.Find(o => o.Nukeg.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<KegunitControl> children = GetChildren(list, parent);
          foreach (KegunitControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<KegunitControl> GetChildren(List<KegunitControl> domainset, KegunitControl parent)
    {
      List<KegunitControl> children = domainset.FindAll(o => o.Nukeg.StartsWith(parent.Nukeg) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<KegunitControl> list, KegunitControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<KegunitControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Nukeg,
          ExtTreePanelUtil.GetUraian(parent.Nmkegunit, new string[] { "Nmkegunit", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Nukeg,
          ExtTreePanelUtil.GetUraian(parent.Nmkegunit, new string[] { "Nmkegunit", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
    //#region Sum All
    //public static void Update(List<KegunitControl> domainset)
    //{
    //  List<KegunitControl> roots = domainset.FindAll(i => IsRootCondition(i));
    //  if (roots.Count > 0)
    //  {
    //    foreach (KegunitControl r in roots)
    //    {
    //      UpdatedRecFields bobotProgress = SumChildren(domainset, r);
    //      r.SetValue(bobotProgress);
    //    }
    //  }
    //}
    //static UpdatedRecFields SumChildren(List<KegunitControl> domainsetParent, KegunitControl parent)
    //{
    //  UpdatedRecFields localBobotProgress = new UpdatedRecFields();
    //  List<KegunitControl> localDomainset = domainsetParent.FindAll(o => o.Nukeg.StartsWith(parent.Nukeg));

    //  List<KegunitControl> children = GetChildren(localDomainset, parent);
    //  int localNumChildren = 0;
    //  if ((children.Count > 0) && (parent.Type == "H"))
    //  {
    //    localBobotProgress.Nchild = children.Count;
    //    foreach (KegunitControl o in children)
    //    {
    //      localNumChildren++;
    //      UpdatedRecFields childrenBobotProgress = SumChildren(localDomainset, o);
    //      localBobotProgress.Sum(childrenBobotProgress);
    //    }
    //    parent.SetValue(localBobotProgress);
    //    return localBobotProgress;
    //  }
    //  else
    //  {
    //    localBobotProgress.SetValue(parent);
    //    localBobotProgress.Nchild = 0;
    //    return localBobotProgress;
    //  }
    //}
    //void SetValue(UpdatedRecFields rec)
    //{
    //  Nchild = rec.Nchild;
    //}
    //class UpdatedRecFields
    //{
    //  public int Nchild = 0;

    //  public void Sum(UpdatedRecFields rec)
    //  {
    //    Nchild += rec.Nchild;
    //  }

    //  public void SetValue(KegunitControl rec)
    //  {
    //    Nchild = rec.Nchild;
    //  }
    //}
    //#endregion

  }
  #endregion Kegunit
}


<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="Portal" CodeFile="Portal.aspx.cs" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Portal</title>
  <link href="Res/css/common.css" rel="stylesheet" type="text/css" />
  <link href="Res/css/userbox.css" rel="stylesheet" type="text/css" />
  <link href="Res/img/icon.ico" rel="shortcut icon" />
  <script type="text/javascript">
  </script>
</head>
<body>
  <form id="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
      <Items>
        <ext:BorderLayout ID="BorderLayout1" runat="server">
          <North>
            <ext:Panel ID="TopPanel" runat="server" Height="64px" Layout="BorderLayout">
              <TopBar>
                <ext:Toolbar ID="StatusBar1" runat="server" LabelAlign="Left" Height="32px" Layout="BorderLayout">
                  <Items>
                    <ext:Panel ID="Panel1" runat="server" Visible="true" AutoHeight="true" Border="false"
                      Header="false" Region="North" Html="">
                    </ext:Panel>
                    <ext:Panel ID="pnlUser" runat="server" Visible="true" AutoHeight="true" Border="false"
                      Header="false" Region="Center" Html="" AnchorHorizontal="100%">
                    </ext:Panel>
                  </Items>
                </ext:Toolbar>
              </TopBar>
              <Items>
                <ext:Toolbar runat="server" ID="toolBar" Region="Center">
                  <Items>
                  </Items>
                </ext:Toolbar>
              </Items>
            </ext:Panel>
          </North>
          <Center>
            <ext:Panel ID="PanelCenter" runat="server">
              <AutoLoad Mode="IFrame" ShowMask="true" />
<%--              <BottomBar>
                <ext:StatusBar ID="StatusBar2" runat="server">
                  <Items>
                    <ext:ToolbarFill />
                    <ext:Label ID="Watch" runat="server" Cls="watch" />
                  </Items>
                </ext:StatusBar>
              </BottomBar>--%>
            </ext:Panel>
          </Center>
          <East>
          </East>
        </ext:BorderLayout>
      </Items>
    </ext:Viewport>
    <ext:Window ID="WindowLink" runat="server" Icon="CarRed" Title="Garuda" Hidden="true"
      Modal="true" Width="700" Height="450">
      <Items>
        <ext:Panel runat="server" ID="PanelLink" Frame="true" Height="420">
        </ext:Panel>
      </Items>
    </ext:Window>
  </form>
</body>
</html>

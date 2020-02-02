<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFlexMLS_Featured.ascx.cs" Inherits="GIBS.Modules.FlexMLS_Featured.ViewFlexMLS_Featured" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS/css/Style.css?1=1" />

<!-- fotorama.css & fotorama.js. -->
<link  href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet" /> <!-- 3 KB -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js" type="text/javascript"></script> <!-- 16 KB -->

<!-- 2. Add images to <div class="fotorama"></div>. -->


<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
<div class="searchcriteria" id="divSearchCriteria" runat="server"><asp:Label ID="lblSearchCriteria" runat="server" Text="" Visible="True"></asp:Label></div>


<asp:Label ID="lblSearchSummary" runat="server" Text="0 Records Found"></asp:Label>


<!-- BROKEN ---->
<div class="container-fluid">
<div class="row row-eq-height">
    <asp:DataList ID="lstContent" DataKeyField="ItemID" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemDataBound="lstContent_ItemDataBound">

        <ItemTemplate>

            <div class='<% = _ItemCssClass %>'>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="fotorama" data-fit="cover" data-width="100%" data-ratio="800/600">
                            <asp:Image ID="imgListingImage" runat="server" CssClass="img-responsive" BorderWidth="1px" />
                        </div>
                    </div>
                    <div class="panel-body text-center">
                        <div><b><asp:HyperLink ID="hyperLink1" runat="server" /></b>
                        </div>
                        <div>
                            <asp:HyperLink ID="hyperLink2" runat="server" />
                        </div>
                        <asp:Label ID="lblContent" runat="server" />
                    </div>
                </div>



            </div>

        </ItemTemplate>
    </asp:DataList>
</div>
</div>

<div class="row">
    <div id="Paging_Data" runat="server" class="col-md-12">
        <div class="text-center">
            <ul class="pagination text-center">
                <li>
                    <asp:LinkButton ID="lbPrev" runat="server" OnClick="lbPrev_Click"> << </asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click"> >> </asp:LinkButton></li>
            </ul>
        </div>
        <div class="text-center">
            <asp:Label ID="lblCurrentPage" runat="server" />
        </div>
     
    </div>
</div>
<asp:HiddenField ID="hidPage" runat="server" />

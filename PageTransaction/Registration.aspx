<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="PageTransaction_Registration" %>

<%@ Register Src="~/MessageBox/AlertMessage.ascx" TagPrefix="uc1" TagName="AlertMessage" %>

<%@ Register Src="~/EmpSearch/EmpSearch.ascx" TagPrefix="uc1" TagName="EmpSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .rowHeight
        {
            padding: 2px !important;
            margin: auto !important;
            /*line-height: 0px !important;*/        
        }
        .alignRight
        {
            text-align: right !important;      
        }
        .alignCenter
        {
            text-align: center !important;        
        }
         /* The container */
        .chkContainer {
            display: block;
            position: relative;
            padding-left: 40px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: medium;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            top: 3px;
            left: 0px;
            width: 264px;
            height: 24px;
        }

        /* Hide the browser's default checkbox */
        .chkContainer input {
            position: absolute;
            opacity: 0;
            cursor: pointer;
        }

        /* Create a custom checkbox */
        .checkmark {
            position: absolute;
            top: 3px;
            left: 0px;
            height: 22px;
            width: 22px;
            background-color: #ccc;
        }

        /* On mouse-over, add a grey background color */
        .chkContainer:hover input ~ .checkmark {
            background-color: #aaa;
        }

        /* When the checkbox is checked, add a blue background */
        .chkContainer input:checked ~ .checkmark {
            background-color: #555;
        }

        /* Create the checkmark/indicator (hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */
        .chkContainer input:checked ~ .checkmark:after {
            display: block;
        }
        .uppercase
        {
            text-transform: uppercase;
        }
        .center-dropdown {
            display: block;
            margin: 0 auto;
        }


        /* Style the checkmark/indicator */
        .chkContainer .checkmark:after {
            left: 8px;
            top: 3px;
            width: 7px;
            height: 14px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divStatusMenu">
    <asp:UpdatePanel ID="upnlStatusMenu" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
          
            <div class="container-fluid" id="divControlNo" runat="server">
                    
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

    <div id="divRequestorInfo" runat="server">
        <asp:UpdatePanel ID="uPnlRequestorInfo" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divRequestorInfo_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                        <div class="card-header text-white" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divRequestorInfo_Body">
                            <asp:Label ID="lblPCInfoHeader" runat="server" CssClass="font-weight-bold">
                                R E Q U E S T O R ' S &nbsp; I N F O R M A T I O N
                            </asp:Label>
                        </div>
                        <div id="divRequestorInfo_Body" class="card-body collapse show py-0">
                        <div class="row small text-secondary mb-2">
                            <div class="col-xl-12 col-lg-6 col-md-6 col-sm-12 input-group-sm">
                              <div class="row text-secondary mt-2">
                                  <div class="col-xl-4 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                      <asp:Label ID="Label9" runat="server" CssClass="col-form-label text-dark "> Control Number: </asp:Label>
                                       <asp:TextBox ID="txtControlNo" runat="server" CssClass="form-control font-weight-bold text-center uppercase" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                                  </div> 
                                   <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-3">
                                  </div> 
                                  <div class="col-xl-4 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                      <asp:Label ID="Label8" runat="server" CssClass="col-form-label text-dark "> Status: </asp:Label>
                                      <asp:TextBox ID="txtReqStatus" runat="server" CssClass="form-control font-weight-bold text-center uppercase" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                                      
                                  </div> 
                                </div>
                             </div>
                         </div>

                            <div class="row small text-secondary my-3">
                                <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblRequestorName" runat="server" CssClass="col-form-label text-dark"> Requestor's Name: </asp:Label>
                                    <asp:TextBox ID="txtRequestor_EmpName" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>                             
                                <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblRequestorDept" runat="server" CssClass="col-form-label text-dark"> Department: </asp:Label>
                                    <asp:TextBox ID="txtRequestor_Dept" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>
                                <div class="col-xl-3 col-lg-2 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblRequestorLocalNo" runat="server" CssClass="col-form-label text-dark"> Local Number: </asp:Label>
                                    <asp:TextBox ID="txtRequestor_LocalNo" runat="server" CssClass="form-control text-dark text-center" BackColor="White" MaxLength="4" onkeypress="return isNumberKey(event)" onpaste="return false" EnableViewState="False" autocomplete="off"></asp:TextBox>
                                    <script type="text/javascript">
                                       function isNumberKey(evt) {
                                           var charCode = (evt.which) ? evt.which : event.keyCode;
                                           if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                               return false;
                                           }
                                           return true;
                                       }
                                    </script>

                                </div>  
                                <div class="col-xl-3 col-lg-2 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblRequestorLocation" runat="server" CssClass="col-form-label text-dark"> Location: </asp:Label>
                                    <asp:DropDownList ID="ddlLocation" CssClass="form-control text-dark text-center" runat="server"></asp:DropDownList>
                                   </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divDetailsOfReq" runat="server">
        <asp:UpdatePanel ID="upnlDetailsOfReq" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divDetailsOfReq_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;" >
                        <div class="card-header text-white" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divDetailsOfReq_Body">
                            <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold">
                                D E T A I L S  &nbsp; O F &nbsp; R E Q U E S T &nbsp; F O R &nbsp; Q U O T A T I O N
                            </asp:Label>
                        </div>
                        <div id="divDetailsOfReq_Body" class="card-body collapse show py-0">                        
                            <div class="row small text-secondary mt-2">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblCategory" runat="server" CssClass="col-form-label text-dark"> Category: </asp:Label>
                                </div>                             
                                <div class="col-xl-5 col-lg-4 col-md-4 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblNumOfReq" runat="server" CssClass="col-form-label text-dark"> Number of Request: &nbsp; </asp:Label>
                                    <asp:Label ID="lblMax" runat="server" CssClass="col-form-label text-danger font-italic medium" Font-Bold="True"> &nbsp; (Note: Maximum of 30 items)</asp:Label>
                                    
                                </div>    
                            </div>
                            <div class="row small text-secondary mb-3">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 input-group-sm mt-2">
                                    <%--<asp:DropDownList ID="ddlCategory" CssClass="form-control text-dark text-center" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" >
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control text-dark text-center" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Select Category --" Value="0" />
                                        <asp:ListItem Text="Technology" Value="1" />
                                        <asp:ListItem Text="Health" Value="2" />
                                        <asp:ListItem Text="Finance" Value="3" />
                                        <asp:ListItem Text="Education" Value="4" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control text-dark" Visible="false" ReadOnly="true" BackColor="White"></asp:TextBox>    
                                </div>  
                                <div class="col-xl-3 col-lg-4 col-md-4 col-sm-12 input-group-sm mt-2">
                                    <asp:TextBox ID="txtNumOfReq" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                                </div>
                            </div>
                             <div class="row small text-secondary">
                                <div class="col-xl-12 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-6">
                                    <asp:Label ID="lblNote" runat="server" CssClass="col-form-label text-warning font-italic medium" Visible="False" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row medium text-secondary">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 mb-2" id="divRFQ_List" runat="server" visible="true">
                                    <asp:HiddenField ID="hdnRFQ_Source" runat="server" Value="type"/>
                                    <div class="table-responsive">  
                                        <asp:GridView ID="gvRFQ_List" CssClass="footable" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvRFQ_List_RowDataBound"
                                             FooterStyle-CssClass="font-weight-bold text-toshiba-orange" ShowFooter="false" FooterStyle-Height="5" Visible="False">
                                            <Columns>                                                
                                                <asp:TemplateField HeaderText="         ">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                     </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter " ForeColor="White" Font-Bold="True" /> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />                                                                               
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="RFQ No">
                                                    <ItemTemplate>   
                                                         <asp:Label ID="lblControlNo" runat="server" CssClass="col-form-label" Visible="true" Text='<%# Eval("TempItemCode") %>' ForeColor="White" Font-Bold="True"> lblItem </asp:Label>
                                                         <asp:HiddenField ID="hdnControlNo" runat="server" Value='<%# Eval("TempItemCode") %>' />
                                                   </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description">
                                                    <ItemTemplate>      
                                                        <asp:TextBox ID="txtItemDesc" Width="300" runat="server" CssClass="form-control text-dark" Visible="true" Text='<%# Eval("ItemDescription") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblItemDesc" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("ItemDescription") %>' ForeColor="White" Font-Bold="True"> lblItem </asp:Label>
                                                        <asp:HiddenField ID="hdnValid" runat="server" Value='<%# Eval("Valid") %>'/>
                                                        <asp:HiddenField ID="hdnTempItemCode" runat="server"  Value='<%# Eval("TempItemCode") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" Width="100" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control text-dark" Text='<%# Eval("Quantity") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <script type="text/javascript">
                                                            function isNumberKey(evt) {
                                                                var charCode = (evt.which) ? evt.which : event.keyCode;
                                                                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                                                    return false;
                                                                }
                                                                return true;
                                                            }
                                                        </script>
                                                        <asp:Label ID="lblQty" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("Quantity") %>' ForeColor="White" Font-Bold="True"> lblQty </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUOM" Width="200" runat="server" CssClass="form-control text-dark" Text='<%# Eval("UOM") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblUOM" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("UOM") %>' ForeColor="White" Font-Bold="True"> lblUOM </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBudget" Width="150" runat="server" CssClass="form-control text-dark" Text='<%# Eval("Budget") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblBudget" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("Budget") %>' ForeColor="White" Font-Bold="True"> lblBudget </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCurrency" Width="170" CssClass="form-control text-dark center-dropdown" runat="server" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="lblCurrency" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("Currency") %>' ForeColor="White" Font-Bold="True"> lblCurrency </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPurpose" Width="300" runat="server" CssClass="form-control text-dark" Text='<%# Eval("Purpose") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblPurpose" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("Purpose") %>' ForeColor="White" Font-Bold="True"> lblPurpose </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" Width="200" runat="server" CssClass="form-control text-dark" Text='<%# Eval("Remarks") %>' BackColor="White" autocomplete="off">
                                                        </asp:TextBox>
                                                        <asp:Label ID="lblRemarks" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("Remarks") %>' ForeColor="White" Font-Bold="True"> lblRemarks </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buyer In-charge" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlBuyer" Width="200" CssClass="form-control text-dark center-dropdown" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                                                        <asp:Label ID="lblBuyer" runat="server" CssClass="col-form-label" Visible="false" Text='<%# Eval("BuyerInCharge") %>' ForeColor="White" Font-Bold="True"> </asp:Label>
                                                         <asp:HiddenField ID="hdnBuyerEmpNo" runat="server" Value='<%# Eval("BuyerInChargeEmpNo") %>'/>
                                                         <asp:HiddenField ID="hdnStats" runat="server" Value='<%# Eval("Status") %>'/>
                                                      </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAddItem" runat="server" CssClass="btn btn-success rounded-left" OnCommand="lbtnAddItem_Command" CommandArgument='<%# Eval("TempItemCode") %>'>
                                                            &nbsp; Add &nbsp;
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeleteItem" runat="server" CssClass="btn btn-danger rounded-left" OnCommand="lbtnDeleteItem_Command" CommandArgument='<%# Eval("TempItemCode") %>' Visible="false">
                                                            &nbsp; Delete &nbsp;
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="font-weight-bold text-toshiba-orange" Height="5px" />
                                        </asp:GridView>                                    
                                    </div>
                                </div>
                            </div>

                             <div class="row medium text-secondary">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 mb-2" id="div1" runat="server" visible="true">
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="type"/>
                                    <div class="table-responsive">  
                                        <asp:GridView ID="gvDetailsViewOnly" CssClass="footable" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDetailsViewOnly_RowDataBound" 
                                            FooterStyle-CssClass="font-weight-bold text-toshiba-orange" ShowFooter="false" FooterStyle-Height="5" Visible="False">
                                            <Columns>                                                
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter text-dark"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />                                                                               
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="View" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-link small" CommandArgument='<%# Eval("TempItemCode") %>' CommandName='<%# Eval("TempItemCode") %>' OnCommand="lbtnView_Command">
                                                            <i class="fa fa-info-circle"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RFQ No">
                                                    <ItemTemplate>  
                                                        <asp:Label ID="lblControlNo" runat="server" CssClass="col-form-label text-dark" Visible="true" Text='<%# Eval("TempItemCode") %>' > lblControlNo </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description">
                                                    <ItemTemplate>  
                                                        <asp:Label ID="lblItemDesc" Width="300" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("ItemDescription") %>' > lblItem </asp:Label>
                                                        <asp:HiddenField ID="hdnValid" runat="server" Value='<%# Eval("Valid") %>'/>
                                                        <asp:HiddenField ID="hdnTempItemCode" runat="server"  Value='<%# Eval("TempItemCode") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Width="50" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Quantity") %>' > lblQty </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" Width="200" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("UOM") %>' > lblUOM </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBudget" Width="150" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Budget") %>' > lblBudget </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrency" Width="100" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Currency") %>' > lblCurrency </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurpose" Width="300" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Purpose") %>' > lblPurpose </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" Width="200" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Remarks") %>'> lblRemarks </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buyer In-charge" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBuyer" Width="200" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("BuyerInCharge") %>'> lblBuyer</asp:Label>
                                                      </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Width="200" runat="server" ForeColor="White" CssClass="col-form-label font-weight-bold" Text='<%# Eval("Status") %>'> lblStatus</asp:Label>
                                                      </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="font-weight-bold text-toshiba-orange" Height="5px" />
                                        </asp:GridView>                                    
                                    </div>
                                </div>
                            </div>
                             <div id="divFileUpload" runat="server" class="row small text-secondary" visible="false">
                                <div class="col-xl-2 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2 form-group">  
                                </div>
                                <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2 form-group">                                    
                                    <div class="input-group input-group-sm">
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-secondary text-light"> Select File </span>
                                        </div>
                                        <div class="custom-file">
                                            <asp:FileUpload ID="fuRFQList" runat="server" class="custom-file-input"/>
                                            <label class="custom-file-label" for="fuRFQList" runat="server">Choose file</label>
                                        </div>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbtnUpload_RFQList" runat="server" CssClass="btn btn-primary rounded-left" OnClick="lbtnUpload_RFQList_Click">
                                            &nbsp; <i class="fas fa-upload"></i> &nbsp; Upload &nbsp;
                                        </asp:LinkButton> &nbsp; 
                                        <asp:LinkButton ID="lbtnDownloadTemplate" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnDownloadTemplate_Click">
                                            &nbsp; <i class="fas fa-download"></i> &nbsp; Download Template &nbsp;
                                        </asp:LinkButton>                                                              
                                    </div> 
                                </div>  
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="gvRFQ_List" />
                 <asp:PostBackTrigger ControlID="lbtnDownloadTemplate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divReqAttach" runat="server">
        <asp:UpdatePanel ID="upnlReqAttach" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divReqAttach_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                        <div class="card-header" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divReqAttach_Body">
                            <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold">
                                R E Q U E S T O R ' S &nbsp; A T T A C H M E N T
                            </asp:Label>
                        </div>
                        <div id="divReqAttach_Body" class="card-body collapse show py-0">  
                               <div class="row small text-secondary mt-3">
                            <div class="col-xl-3 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 alignRight">
                                <asp:Label ID="Label44" runat="server" CssClass="font-weight-bold large text-primary"> ATTACHMENT: </asp:Label>
                            </div>
                            <div id="divUploadAttach" runat="server" class="col-xl-6 col-lg-8 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-append">
                                        <span class="input-group-text bg-secondary text-light"> Select File </span>
                                    </div>
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuAttachment" runat="server" class="custom-file-input" />
                                        <asp:Label CssClass="custom-file-label" ID="lblFilePath" AssociatedControlID="fuAttachment" runat="server"> Choose file </asp:Label>
                                       <%-- <label class="custom-file-label" for="fuAttachment">Choose file</label>--%>
                                    </div> &nbsp;&nbsp;
                                    <%--<asp:FileUpload ID="fuAttachment" runat="server" CssClass="w-50"/> &nbsp;--%>
                                    <asp:LinkButton ID="lbtnUpload" runat="server" CssClass="btn btn-primary rounded-left" OnCommand="lbtnUpload_Command">
                                        &nbsp; <i class="fas fa-upload"></i> &nbsp; Upload &nbsp;
                                    </asp:LinkButton>                                            
                                </div>  
                            </div>     
                        </div> 
                            <div class="row small text-secondary mt-1">
                                <div class="col-xl-3 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                      <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold large text-primary"></asp:Label>
                                </div>
                                <div class="col-xl-8 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                      <asp:Label ID="lblAtt" runat="server" CssClass="col-form-label"> List of Attachments: </asp:Label>
                                      <asp:Label ID="lblAtNote" runat="server" CssClass="col-form-label text-warning font-italic small" Text=" (Note: All changes on list of attachments is automatically saved to database.): " Font-Bold="True"></asp:Label>
                                 </div>
                            </div>
                             <div class="row small text-secondary">
                            <div class="col-xl-1 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:Label ID="Label47" runat="server" CssClass="font-weight-bold large">   </asp:Label>
                             </div> 
                            <div class="col-xl-10 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="row medium text-secondary"> 
                                    
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mb-3">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAttachment" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                                EmptyDataText="There are no data records to display." AllowSorting="True" AllowPaging="true" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />                                                                               
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filename">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFilename" runat="server" CssClass="col-form-label" Text='<%# Eval("FileName_Orig") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transacted By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedBy_EmpName" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedBy_EmpName") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transacted Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnOpen" runat="server" CssClass="btn btn-link text-success" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnOpen_Command">
                                                                <i class="fas fa-folder-open"></i> Open
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-link text-danger" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnDeleteAttachment_Command" >
                                                                <i class="fas fa-trash"></i> Delete
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>                               
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnUpload" />
                <asp:PostBackTrigger ControlID="gvAttachment" />
                <%--<asp:PostBackTrigger ControlID="lblFilePath" />--%>
        </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divApprover" runat="server">
        <asp:UpdatePanel ID="uPnlApprover" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="div2" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                        <div class="card-header" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divApprovers_Body">
                            <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">
                                A P P R O V E R S
                            </asp:Label>
                        </div>
                        <div id="divApprovers_Body" class="card-body collapse show py-0">  
                            <div class="row medium text-secondary mt-3 mb-1">
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm alignRight">
                                            <asp:Label ID="Label25" runat="server" CssClass="col-form-label font-weight-bold text-primary"> LIST OF APPROVERS : </asp:Label>
                                    </div>
                                    
                                </div>

                          <div class="row small text-secondary">
                            <div  id="divApprover1" runat="server" class="col-xl-10 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2" style="margin-left:auto;margin-right:auto;">
                                <div class="row medium text-secondary"> 
                                       <div class="col-xl-1 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                        <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <div class="table-responsive">
                                             <asp:GridView ID="gvApproval" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                                 PageSize="5" AllowSorting="False" AllowPaging="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />                                                                               
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Position">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPosition" runat="server" CssClass="col-form-label" Text='<%# Eval("Position") %>'></asp:Label>       
                                                            <asp:Label ID="lblPosition1" runat="server" CssClass="col-form-label" Visible="false"></asp:Label>                                                 
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>                                            
                                                    <asp:TemplateField HeaderText="Approver">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlApprover" CssClass="form-control text-dark" runat="server"></asp:DropDownList>  
                                                            <asp:Label ID="lblApprover" runat="server" CssClass="col-form-label" Text='<%# Eval("EmpName") %>' Visible="false"></asp:Label>    
                                                            <asp:Label ID="lblApprover1" runat="server" CssClass="col-form-label" Visible="false"></asp:Label>             
                                                            <asp:HiddenField ID="hdnApprover_EmpNo" runat="server" Value='<%# Eval("EmpNo") %>' />                                        
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>   
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSaveApp" runat="server" CssClass="btn btn-primary rounded-left">
                                                                &nbsp; <i class="fas fa-save"></i> &nbsp;&nbsp; Save &nbsp;
                                                            </asp:LinkButton>           
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField> 
                                                </Columns>
                                                 <HeaderStyle ForeColor="Black" />
                                            </asp:GridView>                              
                                        </div>
                                    </div>
                                </div>
                            </div>
                           </div>

                                <div class="row small text-secondary">
                            <div  id="divApprover2" runat="server" class="col-xl-10 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2" visible="false" style="margin-left:auto;margin-right:auto;">
                                <div class="row medium text-secondary"> 
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <div class="table-responsive">                         
                                            <asp:GridView ID="gvApproval1" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                                 PageSize="5" AllowSorting="False" AllowPaging="False" OnRowDataBound="OnRowDataBound" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />                                                                               
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Position">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPosition" runat="server" CssClass="col-form-label" Text='<%# Eval("Position") %>'></asp:Label> 
                                                            <asp:Label ID="lblPosition1" runat="server" CssClass="col-form-label" Visible="false"></asp:Label>  
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>                                            
                                                    <asp:TemplateField HeaderText="Approver">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlApprover" CssClass="form-control text-dark" runat="server" Visible="false" ></asp:DropDownList>  
                                                            <asp:Label ID="lblApprover" runat="server" CssClass="col-form-label" Text='<%# Eval("EmpName") %>'></asp:Label>   
                                                            <asp:HiddenField ID="hdnApprover_EmpNo" runat="server" Value='<%# Eval("EmpNo") %>' />  
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" ForeColor="White" CssClass="col-form-label font-weight-bold" Text='<%# Eval("Status") %>'></asp:Label> 
                                                            <asp:HiddenField ID="hdnApprover_Status" runat="server" Value='<%# Eval("Status") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" CssClass="col-form-label" Text='<%# Eval("Remarks") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Transaction Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUpdateDate" runat="server" CssClass="col-form-label" Text='<%# Eval("DateApproved", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label> 
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>                                                    
                                                </Columns>
                                                <HeaderStyle ForeColor="Black" />
                                            </asp:GridView>                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                           
                            <div class="row small text-secondary">
                             <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 input-group-sm mt-4 text-center" style="margin-left:auto;margin-right:auto;">
                                  <asp:LinkButton ID="lbtnEditApprover" Visible="false" runat="server" CssClass="btn btn-success rounded-center" OnClick="lbtnEditApprover_Click" > <i class="fa fa-edit"></i> Edit Approvers</asp:LinkButton>
                                  <asp:LinkButton ID="lbtnSaveApprover" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnSaveApprover_Click" > <i class="fa fa-save"></i> Save Approvers</asp:LinkButton>
                                  <asp:LinkButton ID="lbtnCancelEdit" Visible="false" runat="server" CssClass="btn btn-danger rounded-center" OnClick="lbtnCancelEdit_Click" ><i class="fa fa-ban"></i> Cancel Edit Approvers</asp:LinkButton>

                             </div>
                           </div>
                            <div class="row small text-secondary mt-3">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 input-group-sm mt-1 mb-2 text-center" style="margin-left:auto;margin-right:auto;">
                                <asp:LinkButton ID="lbtnSubmit" Visible="false" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnSubmit_Click" > <i class="fa fa-save"></i> Submit </asp:LinkButton> 
                                <asp:LinkButton ID="lbtnUpdate" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnUpdate_Click"> <i class="fa fa-edit"></i> Update </asp:LinkButton>
                                <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnSave_Click"> <i class="fa fa-save"></i> Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveUpdate" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnSaveUpdate_Click"> <i class="fa fa-save"></i> Save Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveBuyer" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnSaveBuyer_Click"> <i class="fa fa-save"></i> Save </asp:LinkButton>
                                <asp:LinkButton ID="lbtnApprove" Visible="false" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnApprove_Click"> <i class="fa fa-thumbs-up"></i> Approve</asp:LinkButton> 
                                <asp:LinkButton ID="lbtnDeny" Visible="false" runat="server" CssClass="btn btn-danger rounded-center" OnClick="lbtnDeny_Click" > <i class="fa fa-thumbs-down"></i> Deny</asp:LinkButton>
                                <asp:LinkButton ID="lbtnReSubmit" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnReSubmit_Click"> <i class="fa fa-undo"></i> Re-Submit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnAssignBuyer" Visible="false" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnAssignBuyer_Click" > <i class="fa fa-user"></i> Assign Buyer </asp:LinkButton> 
                                <asp:LinkButton ID="lbtnReAssignBuyer" Visible="false" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnReAssignBuyer_Click" > <i class="fa fa-user"></i> Re-assign Buyer </asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveNewBuyer" Visible="false" runat="server" CssClass="btn btn-info rounded-center" OnClick="lbtnSaveNewBuyer_Click"> <i class="fa fa-save"></i> Save new buyer</asp:LinkButton> 
                                <asp:LinkButton ID="lbtnCancelUpdate" Visible="false" runat="server" CssClass="btn btn-danger rounded-center" OnClick="lbtnCancelUpdate_Click"><i class="fa fa-ban"></i> Cancel</asp:LinkButton>
                                <asp:LinkButton ID="lbtnExport" runat="server" CssClass="btn btn-success rounded-left" Visible="false" OnClick="lbtnExport_Click1">
                                        &nbsp; <i class="fas fa-download"></i> &nbsp; Export to Excel &nbsp;
                                </asp:LinkButton>
                            </div>
                       </div>
                        </div>
                        
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnUpload" />
                <asp:PostBackTrigger ControlID="gvAttachment" />
                <asp:PostBackTrigger ControlID="lbtnExport" />
                <%--<asp:PostBackTrigger ControlID="lblFilePath" />--%>
        </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divReminders" runat="server">
        <asp:UpdatePanel ID="upnlReminders" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divReminders_Header" runat="server">
                    <div class="card text-light text-info mt-1 " style="border-color: #0d98ba ;">
                        <div class="card-header" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divReminders_Body">
                            <asp:Label ID="Label38" runat="server" CssClass="font-weight-bold">
                                R E M I N D E R S 
                            </asp:Label>
                        </div>
                        <div id="divReminders_Body" class="card-body collapse show py-0"> 
                            <div id="divRemindersNote" runat="server">
                                <div class="row small text-secondary" >
                                    <div class="col-xl-1 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 ">
                                         <asp:Label ID="Label50" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="NOTE: "></asp:Label>
                                        </div>
                                    <div class="col-xl-11 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                        <asp:Label ID="Label49" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="ITEM DESCRIPTION MUST HAVE THE FOLLOWING:"></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label41" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="1. Complete specification."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label40" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="2. Brand."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label4" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="3. Part Number."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label5" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="4. Size."></asp:Label>
                                    </div>
                                </div>    
                                
                                <div class="row small text-secondary">
                                    <div class="col-xl-1 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                    <div class="col-xl-11 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                        <asp:Label ID="Label39" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="ATTACHMENT REQUIRED:"></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label46" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="1. Item specification."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label7" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="2. Reference picture."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label10" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="3. Max of 10 MB per attached file."></asp:Label>
                                    </div>
                                    <div class="col-xl-2 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        </div>
                                     <div class="col-xl-10 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <asp:Label ID="Label11" runat="server" CssClass="col-form-label text-danger font-italic small" Font-Bold="True" Text="4. Please avoid special character on attached file name (example: @,  #, %, &,”, +, etc.) ."></asp:Label>
                                    </div>
                                </div> 
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </div>
     <asp:UpdatePanel ID="upnlLogs" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="container-fluid" id="divLogs" runat="server">
                <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                    <div class="card-header " style="background-color: #0d98ba ;">
                        <asp:Label ID="lblLogsHeader" runat="server" CssClass="font-weight-bold">
                            L O G S
                        </asp:Label>
                    </div>
                    <div id="divLogs_Body" class="card-body">                
                        <div class="row small text-info">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                <div class="table-responsive">                          
                                    <asp:GridView ID="gvHistoryLogs" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                        EmptyDataText="There are no data records to display." PageSize="5" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvHistoryLogs_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />                                                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Taken">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActionTaken" runat="server" CssClass="col-form-label" Text='<%# Eval("ActionTaken") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>   
                                           <%-- <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetails" runat="server" CssClass="col-form-label" Text='<%# Eval("Details") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField> --%>                                                                             
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="col-form-label" Text='<%# Eval("Remarks") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy_EmpName" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedBy") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
                    
    <div id="divActionButton">
        <asp:UpdatePanel ID="upnlActionButton" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row input-group input-group-sm mt-2 mb-2">
                        <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                            <asp:HiddenField ID="hdnRefID" runat="server" />     
                            <asp:HiddenField ID="hdnAction" runat="server" />
                            <asp:HiddenField ID="hdnUserEmpNo" runat="server" />
                            <asp:HiddenField ID="hdnReqEmpNo" runat="server" />
                            <asp:HiddenField ID="hdnUserEmpName" runat="server" />
                            <asp:HiddenField ID="hdnUserDepCode" runat="server" />
                            <asp:HiddenField ID="hdnUserDepartment" runat="server" />
                            <asp:HiddenField ID="hdnUserDept" runat="server" />
                            <asp:HiddenField ID="hdnUserSecCode" runat="server" />
                            <asp:HiddenField ID="hdnUserType" runat="server" />
                            <asp:HiddenField ID="hdnAttachmentID" runat="server" />
                            <asp:HiddenField ID="hdnAttachmentPath" runat="server" />    
                            <asp:HiddenField ID="hdnGV_Action" runat="server"/>
                            <asp:HiddenField ID="hdnDltRow" runat="server"/>
                            <asp:HiddenField ID="hdnButton" runat="server"/>
                            <asp:HiddenField ID="hdnStatus" runat="server"/>
                        </div> 
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <uc1:AlertMessage runat="server" ID="AlertMessage" /> 
    </div> 
    <div>
        <uc1:EmpSearch runat="server" ID="EmpSearch" />
    </div> 
     

</asp:Content>


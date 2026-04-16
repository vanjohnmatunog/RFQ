<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewDetails.aspx.cs" Inherits="PageTransaction_ViewDetails" %>

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

    <div id="divDetailsOfItem" runat="server">
        <asp:UpdatePanel ID="upnlDetailsOfItem" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divDetailsOfItem_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;" >
                        <div class="card-header text-white" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divDetailsOfItem_Body">
                            <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold">
                                D E T A I L S  &nbsp; O F &nbsp; I T E M
                            </asp:Label>
                        </div>
                        <div id="divDetailsOfItem_Body" class="card-body collapse show py-0">  
                             <div class="row small text-secondary mt-3">
                                 <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm">
                                   <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn btn-danger rounded-center" OnClick="lbtnBack_Click" > <i class="fa fa-arrow-left"></i> Back</asp:LinkButton>
                                 </div>
                             </div>
                            <div class="row small text-secondary mt-3">
                                 <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="Label24" runat="server" CssClass="col-form-label text-dark"> RFQ No: </asp:Label>
                                    <asp:TextBox ID="txtControlNo" runat="server" CssClass="form-control font-weight-bold text-center uppercase" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>                             
                                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="Label26" runat="server" CssClass="col-form-label text-dark"> Buyer In-charge: </asp:Label>
                                    <asp:TextBox ID="txtBuyer" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>
                                 <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="Label27" runat="server" CssClass="col-form-label text-dark"> Date Closed: </asp:Label>
                                    <asp:TextBox ID="txtDateClose" runat="server" CssClass="form-control text-dark text-center uppercase" ReadOnly="true" BackColor="White"  placeholder="-- Auto Generated --"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row small text-secondary mb-3">
                                 <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblRequestorName" runat="server" CssClass="col-form-label text-dark"> Requestor: </asp:Label>
                                    <asp:TextBox ID="txtRequestor" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>  
                                 <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblCategory" runat="server" CssClass="col-form-label text-dark"> Category: </asp:Label>
                                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>
                                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblStatus" runat="server" CssClass="col-form-label text-dark "> Status: </asp:Label>
                                    <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control font-weight-bold text-center uppercase" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                                </div>
                            </div>

                             <div class="row medium text-secondary">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 mb-2" id="div1" runat="server" visible="true">
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="type"/>
                                    <div class="table-responsive">  
                                        <asp:GridView ID="gvDetailsViewOnly" CssClass="footable" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDetailsViewOnly_RowDataBound" 
                                            FooterStyle-CssClass="font-weight-bold text-toshiba-orange" ShowFooter="false" FooterStyle-Height="5" Visible="true">
                                            <Columns>                                                
                                                  <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter text-dark"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />                                                                               
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description">
                                                    <ItemTemplate>  
                                                        <asp:Label ID="lblItemDesc" Width="300" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("ItemDescription") %>'> lblItem </asp:Label>
                                                    <%--    <asp:HiddenField ID="hdnValid" runat="server" Value='<%# Eval("Valid") %>'/>--%>
                                                       <%-- <asp:HiddenField ID="hdnTempItemCode" runat="server"  Value='<%# Eval("TempItemCode") %>'/>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Width="50" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Quantity") %>'> lblQty </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" Width="200" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("UOM") %>'> lblUOM </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBudget" Width="150" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Budget") %>'> lblBudget </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrency" Width="100" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Currency") %>'> lblCurrency </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurpose" Width="300" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("Purpose") %>'> lblPurpose </asp:Label>
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
                                                <asp:TemplateField HeaderText="Buyer In-charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBuyer" Width="200" runat="server" CssClass="col-form-label text-dark" Text='<%# Eval("BuyerInCharge") %>'> lblBuyer</asp:Label>
                                                      </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Width="200" runat="server" CssClass="col-form-label" Text='<%# Eval("BuyerInCharge") %>' ForeColor="White" Font-Bold="True"> lblStatus</asp:Label>
                                                      </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter scrolling" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <FooterStyle CssClass="font-weight-bold text-toshiba-orange" Height="5px" />
                                        </asp:GridView>                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divReqAttach" runat="server">
        <asp:UpdatePanel ID="upnlReqAttach" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divReqAttach_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                        <div class="card-header" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divReqAttach_Body">
                            <asp:Label ID="Label10" runat="server" CssClass="font-weight-bold">
                                R E Q U E S T O R ' S &nbsp; A T T A C H M E N T
                            </asp:Label>
                        </div>
                        <div id="divReqAttach_Body" class="card-body collapse show py-0">  
                               <div class="row small text-secondary mt-3">
                                    <div class="col-xl-3 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 alignRight">
                                        <asp:Label ID="Label44" runat="server" CssClass="font-weight-bold large text-primary"> ATTACHMENT: </asp:Label>
                                    </div>
                              </div> 
                            
                             <div class="row small text-secondary">
                             <div class="col-xl-1 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
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
               <%-- <asp:PostBackTrigger ControlID="lbtnUpload" />--%>
                <asp:PostBackTrigger ControlID="gvAttachment" />
                <%--<asp:PostBackTrigger ControlID="lblFilePath" />--%>
        </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divMPDAttach" runat="server">
        <asp:UpdatePanel ID="upnlMPDAttach" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>            
                <div class="container-fluid" id="divMPDAttach_Header" runat="server">
                    <div class="card text-light text-info mt-1" style="border-color: #0d98ba ;">
                        <div class="card-header" style="background-color: #0d98ba ;" data-toggle="collapse" data-target="#divMPDAttach_Body">
                            <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">
                                M P D &nbsp; A T T A C H M E N T
                            </asp:Label>
                        </div>
                        <div id="divMPDAttach_Body" class="card-body collapse show py-0">  
                               <div class="row small text-secondary mt-3">
                            <div class="col-xl-3 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 alignRight">
                                <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold large text-primary"> ATTACHMENT: </asp:Label>
                            </div>
                            <div id="divUploadAttach_MPD" Visible="false" runat="server" class="col-xl-6 col-lg-8 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-append">
                                        <span class="input-group-text bg-secondary text-light"> Select File </span>
                                    </div>
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuAttachment" runat="server" class="custom-file-input" />
                                        <asp:Label CssClass="custom-file-label" ID="Label4" AssociatedControlID="fuAttachment" runat="server"> Choose file </asp:Label>
                                       <%-- <label class="custom-file-label" for="fuAttachment">Choose file</label>--%>
                                    </div> &nbsp;&nbsp;
                                    <%--<asp:FileUpload ID="fuAttachment" runat="server" CssClass="w-50"/> &nbsp;--%>
                                    <asp:LinkButton ID="lbtnUpload_MPD" runat="server" CssClass="btn btn-primary rounded-left" OnCommand="lbtnUpload_MPD_Command">
                                        &nbsp; <i class="fas fa-upload"></i> &nbsp; Upload &nbsp;
                                    </asp:LinkButton>                                            
                                </div>  
                            </div>     
                        </div> 
                            <div class="row small text-secondary mt-1">
                                <div class="col-xl-3 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                      <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold large text-primary"></asp:Label>
                                </div>
                                <div class="col-xl-8 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                      <asp:Label ID="lblAtt_MPD" Visible ="false" runat="server" CssClass="col-form-label"> List of Attachments: </asp:Label>
                                      <asp:Label ID="lblAtNote_MPD" Visible ="false" runat="server" CssClass="col-form-label text-warning font-italic small" Text=" (Note: All changes on list of attachments is automatically saved to database.): " Font-Bold="True"></asp:Label>
                                 </div>
                            </div>
                             <div class="row small text-secondary">
                            <div class="col-xl-1 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold large">   </asp:Label>
                             </div> 
                            <div class="col-xl-10 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="row medium text-secondary"> 
                                    
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mb-3">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAttachmentMPD" CssClass="footable" runat="server" AutoGenerateColumns="false" 
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
                                                            <asp:LinkButton ID="lbtnOpen_MPD" runat="server" CssClass="btn btn-link text-success" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnOpen_MPD_Command">
                                                                <i class="fas fa-folder-open"></i> Open
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnDelete_MPD" runat="server" CssClass="btn btn-link text-danger" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnDeleteAttachment_MPD_Command" >
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
                             <div class="row small text-secondary mt-3">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 input-group-sm mt-1 mb-2 text-center" style="margin-left:auto;margin-right:auto;">
                                <asp:LinkButton ID="lbtnInprocess" Visible="false" runat="server" CssClass="btn btn-success rounded-center" OnClick="lbtnInprocess_Click"> In-process</asp:LinkButton>
                                <asp:LinkButton ID="lbtnPartiallyClosed" Visible="false" runat="server" CssClass="btn btn-info rounded-left" OnClick="lbtnPartiallyClosed_Click"> Partially Closed</asp:LinkButton> 
                                
                            </div>
                       </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnUpload_MPD" />
                <asp:PostBackTrigger ControlID="gvAttachmentMPD" />
                <%--<asp:PostBackTrigger ControlID="lblFilePath" />--%>
        </Triggers>
        </asp:UpdatePanel>
    </div>
   
     <asp:UpdatePanel ID="upnlLogs" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="container-fluid" id="divLogs" runat="server" visible="false">
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
                                    <asp:GridView ID="gvHistoryLogs_MPD" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                        EmptyDataText="There are no data records to display." PageSize="5" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvHistoryLogs_MPD_PageIndexChanging" >
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
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label20" runat="server" CssClass="col-form-label" Text='<%# Eval("Remarks") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted By">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedBy") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label22" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
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
                            <asp:HiddenField ID="hdnTempItemCode" runat="server"/>
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


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceipts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.tabCtrlReceipts = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.vatO = New System.Windows.Forms.Label
        Me.txtBoxTotal0 = New System.Windows.Forms.TextBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.lstBoxSerno = New System.Windows.Forms.ListBox
        Me.lstBoxReceipts = New System.Windows.Forms.ListBox
        Me.txtBoxTotal5 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtBoxTotal19 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBoxReturnAmt = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtBoxPayment = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtBoxTotalWithDiscount = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtBoxTotalDiscount = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBoxCreatedOn = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBoxTotalAmt = New System.Windows.Forms.TextBox
        Me.lblTotalAmt = New System.Windows.Forms.Label
        Me.txtBoxCashier = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.lblProducts = New System.Windows.Forms.Label
        Me.txtBoxPaymentType = New System.Windows.Forms.TextBox
        Me.lblPaymentType = New System.Windows.Forms.Label
        Me.txtBoxReceiptNum = New System.Windows.Forms.TextBox
        Me.lblReceiptNum = New System.Windows.Forms.Label
        Me.grBoxSearch = New System.Windows.Forms.GroupBox
        Me.chkBoxCash = New System.Windows.Forms.CheckBox
        Me.chkBoxVISA = New System.Windows.Forms.CheckBox
        Me.lblTotalRecAmt = New System.Windows.Forms.Label
        Me.lblUser = New System.Windows.Forms.Label
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.lblTotalReceipts = New System.Windows.Forms.Label
        Me.chkBoxWithRet = New System.Windows.Forms.CheckBox
        Me.chkBoxWithDisc = New System.Windows.Forms.CheckBox
        Me.txtBoxToTimeM = New System.Windows.Forms.TextBox
        Me.txtBoxFromTimeM = New System.Windows.Forms.TextBox
        Me.txtBoxToTimeH = New System.Windows.Forms.TextBox
        Me.txtBoxFromTimeH = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblToDate = New System.Windows.Forms.Label
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.tabCtrlReceipts.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.grBoxSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintDocument1
        '
        '
        'tabCtrlReceipts
        '
        Me.tabCtrlReceipts.Controls.Add(Me.TabPage1)
        Me.tabCtrlReceipts.Location = New System.Drawing.Point(13, 12)
        Me.tabCtrlReceipts.Name = "tabCtrlReceipts"
        Me.tabCtrlReceipts.SelectedIndex = 0
        Me.tabCtrlReceipts.Size = New System.Drawing.Size(990, 715)
        Me.tabCtrlReceipts.TabIndex = 100
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.vatO)
        Me.TabPage1.Controls.Add(Me.txtBoxTotal0)
        Me.TabPage1.Controls.Add(Me.btnPrint)
        Me.TabPage1.Controls.Add(Me.lstBoxSerno)
        Me.TabPage1.Controls.Add(Me.lstBoxReceipts)
        Me.TabPage1.Controls.Add(Me.txtBoxTotal5)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtBoxTotal19)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtBoxReturnAmt)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.txtBoxPayment)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtBoxTotalWithDiscount)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtBoxTotalDiscount)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtBoxCreatedOn)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtBoxTotalAmt)
        Me.TabPage1.Controls.Add(Me.lblTotalAmt)
        Me.TabPage1.Controls.Add(Me.txtBoxCashier)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtBoxDescription)
        Me.TabPage1.Controls.Add(Me.lblProducts)
        Me.TabPage1.Controls.Add(Me.txtBoxPaymentType)
        Me.TabPage1.Controls.Add(Me.lblPaymentType)
        Me.TabPage1.Controls.Add(Me.txtBoxReceiptNum)
        Me.TabPage1.Controls.Add(Me.lblReceiptNum)
        Me.TabPage1.Controls.Add(Me.grBoxSearch)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(982, 689)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Ιστορικό Αποδείξεων"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'vatO
        '
        Me.vatO.AutoSize = True
        Me.vatO.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vatO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.vatO.Location = New System.Drawing.Point(6, 633)
        Me.vatO.Name = "vatO"
        Me.vatO.Size = New System.Drawing.Size(107, 30)
        Me.vatO.TabIndex = 129
        Me.vatO.Text = "Φ.Π.Α. 0%"
        '
        'txtBoxTotal0
        '
        Me.txtBoxTotal0.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotal0.Location = New System.Drawing.Point(215, 638)
        Me.txtBoxTotal0.MaxLength = 180
        Me.txtBoxTotal0.Name = "txtBoxTotal0"
        Me.txtBoxTotal0.ReadOnly = True
        Me.txtBoxTotal0.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotal0.TabIndex = 128
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrint.Image = Global.POS.My.Resources.Resources.printer
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(787, 607)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(189, 76)
        Me.btnPrint.TabIndex = 101
        Me.btnPrint.Text = "       Εκτύπωση"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'lstBoxSerno
        '
        Me.lstBoxSerno.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxSerno.FormattingEnabled = True
        Me.lstBoxSerno.ItemHeight = 25
        Me.lstBoxSerno.Location = New System.Drawing.Point(654, 629)
        Me.lstBoxSerno.Name = "lstBoxSerno"
        Me.lstBoxSerno.Size = New System.Drawing.Size(79, 54)
        Me.lstBoxSerno.TabIndex = 127
        Me.lstBoxSerno.Visible = False
        '
        'lstBoxReceipts
        '
        Me.lstBoxReceipts.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxReceipts.FormattingEnabled = True
        Me.lstBoxReceipts.ItemHeight = 25
        Me.lstBoxReceipts.Location = New System.Drawing.Point(739, 146)
        Me.lstBoxReceipts.Name = "lstBoxReceipts"
        Me.lstBoxReceipts.Size = New System.Drawing.Size(237, 454)
        Me.lstBoxReceipts.TabIndex = 126
        '
        'txtBoxTotal5
        '
        Me.txtBoxTotal5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotal5.Location = New System.Drawing.Point(474, 590)
        Me.txtBoxTotal5.MaxLength = 180
        Me.txtBoxTotal5.Name = "txtBoxTotal5"
        Me.txtBoxTotal5.ReadOnly = True
        Me.txtBoxTotal5.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotal5.TabIndex = 125
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(325, 590)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(107, 30)
        Me.Label8.TabIndex = 124
        Me.Label8.Text = "Φ.Π.Α. 5%"
        '
        'txtBoxTotal19
        '
        Me.txtBoxTotal19.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotal19.Location = New System.Drawing.Point(215, 593)
        Me.txtBoxTotal19.MaxLength = 180
        Me.txtBoxTotal19.Name = "txtBoxTotal19"
        Me.txtBoxTotal19.ReadOnly = True
        Me.txtBoxTotal19.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotal19.TabIndex = 123
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 593)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 30)
        Me.Label7.TabIndex = 122
        Me.Label7.Text = "Φ.Π.Α. 19%"
        '
        'txtBoxReturnAmt
        '
        Me.txtBoxReturnAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxReturnAmt.Location = New System.Drawing.Point(474, 545)
        Me.txtBoxReturnAmt.MaxLength = 180
        Me.txtBoxReturnAmt.Name = "txtBoxReturnAmt"
        Me.txtBoxReturnAmt.ReadOnly = True
        Me.txtBoxReturnAmt.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxReturnAmt.TabIndex = 121
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(325, 545)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 30)
        Me.Label6.TabIndex = 120
        Me.Label6.Text = "Επιστροφή"
        '
        'txtBoxPayment
        '
        Me.txtBoxPayment.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPayment.Location = New System.Drawing.Point(215, 548)
        Me.txtBoxPayment.MaxLength = 180
        Me.txtBoxPayment.Name = "txtBoxPayment"
        Me.txtBoxPayment.ReadOnly = True
        Me.txtBoxPayment.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxPayment.TabIndex = 119
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 545)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 30)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Πληρωμή"
        '
        'txtBoxTotalWithDiscount
        '
        Me.txtBoxTotalWithDiscount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalWithDiscount.Location = New System.Drawing.Point(215, 503)
        Me.txtBoxTotalWithDiscount.MaxLength = 180
        Me.txtBoxTotalWithDiscount.Name = "txtBoxTotalWithDiscount"
        Me.txtBoxTotalWithDiscount.ReadOnly = True
        Me.txtBoxTotalWithDiscount.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotalWithDiscount.TabIndex = 117
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 503)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(185, 30)
        Me.Label4.TabIndex = 116
        Me.Label4.Text = "Ολικό με έκπτωση"
        '
        'txtBoxTotalDiscount
        '
        Me.txtBoxTotalDiscount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalDiscount.Location = New System.Drawing.Point(474, 458)
        Me.txtBoxTotalDiscount.MaxLength = 180
        Me.txtBoxTotalDiscount.Name = "txtBoxTotalDiscount"
        Me.txtBoxTotalDiscount.ReadOnly = True
        Me.txtBoxTotalDiscount.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotalDiscount.TabIndex = 115
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(325, 458)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 30)
        Me.Label3.TabIndex = 114
        Me.Label3.Text = "Έκπτωση (€)"
        '
        'txtBoxCreatedOn
        '
        Me.txtBoxCreatedOn.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxCreatedOn.Location = New System.Drawing.Point(474, 146)
        Me.txtBoxCreatedOn.MaxLength = 13
        Me.txtBoxCreatedOn.Name = "txtBoxCreatedOn"
        Me.txtBoxCreatedOn.ReadOnly = True
        Me.txtBoxCreatedOn.Size = New System.Drawing.Size(259, 35)
        Me.txtBoxCreatedOn.TabIndex = 113
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(325, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 30)
        Me.Label2.TabIndex = 112
        Me.Label2.Text = "Ημερομηνία"
        '
        'txtBoxTotalAmt
        '
        Me.txtBoxTotalAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalAmt.Location = New System.Drawing.Point(215, 458)
        Me.txtBoxTotalAmt.MaxLength = 180
        Me.txtBoxTotalAmt.Name = "txtBoxTotalAmt"
        Me.txtBoxTotalAmt.ReadOnly = True
        Me.txtBoxTotalAmt.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxTotalAmt.TabIndex = 111
        '
        'lblTotalAmt
        '
        Me.lblTotalAmt.AutoSize = True
        Me.lblTotalAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotalAmt.Location = New System.Drawing.Point(6, 458)
        Me.lblTotalAmt.Name = "lblTotalAmt"
        Me.lblTotalAmt.Size = New System.Drawing.Size(154, 30)
        Me.lblTotalAmt.TabIndex = 110
        Me.lblTotalAmt.Text = "Ολικό Ποσό (€)"
        '
        'txtBoxCashier
        '
        Me.txtBoxCashier.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxCashier.Location = New System.Drawing.Point(474, 191)
        Me.txtBoxCashier.MaxLength = 13
        Me.txtBoxCashier.Name = "txtBoxCashier"
        Me.txtBoxCashier.ReadOnly = True
        Me.txtBoxCashier.Size = New System.Drawing.Size(259, 35)
        Me.txtBoxCashier.TabIndex = 109
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(325, 191)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 30)
        Me.Label1.TabIndex = 108
        Me.Label1.Text = "Ταμείας"
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDescription.Location = New System.Drawing.Point(215, 239)
        Me.txtBoxDescription.MaxLength = 4000
        Me.txtBoxDescription.Multiline = True
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.Size = New System.Drawing.Size(518, 213)
        Me.txtBoxDescription.TabIndex = 105
        '
        'lblProducts
        '
        Me.lblProducts.AutoSize = True
        Me.lblProducts.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProducts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProducts.Location = New System.Drawing.Point(6, 239)
        Me.lblProducts.Name = "lblProducts"
        Me.lblProducts.Size = New System.Drawing.Size(104, 30)
        Me.lblProducts.TabIndex = 107
        Me.lblProducts.Text = "Προϊόντα"
        '
        'txtBoxPaymentType
        '
        Me.txtBoxPaymentType.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPaymentType.Location = New System.Drawing.Point(215, 191)
        Me.txtBoxPaymentType.MaxLength = 180
        Me.txtBoxPaymentType.Name = "txtBoxPaymentType"
        Me.txtBoxPaymentType.ReadOnly = True
        Me.txtBoxPaymentType.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxPaymentType.TabIndex = 104
        '
        'lblPaymentType
        '
        Me.lblPaymentType.AutoSize = True
        Me.lblPaymentType.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentType.Location = New System.Drawing.Point(6, 191)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.lblPaymentType.Size = New System.Drawing.Size(189, 30)
        Me.lblPaymentType.TabIndex = 106
        Me.lblPaymentType.Text = "Τρόπος Πληρωμής"
        '
        'txtBoxReceiptNum
        '
        Me.txtBoxReceiptNum.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxReceiptNum.Location = New System.Drawing.Point(215, 146)
        Me.txtBoxReceiptNum.MaxLength = 13
        Me.txtBoxReceiptNum.Name = "txtBoxReceiptNum"
        Me.txtBoxReceiptNum.ReadOnly = True
        Me.txtBoxReceiptNum.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxReceiptNum.TabIndex = 103
        '
        'lblReceiptNum
        '
        Me.lblReceiptNum.AutoSize = True
        Me.lblReceiptNum.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiptNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReceiptNum.Location = New System.Drawing.Point(6, 146)
        Me.lblReceiptNum.Name = "lblReceiptNum"
        Me.lblReceiptNum.Size = New System.Drawing.Size(194, 30)
        Me.lblReceiptNum.TabIndex = 102
        Me.lblReceiptNum.Text = "Αριθμός Απόδειξης"
        '
        'grBoxSearch
        '
        Me.grBoxSearch.Controls.Add(Me.chkBoxCash)
        Me.grBoxSearch.Controls.Add(Me.chkBoxVISA)
        Me.grBoxSearch.Controls.Add(Me.lblTotalRecAmt)
        Me.grBoxSearch.Controls.Add(Me.lblUser)
        Me.grBoxSearch.Controls.Add(Me.cmbUsers)
        Me.grBoxSearch.Controls.Add(Me.lblTotalReceipts)
        Me.grBoxSearch.Controls.Add(Me.chkBoxWithRet)
        Me.grBoxSearch.Controls.Add(Me.chkBoxWithDisc)
        Me.grBoxSearch.Controls.Add(Me.txtBoxToTimeM)
        Me.grBoxSearch.Controls.Add(Me.txtBoxFromTimeM)
        Me.grBoxSearch.Controls.Add(Me.txtBoxToTimeH)
        Me.grBoxSearch.Controls.Add(Me.txtBoxFromTimeH)
        Me.grBoxSearch.Controls.Add(Me.Label9)
        Me.grBoxSearch.Controls.Add(Me.btnExit)
        Me.grBoxSearch.Controls.Add(Me.btnSearch)
        Me.grBoxSearch.Controls.Add(Me.lblToDate)
        Me.grBoxSearch.Controls.Add(Me.dtpFrom)
        Me.grBoxSearch.Controls.Add(Me.dtpTo)
        Me.grBoxSearch.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.grBoxSearch.Location = New System.Drawing.Point(-6, -33)
        Me.grBoxSearch.Name = "grBoxSearch"
        Me.grBoxSearch.Size = New System.Drawing.Size(1000, 165)
        Me.grBoxSearch.TabIndex = 100
        Me.grBoxSearch.TabStop = False
        Me.grBoxSearch.Text = "Αναζήτηση"
        '
        'chkBoxCash
        '
        Me.chkBoxCash.AutoSize = True
        Me.chkBoxCash.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxCash.Location = New System.Drawing.Point(341, 122)
        Me.chkBoxCash.Name = "chkBoxCash"
        Me.chkBoxCash.Size = New System.Drawing.Size(69, 25)
        Me.chkBoxCash.TabIndex = 51
        Me.chkBoxCash.Text = "CASH"
        Me.chkBoxCash.UseVisualStyleBackColor = True
        '
        'chkBoxVISA
        '
        Me.chkBoxVISA.AutoSize = True
        Me.chkBoxVISA.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxVISA.Location = New System.Drawing.Point(273, 122)
        Me.chkBoxVISA.Name = "chkBoxVISA"
        Me.chkBoxVISA.Size = New System.Drawing.Size(62, 25)
        Me.chkBoxVISA.TabIndex = 50
        Me.chkBoxVISA.Text = "VISA"
        Me.chkBoxVISA.UseVisualStyleBackColor = True
        '
        'lblTotalRecAmt
        '
        Me.lblTotalRecAmt.AutoSize = True
        Me.lblTotalRecAmt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRecAmt.Location = New System.Drawing.Point(728, 99)
        Me.lblTotalRecAmt.Name = "lblTotalRecAmt"
        Me.lblTotalRecAmt.Size = New System.Drawing.Size(197, 25)
        Me.lblTotalRecAmt.TabIndex = 49
        Me.lblTotalRecAmt.Text = "Συνολικό Ποσό (€) :  0"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.Location = New System.Drawing.Point(433, 122)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(70, 21)
        Me.lblUser.TabIndex = 48
        Me.lblUser.Text = "Χρήστης"
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownHeight = 150
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsers.FormattingEnabled = True
        Me.cmbUsers.IntegralHeight = False
        Me.cmbUsers.Location = New System.Drawing.Point(509, 127)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(207, 29)
        Me.cmbUsers.TabIndex = 47
        '
        'lblTotalReceipts
        '
        Me.lblTotalReceipts.AutoSize = True
        Me.lblTotalReceipts.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalReceipts.Location = New System.Drawing.Point(728, 126)
        Me.lblTotalReceipts.Name = "lblTotalReceipts"
        Me.lblTotalReceipts.Size = New System.Drawing.Size(198, 25)
        Me.lblTotalReceipts.TabIndex = 46
        Me.lblTotalReceipts.Text = "Σύνολο Αποδείξεων: 0"
        '
        'chkBoxWithRet
        '
        Me.chkBoxWithRet.AutoSize = True
        Me.chkBoxWithRet.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxWithRet.Location = New System.Drawing.Point(137, 122)
        Me.chkBoxWithRet.Name = "chkBoxWithRet"
        Me.chkBoxWithRet.Size = New System.Drawing.Size(130, 25)
        Me.chkBoxWithRet.TabIndex = 45
        Me.chkBoxWithRet.Text = "Με επιστροφή"
        Me.chkBoxWithRet.UseVisualStyleBackColor = True
        '
        'chkBoxWithDisc
        '
        Me.chkBoxWithDisc.AutoSize = True
        Me.chkBoxWithDisc.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxWithDisc.Location = New System.Drawing.Point(11, 122)
        Me.chkBoxWithDisc.Name = "chkBoxWithDisc"
        Me.chkBoxWithDisc.Size = New System.Drawing.Size(120, 25)
        Me.chkBoxWithDisc.TabIndex = 44
        Me.chkBoxWithDisc.Text = "Με Έκπτωση"
        Me.chkBoxWithDisc.UseVisualStyleBackColor = True
        '
        'txtBoxToTimeM
        '
        Me.txtBoxToTimeM.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxToTimeM.Location = New System.Drawing.Point(480, 77)
        Me.txtBoxToTimeM.MaxLength = 13
        Me.txtBoxToTimeM.Name = "txtBoxToTimeM"
        Me.txtBoxToTimeM.Size = New System.Drawing.Size(41, 33)
        Me.txtBoxToTimeM.TabIndex = 6
        '
        'txtBoxFromTimeM
        '
        Me.txtBoxFromTimeM.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxFromTimeM.Location = New System.Drawing.Point(480, 37)
        Me.txtBoxFromTimeM.MaxLength = 13
        Me.txtBoxFromTimeM.Name = "txtBoxFromTimeM"
        Me.txtBoxFromTimeM.Size = New System.Drawing.Size(41, 33)
        Me.txtBoxFromTimeM.TabIndex = 4
        '
        'txtBoxToTimeH
        '
        Me.txtBoxToTimeH.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxToTimeH.Location = New System.Drawing.Point(437, 77)
        Me.txtBoxToTimeH.MaxLength = 13
        Me.txtBoxToTimeH.Name = "txtBoxToTimeH"
        Me.txtBoxToTimeH.Size = New System.Drawing.Size(41, 33)
        Me.txtBoxToTimeH.TabIndex = 5
        '
        'txtBoxFromTimeH
        '
        Me.txtBoxFromTimeH.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxFromTimeH.Location = New System.Drawing.Point(437, 37)
        Me.txtBoxFromTimeH.MaxLength = 13
        Me.txtBoxFromTimeH.Name = "txtBoxFromTimeH"
        Me.txtBoxFromTimeH.Size = New System.Drawing.Size(41, 33)
        Me.txtBoxFromTimeH.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 25)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Από"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(802, 40)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(183, 49)
        Me.btnExit.TabIndex = 42
        Me.btnExit.Text = "   Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSearch.Image = Global.POS.My.Resources.Resources.search
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(527, 40)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(189, 76)
        Me.btnSearch.TabIndex = 38
        Me.btnSearch.Text = "       Αναζήτηση"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(6, 81)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(63, 25)
        Me.lblToDate.TabIndex = 40
        Me.lblToDate.Text = "Μέχρι"
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Location = New System.Drawing.Point(79, 40)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(352, 33)
        Me.dtpFrom.TabIndex = 1
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Location = New System.Drawing.Point(79, 81)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(352, 33)
        Me.dtpTo.TabIndex = 2
        '
        'frmReceipts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.tabCtrlReceipts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmReceipts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Αποδείξεων"
        Me.tabCtrlReceipts.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.grBoxSearch.ResumeLayout(False)
        Me.grBoxSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents tabCtrlReceipts As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents vatO As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotal0 As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents lstBoxSerno As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxReceipts As System.Windows.Forms.ListBox
    Friend WithEvents txtBoxTotal5 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotal19 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBoxReturnAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBoxPayment As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotalWithDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotalDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBoxCreatedOn As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalAmt As System.Windows.Forms.Label
    Friend WithEvents txtBoxCashier As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblProducts As System.Windows.Forms.Label
    Friend WithEvents txtBoxPaymentType As System.Windows.Forms.TextBox
    Friend WithEvents lblPaymentType As System.Windows.Forms.Label
    Friend WithEvents txtBoxReceiptNum As System.Windows.Forms.TextBox
    Friend WithEvents lblReceiptNum As System.Windows.Forms.Label
    Friend WithEvents grBoxSearch As System.Windows.Forms.GroupBox
    Friend WithEvents chkBoxCash As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxVISA As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalRecAmt As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents lblTotalReceipts As System.Windows.Forms.Label
    Friend WithEvents chkBoxWithRet As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxWithDisc As System.Windows.Forms.CheckBox
    Friend WithEvents txtBoxToTimeM As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxFromTimeM As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxToTimeH As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxFromTimeH As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProducts
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
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtBoxSearchBox = New System.Windows.Forms.TextBox
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblBuyAmt = New System.Windows.Forms.Label
        Me.txtBoxBuyAmt = New System.Windows.Forms.TextBox
        Me.txtBoxSellAmt = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtBoxAvailQuantity = New System.Windows.Forms.TextBox
        Me.lnkLblAddQuantity = New System.Windows.Forms.LinkLabel
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtBoxMinQuantity = New System.Windows.Forms.TextBox
        Me.ckboxAlert = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmdBoxCategoy = New System.Windows.Forms.ComboBox
        Me.lblVat = New System.Windows.Forms.Label
        Me.txtBoxVAT = New System.Windows.Forms.TextBox
        Me.lblPercent = New System.Windows.Forms.Label
        Me.cmbSupplier = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtBoxSupplierPhone = New System.Windows.Forms.TextBox
        Me.lnkLblBarcodes = New System.Windows.Forms.LinkLabel
        Me.lstBoxBarcodes = New System.Windows.Forms.ListBox
        Me.lblExpiry = New System.Windows.Forms.Label
        Me.dtpExpiry = New System.Windows.Forms.DateTimePicker
        Me.lblAlertDate = New System.Windows.Forms.Label
        Me.dtpAlert = New System.Windows.Forms.DateTimePicker
        Me.lblNotes = New System.Windows.Forms.Label
        Me.txtBoxNotes = New System.Windows.Forms.TextBox
        Me.txtBoxBuyAmtNoVAT = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkBoxAlertExpiry = New System.Windows.Forms.CheckBox
        Me.txtBoxProfit = New System.Windows.Forms.TextBox
        Me.txtBoxProfitPercentage = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtBoxStockQuantity = New System.Windows.Forms.TextBox
        Me.lnkLblAddStock = New System.Windows.Forms.LinkLabel
        Me.chkBoxOffer = New System.Windows.Forms.CheckBox
        Me.txtBoxOfferX = New System.Windows.Forms.TextBox
        Me.txtBoxOfferDisc = New System.Windows.Forms.TextBox
        Me.lblOfferDisc = New System.Windows.Forms.Label
        Me.txtBoxOfferDiscAt = New System.Windows.Forms.TextBox
        Me.cmdBoxVatType = New System.Windows.Forms.ComboBox
        Me.chkboxBox = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtBoxBoxUnits = New System.Windows.Forms.TextBox
        Me.lnkLblBoxBarcodes = New System.Windows.Forms.LinkLabel
        Me.btnCalculateWithVAT = New System.Windows.Forms.Button
        Me.btnCalculateWithoutVAT = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblOfferTo = New System.Windows.Forms.Label
        Me.dtpOfferFrom = New System.Windows.Forms.DateTimePicker
        Me.lblOfferFrom = New System.Windows.Forms.Label
        Me.dtpOfferTo = New System.Windows.Forms.DateTimePicker
        Me.SuspendLayout()
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSearch.Location = New System.Drawing.Point(13, 25)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(205, 30)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Αναζήτηση Barcode "
        '
        'txtBoxSearchBox
        '
        Me.txtBoxSearchBox.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxSearchBox.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSearchBox.Location = New System.Drawing.Point(268, 25)
        Me.txtBoxSearchBox.MaxLength = 50
        Me.txtBoxSearchBox.Name = "txtBoxSearchBox"
        Me.txtBoxSearchBox.Size = New System.Drawing.Size(254, 35)
        Me.txtBoxSearchBox.TabIndex = 1
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxDescription.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDescription.Location = New System.Drawing.Point(268, 66)
        Me.txtBoxDescription.MaxLength = 30
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.ReadOnly = True
        Me.txtBoxDescription.Size = New System.Drawing.Size(418, 35)
        Me.txtBoxDescription.TabIndex = 3
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescription.Location = New System.Drawing.Point(13, 66)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(118, 30)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "Περιγραφή"
        '
        'lblBuyAmt
        '
        Me.lblBuyAmt.AutoSize = True
        Me.lblBuyAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuyAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBuyAmt.Location = New System.Drawing.Point(12, 226)
        Me.lblBuyAmt.Name = "lblBuyAmt"
        Me.lblBuyAmt.Size = New System.Drawing.Size(220, 30)
        Me.lblBuyAmt.TabIndex = 4
        Me.lblBuyAmt.Text = "Τιμή Αγοράς με Φ.Π.Α"
        '
        'txtBoxBuyAmt
        '
        Me.txtBoxBuyAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxBuyAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBuyAmt.Location = New System.Drawing.Point(267, 226)
        Me.txtBoxBuyAmt.MaxLength = 13
        Me.txtBoxBuyAmt.Name = "txtBoxBuyAmt"
        Me.txtBoxBuyAmt.ReadOnly = True
        Me.txtBoxBuyAmt.Size = New System.Drawing.Size(126, 35)
        Me.txtBoxBuyAmt.TabIndex = 7
        '
        'txtBoxSellAmt
        '
        Me.txtBoxSellAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxSellAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSellAmt.Location = New System.Drawing.Point(267, 267)
        Me.txtBoxSellAmt.MaxLength = 13
        Me.txtBoxSellAmt.Name = "txtBoxSellAmt"
        Me.txtBoxSellAmt.ReadOnly = True
        Me.txtBoxSellAmt.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxSellAmt.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 267)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(177, 30)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Τιμή Πώλησης (€)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(374, 359)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(211, 30)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Διαθέσιμη Ποσότητα"
        '
        'txtBoxAvailQuantity
        '
        Me.txtBoxAvailQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxAvailQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxAvailQuantity.Location = New System.Drawing.Point(591, 359)
        Me.txtBoxAvailQuantity.MaxLength = 13
        Me.txtBoxAvailQuantity.Name = "txtBoxAvailQuantity"
        Me.txtBoxAvailQuantity.ReadOnly = True
        Me.txtBoxAvailQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxAvailQuantity.TabIndex = 17
        '
        'lnkLblAddQuantity
        '
        Me.lnkLblAddQuantity.AutoSize = True
        Me.lnkLblAddQuantity.Enabled = False
        Me.lnkLblAddQuantity.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblAddQuantity.Location = New System.Drawing.Point(698, 357)
        Me.lnkLblAddQuantity.Name = "lnkLblAddQuantity"
        Me.lnkLblAddQuantity.Size = New System.Drawing.Size(35, 37)
        Me.lnkLblAddQuantity.TabIndex = 15
        Me.lnkLblAddQuantity.TabStop = True
        Me.lnkLblAddQuantity.Text = "+"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 392)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(200, 30)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Ελάχιστη Ποσότητα"
        '
        'txtBoxMinQuantity
        '
        Me.txtBoxMinQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxMinQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxMinQuantity.Location = New System.Drawing.Point(267, 395)
        Me.txtBoxMinQuantity.MaxLength = 13
        Me.txtBoxMinQuantity.Name = "txtBoxMinQuantity"
        Me.txtBoxMinQuantity.ReadOnly = True
        Me.txtBoxMinQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxMinQuantity.TabIndex = 16
        '
        'ckboxAlert
        '
        Me.ckboxAlert.AutoSize = True
        Me.ckboxAlert.Enabled = False
        Me.ckboxAlert.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ckboxAlert.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckboxAlert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckboxAlert.Location = New System.Drawing.Point(17, 355)
        Me.ckboxAlert.Name = "ckboxAlert"
        Me.ckboxAlert.Size = New System.Drawing.Size(351, 34)
        Me.ckboxAlert.TabIndex = 15
        Me.ckboxAlert.Text = "Ειδοποίηση Ελάχιστης Ποσότητας"
        Me.ckboxAlert.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 141)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(189, 30)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Φ.Π.Α / Κατηγορία"
        '
        'cmdBoxCategoy
        '
        Me.cmdBoxCategoy.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmdBoxCategoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdBoxCategoy.Enabled = False
        Me.cmdBoxCategoy.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBoxCategoy.FormattingEnabled = True
        Me.cmdBoxCategoy.Location = New System.Drawing.Point(418, 141)
        Me.cmdBoxCategoy.Name = "cmdBoxCategoy"
        Me.cmdBoxCategoy.Size = New System.Drawing.Size(267, 38)
        Me.cmdBoxCategoy.TabIndex = 4
        '
        'lblVat
        '
        Me.lblVat.AutoSize = True
        Me.lblVat.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVat.Location = New System.Drawing.Point(442, 229)
        Me.lblVat.Name = "lblVat"
        Me.lblVat.Size = New System.Drawing.Size(73, 30)
        Me.lblVat.TabIndex = 20
        Me.lblVat.Text = "Φ.Π.Α."
        '
        'txtBoxVAT
        '
        Me.txtBoxVAT.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxVAT.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxVAT.Location = New System.Drawing.Point(521, 229)
        Me.txtBoxVAT.MaxLength = 13
        Me.txtBoxVAT.Name = "txtBoxVAT"
        Me.txtBoxVAT.ReadOnly = True
        Me.txtBoxVAT.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxVAT.TabIndex = 9
        '
        'lblPercent
        '
        Me.lblPercent.AutoSize = True
        Me.lblPercent.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPercent.Location = New System.Drawing.Point(628, 230)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(30, 30)
        Me.lblPercent.TabIndex = 22
        Me.lblPercent.Text = "%"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmbSupplier.DropDownHeight = 95
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Enabled = False
        Me.cmbSupplier.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.IntegralHeight = False
        Me.cmbSupplier.Location = New System.Drawing.Point(267, 309)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(297, 33)
        Me.cmbSupplier.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(12, 305)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(199, 30)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Προμηθευτής / Τηλ."
        '
        'txtBoxSupplierPhone
        '
        Me.txtBoxSupplierPhone.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxSupplierPhone.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSupplierPhone.Location = New System.Drawing.Point(570, 309)
        Me.txtBoxSupplierPhone.MaxLength = 180
        Me.txtBoxSupplierPhone.Name = "txtBoxSupplierPhone"
        Me.txtBoxSupplierPhone.ReadOnly = True
        Me.txtBoxSupplierPhone.Size = New System.Drawing.Size(225, 35)
        Me.txtBoxSupplierPhone.TabIndex = 14
        '
        'lnkLblBarcodes
        '
        Me.lnkLblBarcodes.AutoSize = True
        Me.lnkLblBarcodes.Enabled = False
        Me.lnkLblBarcodes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblBarcodes.LinkColor = System.Drawing.Color.MediumBlue
        Me.lnkLblBarcodes.Location = New System.Drawing.Point(574, 25)
        Me.lnkLblBarcodes.Name = "lnkLblBarcodes"
        Me.lnkLblBarcodes.Size = New System.Drawing.Size(192, 30)
        Me.lnkLblBarcodes.TabIndex = 20
        Me.lnkLblBarcodes.TabStop = True
        Me.lnkLblBarcodes.Text = "Προσθήκη Barcode"
        '
        'lstBoxBarcodes
        '
        Me.lstBoxBarcodes.BackColor = System.Drawing.Color.LemonChiffon
        Me.lstBoxBarcodes.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lstBoxBarcodes.FormattingEnabled = True
        Me.lstBoxBarcodes.ItemHeight = 32
        Me.lstBoxBarcodes.Location = New System.Drawing.Point(802, 25)
        Me.lstBoxBarcodes.Name = "lstBoxBarcodes"
        Me.lstBoxBarcodes.Size = New System.Drawing.Size(202, 196)
        Me.lstBoxBarcodes.TabIndex = 26
        '
        'lblExpiry
        '
        Me.lblExpiry.AutoSize = True
        Me.lblExpiry.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExpiry.Location = New System.Drawing.Point(13, 473)
        Me.lblExpiry.Name = "lblExpiry"
        Me.lblExpiry.Size = New System.Drawing.Size(189, 30)
        Me.lblExpiry.TabIndex = 35
        Me.lblExpiry.Text = "Ημερομηνία Λήξης"
        '
        'dtpExpiry
        '
        Me.dtpExpiry.CalendarMonthBackground = System.Drawing.Color.LemonChiffon
        Me.dtpExpiry.CalendarTitleForeColor = System.Drawing.Color.LemonChiffon
        Me.dtpExpiry.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpiry.Location = New System.Drawing.Point(268, 476)
        Me.dtpExpiry.Name = "dtpExpiry"
        Me.dtpExpiry.Size = New System.Drawing.Size(209, 33)
        Me.dtpExpiry.TabIndex = 20
        '
        'lblAlertDate
        '
        Me.lblAlertDate.AutoSize = True
        Me.lblAlertDate.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlertDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAlertDate.Location = New System.Drawing.Point(13, 517)
        Me.lblAlertDate.Name = "lblAlertDate"
        Me.lblAlertDate.Size = New System.Drawing.Size(190, 30)
        Me.lblAlertDate.TabIndex = 37
        Me.lblAlertDate.Text = "Ημερ. Ειδοποίησης"
        '
        'dtpAlert
        '
        Me.dtpAlert.CalendarMonthBackground = System.Drawing.Color.LemonChiffon
        Me.dtpAlert.CalendarTitleForeColor = System.Drawing.Color.LemonChiffon
        Me.dtpAlert.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAlert.Location = New System.Drawing.Point(268, 515)
        Me.dtpAlert.Name = "dtpAlert"
        Me.dtpAlert.Size = New System.Drawing.Size(209, 33)
        Me.dtpAlert.TabIndex = 21
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNotes.Location = New System.Drawing.Point(796, 236)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(117, 30)
        Me.lblNotes.TabIndex = 39
        Me.lblNotes.Text = "Σημειώσεις"
        '
        'txtBoxNotes
        '
        Me.txtBoxNotes.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxNotes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxNotes.Location = New System.Drawing.Point(802, 271)
        Me.txtBoxNotes.MaxLength = 4000
        Me.txtBoxNotes.Multiline = True
        Me.txtBoxNotes.Name = "txtBoxNotes"
        Me.txtBoxNotes.ReadOnly = True
        Me.txtBoxNotes.Size = New System.Drawing.Size(203, 290)
        Me.txtBoxNotes.TabIndex = 22
        '
        'txtBoxBuyAmtNoVAT
        '
        Me.txtBoxBuyAmtNoVAT.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxBuyAmtNoVAT.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBuyAmtNoVAT.Location = New System.Drawing.Point(267, 185)
        Me.txtBoxBuyAmtNoVAT.MaxLength = 13
        Me.txtBoxBuyAmtNoVAT.Name = "txtBoxBuyAmtNoVAT"
        Me.txtBoxBuyAmtNoVAT.ReadOnly = True
        Me.txtBoxBuyAmtNoVAT.Size = New System.Drawing.Size(126, 35)
        Me.txtBoxBuyAmtNoVAT.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 185)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 30)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Τιμή Αγοράς "
        '
        'chkBoxAlertExpiry
        '
        Me.chkBoxAlertExpiry.AutoSize = True
        Me.chkBoxAlertExpiry.Enabled = False
        Me.chkBoxAlertExpiry.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkBoxAlertExpiry.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxAlertExpiry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBoxAlertExpiry.Location = New System.Drawing.Point(18, 436)
        Me.chkBoxAlertExpiry.Name = "chkBoxAlertExpiry"
        Me.chkBoxAlertExpiry.Size = New System.Drawing.Size(330, 34)
        Me.chkBoxAlertExpiry.TabIndex = 19
        Me.chkBoxAlertExpiry.Text = "Ειδοποίηση Ημερομηνίας Λήξης"
        Me.chkBoxAlertExpiry.UseVisualStyleBackColor = True
        '
        'txtBoxProfit
        '
        Me.txtBoxProfit.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxProfit.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxProfit.Location = New System.Drawing.Point(377, 267)
        Me.txtBoxProfit.MaxLength = 13
        Me.txtBoxProfit.Name = "txtBoxProfit"
        Me.txtBoxProfit.ReadOnly = True
        Me.txtBoxProfit.Size = New System.Drawing.Size(87, 35)
        Me.txtBoxProfit.TabIndex = 11
        '
        'txtBoxProfitPercentage
        '
        Me.txtBoxProfitPercentage.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxProfitPercentage.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxProfitPercentage.Location = New System.Drawing.Point(470, 267)
        Me.txtBoxProfitPercentage.MaxLength = 13
        Me.txtBoxProfitPercentage.Name = "txtBoxProfitPercentage"
        Me.txtBoxProfitPercentage.ReadOnly = True
        Me.txtBoxProfitPercentage.Size = New System.Drawing.Size(94, 35)
        Me.txtBoxProfitPercentage.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(570, 272)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 30)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "%"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(374, 398)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(211, 30)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Ποσότητα Αποθήκης"
        '
        'txtBoxStockQuantity
        '
        Me.txtBoxStockQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxStockQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxStockQuantity.Location = New System.Drawing.Point(591, 401)
        Me.txtBoxStockQuantity.MaxLength = 13
        Me.txtBoxStockQuantity.Name = "txtBoxStockQuantity"
        Me.txtBoxStockQuantity.ReadOnly = True
        Me.txtBoxStockQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxStockQuantity.TabIndex = 18
        '
        'lnkLblAddStock
        '
        Me.lnkLblAddStock.AutoSize = True
        Me.lnkLblAddStock.Enabled = False
        Me.lnkLblAddStock.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblAddStock.Location = New System.Drawing.Point(698, 398)
        Me.lnkLblAddStock.Name = "lnkLblAddStock"
        Me.lnkLblAddStock.Size = New System.Drawing.Size(35, 37)
        Me.lnkLblAddStock.TabIndex = 51
        Me.lnkLblAddStock.TabStop = True
        Me.lnkLblAddStock.Text = "+"
        '
        'chkBoxOffer
        '
        Me.chkBoxOffer.AutoSize = True
        Me.chkBoxOffer.Enabled = False
        Me.chkBoxOffer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkBoxOffer.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxOffer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBoxOffer.Location = New System.Drawing.Point(18, 564)
        Me.chkBoxOffer.Name = "chkBoxOffer"
        Me.chkBoxOffer.Size = New System.Drawing.Size(133, 34)
        Me.chkBoxOffer.TabIndex = 52
        Me.chkBoxOffer.Text = "Προσφορά"
        Me.chkBoxOffer.UseVisualStyleBackColor = True
        '
        'txtBoxOfferX
        '
        Me.txtBoxOfferX.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxOfferX.Location = New System.Drawing.Point(268, 564)
        Me.txtBoxOfferX.MaxLength = 13
        Me.txtBoxOfferX.Name = "txtBoxOfferX"
        Me.txtBoxOfferX.Size = New System.Drawing.Size(44, 35)
        Me.txtBoxOfferX.TabIndex = 53
        Me.txtBoxOfferX.Visible = False
        '
        'txtBoxOfferDisc
        '
        Me.txtBoxOfferDisc.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxOfferDisc.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxOfferDisc.Location = New System.Drawing.Point(268, 564)
        Me.txtBoxOfferDisc.MaxLength = 13
        Me.txtBoxOfferDisc.Name = "txtBoxOfferDisc"
        Me.txtBoxOfferDisc.Size = New System.Drawing.Size(54, 35)
        Me.txtBoxOfferDisc.TabIndex = 58
        Me.txtBoxOfferDisc.Visible = False
        '
        'lblOfferDisc
        '
        Me.lblOfferDisc.AutoSize = True
        Me.lblOfferDisc.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferDisc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOfferDisc.Location = New System.Drawing.Point(328, 564)
        Me.lblOfferDisc.Name = "lblOfferDisc"
        Me.lblOfferDisc.Size = New System.Drawing.Size(155, 30)
        Me.lblOfferDisc.TabIndex = 59
        Me.lblOfferDisc.Text = "€ έκπτωση στα"
        Me.lblOfferDisc.Visible = False
        '
        'txtBoxOfferDiscAt
        '
        Me.txtBoxOfferDiscAt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxOfferDiscAt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxOfferDiscAt.Location = New System.Drawing.Point(489, 564)
        Me.txtBoxOfferDiscAt.MaxLength = 13
        Me.txtBoxOfferDiscAt.Name = "txtBoxOfferDiscAt"
        Me.txtBoxOfferDiscAt.Size = New System.Drawing.Size(54, 35)
        Me.txtBoxOfferDiscAt.TabIndex = 60
        Me.txtBoxOfferDiscAt.Visible = False
        '
        'cmdBoxVatType
        '
        Me.cmdBoxVatType.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmdBoxVatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdBoxVatType.Enabled = False
        Me.cmdBoxVatType.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBoxVatType.FormattingEnabled = True
        Me.cmdBoxVatType.Location = New System.Drawing.Point(267, 141)
        Me.cmdBoxVatType.Name = "cmdBoxVatType"
        Me.cmdBoxVatType.Size = New System.Drawing.Size(145, 38)
        Me.cmdBoxVatType.TabIndex = 61
        '
        'chkboxBox
        '
        Me.chkboxBox.AutoSize = True
        Me.chkboxBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkboxBox.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkboxBox.Location = New System.Drawing.Point(18, 104)
        Me.chkboxBox.Name = "chkboxBox"
        Me.chkboxBox.Size = New System.Drawing.Size(141, 34)
        Me.chkboxBox.TabIndex = 62
        Me.chkboxBox.Text = "Συσκευασία"
        Me.chkboxBox.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(263, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(260, 30)
        Me.Label8.TabIndex = 63
        Me.Label8.Text = "Μονάδες Ανά Συσκευασία"
        '
        'txtBoxBoxUnits
        '
        Me.txtBoxBoxUnits.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxBoxUnits.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBoxUnits.Location = New System.Drawing.Point(522, 105)
        Me.txtBoxBoxUnits.MaxLength = 30
        Me.txtBoxBoxUnits.Name = "txtBoxBoxUnits"
        Me.txtBoxBoxUnits.ReadOnly = True
        Me.txtBoxBoxUnits.Size = New System.Drawing.Size(67, 35)
        Me.txtBoxBoxUnits.TabIndex = 64
        '
        'lnkLblBoxBarcodes
        '
        Me.lnkLblBoxBarcodes.AutoSize = True
        Me.lnkLblBoxBarcodes.Enabled = False
        Me.lnkLblBoxBarcodes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblBoxBarcodes.LinkColor = System.Drawing.Color.MediumBlue
        Me.lnkLblBoxBarcodes.Location = New System.Drawing.Point(595, 108)
        Me.lnkLblBoxBarcodes.Name = "lnkLblBoxBarcodes"
        Me.lnkLblBoxBarcodes.Size = New System.Drawing.Size(109, 30)
        Me.lnkLblBoxBarcodes.TabIndex = 65
        Me.lnkLblBoxBarcodes.TabStop = True
        Me.lnkLblBoxBarcodes.Text = "Barcode(s)"
        '
        'btnCalculateWithVAT
        '
        Me.btnCalculateWithVAT.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCalculateWithVAT.BackgroundImage = Global.POS.My.Resources.Resources.calculator
        Me.btnCalculateWithVAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCalculateWithVAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCalculateWithVAT.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculateWithVAT.Location = New System.Drawing.Point(399, 228)
        Me.btnCalculateWithVAT.Name = "btnCalculateWithVAT"
        Me.btnCalculateWithVAT.Size = New System.Drawing.Size(37, 33)
        Me.btnCalculateWithVAT.TabIndex = 8
        Me.btnCalculateWithVAT.UseVisualStyleBackColor = False
        '
        'btnCalculateWithoutVAT
        '
        Me.btnCalculateWithoutVAT.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCalculateWithoutVAT.BackgroundImage = Global.POS.My.Resources.Resources.calculator
        Me.btnCalculateWithoutVAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCalculateWithoutVAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCalculateWithoutVAT.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculateWithoutVAT.Location = New System.Drawing.Point(399, 187)
        Me.btnCalculateWithoutVAT.Name = "btnCalculateWithoutVAT"
        Me.btnCalculateWithoutVAT.Size = New System.Drawing.Size(37, 33)
        Me.btnCalculateWithoutVAT.TabIndex = 6
        Me.btnCalculateWithoutVAT.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Image = Global.POS.My.Resources.Resources.undo
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(447, 659)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(182, 75)
        Me.btnClear.TabIndex = 24
        Me.btnClear.Text = "      Καθαρισμός Πεδίων"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.BackgroundImage = Global.POS.My.Resources.Resources.search
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(528, 25)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(37, 35)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(635, 659)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(182, 75)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "       Αποθήκευση"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(823, 659)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(182, 75)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "  Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblOfferTo
        '
        Me.lblOfferTo.AutoSize = True
        Me.lblOfferTo.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOfferTo.Location = New System.Drawing.Point(483, 604)
        Me.lblOfferTo.Name = "lblOfferTo"
        Me.lblOfferTo.Size = New System.Drawing.Size(52, 30)
        Me.lblOfferTo.TabIndex = 69
        Me.lblOfferTo.Text = "Έως"
        Me.lblOfferTo.Visible = False
        '
        'dtpOfferFrom
        '
        Me.dtpOfferFrom.CalendarMonthBackground = System.Drawing.Color.LemonChiffon
        Me.dtpOfferFrom.CalendarTitleForeColor = System.Drawing.Color.LemonChiffon
        Me.dtpOfferFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpOfferFrom.Location = New System.Drawing.Point(267, 604)
        Me.dtpOfferFrom.Name = "dtpOfferFrom"
        Me.dtpOfferFrom.Size = New System.Drawing.Size(210, 33)
        Me.dtpOfferFrom.TabIndex = 66
        Me.dtpOfferFrom.Visible = False
        '
        'lblOfferFrom
        '
        Me.lblOfferFrom.AutoSize = True
        Me.lblOfferFrom.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOfferFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOfferFrom.Location = New System.Drawing.Point(98, 601)
        Me.lblOfferFrom.Name = "lblOfferFrom"
        Me.lblOfferFrom.Size = New System.Drawing.Size(52, 30)
        Me.lblOfferFrom.TabIndex = 68
        Me.lblOfferFrom.Text = "Από"
        Me.lblOfferFrom.Visible = False
        '
        'dtpOfferTo
        '
        Me.dtpOfferTo.CalendarMonthBackground = System.Drawing.Color.LemonChiffon
        Me.dtpOfferTo.CalendarTitleForeColor = System.Drawing.Color.LemonChiffon
        Me.dtpOfferTo.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpOfferTo.Location = New System.Drawing.Point(541, 602)
        Me.dtpOfferTo.Name = "dtpOfferTo"
        Me.dtpOfferTo.Size = New System.Drawing.Size(210, 33)
        Me.dtpOfferTo.TabIndex = 70
        Me.dtpOfferTo.Visible = False
        '
        'frmProducts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.dtpOfferTo)
        Me.Controls.Add(Me.lblOfferTo)
        Me.Controls.Add(Me.dtpOfferFrom)
        Me.Controls.Add(Me.lblOfferFrom)
        Me.Controls.Add(Me.lnkLblBoxBarcodes)
        Me.Controls.Add(Me.txtBoxBoxUnits)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.chkboxBox)
        Me.Controls.Add(Me.cmdBoxVatType)
        Me.Controls.Add(Me.txtBoxOfferDiscAt)
        Me.Controls.Add(Me.lblOfferDisc)
        Me.Controls.Add(Me.txtBoxOfferDisc)
        Me.Controls.Add(Me.txtBoxOfferX)
        Me.Controls.Add(Me.chkBoxOffer)
        Me.Controls.Add(Me.lnkLblAddStock)
        Me.Controls.Add(Me.txtBoxStockQuantity)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBoxProfitPercentage)
        Me.Controls.Add(Me.txtBoxProfit)
        Me.Controls.Add(Me.chkBoxAlertExpiry)
        Me.Controls.Add(Me.btnCalculateWithVAT)
        Me.Controls.Add(Me.btnCalculateWithoutVAT)
        Me.Controls.Add(Me.txtBoxBuyAmtNoVAT)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBoxNotes)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.dtpAlert)
        Me.Controls.Add(Me.lblAlertDate)
        Me.Controls.Add(Me.dtpExpiry)
        Me.Controls.Add(Me.lblExpiry)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lstBoxBarcodes)
        Me.Controls.Add(Me.lnkLblBarcodes)
        Me.Controls.Add(Me.txtBoxSupplierPhone)
        Me.Controls.Add(Me.cmbSupplier)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.txtBoxVAT)
        Me.Controls.Add(Me.lblVat)
        Me.Controls.Add(Me.cmdBoxCategoy)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ckboxAlert)
        Me.Controls.Add(Me.txtBoxMinQuantity)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lnkLblAddQuantity)
        Me.Controls.Add(Me.txtBoxAvailQuantity)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBoxSellAmt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBoxBuyAmt)
        Me.Controls.Add(Me.lblBuyAmt)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.txtBoxSearchBox)
        Me.Controls.Add(Me.lblSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1022, 722)
        Me.Name = "frmProducts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Προϊόντων"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtBoxSearchBox As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblBuyAmt As System.Windows.Forms.Label
    Friend WithEvents txtBoxBuyAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxSellAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBoxAvailQuantity As System.Windows.Forms.TextBox
    Friend WithEvents lnkLblAddQuantity As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBoxMinQuantity As System.Windows.Forms.TextBox
    Friend WithEvents ckboxAlert As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmdBoxCategoy As System.Windows.Forms.ComboBox
    Friend WithEvents lblVat As System.Windows.Forms.Label
    Friend WithEvents txtBoxVAT As System.Windows.Forms.TextBox
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBoxSupplierPhone As System.Windows.Forms.TextBox
    Friend WithEvents lnkLblBarcodes As System.Windows.Forms.LinkLabel
    Friend WithEvents lstBoxBarcodes As System.Windows.Forms.ListBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblExpiry As System.Windows.Forms.Label
    Friend WithEvents dtpExpiry As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAlertDate As System.Windows.Forms.Label
    Friend WithEvents dtpAlert As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents txtBoxNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxBuyAmtNoVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCalculateWithoutVAT As System.Windows.Forms.Button
    Friend WithEvents btnCalculateWithVAT As System.Windows.Forms.Button
    Friend WithEvents chkBoxAlertExpiry As System.Windows.Forms.CheckBox
    Friend WithEvents txtBoxProfit As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxProfitPercentage As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBoxStockQuantity As System.Windows.Forms.TextBox
    Friend WithEvents lnkLblAddStock As System.Windows.Forms.LinkLabel
    Friend WithEvents chkBoxOffer As System.Windows.Forms.CheckBox
    Friend WithEvents txtBoxOfferX As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxOfferDisc As System.Windows.Forms.TextBox
    Friend WithEvents lblOfferDisc As System.Windows.Forms.Label
    Friend WithEvents txtBoxOfferDiscAt As System.Windows.Forms.TextBox
    Friend WithEvents cmdBoxVatType As System.Windows.Forms.ComboBox
    Friend WithEvents chkboxBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBoxBoxUnits As System.Windows.Forms.TextBox
    Friend WithEvents lnkLblBoxBarcodes As System.Windows.Forms.LinkLabel
    Friend WithEvents lblOfferTo As System.Windows.Forms.Label
    Friend WithEvents dtpOfferFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblOfferFrom As System.Windows.Forms.Label
    Friend WithEvents dtpOfferTo As System.Windows.Forms.DateTimePicker
End Class

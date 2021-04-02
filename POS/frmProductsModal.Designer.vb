<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductsModal
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnCalculateWithoutVAT = New System.Windows.Forms.Button
        Me.btnCalculateWithVAT = New System.Windows.Forms.Button
        Me.cmdBoxVatType = New System.Windows.Forms.ComboBox
        Me.lnkLblAddStock = New System.Windows.Forms.LinkLabel
        Me.txtBoxStockQuantity = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBoxProfitPercentage = New System.Windows.Forms.TextBox
        Me.txtBoxProfit = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBoxBuyAmtNoVAT = New System.Windows.Forms.TextBox
        Me.txtBoxNotes = New System.Windows.Forms.TextBox
        Me.lblNotes = New System.Windows.Forms.Label
        Me.lstBoxBarcodes = New System.Windows.Forms.ListBox
        Me.lnkLblBarcodes = New System.Windows.Forms.LinkLabel
        Me.txtBoxSupplierPhone = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmbSupplier = New System.Windows.Forms.ComboBox
        Me.lblPercent = New System.Windows.Forms.Label
        Me.txtBoxVAT = New System.Windows.Forms.TextBox
        Me.lblVat = New System.Windows.Forms.Label
        Me.cmdBoxCategoy = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.ckboxAlert = New System.Windows.Forms.CheckBox
        Me.txtBoxMinQuantity = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lnkLblAddQuantity = New System.Windows.Forms.LinkLabel
        Me.txtBoxAvailQuantity = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBoxSellAmt = New System.Windows.Forms.TextBox
        Me.txtBoxBuyAmt = New System.Windows.Forms.TextBox
        Me.lblBuyAmt = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.txtBoxSearchBox = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.lblExpiry = New System.Windows.Forms.Label
        Me.dtpExpiry = New System.Windows.Forms.DateTimePicker
        Me.lblAlertDate = New System.Windows.Forms.Label
        Me.dtpAlert = New System.Windows.Forms.DateTimePicker
        Me.chkBoxAlertExpiry = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
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
        'btnCalculateWithoutVAT
        '
        Me.btnCalculateWithoutVAT.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCalculateWithoutVAT.BackgroundImage = Global.POS.My.Resources.Resources.calculator
        Me.btnCalculateWithoutVAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCalculateWithoutVAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCalculateWithoutVAT.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculateWithoutVAT.Location = New System.Drawing.Point(400, 153)
        Me.btnCalculateWithoutVAT.Name = "btnCalculateWithoutVAT"
        Me.btnCalculateWithoutVAT.Size = New System.Drawing.Size(37, 33)
        Me.btnCalculateWithoutVAT.TabIndex = 6
        Me.btnCalculateWithoutVAT.UseVisualStyleBackColor = False
        '
        'btnCalculateWithVAT
        '
        Me.btnCalculateWithVAT.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCalculateWithVAT.BackgroundImage = Global.POS.My.Resources.Resources.calculator
        Me.btnCalculateWithVAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCalculateWithVAT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCalculateWithVAT.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculateWithVAT.Location = New System.Drawing.Point(400, 194)
        Me.btnCalculateWithVAT.Name = "btnCalculateWithVAT"
        Me.btnCalculateWithVAT.Size = New System.Drawing.Size(37, 33)
        Me.btnCalculateWithVAT.TabIndex = 8
        Me.btnCalculateWithVAT.UseVisualStyleBackColor = False
        '
        'cmdBoxVatType
        '
        Me.cmdBoxVatType.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmdBoxVatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdBoxVatType.Enabled = False
        Me.cmdBoxVatType.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBoxVatType.FormattingEnabled = True
        Me.cmdBoxVatType.Location = New System.Drawing.Point(268, 107)
        Me.cmdBoxVatType.Name = "cmdBoxVatType"
        Me.cmdBoxVatType.Size = New System.Drawing.Size(145, 38)
        Me.cmdBoxVatType.TabIndex = 61
        '
        'lnkLblAddStock
        '
        Me.lnkLblAddStock.AutoSize = True
        Me.lnkLblAddStock.Enabled = False
        Me.lnkLblAddStock.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblAddStock.Location = New System.Drawing.Point(372, 395)
        Me.lnkLblAddStock.Name = "lnkLblAddStock"
        Me.lnkLblAddStock.Size = New System.Drawing.Size(35, 37)
        Me.lnkLblAddStock.TabIndex = 51
        Me.lnkLblAddStock.TabStop = True
        Me.lnkLblAddStock.Text = "+"
        '
        'txtBoxStockQuantity
        '
        Me.txtBoxStockQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxStockQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxStockQuantity.Location = New System.Drawing.Point(262, 395)
        Me.txtBoxStockQuantity.MaxLength = 13
        Me.txtBoxStockQuantity.Name = "txtBoxStockQuantity"
        Me.txtBoxStockQuantity.ReadOnly = True
        Me.txtBoxStockQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxStockQuantity.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(7, 395)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(211, 30)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Ποσότητα Αποθήκης"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(571, 238)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 30)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "%"
        '
        'txtBoxProfitPercentage
        '
        Me.txtBoxProfitPercentage.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxProfitPercentage.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxProfitPercentage.Location = New System.Drawing.Point(471, 233)
        Me.txtBoxProfitPercentage.MaxLength = 13
        Me.txtBoxProfitPercentage.Name = "txtBoxProfitPercentage"
        Me.txtBoxProfitPercentage.ReadOnly = True
        Me.txtBoxProfitPercentage.Size = New System.Drawing.Size(94, 35)
        Me.txtBoxProfitPercentage.TabIndex = 12
        '
        'txtBoxProfit
        '
        Me.txtBoxProfit.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxProfit.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxProfit.Location = New System.Drawing.Point(378, 233)
        Me.txtBoxProfit.MaxLength = 13
        Me.txtBoxProfit.Name = "txtBoxProfit"
        Me.txtBoxProfit.ReadOnly = True
        Me.txtBoxProfit.Size = New System.Drawing.Size(87, 35)
        Me.txtBoxProfit.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(13, 151)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 30)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Τιμή Αγοράς "
        '
        'txtBoxBuyAmtNoVAT
        '
        Me.txtBoxBuyAmtNoVAT.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxBuyAmtNoVAT.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBuyAmtNoVAT.Location = New System.Drawing.Point(268, 151)
        Me.txtBoxBuyAmtNoVAT.MaxLength = 13
        Me.txtBoxBuyAmtNoVAT.Name = "txtBoxBuyAmtNoVAT"
        Me.txtBoxBuyAmtNoVAT.ReadOnly = True
        Me.txtBoxBuyAmtNoVAT.Size = New System.Drawing.Size(126, 35)
        Me.txtBoxBuyAmtNoVAT.TabIndex = 5
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
        'txtBoxSupplierPhone
        '
        Me.txtBoxSupplierPhone.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxSupplierPhone.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSupplierPhone.Location = New System.Drawing.Point(571, 275)
        Me.txtBoxSupplierPhone.MaxLength = 180
        Me.txtBoxSupplierPhone.Name = "txtBoxSupplierPhone"
        Me.txtBoxSupplierPhone.ReadOnly = True
        Me.txtBoxSupplierPhone.Size = New System.Drawing.Size(225, 35)
        Me.txtBoxSupplierPhone.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(13, 271)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(199, 30)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Προμηθευτής \ Τηλ."
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
        Me.cmbSupplier.Location = New System.Drawing.Point(268, 275)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(297, 33)
        Me.cmbSupplier.TabIndex = 13
        '
        'lblPercent
        '
        Me.lblPercent.AutoSize = True
        Me.lblPercent.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPercent.Location = New System.Drawing.Point(629, 196)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(30, 30)
        Me.lblPercent.TabIndex = 22
        Me.lblPercent.Text = "%"
        '
        'txtBoxVAT
        '
        Me.txtBoxVAT.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxVAT.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxVAT.Location = New System.Drawing.Point(522, 195)
        Me.txtBoxVAT.MaxLength = 13
        Me.txtBoxVAT.Name = "txtBoxVAT"
        Me.txtBoxVAT.ReadOnly = True
        Me.txtBoxVAT.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxVAT.TabIndex = 9
        '
        'lblVat
        '
        Me.lblVat.AutoSize = True
        Me.lblVat.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVat.Location = New System.Drawing.Point(443, 195)
        Me.lblVat.Name = "lblVat"
        Me.lblVat.Size = New System.Drawing.Size(73, 30)
        Me.lblVat.TabIndex = 20
        Me.lblVat.Text = "Φ.Π.Α."
        '
        'cmdBoxCategoy
        '
        Me.cmdBoxCategoy.BackColor = System.Drawing.Color.LemonChiffon
        Me.cmdBoxCategoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdBoxCategoy.Enabled = False
        Me.cmdBoxCategoy.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBoxCategoy.FormattingEnabled = True
        Me.cmdBoxCategoy.Location = New System.Drawing.Point(419, 107)
        Me.cmdBoxCategoy.Name = "cmdBoxCategoy"
        Me.cmdBoxCategoy.Size = New System.Drawing.Size(267, 38)
        Me.cmdBoxCategoy.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(13, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(189, 30)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Φ.Π.Α / Κατηγορία"
        '
        'ckboxAlert
        '
        Me.ckboxAlert.AutoSize = True
        Me.ckboxAlert.Enabled = False
        Me.ckboxAlert.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ckboxAlert.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckboxAlert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckboxAlert.Location = New System.Drawing.Point(12, 314)
        Me.ckboxAlert.Name = "ckboxAlert"
        Me.ckboxAlert.Size = New System.Drawing.Size(351, 34)
        Me.ckboxAlert.TabIndex = 15
        Me.ckboxAlert.Text = "Ειδοποίηση Ελάχιστης Ποσότητας"
        Me.ckboxAlert.UseVisualStyleBackColor = True
        '
        'txtBoxMinQuantity
        '
        Me.txtBoxMinQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxMinQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxMinQuantity.Location = New System.Drawing.Point(262, 354)
        Me.txtBoxMinQuantity.MaxLength = 13
        Me.txtBoxMinQuantity.Name = "txtBoxMinQuantity"
        Me.txtBoxMinQuantity.ReadOnly = True
        Me.txtBoxMinQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxMinQuantity.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(7, 351)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(200, 30)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Ελάχιστη Ποσότητα"
        '
        'lnkLblAddQuantity
        '
        Me.lnkLblAddQuantity.AutoSize = True
        Me.lnkLblAddQuantity.Enabled = False
        Me.lnkLblAddQuantity.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblAddQuantity.Location = New System.Drawing.Point(698, 354)
        Me.lnkLblAddQuantity.Name = "lnkLblAddQuantity"
        Me.lnkLblAddQuantity.Size = New System.Drawing.Size(35, 37)
        Me.lnkLblAddQuantity.TabIndex = 15
        Me.lnkLblAddQuantity.TabStop = True
        Me.lnkLblAddQuantity.Text = "+"
        '
        'txtBoxAvailQuantity
        '
        Me.txtBoxAvailQuantity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxAvailQuantity.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxAvailQuantity.Location = New System.Drawing.Point(591, 354)
        Me.txtBoxAvailQuantity.MaxLength = 13
        Me.txtBoxAvailQuantity.Name = "txtBoxAvailQuantity"
        Me.txtBoxAvailQuantity.ReadOnly = True
        Me.txtBoxAvailQuantity.Size = New System.Drawing.Size(101, 35)
        Me.txtBoxAvailQuantity.TabIndex = 17
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(374, 354)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(211, 30)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Διαθέσιμη Ποσότητα"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(13, 233)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(177, 30)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Τιμή Πώλησης (€)"
        '
        'txtBoxSellAmt
        '
        Me.txtBoxSellAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxSellAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSellAmt.Location = New System.Drawing.Point(268, 233)
        Me.txtBoxSellAmt.MaxLength = 13
        Me.txtBoxSellAmt.Name = "txtBoxSellAmt"
        Me.txtBoxSellAmt.ReadOnly = True
        Me.txtBoxSellAmt.Size = New System.Drawing.Size(104, 35)
        Me.txtBoxSellAmt.TabIndex = 10
        '
        'txtBoxBuyAmt
        '
        Me.txtBoxBuyAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxBuyAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBuyAmt.Location = New System.Drawing.Point(268, 192)
        Me.txtBoxBuyAmt.MaxLength = 13
        Me.txtBoxBuyAmt.Name = "txtBoxBuyAmt"
        Me.txtBoxBuyAmt.ReadOnly = True
        Me.txtBoxBuyAmt.Size = New System.Drawing.Size(126, 35)
        Me.txtBoxBuyAmt.TabIndex = 7
        '
        'lblBuyAmt
        '
        Me.lblBuyAmt.AutoSize = True
        Me.lblBuyAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuyAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBuyAmt.Location = New System.Drawing.Point(13, 192)
        Me.lblBuyAmt.Name = "lblBuyAmt"
        Me.lblBuyAmt.Size = New System.Drawing.Size(220, 30)
        Me.lblBuyAmt.TabIndex = 4
        Me.lblBuyAmt.Text = "Τιμή Αγοράς με Φ.Π.Α"
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
        'lblExpiry
        '
        Me.lblExpiry.AutoSize = True
        Me.lblExpiry.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExpiry.Location = New System.Drawing.Point(7, 473)
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
        Me.dtpExpiry.Location = New System.Drawing.Point(262, 476)
        Me.dtpExpiry.Name = "dtpExpiry"
        Me.dtpExpiry.Size = New System.Drawing.Size(308, 33)
        Me.dtpExpiry.TabIndex = 20
        '
        'lblAlertDate
        '
        Me.lblAlertDate.AutoSize = True
        Me.lblAlertDate.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlertDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAlertDate.Location = New System.Drawing.Point(7, 517)
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
        Me.dtpAlert.Location = New System.Drawing.Point(262, 515)
        Me.dtpAlert.Name = "dtpAlert"
        Me.dtpAlert.Size = New System.Drawing.Size(308, 33)
        Me.dtpAlert.TabIndex = 21
        '
        'chkBoxAlertExpiry
        '
        Me.chkBoxAlertExpiry.AutoSize = True
        Me.chkBoxAlertExpiry.Enabled = False
        Me.chkBoxAlertExpiry.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkBoxAlertExpiry.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxAlertExpiry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBoxAlertExpiry.Location = New System.Drawing.Point(12, 436)
        Me.chkBoxAlertExpiry.Name = "chkBoxAlertExpiry"
        Me.chkBoxAlertExpiry.Size = New System.Drawing.Size(330, 34)
        Me.chkBoxAlertExpiry.TabIndex = 19
        Me.chkBoxAlertExpiry.Text = "Ειδοποίηση Ημερομηνίας Λήξης"
        Me.chkBoxAlertExpiry.UseVisualStyleBackColor = True
        '
        'frmProductsModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.cmdBoxVatType)
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
        Me.Name = "frmProductsModal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Προϊόντων"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCalculateWithoutVAT As System.Windows.Forms.Button
    Friend WithEvents btnCalculateWithVAT As System.Windows.Forms.Button
    Friend WithEvents cmdBoxVatType As System.Windows.Forms.ComboBox
    Friend WithEvents lnkLblAddStock As System.Windows.Forms.LinkLabel
    Friend WithEvents txtBoxStockQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBoxProfitPercentage As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxProfit As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBoxBuyAmtNoVAT As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents lstBoxBarcodes As System.Windows.Forms.ListBox
    Friend WithEvents lnkLblBarcodes As System.Windows.Forms.LinkLabel
    Friend WithEvents txtBoxSupplierPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents txtBoxVAT As System.Windows.Forms.TextBox
    Friend WithEvents lblVat As System.Windows.Forms.Label
    Friend WithEvents cmdBoxCategoy As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ckboxAlert As System.Windows.Forms.CheckBox
    Friend WithEvents txtBoxMinQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lnkLblAddQuantity As System.Windows.Forms.LinkLabel
    Friend WithEvents txtBoxAvailQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBoxSellAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxBuyAmt As System.Windows.Forms.TextBox
    Friend WithEvents lblBuyAmt As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxSearchBox As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblExpiry As System.Windows.Forms.Label
    Friend WithEvents dtpExpiry As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAlertDate As System.Windows.Forms.Label
    Friend WithEvents dtpAlert As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkBoxAlertExpiry As System.Windows.Forms.CheckBox
End Class

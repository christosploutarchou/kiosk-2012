<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoices
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
        Me.txtBoxTotalAmt = New System.Windows.Forms.TextBox
        Me.txtInvNumber = New System.Windows.Forms.TextBox
        Me.lblInvDate = New System.Windows.Forms.Label
        Me.lblPhone1 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbExisting = New System.Windows.Forms.RadioButton
        Me.rdbNewInvoice = New System.Windows.Forms.RadioButton
        Me.lstBoxInvNumber = New System.Windows.Forms.ListBox
        Me.lblContactName = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.cmbSuppliers = New System.Windows.Forms.ComboBox
        Me.dgvProductsAndQnt = New System.Windows.Forms.DataGridView
        Me.productSerno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.serno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.buyAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.invPrDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sellamt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.currentQnt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.newQnt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.vat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtBoxInvDateRO = New System.Windows.Forms.TextBox
        Me.txtBoxSNameRO = New System.Windows.Forms.TextBox
        Me.chkBoxTmpSave = New System.Windows.Forms.CheckBox
        Me.btnOverrideExisting = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBoxExtraDiscount = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.grbBoxInvType = New System.Windows.Forms.GroupBox
        Me.rdbClosedInvoices = New System.Windows.Forms.RadioButton
        Me.rdbOpenInvoices = New System.Windows.Forms.RadioButton
        Me.rdbAllInvoices = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvProductsAndQnt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbBoxInvType.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtBoxTotalAmt
        '
        Me.txtBoxTotalAmt.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalAmt.Location = New System.Drawing.Point(308, 204)
        Me.txtBoxTotalAmt.Name = "txtBoxTotalAmt"
        Me.txtBoxTotalAmt.Size = New System.Drawing.Size(172, 39)
        Me.txtBoxTotalAmt.TabIndex = 3
        '
        'txtInvNumber
        '
        Me.txtInvNumber.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNumber.Location = New System.Drawing.Point(308, 112)
        Me.txtInvNumber.Name = "txtInvNumber"
        Me.txtInvNumber.Size = New System.Drawing.Size(322, 39)
        Me.txtInvNumber.TabIndex = 1
        '
        'lblInvDate
        '
        Me.lblInvDate.AutoSize = True
        Me.lblInvDate.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvDate.Location = New System.Drawing.Point(12, 250)
        Me.lblInvDate.Name = "lblInvDate"
        Me.lblInvDate.Size = New System.Drawing.Size(147, 32)
        Me.lblInvDate.TabIndex = 22
        Me.lblInvDate.Text = "Ημερομηνία"
        '
        'lblPhone1
        '
        Me.lblPhone1.AutoSize = True
        Me.lblPhone1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone1.Location = New System.Drawing.Point(12, 204)
        Me.lblPhone1.Name = "lblPhone1"
        Me.lblPhone1.Size = New System.Drawing.Size(153, 32)
        Me.lblPhone1.TabIndex = 21
        Me.lblPhone1.Text = "Ολικό Ποσό/"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 32)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Αριθμός Τιμολογίου"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbExisting)
        Me.GroupBox1.Controls.Add(Me.rdbNewInvoice)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(531, 88)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Επιλογή"
        '
        'rdbExisting
        '
        Me.rdbExisting.AutoSize = True
        Me.rdbExisting.Location = New System.Drawing.Point(231, 39)
        Me.rdbExisting.Name = "rdbExisting"
        Me.rdbExisting.Size = New System.Drawing.Size(280, 36)
        Me.rdbExisting.TabIndex = 14
        Me.rdbExisting.TabStop = True
        Me.rdbExisting.Text = "Προβολή Υφιστάμενου"
        Me.rdbExisting.UseVisualStyleBackColor = True
        '
        'rdbNewInvoice
        '
        Me.rdbNewInvoice.AutoSize = True
        Me.rdbNewInvoice.Location = New System.Drawing.Point(6, 39)
        Me.rdbNewInvoice.Name = "rdbNewInvoice"
        Me.rdbNewInvoice.Size = New System.Drawing.Size(219, 36)
        Me.rdbNewInvoice.TabIndex = 13
        Me.rdbNewInvoice.TabStop = True
        Me.rdbNewInvoice.Text = "Δημιουργία Νέου"
        Me.rdbNewInvoice.UseVisualStyleBackColor = True
        '
        'lstBoxInvNumber
        '
        Me.lstBoxInvNumber.Enabled = False
        Me.lstBoxInvNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxInvNumber.FormattingEnabled = True
        Me.lstBoxInvNumber.HorizontalScrollbar = True
        Me.lstBoxInvNumber.ItemHeight = 32
        Me.lstBoxInvNumber.Location = New System.Drawing.Point(658, 117)
        Me.lstBoxInvNumber.Name = "lstBoxInvNumber"
        Me.lstBoxInvNumber.Size = New System.Drawing.Size(346, 516)
        Me.lstBoxInvNumber.TabIndex = 14
        '
        'lblContactName
        '
        Me.lblContactName.AutoSize = True
        Me.lblContactName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.Location = New System.Drawing.Point(12, 158)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(163, 32)
        Me.lblContactName.TabIndex = 37
        Me.lblContactName.Text = "Προμηθευτής"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(789, 643)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(215, 75)
        Me.btnExit.TabIndex = 15
        Me.btnExit.Text = "Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(567, 643)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(215, 75)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "     Αποθήκευση"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Image = Global.POS.My.Resources.Resources.undo
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(346, 643)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(215, 75)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "   Καθαρισμός Πεδίων"
        Me.btnClear.UseVisualStyleBackColor = False
        Me.btnClear.Visible = False
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBarcode.Location = New System.Drawing.Point(308, 295)
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(279, 39)
        Me.txtBoxBarcode.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 295)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 32)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Barcode"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.DateTimePicker1.Location = New System.Drawing.Point(308, 250)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(322, 35)
        Me.DateTimePicker1.TabIndex = 4
        '
        'cmbSuppliers
        '
        Me.cmbSuppliers.DropDownHeight = 150
        Me.cmbSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuppliers.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuppliers.FormattingEnabled = True
        Me.cmbSuppliers.IntegralHeight = False
        Me.cmbSuppliers.Location = New System.Drawing.Point(308, 165)
        Me.cmbSuppliers.Name = "cmbSuppliers"
        Me.cmbSuppliers.Size = New System.Drawing.Size(322, 33)
        Me.cmbSuppliers.TabIndex = 2
        '
        'dgvProductsAndQnt
        '
        Me.dgvProductsAndQnt.AllowUserToAddRows = False
        Me.dgvProductsAndQnt.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvProductsAndQnt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductsAndQnt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.productSerno, Me.serno, Me.description, Me.buyAmt, Me.invPrDiscount, Me.sellamt, Me.currentQnt, Me.newQnt, Me.vat})
        Me.dgvProductsAndQnt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.dgvProductsAndQnt.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvProductsAndQnt.Location = New System.Drawing.Point(18, 388)
        Me.dgvProductsAndQnt.Name = "dgvProductsAndQnt"
        Me.dgvProductsAndQnt.Size = New System.Drawing.Size(612, 245)
        Me.dgvProductsAndQnt.TabIndex = 63
        '
        'productSerno
        '
        Me.productSerno.HeaderText = "serno"
        Me.productSerno.Name = "productSerno"
        Me.productSerno.Visible = False
        '
        'serno
        '
        Me.serno.HeaderText = "Α/Α"
        Me.serno.Name = "serno"
        Me.serno.ReadOnly = True
        Me.serno.Width = 40
        '
        'description
        '
        Me.description.HeaderText = "Περιγραφή"
        Me.description.Name = "description"
        Me.description.Width = 135
        '
        'buyAmt
        '
        Me.buyAmt.HeaderText = "Τιμή Αγ.(€)"
        Me.buyAmt.Name = "buyAmt"
        Me.buyAmt.Width = 70
        '
        'invPrDiscount
        '
        Me.invPrDiscount.HeaderText = "Έκπτωση (%)"
        Me.invPrDiscount.Name = "invPrDiscount"
        Me.invPrDiscount.Width = 70
        '
        'sellamt
        '
        Me.sellamt.HeaderText = "Τιμή Πώλ.(€)"
        Me.sellamt.Name = "sellamt"
        Me.sellamt.Width = 70
        '
        'currentQnt
        '
        Me.currentQnt.HeaderText = "Υφ. Ποσ."
        Me.currentQnt.Name = "currentQnt"
        Me.currentQnt.Width = 60
        '
        'newQnt
        '
        Me.newQnt.HeaderText = "Ποσ. Παραλαβής"
        Me.newQnt.Name = "newQnt"
        Me.newQnt.Width = 70
        '
        'vat
        '
        Me.vat.HeaderText = "Φ.Π.Α"
        Me.vat.Name = "vat"
        Me.vat.ReadOnly = True
        '
        'txtBoxInvDateRO
        '
        Me.txtBoxInvDateRO.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxInvDateRO.Location = New System.Drawing.Point(308, 250)
        Me.txtBoxInvDateRO.Name = "txtBoxInvDateRO"
        Me.txtBoxInvDateRO.ReadOnly = True
        Me.txtBoxInvDateRO.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxInvDateRO.TabIndex = 64
        Me.txtBoxInvDateRO.Visible = False
        '
        'txtBoxSNameRO
        '
        Me.txtBoxSNameRO.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxSNameRO.Location = New System.Drawing.Point(308, 159)
        Me.txtBoxSNameRO.Name = "txtBoxSNameRO"
        Me.txtBoxSNameRO.ReadOnly = True
        Me.txtBoxSNameRO.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxSNameRO.TabIndex = 65
        Me.txtBoxSNameRO.Visible = False
        '
        'chkBoxTmpSave
        '
        Me.chkBoxTmpSave.AutoSize = True
        Me.chkBoxTmpSave.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.chkBoxTmpSave.Location = New System.Drawing.Point(18, 348)
        Me.chkBoxTmpSave.Name = "chkBoxTmpSave"
        Me.chkBoxTmpSave.Size = New System.Drawing.Size(303, 36)
        Me.chkBoxTmpSave.TabIndex = 66
        Me.chkBoxTmpSave.Text = "Προσωρινή Αποθήκευση"
        Me.chkBoxTmpSave.UseVisualStyleBackColor = True
        '
        'btnOverrideExisting
        '
        Me.btnOverrideExisting.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnOverrideExisting.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOverrideExisting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOverrideExisting.Image = Global.POS.My.Resources.Resources.save
        Me.btnOverrideExisting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOverrideExisting.Location = New System.Drawing.Point(568, 643)
        Me.btnOverrideExisting.Name = "btnOverrideExisting"
        Me.btnOverrideExisting.Size = New System.Drawing.Size(215, 75)
        Me.btnOverrideExisting.TabIndex = 67
        Me.btnOverrideExisting.Text = "     Αποθήκευση"
        Me.btnOverrideExisting.UseVisualStyleBackColor = False
        Me.btnOverrideExisting.Visible = False
        '
        'PrintDocument1
        '
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 204)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 32)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "Επ.Έκπτωση"
        '
        'txtBoxExtraDiscount
        '
        Me.txtBoxExtraDiscount.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxExtraDiscount.Location = New System.Drawing.Point(486, 205)
        Me.txtBoxExtraDiscount.Name = "txtBoxExtraDiscount"
        Me.txtBoxExtraDiscount.Size = New System.Drawing.Size(144, 39)
        Me.txtBoxExtraDiscount.TabIndex = 69
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.BackgroundImage = Global.POS.My.Resources.Resources.search
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(593, 295)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(37, 39)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'grbBoxInvType
        '
        Me.grbBoxInvType.Controls.Add(Me.rdbClosedInvoices)
        Me.grbBoxInvType.Controls.Add(Me.rdbOpenInvoices)
        Me.grbBoxInvType.Controls.Add(Me.rdbAllInvoices)
        Me.grbBoxInvType.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbBoxInvType.Location = New System.Drawing.Point(549, 11)
        Me.grbBoxInvType.Name = "grbBoxInvType"
        Me.grbBoxInvType.Size = New System.Drawing.Size(455, 88)
        Me.grbBoxInvType.TabIndex = 20
        Me.grbBoxInvType.TabStop = False
        Me.grbBoxInvType.Text = "Είδος Τιμολογίου"
        Me.grbBoxInvType.Visible = False
        '
        'rdbClosedInvoices
        '
        Me.rdbClosedInvoices.AutoSize = True
        Me.rdbClosedInvoices.Location = New System.Drawing.Point(218, 39)
        Me.rdbClosedInvoices.Name = "rdbClosedInvoices"
        Me.rdbClosedInvoices.Size = New System.Drawing.Size(117, 36)
        Me.rdbClosedInvoices.TabIndex = 15
        Me.rdbClosedInvoices.Text = "Κλειστά"
        Me.rdbClosedInvoices.UseVisualStyleBackColor = True
        '
        'rdbOpenInvoices
        '
        Me.rdbOpenInvoices.AutoSize = True
        Me.rdbOpenInvoices.Location = New System.Drawing.Point(91, 39)
        Me.rdbOpenInvoices.Name = "rdbOpenInvoices"
        Me.rdbOpenInvoices.Size = New System.Drawing.Size(121, 36)
        Me.rdbOpenInvoices.TabIndex = 14
        Me.rdbOpenInvoices.Text = "Ανοιχτά"
        Me.rdbOpenInvoices.UseVisualStyleBackColor = True
        '
        'rdbAllInvoices
        '
        Me.rdbAllInvoices.AutoSize = True
        Me.rdbAllInvoices.Checked = True
        Me.rdbAllInvoices.Location = New System.Drawing.Point(6, 39)
        Me.rdbAllInvoices.Name = "rdbAllInvoices"
        Me.rdbAllInvoices.Size = New System.Drawing.Size(79, 36)
        Me.rdbAllInvoices.TabIndex = 13
        Me.rdbAllInvoices.TabStop = True
        Me.rdbAllInvoices.Text = "Όλα"
        Me.rdbAllInvoices.UseVisualStyleBackColor = True
        '
        'frmInvoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.grbBoxInvType)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtBoxExtraDiscount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnOverrideExisting)
        Me.Controls.Add(Me.chkBoxTmpSave)
        Me.Controls.Add(Me.txtBoxSNameRO)
        Me.Controls.Add(Me.txtBoxInvDateRO)
        Me.Controls.Add(Me.dgvProductsAndQnt)
        Me.Controls.Add(Me.cmbSuppliers)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.txtBoxBarcode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblContactName)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtBoxTotalAmt)
        Me.Controls.Add(Me.txtInvNumber)
        Me.Controls.Add(Me.lblInvDate)
        Me.Controls.Add(Me.lblPhone1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstBoxInvNumber)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmInvoices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Τιμολογίων"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvProductsAndQnt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbBoxInvType.ResumeLayout(False)
        Me.grbBoxInvType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtBoxTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtInvNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblInvDate As System.Windows.Forms.Label
    Friend WithEvents lblPhone1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbExisting As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents lstBoxInvNumber As System.Windows.Forms.ListBox
    Friend WithEvents lblContactName As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbSuppliers As System.Windows.Forms.ComboBox
    Friend WithEvents dgvProductsAndQnt As System.Windows.Forms.DataGridView
    Friend WithEvents txtBoxInvDateRO As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxSNameRO As System.Windows.Forms.TextBox
    Friend WithEvents chkBoxTmpSave As System.Windows.Forms.CheckBox
    Friend WithEvents btnOverrideExisting As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents productSerno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents buyAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invPrDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sellamt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currentQnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents newQnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBoxExtraDiscount As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents grbBoxInvType As System.Windows.Forms.GroupBox
    Friend WithEvents rdbClosedInvoices As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOpenInvoices As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAllInvoices As System.Windows.Forms.RadioButton
End Class

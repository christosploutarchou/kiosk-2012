<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReports))
        Me.grpBoxReports = New System.Windows.Forms.GroupBox
        Me.rdbSalesPerCategory = New System.Windows.Forms.RadioButton
        Me.rdbSessions = New System.Windows.Forms.RadioButton
        Me.rdbQntHistory = New System.Windows.Forms.RadioButton
        Me.rdbPayments = New System.Windows.Forms.RadioButton
        Me.rdbUsers = New System.Windows.Forms.RadioButton
        Me.rdbSupplierPr = New System.Windows.Forms.RadioButton
        Me.rdbBuySellSupplier = New System.Windows.Forms.RadioButton
        Me.rdbZReport = New System.Windows.Forms.RadioButton
        Me.rdbXReport = New System.Windows.Forms.RadioButton
        Me.rdbSalesPerVAT = New System.Windows.Forms.RadioButton
        Me.rdbSalesPerProduct = New System.Windows.Forms.RadioButton
        Me.rdbQuantity = New System.Windows.Forms.RadioButton
        Me.dgvReports = New System.Windows.Forms.DataGridView
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.lblFromDate = New System.Windows.Forms.Label
        Me.lblToDate = New System.Windows.Forms.Label
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox
        Me.lblBarcode = New System.Windows.Forms.Label
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.cmbSupplier = New System.Windows.Forms.ComboBox
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.lblTotalHoursOrAmount = New System.Windows.Forms.Label
        Me.txtBoxTotalHoursOrPayments = New System.Windows.Forms.TextBox
        Me.rdbProfit = New System.Windows.Forms.RadioButton
        Me.cmbNoBarcode = New System.Windows.Forms.ComboBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.btnClearBarcode = New System.Windows.Forms.Button
        Me.lblAmountVAT = New System.Windows.Forms.Label
        Me.chkBoxSalesPerSupplier = New System.Windows.Forms.CheckBox
        Me.lblTotalSalesAmount = New System.Windows.Forms.Label
        Me.cmbCategories = New System.Windows.Forms.ComboBox
        Me.btnExportToExcel = New System.Windows.Forms.Button
        Me.grpBoxReports.SuspendLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBoxReports
        '
        Me.grpBoxReports.Controls.Add(Me.rdbSalesPerCategory)
        Me.grpBoxReports.Controls.Add(Me.rdbSessions)
        Me.grpBoxReports.Controls.Add(Me.rdbQntHistory)
        Me.grpBoxReports.Controls.Add(Me.rdbPayments)
        Me.grpBoxReports.Controls.Add(Me.rdbUsers)
        Me.grpBoxReports.Controls.Add(Me.rdbSupplierPr)
        Me.grpBoxReports.Controls.Add(Me.rdbBuySellSupplier)
        Me.grpBoxReports.Controls.Add(Me.rdbZReport)
        Me.grpBoxReports.Controls.Add(Me.rdbXReport)
        Me.grpBoxReports.Controls.Add(Me.rdbSalesPerVAT)
        Me.grpBoxReports.Controls.Add(Me.rdbSalesPerProduct)
        Me.grpBoxReports.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxReports.Location = New System.Drawing.Point(12, 12)
        Me.grpBoxReports.Name = "grpBoxReports"
        Me.grpBoxReports.Size = New System.Drawing.Size(306, 353)
        Me.grpBoxReports.TabIndex = 20
        Me.grpBoxReports.TabStop = False
        Me.grpBoxReports.Text = "Επιλογή Αναφοράς"
        '
        'rdbSalesPerCategory
        '
        Me.rdbSalesPerCategory.AutoSize = True
        Me.rdbSalesPerCategory.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSalesPerCategory.Location = New System.Drawing.Point(6, 163)
        Me.rdbSalesPerCategory.Name = "rdbSalesPerCategory"
        Me.rdbSalesPerCategory.Size = New System.Drawing.Size(207, 25)
        Me.rdbSalesPerCategory.TabIndex = 12
        Me.rdbSalesPerCategory.TabStop = True
        Me.rdbSalesPerCategory.Text = "Πωλήσεις ανά κατηγορία"
        Me.rdbSalesPerCategory.UseVisualStyleBackColor = True
        '
        'rdbSessions
        '
        Me.rdbSessions.AutoSize = True
        Me.rdbSessions.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSessions.Location = New System.Drawing.Point(6, 318)
        Me.rdbSessions.Name = "rdbSessions"
        Me.rdbSessions.Size = New System.Drawing.Size(158, 25)
        Me.rdbSessions.TabIndex = 11
        Me.rdbSessions.TabStop = True
        Me.rdbSessions.Text = "Ιστορικό Σύνδεσης"
        Me.rdbSessions.UseVisualStyleBackColor = True
        '
        'rdbQntHistory
        '
        Me.rdbQntHistory.AutoSize = True
        Me.rdbQntHistory.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbQntHistory.Location = New System.Drawing.Point(6, 287)
        Me.rdbQntHistory.Name = "rdbQntHistory"
        Me.rdbQntHistory.Size = New System.Drawing.Size(223, 25)
        Me.rdbQntHistory.TabIndex = 10
        Me.rdbQntHistory.TabStop = True
        Me.rdbQntHistory.Text = "Ιστορικό Ποσότητας / Τιμής"
        Me.rdbQntHistory.UseVisualStyleBackColor = True
        '
        'rdbPayments
        '
        Me.rdbPayments.AutoSize = True
        Me.rdbPayments.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPayments.Location = New System.Drawing.Point(165, 256)
        Me.rdbPayments.Name = "rdbPayments"
        Me.rdbPayments.Size = New System.Drawing.Size(101, 25)
        Me.rdbPayments.TabIndex = 9
        Me.rdbPayments.TabStop = True
        Me.rdbPayments.Text = "Πληρωμές"
        Me.rdbPayments.UseVisualStyleBackColor = True
        '
        'rdbUsers
        '
        Me.rdbUsers.AutoSize = True
        Me.rdbUsers.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUsers.Location = New System.Drawing.Point(6, 256)
        Me.rdbUsers.Name = "rdbUsers"
        Me.rdbUsers.Size = New System.Drawing.Size(153, 25)
        Me.rdbUsers.TabIndex = 8
        Me.rdbUsers.TabStop = True
        Me.rdbUsers.Text = "Ώρες ανά χρήστη"
        Me.rdbUsers.UseVisualStyleBackColor = True
        '
        'rdbSupplierPr
        '
        Me.rdbSupplierPr.AutoSize = True
        Me.rdbSupplierPr.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSupplierPr.Location = New System.Drawing.Point(6, 225)
        Me.rdbSupplierPr.Name = "rdbSupplierPr"
        Me.rdbSupplierPr.Size = New System.Drawing.Size(295, 25)
        Me.rdbSupplierPr.TabIndex = 7
        Me.rdbSupplierPr.TabStop = True
        Me.rdbSupplierPr.Text = "Προϊόντα/Πωλήσεις ανά Προμηθευτή"
        Me.rdbSupplierPr.UseVisualStyleBackColor = True
        '
        'rdbBuySellSupplier
        '
        Me.rdbBuySellSupplier.AutoSize = True
        Me.rdbBuySellSupplier.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbBuySellSupplier.Location = New System.Drawing.Point(6, 194)
        Me.rdbBuySellSupplier.Name = "rdbBuySellSupplier"
        Me.rdbBuySellSupplier.Size = New System.Drawing.Size(187, 25)
        Me.rdbBuySellSupplier.TabIndex = 6
        Me.rdbBuySellSupplier.TabStop = True
        Me.rdbBuySellSupplier.Text = "Τιμή Αγοράς/Πώλησης"
        Me.rdbBuySellSupplier.UseVisualStyleBackColor = True
        '
        'rdbZReport
        '
        Me.rdbZReport.AutoSize = True
        Me.rdbZReport.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbZReport.Location = New System.Drawing.Point(6, 132)
        Me.rdbZReport.Name = "rdbZReport"
        Me.rdbZReport.Size = New System.Drawing.Size(114, 25)
        Me.rdbZReport.TabIndex = 4
        Me.rdbZReport.TabStop = True
        Me.rdbZReport.Text = "Ζ-Αναφορά "
        Me.rdbZReport.UseVisualStyleBackColor = True
        '
        'rdbXReport
        '
        Me.rdbXReport.AutoSize = True
        Me.rdbXReport.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbXReport.Location = New System.Drawing.Point(6, 101)
        Me.rdbXReport.Name = "rdbXReport"
        Me.rdbXReport.Size = New System.Drawing.Size(205, 25)
        Me.rdbXReport.TabIndex = 3
        Me.rdbXReport.TabStop = True
        Me.rdbXReport.Text = "Αναφορά ανά Βάρδια (Χ)"
        Me.rdbXReport.UseVisualStyleBackColor = True
        '
        'rdbSalesPerVAT
        '
        Me.rdbSalesPerVAT.AutoSize = True
        Me.rdbSalesPerVAT.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSalesPerVAT.Location = New System.Drawing.Point(6, 70)
        Me.rdbSalesPerVAT.Name = "rdbSalesPerVAT"
        Me.rdbSalesPerVAT.Size = New System.Drawing.Size(174, 25)
        Me.rdbSalesPerVAT.TabIndex = 2
        Me.rdbSalesPerVAT.TabStop = True
        Me.rdbSalesPerVAT.Text = "Πωλήσεις ανά Φ.Π.Α."
        Me.rdbSalesPerVAT.UseVisualStyleBackColor = True
        '
        'rdbSalesPerProduct
        '
        Me.rdbSalesPerProduct.AutoSize = True
        Me.rdbSalesPerProduct.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSalesPerProduct.Location = New System.Drawing.Point(6, 39)
        Me.rdbSalesPerProduct.Name = "rdbSalesPerProduct"
        Me.rdbSalesPerProduct.Size = New System.Drawing.Size(182, 25)
        Me.rdbSalesPerProduct.TabIndex = 1
        Me.rdbSalesPerProduct.TabStop = True
        Me.rdbSalesPerProduct.Text = "Πωλήσεις ανά Προϊόν"
        Me.rdbSalesPerProduct.UseVisualStyleBackColor = True
        '
        'rdbQuantity
        '
        Me.rdbQuantity.AutoSize = True
        Me.rdbQuantity.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbQuantity.Location = New System.Drawing.Point(418, 291)
        Me.rdbQuantity.Name = "rdbQuantity"
        Me.rdbQuantity.Size = New System.Drawing.Size(179, 25)
        Me.rdbQuantity.TabIndex = 5
        Me.rdbQuantity.TabStop = True
        Me.rdbQuantity.Text = "Αναφορά Ποσότητας"
        Me.rdbQuantity.UseVisualStyleBackColor = True
        Me.rdbQuantity.Visible = False
        '
        'dgvReports
        '
        Me.dgvReports.AllowUserToAddRows = False
        Me.dgvReports.AllowUserToDeleteRows = False
        Me.dgvReports.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReports.Location = New System.Drawing.Point(12, 371)
        Me.dgvReports.Name = "dgvReports"
        Me.dgvReports.ReadOnly = True
        Me.dgvReports.Size = New System.Drawing.Size(992, 356)
        Me.dgvReports.TabIndex = 21
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Location = New System.Drawing.Point(418, 69)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(389, 35)
        Me.dtpFrom.TabIndex = 8
        Me.dtpFrom.Visible = False
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Location = New System.Drawing.Point(418, 110)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(389, 35)
        Me.dtpTo.TabIndex = 8
        Me.dtpTo.Visible = False
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(324, 71)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(52, 30)
        Me.lblFromDate.TabIndex = 24
        Me.lblFromDate.Text = "Από"
        Me.lblFromDate.Visible = False
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(324, 110)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(70, 30)
        Me.lblToDate.TabIndex = 25
        Me.lblToDate.Text = "Μέχρι"
        Me.lblToDate.Visible = False
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBarcode.Location = New System.Drawing.Point(418, 28)
        Me.txtBoxBarcode.MaxLength = 13
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(346, 35)
        Me.txtBoxBarcode.TabIndex = 6
        Me.txtBoxBarcode.Visible = False
        '
        'lblBarcode
        '
        Me.lblBarcode.AutoSize = True
        Me.lblBarcode.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBarcode.Location = New System.Drawing.Point(324, 28)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(88, 30)
        Me.lblBarcode.TabIndex = 35
        Me.lblBarcode.Text = "Barcode"
        Me.lblBarcode.Visible = False
        '
        'PrintDocument1
        '
        '
        'cmbSupplier
        '
        Me.cmbSupplier.DropDownHeight = 150
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.IntegralHeight = False
        Me.cmbSupplier.Location = New System.Drawing.Point(329, 29)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(342, 33)
        Me.cmbSupplier.TabIndex = 39
        Me.cmbSupplier.Visible = False
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownHeight = 150
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsers.FormattingEnabled = True
        Me.cmbUsers.IntegralHeight = False
        Me.cmbUsers.Location = New System.Drawing.Point(329, 28)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(478, 33)
        Me.cmbUsers.TabIndex = 40
        Me.cmbUsers.Visible = False
        '
        'lblTotalHoursOrAmount
        '
        Me.lblTotalHoursOrAmount.AutoSize = True
        Me.lblTotalHoursOrAmount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalHoursOrAmount.Location = New System.Drawing.Point(324, 158)
        Me.lblTotalHoursOrAmount.Name = "lblTotalHoursOrAmount"
        Me.lblTotalHoursOrAmount.Size = New System.Drawing.Size(0, 30)
        Me.lblTotalHoursOrAmount.TabIndex = 41
        Me.lblTotalHoursOrAmount.Visible = False
        '
        'txtBoxTotalHoursOrPayments
        '
        Me.txtBoxTotalHoursOrPayments.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.txtBoxTotalHoursOrPayments.Location = New System.Drawing.Point(473, 159)
        Me.txtBoxTotalHoursOrPayments.Name = "txtBoxTotalHoursOrPayments"
        Me.txtBoxTotalHoursOrPayments.ReadOnly = True
        Me.txtBoxTotalHoursOrPayments.Size = New System.Drawing.Size(100, 33)
        Me.txtBoxTotalHoursOrPayments.TabIndex = 42
        Me.txtBoxTotalHoursOrPayments.Visible = False
        '
        'rdbProfit
        '
        Me.rdbProfit.AutoSize = True
        Me.rdbProfit.Location = New System.Drawing.Point(418, 268)
        Me.rdbProfit.Name = "rdbProfit"
        Me.rdbProfit.Size = New System.Drawing.Size(102, 17)
        Me.rdbProfit.TabIndex = 9
        Me.rdbProfit.TabStop = True
        Me.rdbProfit.Text = "Καθαρό Κέρδος"
        Me.rdbProfit.UseVisualStyleBackColor = True
        Me.rdbProfit.Visible = False
        '
        'cmbNoBarcode
        '
        Me.cmbNoBarcode.DropDownHeight = 150
        Me.cmbNoBarcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoBarcode.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNoBarcode.FormattingEnabled = True
        Me.cmbNoBarcode.IntegralHeight = False
        Me.cmbNoBarcode.Location = New System.Drawing.Point(329, 158)
        Me.cmbNoBarcode.Name = "cmbNoBarcode"
        Me.cmbNoBarcode.Size = New System.Drawing.Size(478, 33)
        Me.cmbNoBarcode.TabIndex = 43
        Me.cmbNoBarcode.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrint.Image = Global.POS.My.Resources.Resources.printer
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(821, 177)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(183, 75)
        Me.btnPrint.TabIndex = 38
        Me.btnPrint.Text = "       Εκτύπωση"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSearch.Image = Global.POS.My.Resources.Resources.search
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(819, 96)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(183, 75)
        Me.btnSearch.TabIndex = 37
        Me.btnSearch.Text = "       Αναζήτηση"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(819, 15)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(183, 75)
        Me.cmdExit.TabIndex = 36
        Me.cmdExit.Text = "   Έξοδος"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'btnClearBarcode
        '
        Me.btnClearBarcode.BackgroundImage = Global.POS.My.Resources.Resources.undo
        Me.btnClearBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearBarcode.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearBarcode.Location = New System.Drawing.Point(770, 26)
        Me.btnClearBarcode.Name = "btnClearBarcode"
        Me.btnClearBarcode.Size = New System.Drawing.Size(37, 35)
        Me.btnClearBarcode.TabIndex = 7
        Me.btnClearBarcode.UseVisualStyleBackColor = True
        Me.btnClearBarcode.Visible = False
        '
        'lblAmountVAT
        '
        Me.lblAmountVAT.AutoSize = True
        Me.lblAmountVAT.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountVAT.Location = New System.Drawing.Point(327, 206)
        Me.lblAmountVAT.Name = "lblAmountVAT"
        Me.lblAmountVAT.Size = New System.Drawing.Size(0, 30)
        Me.lblAmountVAT.TabIndex = 44
        Me.lblAmountVAT.Visible = False
        '
        'chkBoxSalesPerSupplier
        '
        Me.chkBoxSalesPerSupplier.AutoSize = True
        Me.chkBoxSalesPerSupplier.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxSalesPerSupplier.Location = New System.Drawing.Point(677, 33)
        Me.chkBoxSalesPerSupplier.Name = "chkBoxSalesPerSupplier"
        Me.chkBoxSalesPerSupplier.Size = New System.Drawing.Size(97, 25)
        Me.chkBoxSalesPerSupplier.TabIndex = 45
        Me.chkBoxSalesPerSupplier.Text = "Πωλήσεις"
        Me.chkBoxSalesPerSupplier.UseVisualStyleBackColor = True
        Me.chkBoxSalesPerSupplier.Visible = False
        '
        'lblTotalSalesAmount
        '
        Me.lblTotalSalesAmount.AutoSize = True
        Me.lblTotalSalesAmount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSalesAmount.Location = New System.Drawing.Point(330, 157)
        Me.lblTotalSalesAmount.Name = "lblTotalSalesAmount"
        Me.lblTotalSalesAmount.Size = New System.Drawing.Size(0, 30)
        Me.lblTotalSalesAmount.TabIndex = 46
        '
        'cmbCategories
        '
        Me.cmbCategories.DropDownHeight = 150
        Me.cmbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategories.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategories.FormattingEnabled = True
        Me.cmbCategories.IntegralHeight = False
        Me.cmbCategories.Location = New System.Drawing.Point(329, 29)
        Me.cmbCategories.Name = "cmbCategories"
        Me.cmbCategories.Size = New System.Drawing.Size(478, 33)
        Me.cmbCategories.TabIndex = 47
        Me.cmbCategories.Visible = False
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExportToExcel.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.btnExportToExcel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExportToExcel.Image = CType(resources.GetObject("btnExportToExcel.Image"), System.Drawing.Image)
        Me.btnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportToExcel.Location = New System.Drawing.Point(821, 258)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(183, 75)
        Me.btnExportToExcel.TabIndex = 48
        Me.btnExportToExcel.Text = "       Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = False
        Me.btnExportToExcel.Visible = False
        '
        'frmReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.btnExportToExcel)
        Me.Controls.Add(Me.cmbCategories)
        Me.Controls.Add(Me.lblTotalSalesAmount)
        Me.Controls.Add(Me.chkBoxSalesPerSupplier)
        Me.Controls.Add(Me.lblAmountVAT)
        Me.Controls.Add(Me.cmbNoBarcode)
        Me.Controls.Add(Me.rdbProfit)
        Me.Controls.Add(Me.txtBoxTotalHoursOrPayments)
        Me.Controls.Add(Me.rdbQuantity)
        Me.Controls.Add(Me.lblTotalHoursOrAmount)
        Me.Controls.Add(Me.cmbUsers)
        Me.Controls.Add(Me.cmbSupplier)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.btnClearBarcode)
        Me.Controls.Add(Me.lblBarcode)
        Me.Controls.Add(Me.txtBoxBarcode)
        Me.Controls.Add(Me.lblToDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.dgvReports)
        Me.Controls.Add(Me.grpBoxReports)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Αναφορές"
        Me.grpBoxReports.ResumeLayout(False)
        Me.grpBoxReports.PerformLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpBoxReports As System.Windows.Forms.GroupBox
    Friend WithEvents rdbSalesPerVAT As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSalesPerProduct As System.Windows.Forms.RadioButton
    Friend WithEvents rdbQuantity As System.Windows.Forms.RadioButton
    Friend WithEvents rdbZReport As System.Windows.Forms.RadioButton
    Friend WithEvents rdbXReport As System.Windows.Forms.RadioButton
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents btnClearBarcode As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents rdbBuySellSupplier As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSupplierPr As System.Windows.Forms.RadioButton
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents rdbUsers As System.Windows.Forms.RadioButton
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents lblTotalHoursOrAmount As System.Windows.Forms.Label
    Friend WithEvents txtBoxTotalHoursOrPayments As System.Windows.Forms.TextBox
    Friend WithEvents rdbProfit As System.Windows.Forms.RadioButton
    Friend WithEvents cmbNoBarcode As System.Windows.Forms.ComboBox
    Friend WithEvents rdbPayments As System.Windows.Forms.RadioButton
    Friend WithEvents lblAmountVAT As System.Windows.Forms.Label
    Friend WithEvents chkBoxSalesPerSupplier As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalSalesAmount As System.Windows.Forms.Label
    Friend WithEvents rdbQntHistory As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSessions As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSalesPerCategory As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCategories As System.Windows.Forms.ComboBox
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
End Class

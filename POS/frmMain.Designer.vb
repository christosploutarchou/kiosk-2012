<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lblUser = New System.Windows.Forms.Label()
        Me.txtBoxUser = New System.Windows.Forms.TextBox()
        Me.dgvMessages = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxInitialFiscalAmt = New System.Windows.Forms.TextBox()
        Me.dgvExpiry = New System.Windows.Forms.DataGridView()
        Me.lnkLabelMessages = New System.Windows.Forms.LinkLabel()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.dgvSuppliers = New System.Windows.Forms.DataGridView()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.btnEditPos = New System.Windows.Forms.Button()
        Me.btnLottery = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnInvoices = New System.Windows.Forms.Button()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.btnReceipts = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnProducts = New System.Windows.Forms.Button()
        Me.btnCategories = New System.Windows.Forms.Button()
        Me.cmdSuppliers = New System.Windows.Forms.Button()
        Me.cmdUsers = New System.Windows.Forms.Button()
        Me.btnPos = New System.Windows.Forms.Button()
        CType(Me.dgvMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvExpiry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSuppliers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUser.Location = New System.Drawing.Point(18, 9)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(172, 32)
        Me.lblUser.TabIndex = 9
        Me.lblUser.Text = "Welcome User"
        '
        'txtBoxUser
        '
        Me.txtBoxUser.BackColor = System.Drawing.SystemColors.Control
        Me.txtBoxUser.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxUser.Location = New System.Drawing.Point(196, 9)
        Me.txtBoxUser.Name = "txtBoxUser"
        Me.txtBoxUser.ReadOnly = True
        Me.txtBoxUser.Size = New System.Drawing.Size(282, 39)
        Me.txtBoxUser.TabIndex = 10
        '
        'dgvMessages
        '
        Me.dgvMessages.AllowUserToAddRows = False
        Me.dgvMessages.AllowUserToDeleteRows = False
        Me.dgvMessages.BackgroundColor = System.Drawing.Color.LemonChiffon
        Me.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMessages.GridColor = System.Drawing.SystemColors.ControlText
        Me.dgvMessages.Location = New System.Drawing.Point(21, 414)
        Me.dgvMessages.Name = "dgvMessages"
        Me.dgvMessages.ReadOnly = True
        Me.dgvMessages.Size = New System.Drawing.Size(480, 315)
        Me.dgvMessages.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(28, 280)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 25)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Αρχ. Ποσό Ταμ."
        '
        'txtBoxInitialFiscalAmt
        '
        Me.txtBoxInitialFiscalAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxInitialFiscalAmt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtBoxInitialFiscalAmt.Location = New System.Drawing.Point(23, 308)
        Me.txtBoxInitialFiscalAmt.Name = "txtBoxInitialFiscalAmt"
        Me.txtBoxInitialFiscalAmt.Size = New System.Drawing.Size(165, 33)
        Me.txtBoxInitialFiscalAmt.TabIndex = 14
        '
        'dgvExpiry
        '
        Me.dgvExpiry.AllowUserToAddRows = False
        Me.dgvExpiry.AllowUserToDeleteRows = False
        Me.dgvExpiry.BackgroundColor = System.Drawing.Color.LemonChiffon
        Me.dgvExpiry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExpiry.GridColor = System.Drawing.SystemColors.ControlText
        Me.dgvExpiry.Location = New System.Drawing.Point(522, 414)
        Me.dgvExpiry.Name = "dgvExpiry"
        Me.dgvExpiry.ReadOnly = True
        Me.dgvExpiry.Size = New System.Drawing.Size(480, 151)
        Me.dgvExpiry.TabIndex = 16
        '
        'lnkLabelMessages
        '
        Me.lnkLabelMessages.AutoSize = True
        Me.lnkLabelMessages.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lnkLabelMessages.Location = New System.Drawing.Point(18, 379)
        Me.lnkLabelMessages.Name = "lnkLabelMessages"
        Me.lnkLabelMessages.Size = New System.Drawing.Size(201, 25)
        Me.lnkLabelMessages.TabIndex = 18
        Me.lnkLabelMessages.TabStop = True
        Me.lnkLabelMessages.Text = "Μηνύματα και Ποσά"
        Me.lnkLabelMessages.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'PrintDocument1
        '
        '
        'dgvSuppliers
        '
        Me.dgvSuppliers.AllowUserToAddRows = False
        Me.dgvSuppliers.AllowUserToDeleteRows = False
        Me.dgvSuppliers.BackgroundColor = System.Drawing.Color.LemonChiffon
        Me.dgvSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSuppliers.GridColor = System.Drawing.SystemColors.ControlText
        Me.dgvSuppliers.Location = New System.Drawing.Point(522, 571)
        Me.dgvSuppliers.Name = "dgvSuppliers"
        Me.dgvSuppliers.ReadOnly = True
        Me.dgvSuppliers.Size = New System.Drawing.Size(480, 158)
        Me.dgvSuppliers.TabIndex = 22
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(950, 9)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(61, 17)
        Me.lblVersion.TabIndex = 24
        Me.lblVersion.Text = "v.010422"
        '
        'btnEditPos
        '
        Me.btnEditPos.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnEditPos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnEditPos.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.btnEditPos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEditPos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditPos.Location = New System.Drawing.Point(722, 51)
        Me.btnEditPos.Name = "btnEditPos"
        Me.btnEditPos.Size = New System.Drawing.Size(170, 102)
        Me.btnEditPos.TabIndex = 26
        Me.btnEditPos.Text = "  Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ταμείου"
        Me.btnEditPos.UseVisualStyleBackColor = False
        '
        'btnLottery
        '
        Me.btnLottery.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnLottery.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLottery.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.btnLottery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnLottery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLottery.Location = New System.Drawing.Point(722, 159)
        Me.btnLottery.Name = "btnLottery"
        Me.btnLottery.Size = New System.Drawing.Size(170, 102)
        Me.btnLottery.TabIndex = 27
        Me.btnLottery.Text = "  Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Λαχείων"
        Me.btnLottery.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(546, 267)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(170, 102)
        Me.Button2.TabIndex = 28
        Me.Button2.Text = "  Διαχείριση Προσφορών"
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'btnInvoices
        '
        Me.btnInvoices.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnInvoices.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnInvoices.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInvoices.Image = Global.POS.My.Resources.Resources.receipt
        Me.btnInvoices.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInvoices.Location = New System.Drawing.Point(194, 159)
        Me.btnInvoices.Name = "btnInvoices"
        Me.btnInvoices.Size = New System.Drawing.Size(170, 102)
        Me.btnInvoices.TabIndex = 25
        Me.btnInvoices.Text = "  Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Τιμολογίων"
        Me.btnInvoices.UseVisualStyleBackColor = False
        '
        'btnBackup
        '
        Me.btnBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBackup.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.btnBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBackup.Image = Global.POS.My.Resources.Resources.backup
        Me.btnBackup.Location = New System.Drawing.Point(898, 51)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(113, 102)
        Me.btnBackup.TabIndex = 19
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBackup.UseVisualStyleBackColor = False
        '
        'btnReceipts
        '
        Me.btnReceipts.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnReceipts.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReceipts.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.btnReceipts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReceipts.Image = Global.POS.My.Resources.Resources.receipt
        Me.btnReceipts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReceipts.Location = New System.Drawing.Point(546, 159)
        Me.btnReceipts.Name = "btnReceipts"
        Me.btnReceipts.Size = New System.Drawing.Size(170, 102)
        Me.btnReceipts.TabIndex = 17
        Me.btnReceipts.Text = "  Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Αποδείξεων"
        Me.btnReceipts.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUpdate.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnUpdate.Image = Global.POS.My.Resources.Resources.save
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(194, 267)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(170, 102)
        Me.btnUpdate.TabIndex = 15
        Me.btnUpdate.Text = "       Αποθήκευση"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnReports
        '
        Me.btnReports.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReports.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnReports.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReports.Image = Global.POS.My.Resources.Resources.reports
        Me.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReports.Location = New System.Drawing.Point(370, 159)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(170, 102)
        Me.btnReports.TabIndex = 12
        Me.btnReports.Text = "Reports"
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(722, 267)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(170, 102)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnProducts
        '
        Me.btnProducts.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnProducts.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnProducts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnProducts.Image = Global.POS.My.Resources.Resources.products
        Me.btnProducts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProducts.Location = New System.Drawing.Point(18, 159)
        Me.btnProducts.Name = "btnProducts"
        Me.btnProducts.Size = New System.Drawing.Size(170, 102)
        Me.btnProducts.TabIndex = 4
        Me.btnProducts.Text = "    Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Προϊόντων"
        Me.btnProducts.UseVisualStyleBackColor = False
        '
        'btnCategories
        '
        Me.btnCategories.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCategories.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnCategories.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCategories.Image = Global.POS.My.Resources.Resources.categories
        Me.btnCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCategories.Location = New System.Drawing.Point(546, 51)
        Me.btnCategories.Name = "btnCategories"
        Me.btnCategories.Size = New System.Drawing.Size(170, 102)
        Me.btnCategories.TabIndex = 3
        Me.btnCategories.Text = "    Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Κατηγοριών"
        Me.btnCategories.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCategories.UseVisualStyleBackColor = False
        '
        'cmdSuppliers
        '
        Me.cmdSuppliers.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSuppliers.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.cmdSuppliers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSuppliers.Image = Global.POS.My.Resources.Resources.suppliers
        Me.cmdSuppliers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSuppliers.Location = New System.Drawing.Point(370, 51)
        Me.cmdSuppliers.Name = "cmdSuppliers"
        Me.cmdSuppliers.Size = New System.Drawing.Size(170, 102)
        Me.cmdSuppliers.TabIndex = 2
        Me.cmdSuppliers.Text = "   Διαχείριση " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Προμηθευτών"
        Me.cmdSuppliers.UseVisualStyleBackColor = False
        '
        'cmdUsers
        '
        Me.cmdUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdUsers.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdUsers.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.cmdUsers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUsers.Image = CType(resources.GetObject("cmdUsers.Image"), System.Drawing.Image)
        Me.cmdUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUsers.Location = New System.Drawing.Point(194, 51)
        Me.cmdUsers.Name = "cmdUsers"
        Me.cmdUsers.Size = New System.Drawing.Size(170, 102)
        Me.cmdUsers.TabIndex = 1
        Me.cmdUsers.Text = " Διαχείριση Χρηστών"
        Me.cmdUsers.UseVisualStyleBackColor = False
        '
        'btnPos
        '
        Me.btnPos.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPos.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnPos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPos.Image = Global.POS.My.Resources.Resources.pos
        Me.btnPos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPos.Location = New System.Drawing.Point(18, 51)
        Me.btnPos.Name = "btnPos"
        Me.btnPos.Size = New System.Drawing.Size(170, 102)
        Me.btnPos.TabIndex = 0
        Me.btnPos.Text = "POS"
        Me.btnPos.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnLottery)
        Me.Controls.Add(Me.btnEditPos)
        Me.Controls.Add(Me.btnInvoices)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.dgvSuppliers)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.lnkLabelMessages)
        Me.Controls.Add(Me.btnReceipts)
        Me.Controls.Add(Me.dgvExpiry)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.txtBoxInitialFiscalAmt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnReports)
        Me.Controls.Add(Me.dgvMessages)
        Me.Controls.Add(Me.txtBoxUser)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnProducts)
        Me.Controls.Add(Me.btnCategories)
        Me.Controls.Add(Me.cmdSuppliers)
        Me.Controls.Add(Me.cmdUsers)
        Me.Controls.Add(Me.btnPos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1022, 722)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Συστήματος"
        CType(Me.dgvMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvExpiry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSuppliers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPos As System.Windows.Forms.Button
    Friend WithEvents cmdUsers As System.Windows.Forms.Button
    Friend WithEvents cmdSuppliers As System.Windows.Forms.Button
    Friend WithEvents btnCategories As System.Windows.Forms.Button
    Friend WithEvents btnProducts As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents txtBoxUser As System.Windows.Forms.TextBox
    Friend WithEvents dgvMessages As System.Windows.Forms.DataGridView
    Friend WithEvents btnReports As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBoxInitialFiscalAmt As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents dgvExpiry As System.Windows.Forms.DataGridView
    Friend WithEvents btnReceipts As System.Windows.Forms.Button
    Friend WithEvents lnkLabelMessages As System.Windows.Forms.LinkLabel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents dgvSuppliers As System.Windows.Forms.DataGridView
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents btnInvoices As System.Windows.Forms.Button
    Friend WithEvents btnEditPos As System.Windows.Forms.Button
    Friend WithEvents btnLottery As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class

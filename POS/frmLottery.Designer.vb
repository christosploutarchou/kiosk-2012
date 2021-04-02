<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLottery
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
        Me.lblTotalLotteryAmt = New System.Windows.Forms.Label
        Me.txtBoxLotteryAmt = New System.Windows.Forms.TextBox
        Me.lnkLblBarcodes = New System.Windows.Forms.LinkLabel
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.dgvLinkedProducts = New System.Windows.Forms.DataGridView
        Me.productSerno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sellAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.availQnt = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvLinkedProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTotalLotteryAmt
        '
        Me.lblTotalLotteryAmt.AutoSize = True
        Me.lblTotalLotteryAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalLotteryAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotalLotteryAmt.Location = New System.Drawing.Point(13, 25)
        Me.lblTotalLotteryAmt.Name = "lblTotalLotteryAmt"
        Me.lblTotalLotteryAmt.Size = New System.Drawing.Size(211, 30)
        Me.lblTotalLotteryAmt.TabIndex = 0
        Me.lblTotalLotteryAmt.Text = "Ολικό Ποσό Λαχείων"
        '
        'txtBoxLotteryAmt
        '
        Me.txtBoxLotteryAmt.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxLotteryAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxLotteryAmt.Location = New System.Drawing.Point(230, 25)
        Me.txtBoxLotteryAmt.MaxLength = 10
        Me.txtBoxLotteryAmt.Name = "txtBoxLotteryAmt"
        Me.txtBoxLotteryAmt.ReadOnly = True
        Me.txtBoxLotteryAmt.Size = New System.Drawing.Size(201, 35)
        Me.txtBoxLotteryAmt.TabIndex = 1
        '
        'lnkLblBarcodes
        '
        Me.lnkLblBarcodes.AutoSize = True
        Me.lnkLblBarcodes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblBarcodes.LinkColor = System.Drawing.Color.MediumBlue
        Me.lnkLblBarcodes.Location = New System.Drawing.Point(13, 71)
        Me.lnkLblBarcodes.Name = "lnkLblBarcodes"
        Me.lnkLblBarcodes.Size = New System.Drawing.Size(192, 30)
        Me.lnkLblBarcodes.TabIndex = 20
        Me.lnkLblBarcodes.TabStop = True
        Me.lnkLblBarcodes.Text = "Προσθήκη Barcode"
        Me.lnkLblBarcodes.VisitedLinkColor = System.Drawing.Color.MediumBlue
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(54, 416)
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
        Me.btnExit.Location = New System.Drawing.Point(242, 416)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(182, 75)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "  Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'dgvLinkedProducts
        '
        Me.dgvLinkedProducts.AllowUserToAddRows = False
        Me.dgvLinkedProducts.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvLinkedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLinkedProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.productSerno, Me.barcode, Me.description, Me.sellAmt, Me.availQnt})
        Me.dgvLinkedProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.dgvLinkedProducts.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvLinkedProducts.Location = New System.Drawing.Point(12, 115)
        Me.dgvLinkedProducts.Name = "dgvLinkedProducts"
        Me.dgvLinkedProducts.Size = New System.Drawing.Size(466, 295)
        Me.dgvLinkedProducts.TabIndex = 64
        '
        'productSerno
        '
        Me.productSerno.HeaderText = "serno"
        Me.productSerno.Name = "productSerno"
        Me.productSerno.ReadOnly = True
        Me.productSerno.Visible = False
        '
        'barcode
        '
        Me.barcode.HeaderText = "Barcode"
        Me.barcode.Name = "barcode"
        Me.barcode.ReadOnly = True
        '
        'description
        '
        Me.description.HeaderText = "Περιγραφή"
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 135
        '
        'sellAmt
        '
        Me.sellAmt.HeaderText = "Τιμή"
        Me.sellAmt.Name = "sellAmt"
        Me.sellAmt.ReadOnly = True
        Me.sellAmt.Width = 80
        '
        'availQnt
        '
        Me.availQnt.HeaderText = "Διαθέσιμη Ποσότητα"
        Me.availQnt.Name = "availQnt"
        Me.availQnt.ReadOnly = True
        '
        'frmLottery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(490, 521)
        Me.Controls.Add(Me.dgvLinkedProducts)
        Me.Controls.Add(Me.lnkLblBarcodes)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.txtBoxLotteryAmt)
        Me.Controls.Add(Me.lblTotalLotteryAmt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(500, 550)
        Me.MinimumSize = New System.Drawing.Size(500, 550)
        Me.Name = "frmLottery"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Ταμείου"
        CType(Me.dgvLinkedProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTotalLotteryAmt As System.Windows.Forms.Label
    Friend WithEvents txtBoxLotteryAmt As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lnkLblBarcodes As System.Windows.Forms.LinkLabel
    Friend WithEvents dgvLinkedProducts As System.Windows.Forms.DataGridView
    Friend WithEvents productSerno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sellAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents availQnt As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

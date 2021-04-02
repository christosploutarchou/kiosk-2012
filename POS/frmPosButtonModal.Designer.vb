<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPosButtonModal
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
        Me.txtBoxButtonName = New System.Windows.Forms.TextBox
        Me.lnkLblBarcodes = New System.Windows.Forms.LinkLabel
        Me.chkBoxVisibleOnPOS = New System.Windows.Forms.CheckBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.dgvLinkedProducts = New System.Windows.Forms.DataGridView
        Me.productSerno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.posDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvLinkedProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSearch.Location = New System.Drawing.Point(13, 25)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(174, 30)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Όνομα κουμπιού"
        '
        'txtBoxButtonName
        '
        Me.txtBoxButtonName.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxButtonName.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxButtonName.Location = New System.Drawing.Point(268, 25)
        Me.txtBoxButtonName.MaxLength = 10
        Me.txtBoxButtonName.Name = "txtBoxButtonName"
        Me.txtBoxButtonName.Size = New System.Drawing.Size(254, 35)
        Me.txtBoxButtonName.TabIndex = 1
        '
        'lnkLblBarcodes
        '
        Me.lnkLblBarcodes.AutoSize = True
        Me.lnkLblBarcodes.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLblBarcodes.LinkColor = System.Drawing.Color.MediumBlue
        Me.lnkLblBarcodes.Location = New System.Drawing.Point(13, 122)
        Me.lnkLblBarcodes.Name = "lnkLblBarcodes"
        Me.lnkLblBarcodes.Size = New System.Drawing.Size(192, 30)
        Me.lnkLblBarcodes.TabIndex = 20
        Me.lnkLblBarcodes.TabStop = True
        Me.lnkLblBarcodes.Text = "Προσθήκη Barcode"
        Me.lnkLblBarcodes.VisitedLinkColor = System.Drawing.Color.MediumBlue
        '
        'chkBoxVisibleOnPOS
        '
        Me.chkBoxVisibleOnPOS.AutoSize = True
        Me.chkBoxVisibleOnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkBoxVisibleOnPOS.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxVisibleOnPOS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBoxVisibleOnPOS.Location = New System.Drawing.Point(18, 70)
        Me.chkBoxVisibleOnPOS.Name = "chkBoxVisibleOnPOS"
        Me.chkBoxVisibleOnPOS.Size = New System.Drawing.Size(207, 34)
        Me.chkBoxVisibleOnPOS.TabIndex = 19
        Me.chkBoxVisibleOnPOS.Text = "Εμφάνιση στο POS"
        Me.chkBoxVisibleOnPOS.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Image = Global.POS.My.Resources.Resources.delete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(18, 570)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(182, 75)
        Me.btnDelete.TabIndex = 24
        Me.btnDelete.Text = "      Διαγραφή"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(206, 570)
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
        Me.btnExit.Location = New System.Drawing.Point(394, 570)
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
        Me.dgvLinkedProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.productSerno, Me.description, Me.posDescription})
        Me.dgvLinkedProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.dgvLinkedProducts.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvLinkedProducts.Location = New System.Drawing.Point(18, 169)
        Me.dgvLinkedProducts.Name = "dgvLinkedProducts"
        Me.dgvLinkedProducts.Size = New System.Drawing.Size(560, 395)
        Me.dgvLinkedProducts.TabIndex = 64
        '
        'productSerno
        '
        Me.productSerno.HeaderText = "serno"
        Me.productSerno.Name = "productSerno"
        Me.productSerno.ReadOnly = True
        Me.productSerno.Visible = False
        '
        'description
        '
        Me.description.HeaderText = "Προϊόν"
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 135
        '
        'posDescription
        '
        Me.posDescription.HeaderText = "Περιγραφή στο POS"
        Me.posDescription.Name = "posDescription"
        Me.posDescription.Width = 135
        '
        'frmPosButtonModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(590, 693)
        Me.Controls.Add(Me.dgvLinkedProducts)
        Me.Controls.Add(Me.chkBoxVisibleOnPOS)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.lnkLblBarcodes)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.txtBoxButtonName)
        Me.Controls.Add(Me.lblSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(600, 768)
        Me.MinimumSize = New System.Drawing.Size(600, 722)
        Me.Name = "frmPosButtonModal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Ταμείου"
        CType(Me.dgvLinkedProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtBoxButtonName As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lnkLblBarcodes As System.Windows.Forms.LinkLabel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents chkBoxVisibleOnPOS As System.Windows.Forms.CheckBox
    Friend WithEvents dgvLinkedProducts As System.Windows.Forms.DataGridView
    Friend WithEvents productSerno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents posDescription As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

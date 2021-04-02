<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBoxBarcodesModal
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
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnOverrideExisting = New System.Windows.Forms.Button
        Me.dgvProductsAndQnt = New System.Windows.Forms.DataGridView
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.productSerno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvProductsAndQnt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.BackgroundImage = Global.POS.My.Resources.Resources.search
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(441, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(37, 39)
        Me.btnSearch.TabIndex = 76
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnOverrideExisting
        '
        Me.btnOverrideExisting.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnOverrideExisting.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOverrideExisting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOverrideExisting.Image = Global.POS.My.Resources.Resources.save
        Me.btnOverrideExisting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOverrideExisting.Location = New System.Drawing.Point(18, 305)
        Me.btnOverrideExisting.Name = "btnOverrideExisting"
        Me.btnOverrideExisting.Size = New System.Drawing.Size(215, 75)
        Me.btnOverrideExisting.TabIndex = 75
        Me.btnOverrideExisting.Text = "     Αποθήκευση"
        Me.btnOverrideExisting.UseVisualStyleBackColor = False
        '
        'dgvProductsAndQnt
        '
        Me.dgvProductsAndQnt.AllowUserToAddRows = False
        Me.dgvProductsAndQnt.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvProductsAndQnt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductsAndQnt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.productSerno, Me.description, Me.barcode})
        Me.dgvProductsAndQnt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.dgvProductsAndQnt.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvProductsAndQnt.Location = New System.Drawing.Point(18, 54)
        Me.dgvProductsAndQnt.Name = "dgvProductsAndQnt"
        Me.dgvProductsAndQnt.Size = New System.Drawing.Size(460, 245)
        Me.dgvProductsAndQnt.TabIndex = 74
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBarcode.Location = New System.Drawing.Point(119, 12)
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(303, 39)
        Me.txtBoxBarcode.TabIndex = 71
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 32)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = "Barcode"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(263, 305)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(215, 75)
        Me.btnExit.TabIndex = 72
        Me.btnExit.Text = "Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'productSerno
        '
        Me.productSerno.HeaderText = "serno"
        Me.productSerno.Name = "productSerno"
        Me.productSerno.Visible = False
        '
        'description
        '
        Me.description.HeaderText = "Περιγραφή"
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 200
        '
        'barcode
        '
        Me.barcode.HeaderText = "Barcode"
        Me.barcode.Name = "barcode"
        Me.barcode.ReadOnly = True
        Me.barcode.Width = 150
        '
        'frmBoxBarcodesModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(490, 421)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnOverrideExisting)
        Me.Controls.Add(Me.dgvProductsAndQnt)
        Me.Controls.Add(Me.txtBoxBarcode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(500, 450)
        Me.MinimumSize = New System.Drawing.Size(450, 450)
        Me.Name = "frmBoxBarcodesModal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Προϊόντων"
        CType(Me.dgvProductsAndQnt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnOverrideExisting As System.Windows.Forms.Button
    Friend WithEvents dgvProductsAndQnt As System.Windows.Forms.DataGridView
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents productSerno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

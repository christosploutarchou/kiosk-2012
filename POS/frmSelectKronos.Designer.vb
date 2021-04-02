<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectKronos
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
        Me.dgvKronosProducts = New System.Windows.Forms.DataGridView
        Me.btnBackspace = New System.Windows.Forms.Button
        Me.itemName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.itemExtendedName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.issueNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.deliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.price = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvKronosProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvKronosProducts
        '
        Me.dgvKronosProducts.AllowUserToAddRows = False
        Me.dgvKronosProducts.AllowUserToDeleteRows = False
        Me.dgvKronosProducts.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvKronosProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKronosProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.itemName, Me.itemExtendedName, Me.issueNumber, Me.deliveryDate, Me.price})
        Me.dgvKronosProducts.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvKronosProducts.Location = New System.Drawing.Point(2, 3)
        Me.dgvKronosProducts.Name = "dgvKronosProducts"
        Me.dgvKronosProducts.ReadOnly = True
        Me.dgvKronosProducts.Size = New System.Drawing.Size(502, 289)
        Me.dgvKronosProducts.TabIndex = 32
        '
        'btnBackspace
        '
        Me.btnBackspace.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnBackspace.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackspace.Image = Global.POS.My.Resources.Resources._select
        Me.btnBackspace.Location = New System.Drawing.Point(403, 298)
        Me.btnBackspace.Name = "btnBackspace"
        Me.btnBackspace.Size = New System.Drawing.Size(89, 47)
        Me.btnBackspace.TabIndex = 63
        Me.btnBackspace.UseVisualStyleBackColor = False
        '
        'itemName
        '
        Me.itemName.HeaderText = "Περιγραφή"
        Me.itemName.Name = "itemName"
        Me.itemName.ReadOnly = True
        '
        'itemExtendedName
        '
        Me.itemExtendedName.HeaderText = "Περιγραφή 2"
        Me.itemExtendedName.Name = "itemExtendedName"
        Me.itemExtendedName.ReadOnly = True
        '
        'issueNumber
        '
        Me.issueNumber.HeaderText = "Ημερ. Έκδοσης"
        Me.issueNumber.Name = "issueNumber"
        Me.issueNumber.ReadOnly = True
        Me.issueNumber.Width = 90
        '
        'deliveryDate
        '
        Me.deliveryDate.HeaderText = "Ημερ.Παραλαβής"
        Me.deliveryDate.Name = "deliveryDate"
        Me.deliveryDate.ReadOnly = True
        Me.deliveryDate.Width = 90
        '
        'price
        '
        Me.price.HeaderText = "Τιμή (€)"
        Me.price.Name = "price"
        Me.price.ReadOnly = True
        Me.price.Width = 90
        '
        'frmSelectKronos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 355)
        Me.Controls.Add(Me.btnBackspace)
        Me.Controls.Add(Me.dgvKronosProducts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(512, 384)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(512, 384)
        Me.Name = "frmSelectKronos"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Επιλογή Προϊόντος"
        CType(Me.dgvKronosProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvKronosProducts As System.Windows.Forms.DataGridView
    Friend WithEvents btnBackspace As System.Windows.Forms.Button
    Friend WithEvents itemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itemExtendedName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents issueNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents deliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents price As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

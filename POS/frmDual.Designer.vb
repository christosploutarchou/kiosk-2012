<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDual
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
        Me.dgvReceiptDual = New System.Windows.Forms.DataGridView()
        Me.serno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.unitprice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtBoxTotalDual = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.txtBoxDualDisc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBoxDualFinal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.dgvReceiptDual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvReceiptDual
        '
        Me.dgvReceiptDual.AllowUserToAddRows = False
        Me.dgvReceiptDual.BackgroundColor = System.Drawing.SystemColors.ScrollBar
        Me.dgvReceiptDual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReceiptDual.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.serno, Me.description, Me.quantity, Me.unitprice, Me.amount, Me.vat})
        Me.dgvReceiptDual.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvReceiptDual.Location = New System.Drawing.Point(0, 2)
        Me.dgvReceiptDual.Name = "dgvReceiptDual"
        Me.dgvReceiptDual.ReadOnly = True
        Me.dgvReceiptDual.Size = New System.Drawing.Size(723, 263)
        Me.dgvReceiptDual.TabIndex = 31
        '
        'serno
        '
        Me.serno.HeaderText = "Σειρά"
        Me.serno.Name = "serno"
        Me.serno.ReadOnly = True
        Me.serno.Width = 50
        '
        'description
        '
        Me.description.HeaderText = "Περιγραφή"
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 290
        '
        'quantity
        '
        Me.quantity.HeaderText = "Ποσ."
        Me.quantity.Name = "quantity"
        Me.quantity.ReadOnly = True
        Me.quantity.Width = 60
        '
        'unitprice
        '
        Me.unitprice.HeaderText = "Τιμή Μονάδας (€)"
        Me.unitprice.Name = "unitprice"
        Me.unitprice.ReadOnly = True
        Me.unitprice.Width = 90
        '
        'amount
        '
        Me.amount.HeaderText = "Ποσό (€)"
        Me.amount.Name = "amount"
        Me.amount.ReadOnly = True
        '
        'vat
        '
        Me.vat.HeaderText = "Φ.Π.Α"
        Me.vat.Name = "vat"
        Me.vat.ReadOnly = True
        Me.vat.Width = 90
        '
        'txtBoxTotalDual
        '
        Me.txtBoxTotalDual.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxTotalDual.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalDual.Location = New System.Drawing.Point(106, 32)
        Me.txtBoxTotalDual.Name = "txtBoxTotalDual"
        Me.txtBoxTotalDual.ReadOnly = True
        Me.txtBoxTotalDual.Size = New System.Drawing.Size(110, 35)
        Me.txtBoxTotalDual.TabIndex = 77
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblTotal.Location = New System.Drawing.Point(3, 30)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(64, 32)
        Me.lblTotal.TabIndex = 78
        Me.lblTotal.Text = "Total"
        '
        'txtBoxDualDisc
        '
        Me.txtBoxDualDisc.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxDualDisc.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDualDisc.Location = New System.Drawing.Point(106, 73)
        Me.txtBoxDualDisc.Name = "txtBoxDualDisc"
        Me.txtBoxDualDisc.ReadOnly = True
        Me.txtBoxDualDisc.Size = New System.Drawing.Size(110, 35)
        Me.txtBoxDualDisc.TabIndex = 80
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label2.Location = New System.Drawing.Point(3, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 32)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "Discount"
        '
        'txtBoxDualFinal
        '
        Me.txtBoxDualFinal.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxDualFinal.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDualFinal.Location = New System.Drawing.Point(106, 119)
        Me.txtBoxDualFinal.Name = "txtBoxDualFinal"
        Me.txtBoxDualFinal.ReadOnly = True
        Me.txtBoxDualFinal.Size = New System.Drawing.Size(110, 35)
        Me.txtBoxDualFinal.TabIndex = 82
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label3.Location = New System.Drawing.Point(3, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 32)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Final"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel1.Controls.Add(Me.lblTotal)
        Me.Panel1.Controls.Add(Me.txtBoxDualFinal)
        Me.Panel1.Controls.Add(Me.txtBoxTotalDual)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtBoxDualDisc)
        Me.Panel1.Location = New System.Drawing.Point(5, 271)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(712, 195)
        Me.Panel1.TabIndex = 85
        '
        'frmDual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(718, 471)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dgvReceiptDual)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(728, 500)
        Me.MinimumSize = New System.Drawing.Size(728, 500)
        Me.Name = "frmDual"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "X-Drive"
        CType(Me.dgvReceiptDual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvReceiptDual As System.Windows.Forms.DataGridView
    Friend WithEvents txtBoxTotalDual As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents txtBoxDualDisc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBoxDualFinal As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents serno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents unitprice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vat As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

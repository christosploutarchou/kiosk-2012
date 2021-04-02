<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPosDynamic2Items
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
        Me.grbProducts = New System.Windows.Forms.GroupBox
        Me.btnItem1 = New System.Windows.Forms.Button
        Me.btnItem2 = New System.Windows.Forms.Button
        Me.grbProducts.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbProducts
        '
        Me.grbProducts.Controls.Add(Me.btnItem2)
        Me.grbProducts.Controls.Add(Me.btnItem1)
        Me.grbProducts.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbProducts.Location = New System.Drawing.Point(12, 1)
        Me.grbProducts.Name = "grbProducts"
        Me.grbProducts.Size = New System.Drawing.Size(351, 275)
        Me.grbProducts.TabIndex = 64
        Me.grbProducts.TabStop = False
        '
        'btnItem1
        '
        Me.btnItem1.Location = New System.Drawing.Point(69, 30)
        Me.btnItem1.Name = "btnItem1"
        Me.btnItem1.Size = New System.Drawing.Size(214, 100)
        Me.btnItem1.TabIndex = 0
        Me.btnItem1.UseVisualStyleBackColor = True
        '
        'btnItem2
        '
        Me.btnItem2.Location = New System.Drawing.Point(69, 136)
        Me.btnItem2.Name = "btnItem2"
        Me.btnItem2.Size = New System.Drawing.Size(214, 100)
        Me.btnItem2.TabIndex = 1
        Me.btnItem2.UseVisualStyleBackColor = True
        '
        'frmPosDynamic2Items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 288)
        Me.Controls.Add(Me.grbProducts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(385, 317)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(385, 317)
        Me.Name = "frmPosDynamic2Items"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.grbProducts.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbProducts As System.Windows.Forms.GroupBox
    Friend WithEvents btnItem2 As System.Windows.Forms.Button
    Friend WithEvents btnItem1 As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentInfo
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
        Me.btnSelect = New System.Windows.Forms.Button
        Me.grbVAT = New System.Windows.Forms.GroupBox
        Me.rdb19Percent = New System.Windows.Forms.RadioButton
        Me.rdb5Percent = New System.Windows.Forms.RadioButton
        Me.rdb0Percent = New System.Windows.Forms.RadioButton
        Me.grbVAT.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSelect.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Image = Global.POS.My.Resources.Resources._select
        Me.btnSelect.Location = New System.Drawing.Point(401, 296)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(89, 47)
        Me.btnSelect.TabIndex = 63
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'grbVAT
        '
        Me.grbVAT.Controls.Add(Me.rdb19Percent)
        Me.grbVAT.Controls.Add(Me.rdb5Percent)
        Me.grbVAT.Controls.Add(Me.rdb0Percent)
        Me.grbVAT.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbVAT.Location = New System.Drawing.Point(14, 12)
        Me.grbVAT.Name = "grbVAT"
        Me.grbVAT.Size = New System.Drawing.Size(476, 278)
        Me.grbVAT.TabIndex = 64
        Me.grbVAT.TabStop = False
        Me.grbVAT.Text = "Επιλογή Φ.Π.Α. Τιμολογίου"
        '
        'rdb19Percent
        '
        Me.rdb19Percent.AutoSize = True
        Me.rdb19Percent.Location = New System.Drawing.Point(325, 72)
        Me.rdb19Percent.Name = "rdb19Percent"
        Me.rdb19Percent.Size = New System.Drawing.Size(72, 27)
        Me.rdb19Percent.TabIndex = 2
        Me.rdb19Percent.TabStop = True
        Me.rdb19Percent.Text = "19%"
        Me.rdb19Percent.UseVisualStyleBackColor = True
        '
        'rdb5Percent
        '
        Me.rdb5Percent.AutoSize = True
        Me.rdb5Percent.Location = New System.Drawing.Point(184, 72)
        Me.rdb5Percent.Name = "rdb5Percent"
        Me.rdb5Percent.Size = New System.Drawing.Size(60, 27)
        Me.rdb5Percent.TabIndex = 1
        Me.rdb5Percent.TabStop = True
        Me.rdb5Percent.Text = "5%"
        Me.rdb5Percent.UseVisualStyleBackColor = True
        '
        'rdb0Percent
        '
        Me.rdb0Percent.AutoSize = True
        Me.rdb0Percent.Location = New System.Drawing.Point(35, 72)
        Me.rdb0Percent.Name = "rdb0Percent"
        Me.rdb0Percent.Size = New System.Drawing.Size(60, 27)
        Me.rdb0Percent.TabIndex = 0
        Me.rdb0Percent.TabStop = True
        Me.rdb0Percent.Text = "0%"
        Me.rdb0Percent.UseVisualStyleBackColor = True
        '
        'frmPaymentInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 355)
        Me.Controls.Add(Me.grbVAT)
        Me.Controls.Add(Me.btnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(512, 384)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(512, 384)
        Me.Name = "frmPaymentInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Επιλογή Φ.Π.Α."
        Me.grbVAT.ResumeLayout(False)
        Me.grbVAT.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents grbVAT As System.Windows.Forms.GroupBox
    Friend WithEvents rdb19Percent As System.Windows.Forms.RadioButton
    Friend WithEvents rdb5Percent As System.Windows.Forms.RadioButton
    Friend WithEvents rdb0Percent As System.Windows.Forms.RadioButton
End Class

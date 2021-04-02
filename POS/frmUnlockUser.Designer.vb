<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnlockUser
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
        Me.lstBoxLockedUsers = New System.Windows.Forms.ListBox
        Me.lstBoxUUIDS = New System.Windows.Forms.ListBox
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnUnlock = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lstBoxLockedUsers
        '
        Me.lstBoxLockedUsers.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxLockedUsers.FormattingEnabled = True
        Me.lstBoxLockedUsers.ItemHeight = 32
        Me.lstBoxLockedUsers.Location = New System.Drawing.Point(201, 21)
        Me.lstBoxLockedUsers.Name = "lstBoxLockedUsers"
        Me.lstBoxLockedUsers.Size = New System.Drawing.Size(273, 388)
        Me.lstBoxLockedUsers.TabIndex = 3
        '
        'lstBoxUUIDS
        '
        Me.lstBoxUUIDS.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxUUIDS.FormattingEnabled = True
        Me.lstBoxUUIDS.ItemHeight = 32
        Me.lstBoxUUIDS.Location = New System.Drawing.Point(88, 373)
        Me.lstBoxUUIDS.Name = "lstBoxUUIDS"
        Me.lstBoxUUIDS.Size = New System.Drawing.Size(107, 36)
        Me.lstBoxUUIDS.TabIndex = 43
        Me.lstBoxUUIDS.Visible = False
        '
        'PrintDocument1
        '
        '
        'btnExit
        '
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(312, 415)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(162, 74)
        Me.btnExit.TabIndex = 44
        Me.btnExit.Text = "     ΄Εξοδος "
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnUnlock
        '
        Me.btnUnlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUnlock.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.btnUnlock.Image = Global.POS.My.Resources.Resources.unlock
        Me.btnUnlock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUnlock.Location = New System.Drawing.Point(12, 21)
        Me.btnUnlock.Name = "btnUnlock"
        Me.btnUnlock.Size = New System.Drawing.Size(183, 74)
        Me.btnUnlock.TabIndex = 45
        Me.btnUnlock.Text = "    Unlock " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    User "
        Me.btnUnlock.UseVisualStyleBackColor = False
        '
        'frmUnlockUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(492, 501)
        Me.Controls.Add(Me.btnUnlock)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lstBoxUUIDS)
        Me.Controls.Add(Me.lstBoxLockedUsers)
        Me.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(5, 8, 5, 8)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(502, 530)
        Me.MinimumSize = New System.Drawing.Size(502, 530)
        Me.Name = "frmUnlockUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Unlock"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstBoxLockedUsers As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxUUIDS As System.Windows.Forms.ListBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUnlock As System.Windows.Forms.Button
End Class

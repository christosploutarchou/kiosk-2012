<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCategories
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
        Me.lstBoxUUIDs = New System.Windows.Forms.ListBox
        Me.lstBoxDescription = New System.Windows.Forms.ListBox
        Me.lstBoxVAT = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbExisting = New System.Windows.Forms.RadioButton
        Me.rdbNewCategory = New System.Windows.Forms.RadioButton
        Me.txtBoxVAT = New System.Windows.Forms.TextBox
        Me.txtBoxDescription = New System.Windows.Forms.TextBox
        Me.lblVAT = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblPerCent = New System.Windows.Forms.Label
        Me.cmdExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstBoxUUIDs
        '
        Me.lstBoxUUIDs.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxUUIDs.FormattingEnabled = True
        Me.lstBoxUUIDs.ItemHeight = 32
        Me.lstBoxUUIDs.Location = New System.Drawing.Point(417, 211)
        Me.lstBoxUUIDs.Name = "lstBoxUUIDs"
        Me.lstBoxUUIDs.Size = New System.Drawing.Size(106, 132)
        Me.lstBoxUUIDs.TabIndex = 0
        Me.lstBoxUUIDs.Visible = False
        '
        'lstBoxDescription
        '
        Me.lstBoxDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lstBoxDescription.FormattingEnabled = True
        Me.lstBoxDescription.ItemHeight = 32
        Me.lstBoxDescription.Location = New System.Drawing.Point(658, 21)
        Me.lstBoxDescription.Name = "lstBoxDescription"
        Me.lstBoxDescription.Size = New System.Drawing.Size(346, 612)
        Me.lstBoxDescription.TabIndex = 6
        '
        'lstBoxVAT
        '
        Me.lstBoxVAT.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxVAT.FormattingEnabled = True
        Me.lstBoxVAT.ItemHeight = 32
        Me.lstBoxVAT.Location = New System.Drawing.Point(529, 211)
        Me.lstBoxVAT.Name = "lstBoxVAT"
        Me.lstBoxVAT.Size = New System.Drawing.Size(106, 132)
        Me.lstBoxVAT.TabIndex = 2
        Me.lstBoxVAT.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbExisting)
        Me.GroupBox1.Controls.Add(Me.rdbNewCategory)
        Me.GroupBox1.Font = New System.Drawing.Font(REPORT_FONT, 18.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(398, 136)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Επιλογή"
        '
        'rdbExisting
        '
        Me.rdbExisting.AutoSize = True
        Me.rdbExisting.Location = New System.Drawing.Point(6, 81)
        Me.rdbExisting.Name = "rdbExisting"
        Me.rdbExisting.Size = New System.Drawing.Size(316, 36)
        Me.rdbExisting.TabIndex = 1
        Me.rdbExisting.TabStop = True
        Me.rdbExisting.Text = "Επεξεργασία Υφιστάμενης"
        Me.rdbExisting.UseVisualStyleBackColor = True
        '
        'rdbNewCategory
        '
        Me.rdbNewCategory.AutoSize = True
        Me.rdbNewCategory.Location = New System.Drawing.Point(6, 39)
        Me.rdbNewCategory.Name = "rdbNewCategory"
        Me.rdbNewCategory.Size = New System.Drawing.Size(218, 36)
        Me.rdbNewCategory.TabIndex = 0
        Me.rdbNewCategory.TabStop = True
        Me.rdbNewCategory.Text = "Δημιουργία Νέας"
        Me.rdbNewCategory.UseVisualStyleBackColor = True
        '
        'txtBoxVAT
        '
        Me.txtBoxVAT.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxVAT.Location = New System.Drawing.Point(179, 211)
        Me.txtBoxVAT.MaxLength = 32
        Me.txtBoxVAT.Name = "txtBoxVAT"
        Me.txtBoxVAT.Size = New System.Drawing.Size(157, 39)
        Me.txtBoxVAT.TabIndex = 3
        '
        'txtBoxDescription
        '
        Me.txtBoxDescription.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxDescription.Location = New System.Drawing.Point(179, 165)
        Me.txtBoxDescription.MaxLength = 100
        Me.txtBoxDescription.Name = "txtBoxDescription"
        Me.txtBoxDescription.Size = New System.Drawing.Size(456, 39)
        Me.txtBoxDescription.TabIndex = 2
        '
        'lblVAT
        '
        Me.lblVAT.AutoSize = True
        Me.lblVAT.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVAT.Location = New System.Drawing.Point(12, 211)
        Me.lblVAT.Name = "lblVAT"
        Me.lblVAT.Size = New System.Drawing.Size(58, 32)
        Me.lblVAT.TabIndex = 11
        Me.lblVAT.Text = "VAT"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(12, 165)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(135, 32)
        Me.lblDescription.TabIndex = 10
        Me.lblDescription.Text = "Περιγραφή"
        '
        'lblPerCent
        '
        Me.lblPerCent.AutoSize = True
        Me.lblPerCent.Font = New System.Drawing.Font(REPORT_FONT, 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerCent.Location = New System.Drawing.Point(342, 211)
        Me.lblPerCent.Name = "lblPerCent"
        Me.lblPerCent.Size = New System.Drawing.Size(35, 32)
        Me.lblPerCent.TabIndex = 14
        Me.lblPerCent.Text = "%"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font(REPORT_FONT, 18.0!)
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(789, 643)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(215, 75)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "Έξοδος"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font(REPORT_FONT, 18.0!)
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(567, 643)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(215, 75)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "     Αποθήκευση"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmCategories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblPerCent)
        Me.Controls.Add(Me.txtBoxVAT)
        Me.Controls.Add(Me.txtBoxDescription)
        Me.Controls.Add(Me.lblVAT)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstBoxVAT)
        Me.Controls.Add(Me.lstBoxDescription)
        Me.Controls.Add(Me.lstBoxUUIDs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmCategories"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Κατηγοριών"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstBoxUUIDs As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxDescription As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxVAT As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbExisting As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewCategory As System.Windows.Forms.RadioButton
    Friend WithEvents txtBoxVAT As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblVAT As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblPerCent As System.Windows.Forms.Label
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class

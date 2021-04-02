<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewUser
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
        Me.lstBoxUsers = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbExisting = New System.Windows.Forms.RadioButton
        Me.rdbNewUser = New System.Windows.Forms.RadioButton
        Me.lblFullName = New System.Windows.Forms.Label
        Me.lblPhone = New System.Windows.Forms.Label
        Me.lblAddress = New System.Windows.Forms.Label
        Me.lblIdentity = New System.Windows.Forms.Label
        Me.lblUsername = New System.Windows.Forms.Label
        Me.lblPassword = New System.Windows.Forms.Label
        Me.txtBoxFullName = New System.Windows.Forms.TextBox
        Me.txtBoxPhone = New System.Windows.Forms.TextBox
        Me.txtBoxAddress = New System.Windows.Forms.TextBox
        Me.txtBoxIdentity = New System.Windows.Forms.TextBox
        Me.txtBoxUsername = New System.Windows.Forms.TextBox
        Me.txtBoxPassword = New System.Windows.Forms.TextBox
        Me.lnkLabelChangePass = New System.Windows.Forms.LinkLabel
        Me.chkBoxAdmin = New System.Windows.Forms.CheckBox
        Me.lblNewPassword = New System.Windows.Forms.Label
        Me.btnDelete = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.chkBoxReports = New System.Windows.Forms.CheckBox
        Me.chkBoxEditProd = New System.Windows.Forms.CheckBox
        Me.chkBoxEditProdFull = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstBoxUsers
        '
        Me.lstBoxUsers.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lstBoxUsers.FormattingEnabled = True
        Me.lstBoxUsers.ItemHeight = 32
        Me.lstBoxUsers.Location = New System.Drawing.Point(658, 21)
        Me.lstBoxUsers.Name = "lstBoxUsers"
        Me.lstBoxUsers.Size = New System.Drawing.Size(346, 516)
        Me.lstBoxUsers.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbExisting)
        Me.GroupBox1.Controls.Add(Me.rdbNewUser)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(398, 136)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Επιλογή"
        '
        'rdbExisting
        '
        Me.rdbExisting.AutoSize = True
        Me.rdbExisting.Location = New System.Drawing.Point(6, 81)
        Me.rdbExisting.Name = "rdbExisting"
        Me.rdbExisting.Size = New System.Drawing.Size(318, 36)
        Me.rdbExisting.TabIndex = 2
        Me.rdbExisting.TabStop = True
        Me.rdbExisting.Text = "Επεξεργασία Υφιστάμενου"
        Me.rdbExisting.UseVisualStyleBackColor = True
        '
        'rdbNewUser
        '
        Me.rdbNewUser.AutoSize = True
        Me.rdbNewUser.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbNewUser.Location = New System.Drawing.Point(6, 39)
        Me.rdbNewUser.Name = "rdbNewUser"
        Me.rdbNewUser.Size = New System.Drawing.Size(219, 36)
        Me.rdbNewUser.TabIndex = 1
        Me.rdbNewUser.TabStop = True
        Me.rdbNewUser.Text = "Δημιουργία Νέου"
        Me.rdbNewUser.UseVisualStyleBackColor = True
        '
        'lblFullName
        '
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullName.Location = New System.Drawing.Point(15, 180)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(200, 32)
        Me.lblFullName.TabIndex = 2
        Me.lblFullName.Text = "Ονοματεπώνυμο"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(15, 226)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(128, 32)
        Me.lblPhone.TabIndex = 3
        Me.lblPhone.Text = "Τηλέφωνο"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(15, 276)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(128, 32)
        Me.lblAddress.TabIndex = 4
        Me.lblAddress.Text = "Διεύθυνση"
        '
        'lblIdentity
        '
        Me.lblIdentity.AutoSize = True
        Me.lblIdentity.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdentity.Location = New System.Drawing.Point(15, 322)
        Me.lblIdentity.Name = "lblIdentity"
        Me.lblIdentity.Size = New System.Drawing.Size(135, 32)
        Me.lblIdentity.TabIndex = 5
        Me.lblIdentity.Text = "Ταυτότητα"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.Location = New System.Drawing.Point(12, 368)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(178, 32)
        Me.lblUsername.TabIndex = 6
        Me.lblUsername.Text = "Όνομα χρήστη"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(15, 572)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(106, 32)
        Me.lblPassword.TabIndex = 7
        Me.lblPassword.Text = "Κωδικός"
        '
        'txtBoxFullName
        '
        Me.txtBoxFullName.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxFullName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxFullName.Location = New System.Drawing.Point(255, 180)
        Me.txtBoxFullName.MaxLength = 100
        Me.txtBoxFullName.Name = "txtBoxFullName"
        Me.txtBoxFullName.Size = New System.Drawing.Size(397, 39)
        Me.txtBoxFullName.TabIndex = 3
        '
        'txtBoxPhone
        '
        Me.txtBoxPhone.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxPhone.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPhone.Location = New System.Drawing.Point(255, 226)
        Me.txtBoxPhone.MaxLength = 32
        Me.txtBoxPhone.Name = "txtBoxPhone"
        Me.txtBoxPhone.Size = New System.Drawing.Size(397, 39)
        Me.txtBoxPhone.TabIndex = 4
        '
        'txtBoxAddress
        '
        Me.txtBoxAddress.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxAddress.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxAddress.Location = New System.Drawing.Point(255, 276)
        Me.txtBoxAddress.MaxLength = 100
        Me.txtBoxAddress.Name = "txtBoxAddress"
        Me.txtBoxAddress.Size = New System.Drawing.Size(397, 39)
        Me.txtBoxAddress.TabIndex = 5
        '
        'txtBoxIdentity
        '
        Me.txtBoxIdentity.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxIdentity.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxIdentity.Location = New System.Drawing.Point(255, 322)
        Me.txtBoxIdentity.MaxLength = 20
        Me.txtBoxIdentity.Name = "txtBoxIdentity"
        Me.txtBoxIdentity.Size = New System.Drawing.Size(397, 39)
        Me.txtBoxIdentity.TabIndex = 6
        '
        'txtBoxUsername
        '
        Me.txtBoxUsername.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtBoxUsername.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxUsername.Location = New System.Drawing.Point(255, 368)
        Me.txtBoxUsername.MaxLength = 32
        Me.txtBoxUsername.Name = "txtBoxUsername"
        Me.txtBoxUsername.Size = New System.Drawing.Size(397, 39)
        Me.txtBoxUsername.TabIndex = 7
        '
        'txtBoxPassword
        '
        Me.txtBoxPassword.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPassword.Location = New System.Drawing.Point(255, 572)
        Me.txtBoxPassword.MaxLength = 32
        Me.txtBoxPassword.Name = "txtBoxPassword"
        Me.txtBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtBoxPassword.Size = New System.Drawing.Size(187, 39)
        Me.txtBoxPassword.TabIndex = 10
        '
        'lnkLabelChangePass
        '
        Me.lnkLabelChangePass.AutoSize = True
        Me.lnkLabelChangePass.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLabelChangePass.Location = New System.Drawing.Point(249, 522)
        Me.lnkLabelChangePass.Name = "lnkLabelChangePass"
        Me.lnkLabelChangePass.Size = New System.Drawing.Size(195, 32)
        Me.lnkLabelChangePass.TabIndex = 9
        Me.lnkLabelChangePass.TabStop = True
        Me.lnkLabelChangePass.Text = "Αλλαγή Κωδικού"
        Me.lnkLabelChangePass.Visible = False
        Me.lnkLabelChangePass.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'chkBoxAdmin
        '
        Me.chkBoxAdmin.AutoSize = True
        Me.chkBoxAdmin.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxAdmin.Location = New System.Drawing.Point(18, 461)
        Me.chkBoxAdmin.Name = "chkBoxAdmin"
        Me.chkBoxAdmin.Size = New System.Drawing.Size(171, 36)
        Me.chkBoxAdmin.TabIndex = 8
        Me.chkBoxAdmin.Text = "Διαχειριστής"
        Me.chkBoxAdmin.UseVisualStyleBackColor = True
        '
        'lblNewPassword
        '
        Me.lblNewPassword.AutoSize = True
        Me.lblNewPassword.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewPassword.Location = New System.Drawing.Point(15, 572)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(167, 32)
        Me.lblNewPassword.TabIndex = 19
        Me.lblNewPassword.Text = "Νέος Κωδικός"
        Me.lblNewPassword.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Image = Global.POS.My.Resources.Resources.delete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(567, 562)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(215, 75)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Διαγραφή"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(789, 643)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(215, 75)
        Me.cmdExit.TabIndex = 14
        Me.cmdExit.Text = "Έξοδος"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(567, 643)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(215, 75)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "     Αποθήκευση"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Image = Global.POS.My.Resources.Resources.undo
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(789, 562)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(215, 75)
        Me.btnClear.TabIndex = 56
        Me.btnClear.Text = "   Καθαρισμός Πεδίων"
        Me.btnClear.UseVisualStyleBackColor = False
        Me.btnClear.Visible = False
        '
        'chkBoxReports
        '
        Me.chkBoxReports.AutoSize = True
        Me.chkBoxReports.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxReports.Location = New System.Drawing.Point(18, 419)
        Me.chkBoxReports.Name = "chkBoxReports"
        Me.chkBoxReports.Size = New System.Drawing.Size(259, 36)
        Me.chkBoxReports.TabIndex = 57
        Me.chkBoxReports.Text = "Προβολή Αναφορών"
        Me.chkBoxReports.UseVisualStyleBackColor = True
        '
        'chkBoxEditProd
        '
        Me.chkBoxEditProd.AutoSize = True
        Me.chkBoxEditProd.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxEditProd.Location = New System.Drawing.Point(283, 419)
        Me.chkBoxEditProd.Name = "chkBoxEditProd"
        Me.chkBoxEditProd.Size = New System.Drawing.Size(348, 36)
        Me.chkBoxEditProd.TabIndex = 58
        Me.chkBoxEditProd.Text = "Επεξ. Προϊόντων (Read Only)"
        Me.chkBoxEditProd.UseVisualStyleBackColor = True
        '
        'chkBoxEditProdFull
        '
        Me.chkBoxEditProdFull.AutoSize = True
        Me.chkBoxEditProdFull.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxEditProdFull.Location = New System.Drawing.Point(283, 461)
        Me.chkBoxEditProdFull.Name = "chkBoxEditProdFull"
        Me.chkBoxEditProdFull.Size = New System.Drawing.Size(299, 36)
        Me.chkBoxEditProdFull.TabIndex = 59
        Me.chkBoxEditProdFull.Text = "Επεξεργασία Προϊόντων"
        Me.chkBoxEditProdFull.UseVisualStyleBackColor = True
        '
        'frmNewUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.chkBoxEditProdFull)
        Me.Controls.Add(Me.chkBoxEditProd)
        Me.Controls.Add(Me.chkBoxReports)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblNewPassword)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.chkBoxAdmin)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lnkLabelChangePass)
        Me.Controls.Add(Me.txtBoxPassword)
        Me.Controls.Add(Me.txtBoxUsername)
        Me.Controls.Add(Me.txtBoxIdentity)
        Me.Controls.Add(Me.txtBoxAddress)
        Me.Controls.Add(Me.txtBoxPhone)
        Me.Controls.Add(Me.txtBoxFullName)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.lblIdentity)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblFullName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstBoxUsers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1022, 722)
        Me.Name = "frmNewUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Χρηστών"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstBoxUsers As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbExisting As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewUser As System.Windows.Forms.RadioButton
    Friend WithEvents lblFullName As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblIdentity As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtBoxFullName As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxIdentity As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents lnkLabelChangePass As System.Windows.Forms.LinkLabel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents chkBoxAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents chkBoxReports As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxEditProd As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxEditProdFull As System.Windows.Forms.CheckBox
End Class

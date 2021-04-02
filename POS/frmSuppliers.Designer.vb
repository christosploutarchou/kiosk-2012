<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSuppliers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSuppliers))
        Me.txtBoxPhone2 = New System.Windows.Forms.TextBox
        Me.txtBoxPhone1 = New System.Windows.Forms.TextBox
        Me.txtBoxName = New System.Windows.Forms.TextBox
        Me.lblPhone2 = New System.Windows.Forms.Label
        Me.lblPhone1 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbExisting = New System.Windows.Forms.RadioButton
        Me.rdbNewSupplier = New System.Windows.Forms.RadioButton
        Me.lstBoxName = New System.Windows.Forms.ListBox
        Me.txtBoxEmail = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblContactName = New System.Windows.Forms.Label
        Me.txtBoxContactName = New System.Windows.Forms.TextBox
        Me.lblDay = New System.Windows.Forms.Label
        Me.ckbMon = New System.Windows.Forms.CheckBox
        Me.ckbTue = New System.Windows.Forms.CheckBox
        Me.ckbWed = New System.Windows.Forms.CheckBox
        Me.ckbThu = New System.Windows.Forms.CheckBox
        Me.ckbFri = New System.Windows.Forms.CheckBox
        Me.lstBoxUUID = New System.Windows.Forms.ListBox
        Me.lstBoxThu = New System.Windows.Forms.ListBox
        Me.lstBoxWed = New System.Windows.Forms.ListBox
        Me.lstBoxTue = New System.Windows.Forms.ListBox
        Me.lstBoxMon = New System.Windows.Forms.ListBox
        Me.lstBoxContactName = New System.Windows.Forms.ListBox
        Me.lstBoxEmail = New System.Windows.Forms.ListBox
        Me.lstBoxPhone2 = New System.Windows.Forms.ListBox
        Me.lstBoxPhone1 = New System.Windows.Forms.ListBox
        Me.lstBoxFri = New System.Windows.Forms.ListBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.lblNotes = New System.Windows.Forms.Label
        Me.txtBoxNotes = New System.Windows.Forms.TextBox
        Me.lstBoxNotes = New System.Windows.Forms.ListBox
        Me.btnDeleteSupplier = New System.Windows.Forms.Button
        Me.lstBoxIsDefault = New System.Windows.Forms.ListBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtBoxPhone2
        '
        Me.txtBoxPhone2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPhone2.Location = New System.Drawing.Point(308, 299)
        Me.txtBoxPhone2.Name = "txtBoxPhone2"
        Me.txtBoxPhone2.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxPhone2.TabIndex = 4
        '
        'txtBoxPhone1
        '
        Me.txtBoxPhone1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxPhone1.Location = New System.Drawing.Point(308, 253)
        Me.txtBoxPhone1.Name = "txtBoxPhone1"
        Me.txtBoxPhone1.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxPhone1.TabIndex = 3
        '
        'txtBoxName
        '
        Me.txtBoxName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxName.Location = New System.Drawing.Point(308, 161)
        Me.txtBoxName.Name = "txtBoxName"
        Me.txtBoxName.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxName.TabIndex = 1
        '
        'lblPhone2
        '
        Me.lblPhone2.AutoSize = True
        Me.lblPhone2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone2.Location = New System.Drawing.Point(12, 299)
        Me.lblPhone2.Name = "lblPhone2"
        Me.lblPhone2.Size = New System.Drawing.Size(162, 32)
        Me.lblPhone2.TabIndex = 22
        Me.lblPhone2.Text = "Τηλέφωνο (2)"
        '
        'lblPhone1
        '
        Me.lblPhone1.AutoSize = True
        Me.lblPhone1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone1.Location = New System.Drawing.Point(12, 253)
        Me.lblPhone1.Name = "lblPhone1"
        Me.lblPhone1.Size = New System.Drawing.Size(162, 32)
        Me.lblPhone1.TabIndex = 21
        Me.lblPhone1.Text = "Τηλέφωνο (1)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 32)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Όνομα"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbExisting)
        Me.GroupBox1.Controls.Add(Me.rdbNewSupplier)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(398, 136)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Επιλογή"
        '
        'rdbExisting
        '
        Me.rdbExisting.AutoSize = True
        Me.rdbExisting.Location = New System.Drawing.Point(6, 81)
        Me.rdbExisting.Name = "rdbExisting"
        Me.rdbExisting.Size = New System.Drawing.Size(318, 36)
        Me.rdbExisting.TabIndex = 14
        Me.rdbExisting.TabStop = True
        Me.rdbExisting.Text = "Επεξεργασία Υφιστάμενου"
        Me.rdbExisting.UseVisualStyleBackColor = True
        '
        'rdbNewSupplier
        '
        Me.rdbNewSupplier.AutoSize = True
        Me.rdbNewSupplier.Location = New System.Drawing.Point(6, 39)
        Me.rdbNewSupplier.Name = "rdbNewSupplier"
        Me.rdbNewSupplier.Size = New System.Drawing.Size(219, 36)
        Me.rdbNewSupplier.TabIndex = 13
        Me.rdbNewSupplier.TabStop = True
        Me.rdbNewSupplier.Text = "Δημιουργία Νέου"
        Me.rdbNewSupplier.UseVisualStyleBackColor = True
        '
        'lstBoxName
        '
        Me.lstBoxName.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxName.FormattingEnabled = True
        Me.lstBoxName.HorizontalScrollbar = True
        Me.lstBoxName.ItemHeight = 32
        Me.lstBoxName.Location = New System.Drawing.Point(658, 21)
        Me.lstBoxName.Name = "lstBoxName"
        Me.lstBoxName.Size = New System.Drawing.Size(346, 612)
        Me.lstBoxName.TabIndex = 14
        '
        'txtBoxEmail
        '
        Me.txtBoxEmail.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxEmail.Location = New System.Drawing.Point(308, 345)
        Me.txtBoxEmail.Name = "txtBoxEmail"
        Me.txtBoxEmail.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxEmail.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 345)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 32)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Email"
        '
        'lblContactName
        '
        Me.lblContactName.AutoSize = True
        Me.lblContactName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.Location = New System.Drawing.Point(12, 207)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(240, 32)
        Me.lblContactName.TabIndex = 37
        Me.lblContactName.Text = "Όνομα Επικοινωνίας"
        '
        'txtBoxContactName
        '
        Me.txtBoxContactName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxContactName.Location = New System.Drawing.Point(308, 207)
        Me.txtBoxContactName.Name = "txtBoxContactName"
        Me.txtBoxContactName.Size = New System.Drawing.Size(322, 39)
        Me.txtBoxContactName.TabIndex = 2
        '
        'lblDay
        '
        Me.lblDay.AutoSize = True
        Me.lblDay.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDay.Location = New System.Drawing.Point(12, 391)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(86, 32)
        Me.lblDay.TabIndex = 39
        Me.lblDay.Text = "Ημέρα"
        '
        'ckbMon
        '
        Me.ckbMon.AutoSize = True
        Me.ckbMon.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbMon.Location = New System.Drawing.Point(308, 391)
        Me.ckbMon.Name = "ckbMon"
        Me.ckbMon.Size = New System.Drawing.Size(49, 36)
        Me.ckbMon.TabIndex = 6
        Me.ckbMon.Text = "Δ"
        Me.ckbMon.UseVisualStyleBackColor = True
        '
        'ckbTue
        '
        Me.ckbTue.AutoSize = True
        Me.ckbTue.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbTue.Location = New System.Drawing.Point(367, 390)
        Me.ckbTue.Name = "ckbTue"
        Me.ckbTue.Size = New System.Drawing.Size(47, 36)
        Me.ckbTue.TabIndex = 7
        Me.ckbTue.Text = "Τ"
        Me.ckbTue.UseVisualStyleBackColor = True
        '
        'ckbWed
        '
        Me.ckbWed.AutoSize = True
        Me.ckbWed.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbWed.Location = New System.Drawing.Point(424, 391)
        Me.ckbWed.Name = "ckbWed"
        Me.ckbWed.Size = New System.Drawing.Size(47, 36)
        Me.ckbWed.TabIndex = 8
        Me.ckbWed.Text = "Τ"
        Me.ckbWed.UseVisualStyleBackColor = True
        '
        'ckbThu
        '
        Me.ckbThu.AutoSize = True
        Me.ckbThu.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbThu.Location = New System.Drawing.Point(481, 391)
        Me.ckbThu.Name = "ckbThu"
        Me.ckbThu.Size = New System.Drawing.Size(51, 36)
        Me.ckbThu.TabIndex = 9
        Me.ckbThu.Text = "Π"
        Me.ckbThu.UseVisualStyleBackColor = True
        '
        'ckbFri
        '
        Me.ckbFri.AutoSize = True
        Me.ckbFri.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbFri.Location = New System.Drawing.Point(541, 390)
        Me.ckbFri.Name = "ckbFri"
        Me.ckbFri.Size = New System.Drawing.Size(51, 36)
        Me.ckbFri.TabIndex = 10
        Me.ckbFri.Text = "Π"
        Me.ckbFri.UseVisualStyleBackColor = True
        '
        'lstBoxUUID
        '
        Me.lstBoxUUID.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxUUID.FormattingEnabled = True
        Me.lstBoxUUID.ItemHeight = 32
        Me.lstBoxUUID.Location = New System.Drawing.Point(18, 643)
        Me.lstBoxUUID.Name = "lstBoxUUID"
        Me.lstBoxUUID.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxUUID.TabIndex = 45
        Me.lstBoxUUID.Visible = False
        '
        'lstBoxThu
        '
        Me.lstBoxThu.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxThu.FormattingEnabled = True
        Me.lstBoxThu.ItemHeight = 32
        Me.lstBoxThu.Location = New System.Drawing.Point(222, 643)
        Me.lstBoxThu.Name = "lstBoxThu"
        Me.lstBoxThu.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxThu.TabIndex = 46
        Me.lstBoxThu.Visible = False
        '
        'lstBoxWed
        '
        Me.lstBoxWed.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxWed.FormattingEnabled = True
        Me.lstBoxWed.ItemHeight = 32
        Me.lstBoxWed.Location = New System.Drawing.Point(171, 691)
        Me.lstBoxWed.Name = "lstBoxWed"
        Me.lstBoxWed.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxWed.TabIndex = 47
        Me.lstBoxWed.Visible = False
        '
        'lstBoxTue
        '
        Me.lstBoxTue.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxTue.FormattingEnabled = True
        Me.lstBoxTue.ItemHeight = 32
        Me.lstBoxTue.Location = New System.Drawing.Point(171, 643)
        Me.lstBoxTue.Name = "lstBoxTue"
        Me.lstBoxTue.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxTue.TabIndex = 48
        Me.lstBoxTue.Visible = False
        '
        'lstBoxMon
        '
        Me.lstBoxMon.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxMon.FormattingEnabled = True
        Me.lstBoxMon.ItemHeight = 32
        Me.lstBoxMon.Location = New System.Drawing.Point(120, 691)
        Me.lstBoxMon.Name = "lstBoxMon"
        Me.lstBoxMon.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxMon.TabIndex = 49
        Me.lstBoxMon.Visible = False
        '
        'lstBoxContactName
        '
        Me.lstBoxContactName.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxContactName.FormattingEnabled = True
        Me.lstBoxContactName.ItemHeight = 32
        Me.lstBoxContactName.Location = New System.Drawing.Point(120, 643)
        Me.lstBoxContactName.Name = "lstBoxContactName"
        Me.lstBoxContactName.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxContactName.TabIndex = 50
        Me.lstBoxContactName.Visible = False
        '
        'lstBoxEmail
        '
        Me.lstBoxEmail.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxEmail.FormattingEnabled = True
        Me.lstBoxEmail.ItemHeight = 32
        Me.lstBoxEmail.Location = New System.Drawing.Point(69, 691)
        Me.lstBoxEmail.Name = "lstBoxEmail"
        Me.lstBoxEmail.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxEmail.TabIndex = 51
        Me.lstBoxEmail.Visible = False
        '
        'lstBoxPhone2
        '
        Me.lstBoxPhone2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxPhone2.FormattingEnabled = True
        Me.lstBoxPhone2.ItemHeight = 32
        Me.lstBoxPhone2.Location = New System.Drawing.Point(69, 643)
        Me.lstBoxPhone2.Name = "lstBoxPhone2"
        Me.lstBoxPhone2.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxPhone2.TabIndex = 52
        Me.lstBoxPhone2.Visible = False
        '
        'lstBoxPhone1
        '
        Me.lstBoxPhone1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxPhone1.FormattingEnabled = True
        Me.lstBoxPhone1.ItemHeight = 32
        Me.lstBoxPhone1.Location = New System.Drawing.Point(18, 691)
        Me.lstBoxPhone1.Name = "lstBoxPhone1"
        Me.lstBoxPhone1.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxPhone1.TabIndex = 53
        Me.lstBoxPhone1.Visible = False
        '
        'lstBoxFri
        '
        Me.lstBoxFri.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxFri.FormattingEnabled = True
        Me.lstBoxFri.ItemHeight = 32
        Me.lstBoxFri.Location = New System.Drawing.Point(222, 691)
        Me.lstBoxFri.Name = "lstBoxFri"
        Me.lstBoxFri.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxFri.TabIndex = 54
        Me.lstBoxFri.Visible = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(789, 643)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(215, 75)
        Me.btnExit.TabIndex = 15
        Me.btnExit.Text = "Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Image = Global.POS.My.Resources.Resources.save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(567, 643)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(215, 75)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "     Αποθήκευση"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Image = Global.POS.My.Resources.Resources.undo
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(346, 643)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(215, 75)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "   Καθαρισμός Πεδίων"
        Me.btnClear.UseVisualStyleBackColor = False
        Me.btnClear.Visible = False
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(12, 435)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(133, 32)
        Me.lblNotes.TabIndex = 56
        Me.lblNotes.Text = "Σημειώσεις"
        '
        'txtBoxNotes
        '
        Me.txtBoxNotes.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxNotes.Location = New System.Drawing.Point(308, 432)
        Me.txtBoxNotes.Multiline = True
        Me.txtBoxNotes.Name = "txtBoxNotes"
        Me.txtBoxNotes.Size = New System.Drawing.Size(322, 201)
        Me.txtBoxNotes.TabIndex = 11
        '
        'lstBoxNotes
        '
        Me.lstBoxNotes.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxNotes.FormattingEnabled = True
        Me.lstBoxNotes.ItemHeight = 32
        Me.lstBoxNotes.Location = New System.Drawing.Point(18, 597)
        Me.lstBoxNotes.Name = "lstBoxNotes"
        Me.lstBoxNotes.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxNotes.TabIndex = 57
        Me.lstBoxNotes.Visible = False
        '
        'btnDeleteSupplier
        '
        Me.btnDeleteSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDeleteSupplier.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDeleteSupplier.Image = CType(resources.GetObject("btnDeleteSupplier.Image"), System.Drawing.Image)
        Me.btnDeleteSupplier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDeleteSupplier.Location = New System.Drawing.Point(346, 643)
        Me.btnDeleteSupplier.Name = "btnDeleteSupplier"
        Me.btnDeleteSupplier.Size = New System.Drawing.Size(215, 75)
        Me.btnDeleteSupplier.TabIndex = 58
        Me.btnDeleteSupplier.Text = "     Διαγραφη"
        Me.btnDeleteSupplier.UseVisualStyleBackColor = False
        '
        'lstBoxIsDefault
        '
        Me.lstBoxIsDefault.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxIsDefault.FormattingEnabled = True
        Me.lstBoxIsDefault.ItemHeight = 32
        Me.lstBoxIsDefault.Location = New System.Drawing.Point(78, 597)
        Me.lstBoxIsDefault.Name = "lstBoxIsDefault"
        Me.lstBoxIsDefault.Size = New System.Drawing.Size(45, 36)
        Me.lstBoxIsDefault.TabIndex = 59
        Me.lstBoxIsDefault.Visible = False
        '
        'frmSuppliers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.lstBoxIsDefault)
        Me.Controls.Add(Me.btnDeleteSupplier)
        Me.Controls.Add(Me.lstBoxNotes)
        Me.Controls.Add(Me.txtBoxNotes)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lstBoxFri)
        Me.Controls.Add(Me.lstBoxPhone1)
        Me.Controls.Add(Me.lstBoxPhone2)
        Me.Controls.Add(Me.lstBoxEmail)
        Me.Controls.Add(Me.lstBoxContactName)
        Me.Controls.Add(Me.lstBoxMon)
        Me.Controls.Add(Me.lstBoxTue)
        Me.Controls.Add(Me.lstBoxWed)
        Me.Controls.Add(Me.lstBoxThu)
        Me.Controls.Add(Me.lstBoxUUID)
        Me.Controls.Add(Me.ckbFri)
        Me.Controls.Add(Me.ckbThu)
        Me.Controls.Add(Me.ckbWed)
        Me.Controls.Add(Me.ckbTue)
        Me.Controls.Add(Me.ckbMon)
        Me.Controls.Add(Me.lblDay)
        Me.Controls.Add(Me.txtBoxContactName)
        Me.Controls.Add(Me.lblContactName)
        Me.Controls.Add(Me.txtBoxEmail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtBoxPhone2)
        Me.Controls.Add(Me.txtBoxPhone1)
        Me.Controls.Add(Me.txtBoxName)
        Me.Controls.Add(Me.lblPhone2)
        Me.Controls.Add(Me.lblPhone1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstBoxName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmSuppliers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Διαχείριση Προμηθευτών"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtBoxPhone2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxPhone1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxName As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone2 As System.Windows.Forms.Label
    Friend WithEvents lblPhone1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbExisting As System.Windows.Forms.RadioButton
    Friend WithEvents rdbNewSupplier As System.Windows.Forms.RadioButton
    Friend WithEvents lstBoxName As System.Windows.Forms.ListBox
    Friend WithEvents txtBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblContactName As System.Windows.Forms.Label
    Friend WithEvents txtBoxContactName As System.Windows.Forms.TextBox
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents ckbMon As System.Windows.Forms.CheckBox
    Friend WithEvents ckbTue As System.Windows.Forms.CheckBox
    Friend WithEvents ckbWed As System.Windows.Forms.CheckBox
    Friend WithEvents ckbThu As System.Windows.Forms.CheckBox
    Friend WithEvents ckbFri As System.Windows.Forms.CheckBox
    Friend WithEvents lstBoxUUID As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxThu As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxWed As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxTue As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxMon As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxContactName As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxEmail As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxPhone2 As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxPhone1 As System.Windows.Forms.ListBox
    Friend WithEvents lstBoxFri As System.Windows.Forms.ListBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents txtBoxNotes As System.Windows.Forms.TextBox
    Friend WithEvents lstBoxNotes As System.Windows.Forms.ListBox
    Friend WithEvents btnDeleteSupplier As System.Windows.Forms.Button
    Friend WithEvents lstBoxIsDefault As System.Windows.Forms.ListBox
End Class

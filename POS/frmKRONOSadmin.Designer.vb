<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKRONOSadmin
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
        Me.grpBoxKronos = New System.Windows.Forms.GroupBox
        Me.rdbKronosSales = New System.Windows.Forms.RadioButton
        Me.lblToDate = New System.Windows.Forms.Label
        Me.lblFromDate = New System.Windows.Forms.Label
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox
        Me.rdbBarcodes = New System.Windows.Forms.RadioButton
        Me.rdbGenerated = New System.Windows.Forms.RadioButton
        Me.rdbProcessed = New System.Windows.Forms.RadioButton
        Me.rdbJobs = New System.Windows.Forms.RadioButton
        Me.dgvReports = New System.Windows.Forms.DataGridView
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.cmdExit = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblDummy = New System.Windows.Forms.Label
        Me.btnPrint = New System.Windows.Forms.Button
        Me.grpBoxKronos.SuspendLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBoxKronos
        '
        Me.grpBoxKronos.Controls.Add(Me.btnPrint)
        Me.grpBoxKronos.Controls.Add(Me.rdbKronosSales)
        Me.grpBoxKronos.Controls.Add(Me.lblToDate)
        Me.grpBoxKronos.Controls.Add(Me.lblFromDate)
        Me.grpBoxKronos.Controls.Add(Me.dtpTo)
        Me.grpBoxKronos.Controls.Add(Me.dtpFrom)
        Me.grpBoxKronos.Controls.Add(Me.txtBoxBarcode)
        Me.grpBoxKronos.Controls.Add(Me.rdbBarcodes)
        Me.grpBoxKronos.Controls.Add(Me.rdbGenerated)
        Me.grpBoxKronos.Controls.Add(Me.rdbProcessed)
        Me.grpBoxKronos.Controls.Add(Me.rdbJobs)
        Me.grpBoxKronos.Font = New System.Drawing.Font(REPORT_FONT, 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxKronos.Location = New System.Drawing.Point(12, 12)
        Me.grpBoxKronos.Name = "grpBoxKronos"
        Me.grpBoxKronos.Size = New System.Drawing.Size(801, 173)
        Me.grpBoxKronos.TabIndex = 20
        Me.grpBoxKronos.TabStop = False
        Me.grpBoxKronos.Text = "Kronos Admin"
        '
        'rdbKronosSales
        '
        Me.rdbKronosSales.AutoSize = True
        Me.rdbKronosSales.Font = New System.Drawing.Font(REPORT_FONT, 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbKronosSales.Location = New System.Drawing.Point(82, 39)
        Me.rdbKronosSales.Name = "rdbKronosSales"
        Me.rdbKronosSales.Size = New System.Drawing.Size(100, 25)
        Me.rdbKronosSales.TabIndex = 30
        Me.rdbKronosSales.TabStop = True
        Me.rdbKronosSales.Text = "ΠΩΛΗΣΕΙΣ"
        Me.rdbKronosSales.UseVisualStyleBackColor = True
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font(REPORT_FONT, 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(6, 123)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(70, 30)
        Me.lblToDate.TabIndex = 29
        Me.lblToDate.Text = "Μέχρι"
        Me.lblToDate.Visible = False
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font(REPORT_FONT, 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(6, 84)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(52, 30)
        Me.lblFromDate.TabIndex = 28
        Me.lblFromDate.Text = FROM_DATE
        Me.lblFromDate.Visible = False
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font(REPORT_FONT, 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Location = New System.Drawing.Point(100, 123)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(389, 35)
        Me.dtpTo.TabIndex = 26
        Me.dtpTo.Visible = False
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font(REPORT_FONT, 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Location = New System.Drawing.Point(100, 82)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(389, 35)
        Me.dtpFrom.TabIndex = 27
        Me.dtpFrom.Visible = False
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBarcode.Location = New System.Drawing.Point(535, 34)
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(236, 33)
        Me.txtBoxBarcode.TabIndex = 5
        Me.txtBoxBarcode.Visible = False
        '
        'rdbBarcodes
        '
        Me.rdbBarcodes.AutoSize = True
        Me.rdbBarcodes.Font = New System.Drawing.Font(REPORT_FONT, 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbBarcodes.Location = New System.Drawing.Point(422, 39)
        Me.rdbBarcodes.Name = "rdbBarcodes"
        Me.rdbBarcodes.Size = New System.Drawing.Size(107, 25)
        Me.rdbBarcodes.TabIndex = 4
        Me.rdbBarcodes.TabStop = True
        Me.rdbBarcodes.Text = "BARCODES"
        Me.rdbBarcodes.UseVisualStyleBackColor = True
        '
        'rdbGenerated
        '
        Me.rdbGenerated.AutoSize = True
        Me.rdbGenerated.Font = New System.Drawing.Font(REPORT_FONT, 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbGenerated.Location = New System.Drawing.Point(302, 39)
        Me.rdbGenerated.Name = "rdbGenerated"
        Me.rdbGenerated.Size = New System.Drawing.Size(114, 25)
        Me.rdbGenerated.TabIndex = 3
        Me.rdbGenerated.TabStop = True
        Me.rdbGenerated.Text = "GENERATED"
        Me.rdbGenerated.UseVisualStyleBackColor = True
        '
        'rdbProcessed
        '
        Me.rdbProcessed.AutoSize = True
        Me.rdbProcessed.Font = New System.Drawing.Font(REPORT_FONT, 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbProcessed.Location = New System.Drawing.Point(182, 39)
        Me.rdbProcessed.Name = "rdbProcessed"
        Me.rdbProcessed.Size = New System.Drawing.Size(114, 25)
        Me.rdbProcessed.TabIndex = 2
        Me.rdbProcessed.TabStop = True
        Me.rdbProcessed.Text = "PROCESSED"
        Me.rdbProcessed.UseVisualStyleBackColor = True
        '
        'rdbJobs
        '
        Me.rdbJobs.AutoSize = True
        Me.rdbJobs.Font = New System.Drawing.Font(REPORT_FONT, 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbJobs.Location = New System.Drawing.Point(12, 39)
        Me.rdbJobs.Name = "rdbJobs"
        Me.rdbJobs.Size = New System.Drawing.Size(64, 25)
        Me.rdbJobs.TabIndex = 1
        Me.rdbJobs.TabStop = True
        Me.rdbJobs.Text = "JOBS"
        Me.rdbJobs.UseVisualStyleBackColor = True
        '
        'dgvReports
        '
        Me.dgvReports.AllowUserToAddRows = False
        Me.dgvReports.AllowUserToDeleteRows = False
        Me.dgvReports.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReports.Location = New System.Drawing.Point(12, 191)
        Me.dgvReports.Name = "dgvReports"
        Me.dgvReports.ReadOnly = True
        Me.dgvReports.Size = New System.Drawing.Size(992, 536)
        Me.dgvReports.TabIndex = 21
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font(REPORT_FONT, 16.0!)
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(819, 15)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(183, 75)
        Me.cmdExit.TabIndex = 36
        Me.cmdExit.Text = "   Έξοδος"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSearch.Font = New System.Drawing.Font(REPORT_FONT, 16.0!)
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSearch.Image = Global.POS.My.Resources.Resources.search
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(819, 96)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(183, 75)
        Me.btnSearch.TabIndex = 37
        Me.btnSearch.Text = "       Αναζήτηση"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDummy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDummy.Location = New System.Drawing.Point(12, 139)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(0, 32)
        Me.lblDummy.TabIndex = 38
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font(REPORT_FONT, 16.0!)
        Me.btnPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrint.Image = Global.POS.My.Resources.Resources.printer
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(495, 84)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(183, 75)
        Me.btnPrint.TabIndex = 39
        Me.btnPrint.Text = "       Εκτύπωση"
        Me.btnPrint.UseVisualStyleBackColor = False
        Me.btnPrint.Visible = False
        '
        'frmKRONOSadmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.dgvReports)
        Me.Controls.Add(Me.grpBoxKronos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1024, 768)
        Me.Name = "frmKRONOSadmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Αναφορές"
        Me.grpBoxKronos.ResumeLayout(False)
        Me.grpBoxKronos.PerformLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpBoxKronos As System.Windows.Forms.GroupBox
    Friend WithEvents rdbJobs As System.Windows.Forms.RadioButton
    Friend WithEvents dgvReports As System.Windows.Forms.DataGridView
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rdbBarcodes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbGenerated As System.Windows.Forms.RadioButton
    Friend WithEvents rdbProcessed As System.Windows.Forms.RadioButton
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents rdbKronosSales As System.Windows.Forms.RadioButton
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class

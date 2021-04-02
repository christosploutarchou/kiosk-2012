<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOSEdit
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
        Me.dgvReceipt = New System.Windows.Forms.DataGridView
        Me.productSerno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.serno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.unitprice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.amount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.vat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.isKronos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.itemCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.issueNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.deliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btn7 = New System.Windows.Forms.Button
        Me.btn0 = New System.Windows.Forms.Button
        Me.btn3 = New System.Windows.Forms.Button
        Me.btn2 = New System.Windows.Forms.Button
        Me.number1 = New System.Windows.Forms.Button
        Me.btn6 = New System.Windows.Forms.Button
        Me.btn5 = New System.Windows.Forms.Button
        Me.btn4 = New System.Windows.Forms.Button
        Me.btn9 = New System.Windows.Forms.Button
        Me.btn8 = New System.Windows.Forms.Button
        Me.txtBoxManualAmt = New System.Windows.Forms.TextBox
        Me.btnDiscount = New System.Windows.Forms.Button
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox
        Me.lblBarcode = New System.Windows.Forms.Label
        Me.lblQuantity = New System.Windows.Forms.Label
        Me.txtBoxQuantity = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancelPayment = New System.Windows.Forms.Button
        Me.btnCancelDiscount = New System.Windows.Forms.Button
        Me.lblTotal = New System.Windows.Forms.Label
        Me.txtBoxTotalWithDiscount = New System.Windows.Forms.TextBox
        Me.txtBoxPaymentAmt = New System.Windows.Forms.TextBox
        Me.txtBoxReturnAmt = New System.Windows.Forms.TextBox
        Me.txtBoxDiscount = New System.Windows.Forms.TextBox
        Me.txtBoxTotalAmt = New System.Windows.Forms.TextBox
        Me.lblReturn = New System.Windows.Forms.Label
        Me.lblPayment = New System.Windows.Forms.Label
        Me.lblDiscount = New System.Windows.Forms.Label
        Me.lblTotalAmt = New System.Windows.Forms.Label
        Me.btnDot = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Button2 = New System.Windows.Forms.Button
        Me.chkBoxReturnProduct = New System.Windows.Forms.CheckBox
        Me.btnPayments = New System.Windows.Forms.Button
        Me.btnHold = New System.Windows.Forms.Button
        Me.btnKronosSearch = New System.Windows.Forms.Button
        Me.btnKronos = New System.Windows.Forms.Button
        Me.btnClearBarcode = New System.Windows.Forms.Button
        Me.btnCash = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnPayment = New System.Windows.Forms.Button
        Me.btnVisa = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnBackspace = New System.Windows.Forms.Button
        Me.btnRemoveQuantity = New System.Windows.Forms.Button
        Me.btnAddQuantity = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.btnReceipts = New System.Windows.Forms.Button
        Me.btnPos3 = New System.Windows.Forms.Button
        Me.btnPos2 = New System.Windows.Forms.Button
        Me.btnPos7 = New System.Windows.Forms.Button
        Me.btnPos12 = New System.Windows.Forms.Button
        Me.btnPos17 = New System.Windows.Forms.Button
        Me.btnPos16 = New System.Windows.Forms.Button
        Me.btnPos11 = New System.Windows.Forms.Button
        Me.btnPos6 = New System.Windows.Forms.Button
        Me.btnPos1 = New System.Windows.Forms.Button
        Me.btnPos15 = New System.Windows.Forms.Button
        Me.btnPos10 = New System.Windows.Forms.Button
        Me.btnPos5 = New System.Windows.Forms.Button
        Me.btnPos14 = New System.Windows.Forms.Button
        Me.btnPos9 = New System.Windows.Forms.Button
        Me.btnPos4 = New System.Windows.Forms.Button
        Me.btnPos8 = New System.Windows.Forms.Button
        Me.btnPos13 = New System.Windows.Forms.Button
        Me.btnPos18 = New System.Windows.Forms.Button
        Me.btnPos19 = New System.Windows.Forms.Button
        Me.btnPos20 = New System.Windows.Forms.Button
        Me.btnPos21 = New System.Windows.Forms.Button
        Me.btn5percent = New System.Windows.Forms.Button
        Me.btn19percent = New System.Windows.Forms.Button
        Me.btnPos22 = New System.Windows.Forms.Button
        Me.btnPos23 = New System.Windows.Forms.Button
        CType(Me.dgvReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvReceipt
        '
        Me.dgvReceipt.AllowUserToAddRows = False
        Me.dgvReceipt.AllowUserToDeleteRows = False
        Me.dgvReceipt.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReceipt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.productSerno, Me.serno, Me.description, Me.quantity, Me.unitprice, Me.amount, Me.vat, Me.isKronos, Me.itemCode, Me.issueNumber, Me.deliveryDate})
        Me.dgvReceipt.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvReceipt.Location = New System.Drawing.Point(0, 47)
        Me.dgvReceipt.Name = "dgvReceipt"
        Me.dgvReceipt.ReadOnly = True
        Me.dgvReceipt.Size = New System.Drawing.Size(709, 300)
        Me.dgvReceipt.TabIndex = 31
        '
        'productSerno
        '
        Me.productSerno.HeaderText = "serno"
        Me.productSerno.Name = "productSerno"
        Me.productSerno.ReadOnly = True
        Me.productSerno.Visible = False
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
        Me.description.Width = 235
        '
        'quantity
        '
        Me.quantity.HeaderText = "Ποσ."
        Me.quantity.Name = "quantity"
        Me.quantity.ReadOnly = True
        '
        'unitprice
        '
        Me.unitprice.HeaderText = "Τιμή Μονάδας (€)"
        Me.unitprice.Name = "unitprice"
        Me.unitprice.ReadOnly = True
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
        Me.vat.Width = 80
        '
        'isKronos
        '
        Me.isKronos.HeaderText = "isKronos"
        Me.isKronos.Name = "isKronos"
        Me.isKronos.ReadOnly = True
        Me.isKronos.Visible = False
        '
        'itemCode
        '
        Me.itemCode.HeaderText = "itemCode"
        Me.itemCode.Name = "itemCode"
        Me.itemCode.ReadOnly = True
        Me.itemCode.Visible = False
        '
        'issueNumber
        '
        Me.issueNumber.HeaderText = "issueNumber"
        Me.issueNumber.Name = "issueNumber"
        Me.issueNumber.ReadOnly = True
        Me.issueNumber.Visible = False
        '
        'deliveryDate
        '
        Me.deliveryDate.HeaderText = "deliveryDate"
        Me.deliveryDate.Name = "deliveryDate"
        Me.deliveryDate.ReadOnly = True
        Me.deliveryDate.Visible = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn7.Enabled = False
        Me.btn7.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn7.Location = New System.Drawing.Point(601, 407)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(60, 52)
        Me.btn7.TabIndex = 42
        Me.btn7.Text = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn0.Enabled = False
        Me.btn0.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn0.Location = New System.Drawing.Point(601, 581)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(126, 52)
        Me.btn0.TabIndex = 45
        Me.btn0.Text = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn3.Enabled = False
        Me.btn3.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn3.Location = New System.Drawing.Point(733, 522)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(60, 52)
        Me.btn3.TabIndex = 46
        Me.btn3.Text = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn2.Enabled = False
        Me.btn2.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn2.Location = New System.Drawing.Point(667, 522)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(60, 52)
        Me.btn2.TabIndex = 47
        Me.btn2.Text = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'number1
        '
        Me.number1.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.number1.Enabled = False
        Me.number1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.number1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.number1.Location = New System.Drawing.Point(601, 523)
        Me.number1.Name = "number1"
        Me.number1.Size = New System.Drawing.Size(60, 52)
        Me.number1.TabIndex = 48
        Me.number1.Text = "1"
        Me.number1.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn6.Enabled = False
        Me.btn6.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn6.Location = New System.Drawing.Point(733, 465)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(60, 52)
        Me.btn6.TabIndex = 49
        Me.btn6.Text = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn5.Enabled = False
        Me.btn5.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn5.Location = New System.Drawing.Point(667, 464)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(60, 52)
        Me.btn5.TabIndex = 50
        Me.btn5.Text = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn4.Enabled = False
        Me.btn4.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn4.Location = New System.Drawing.Point(601, 465)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(60, 52)
        Me.btn4.TabIndex = 51
        Me.btn4.Text = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn9.Enabled = False
        Me.btn9.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn9.Location = New System.Drawing.Point(734, 407)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(60, 52)
        Me.btn9.TabIndex = 52
        Me.btn9.Text = "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn8.Enabled = False
        Me.btn8.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btn8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn8.Location = New System.Drawing.Point(667, 407)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(60, 52)
        Me.btn8.TabIndex = 53
        Me.btn8.Text = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'txtBoxManualAmt
        '
        Me.txtBoxManualAmt.BackColor = System.Drawing.Color.White
        Me.txtBoxManualAmt.Enabled = False
        Me.txtBoxManualAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxManualAmt.Location = New System.Drawing.Point(601, 368)
        Me.txtBoxManualAmt.Name = "txtBoxManualAmt"
        Me.txtBoxManualAmt.ReadOnly = True
        Me.txtBoxManualAmt.Size = New System.Drawing.Size(193, 35)
        Me.txtBoxManualAmt.TabIndex = 54
        '
        'btnDiscount
        '
        Me.btnDiscount.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDiscount.Enabled = False
        Me.btnDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDiscount.Location = New System.Drawing.Point(896, 468)
        Me.btnDiscount.Name = "btnDiscount"
        Me.btnDiscount.Size = New System.Drawing.Size(99, 52)
        Me.btnDiscount.TabIndex = 57
        Me.btnDiscount.Text = "Disc."
        Me.btnDiscount.UseVisualStyleBackColor = False
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Enabled = False
        Me.txtBoxBarcode.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxBarcode.Location = New System.Drawing.Point(99, 12)
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(236, 33)
        Me.txtBoxBarcode.TabIndex = 0
        '
        'lblBarcode
        '
        Me.lblBarcode.AutoSize = True
        Me.lblBarcode.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBarcode.Location = New System.Drawing.Point(5, 13)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(88, 30)
        Me.lblBarcode.TabIndex = 63
        Me.lblBarcode.Text = "Barcode"
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantity.Location = New System.Drawing.Point(435, 14)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(113, 25)
        Me.lblQuantity.TabIndex = 64
        Me.lblQuantity.Text = "Ποσότητα"
        '
        'txtBoxQuantity
        '
        Me.txtBoxQuantity.Enabled = False
        Me.txtBoxQuantity.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxQuantity.Location = New System.Drawing.Point(552, 10)
        Me.txtBoxQuantity.Name = "txtBoxQuantity"
        Me.txtBoxQuantity.ReadOnly = True
        Me.txtBoxQuantity.Size = New System.Drawing.Size(64, 33)
        Me.txtBoxQuantity.TabIndex = 65
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnCancelPayment)
        Me.Panel1.Controls.Add(Me.btnCancelDiscount)
        Me.Panel1.Controls.Add(Me.lblTotal)
        Me.Panel1.Controls.Add(Me.txtBoxTotalWithDiscount)
        Me.Panel1.Controls.Add(Me.txtBoxPaymentAmt)
        Me.Panel1.Controls.Add(Me.txtBoxReturnAmt)
        Me.Panel1.Controls.Add(Me.txtBoxDiscount)
        Me.Panel1.Controls.Add(Me.txtBoxTotalAmt)
        Me.Panel1.Controls.Add(Me.lblReturn)
        Me.Panel1.Controls.Add(Me.lblPayment)
        Me.Panel1.Controls.Add(Me.lblDiscount)
        Me.Panel1.Controls.Add(Me.lblTotalAmt)
        Me.Panel1.Enabled = False
        Me.Panel1.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(713, 77)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(291, 193)
        Me.Panel1.TabIndex = 68
        '
        'btnCancelPayment
        '
        Me.btnCancelPayment.BackgroundImage = Global.POS.My.Resources.Resources.cancel
        Me.btnCancelPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelPayment.Enabled = False
        Me.btnCancelPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancelPayment.Location = New System.Drawing.Point(236, 122)
        Me.btnCancelPayment.Name = "btnCancelPayment"
        Me.btnCancelPayment.Size = New System.Drawing.Size(52, 34)
        Me.btnCancelPayment.TabIndex = 83
        Me.btnCancelPayment.UseVisualStyleBackColor = True
        '
        'btnCancelDiscount
        '
        Me.btnCancelDiscount.BackgroundImage = Global.POS.My.Resources.Resources.cancel
        Me.btnCancelDiscount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelDiscount.Enabled = False
        Me.btnCancelDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelDiscount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancelDiscount.Location = New System.Drawing.Point(236, 78)
        Me.btnCancelDiscount.Name = "btnCancelDiscount"
        Me.btnCancelDiscount.Size = New System.Drawing.Size(52, 34)
        Me.btnCancelDiscount.TabIndex = 82
        Me.btnCancelDiscount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancelDiscount.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblTotal.Location = New System.Drawing.Point(8, 7)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(62, 30)
        Me.lblTotal.TabIndex = 78
        Me.lblTotal.Text = "Total"
        '
        'txtBoxTotalWithDiscount
        '
        Me.txtBoxTotalWithDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxTotalWithDiscount.Enabled = False
        Me.txtBoxTotalWithDiscount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTotalWithDiscount.Location = New System.Drawing.Point(134, 3)
        Me.txtBoxTotalWithDiscount.Name = "txtBoxTotalWithDiscount"
        Me.txtBoxTotalWithDiscount.ReadOnly = True
        Me.txtBoxTotalWithDiscount.Size = New System.Drawing.Size(96, 35)
        Me.txtBoxTotalWithDiscount.TabIndex = 77
        '
        'txtBoxPaymentAmt
        '
        Me.txtBoxPaymentAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxPaymentAmt.Enabled = False
        Me.txtBoxPaymentAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtBoxPaymentAmt.Location = New System.Drawing.Point(134, 116)
        Me.txtBoxPaymentAmt.Name = "txtBoxPaymentAmt"
        Me.txtBoxPaymentAmt.ReadOnly = True
        Me.txtBoxPaymentAmt.Size = New System.Drawing.Size(96, 35)
        Me.txtBoxPaymentAmt.TabIndex = 76
        '
        'txtBoxReturnAmt
        '
        Me.txtBoxReturnAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxReturnAmt.Enabled = False
        Me.txtBoxReturnAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtBoxReturnAmt.Location = New System.Drawing.Point(134, 153)
        Me.txtBoxReturnAmt.Name = "txtBoxReturnAmt"
        Me.txtBoxReturnAmt.ReadOnly = True
        Me.txtBoxReturnAmt.Size = New System.Drawing.Size(96, 35)
        Me.txtBoxReturnAmt.TabIndex = 75
        '
        'txtBoxDiscount
        '
        Me.txtBoxDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxDiscount.Enabled = False
        Me.txtBoxDiscount.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtBoxDiscount.Location = New System.Drawing.Point(134, 79)
        Me.txtBoxDiscount.Name = "txtBoxDiscount"
        Me.txtBoxDiscount.ReadOnly = True
        Me.txtBoxDiscount.Size = New System.Drawing.Size(96, 35)
        Me.txtBoxDiscount.TabIndex = 74
        '
        'txtBoxTotalAmt
        '
        Me.txtBoxTotalAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtBoxTotalAmt.Enabled = False
        Me.txtBoxTotalAmt.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.txtBoxTotalAmt.Location = New System.Drawing.Point(134, 42)
        Me.txtBoxTotalAmt.Name = "txtBoxTotalAmt"
        Me.txtBoxTotalAmt.ReadOnly = True
        Me.txtBoxTotalAmt.Size = New System.Drawing.Size(96, 35)
        Me.txtBoxTotalAmt.TabIndex = 69
        '
        'lblReturn
        '
        Me.lblReturn.AutoSize = True
        Me.lblReturn.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblReturn.Location = New System.Drawing.Point(8, 159)
        Me.lblReturn.Name = "lblReturn"
        Me.lblReturn.Size = New System.Drawing.Size(120, 30)
        Me.lblReturn.TabIndex = 73
        Me.lblReturn.Text = "Επιστροφή"
        '
        'lblPayment
        '
        Me.lblPayment.AutoSize = True
        Me.lblPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayment.ForeColor = System.Drawing.Color.Green
        Me.lblPayment.Location = New System.Drawing.Point(8, 122)
        Me.lblPayment.Name = "lblPayment"
        Me.lblPayment.Size = New System.Drawing.Size(106, 30)
        Me.lblPayment.TabIndex = 72
        Me.lblPayment.Text = "Πληρωμή"
        '
        'lblDiscount
        '
        Me.lblDiscount.AutoSize = True
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblDiscount.Location = New System.Drawing.Point(8, 85)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(109, 30)
        Me.lblDiscount.TabIndex = 70
        Me.lblDiscount.Text = "Έκπτωση "
        '
        'lblTotalAmt
        '
        Me.lblTotalAmt.AutoSize = True
        Me.lblTotalAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmt.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblTotalAmt.Location = New System.Drawing.Point(8, 48)
        Me.lblTotalAmt.Name = "lblTotalAmt"
        Me.lblTotalAmt.Size = New System.Drawing.Size(70, 30)
        Me.lblTotalAmt.TabIndex = 69
        Me.lblTotalAmt.Text = "Ολικό"
        '
        'btnDot
        '
        Me.btnDot.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDot.Enabled = False
        Me.btnDot.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.btnDot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDot.Location = New System.Drawing.Point(733, 581)
        Me.btnDot.Name = "btnDot"
        Me.btnDot.Size = New System.Drawing.Size(60, 52)
        Me.btnDot.TabIndex = 73
        Me.btnDot.Text = "."
        Me.btnDot.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(1037, 115)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(106, 93)
        Me.Button2.TabIndex = 78
        Me.Button2.Text = "Πληρωμή"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'chkBoxReturnProduct
        '
        Me.chkBoxReturnProduct.AutoSize = True
        Me.chkBoxReturnProduct.Enabled = False
        Me.chkBoxReturnProduct.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxReturnProduct.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkBoxReturnProduct.Location = New System.Drawing.Point(720, 294)
        Me.chkBoxReturnProduct.Name = "chkBoxReturnProduct"
        Me.chkBoxReturnProduct.Size = New System.Drawing.Size(260, 34)
        Me.chkBoxReturnProduct.TabIndex = 79
        Me.chkBoxReturnProduct.Text = "Επιστροφή Προϊόντος"
        Me.chkBoxReturnProduct.UseVisualStyleBackColor = True
        '
        'btnPayments
        '
        Me.btnPayments.Enabled = False
        Me.btnPayments.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnPayments.Location = New System.Drawing.Point(801, 368)
        Me.btnPayments.Name = "btnPayments"
        Me.btnPayments.Size = New System.Drawing.Size(193, 36)
        Me.btnPayments.TabIndex = 81
        Me.btnPayments.Text = "Πληρωμές"
        Me.btnPayments.UseVisualStyleBackColor = True
        '
        'btnHold
        '
        Me.btnHold.BackColor = System.Drawing.Color.LightGray
        Me.btnHold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHold.Enabled = False
        Me.btnHold.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnHold.Image = Global.POS.My.Resources.Resources.hold1
        Me.btnHold.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHold.Location = New System.Drawing.Point(601, 651)
        Me.btnHold.Name = "btnHold"
        Me.btnHold.Size = New System.Drawing.Size(139, 52)
        Me.btnHold.TabIndex = 85
        Me.btnHold.Text = "Κράτημα"
        Me.btnHold.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHold.UseVisualStyleBackColor = False
        '
        'btnKronosSearch
        '
        Me.btnKronosSearch.BackgroundImage = Global.POS.My.Resources.Resources.search
        Me.btnKronosSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnKronosSearch.Enabled = False
        Me.btnKronosSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnKronosSearch.Location = New System.Drawing.Point(385, 10)
        Me.btnKronosSearch.Name = "btnKronosSearch"
        Me.btnKronosSearch.Size = New System.Drawing.Size(41, 34)
        Me.btnKronosSearch.TabIndex = 84
        Me.btnKronosSearch.UseVisualStyleBackColor = True
        '
        'btnKronos
        '
        Me.btnKronos.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnKronos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnKronos.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnKronos.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnKronos.Location = New System.Drawing.Point(833, 47)
        Me.btnKronos.Name = "btnKronos"
        Me.btnKronos.Size = New System.Drawing.Size(29, 21)
        Me.btnKronos.TabIndex = 83
        Me.btnKronos.Text = "KRONOS"
        Me.btnKronos.UseVisualStyleBackColor = False
        Me.btnKronos.Visible = False
        '
        'btnClearBarcode
        '
        Me.btnClearBarcode.BackgroundImage = Global.POS.My.Resources.Resources.undo
        Me.btnClearBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearBarcode.Enabled = False
        Me.btnClearBarcode.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnClearBarcode.Location = New System.Drawing.Point(341, 11)
        Me.btnClearBarcode.Name = "btnClearBarcode"
        Me.btnClearBarcode.Size = New System.Drawing.Size(38, 34)
        Me.btnClearBarcode.TabIndex = 80
        Me.btnClearBarcode.UseVisualStyleBackColor = True
        '
        'btnCash
        '
        Me.btnCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCash.BackgroundImage = Global.POS.My.Resources.Resources.currency_blue_euro
        Me.btnCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCash.Enabled = False
        Me.btnCash.Font = New System.Drawing.Font("Segoe UI", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCash.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnCash.Location = New System.Drawing.Point(801, 584)
        Me.btnCash.Name = "btnCash"
        Me.btnCash.Size = New System.Drawing.Size(193, 49)
        Me.btnCash.TabIndex = 76
        Me.btnCash.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnClear.Enabled = False
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Image = Global.POS.My.Resources.Resources.undo
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClear.Location = New System.Drawing.Point(896, 410)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(99, 47)
        Me.btnClear.TabIndex = 74
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnPayment
        '
        Me.btnPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPayment.BackgroundImage = Global.POS.My.Resources.Resources.payment
        Me.btnPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnPayment.Enabled = False
        Me.btnPayment.Font = New System.Drawing.Font("Segoe UI", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPayment.Location = New System.Drawing.Point(801, 526)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(193, 52)
        Me.btnPayment.TabIndex = 72
        Me.btnPayment.UseVisualStyleBackColor = False
        '
        'btnVisa
        '
        Me.btnVisa.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnVisa.BackgroundImage = Global.POS.My.Resources.Resources.credit_card_visa
        Me.btnVisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVisa.Enabled = False
        Me.btnVisa.Font = New System.Drawing.Font("Segoe UI", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVisa.Location = New System.Drawing.Point(801, 467)
        Me.btnVisa.Name = "btnVisa"
        Me.btnVisa.Size = New System.Drawing.Size(89, 52)
        Me.btnVisa.TabIndex = 58
        Me.btnVisa.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExit.Enabled = False
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.Image = Global.POS.My.Resources.Resources.Logout
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(868, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(136, 60)
        Me.btnExit.TabIndex = 70
        Me.btnExit.Text = "        Έξοδος"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnBackspace
        '
        Me.btnBackspace.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnBackspace.Enabled = False
        Me.btnBackspace.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackspace.Image = Global.POS.My.Resources.Resources.back
        Me.btnBackspace.Location = New System.Drawing.Point(801, 410)
        Me.btnBackspace.Name = "btnBackspace"
        Me.btnBackspace.Size = New System.Drawing.Size(89, 47)
        Me.btnBackspace.TabIndex = 62
        Me.btnBackspace.UseVisualStyleBackColor = False
        '
        'btnRemoveQuantity
        '
        Me.btnRemoveQuantity.BackgroundImage = Global.POS.My.Resources.Resources.minus
        Me.btnRemoveQuantity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveQuantity.Enabled = False
        Me.btnRemoveQuantity.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnRemoveQuantity.Location = New System.Drawing.Point(668, 9)
        Me.btnRemoveQuantity.Name = "btnRemoveQuantity"
        Me.btnRemoveQuantity.Size = New System.Drawing.Size(41, 34)
        Me.btnRemoveQuantity.TabIndex = 67
        Me.btnRemoveQuantity.UseVisualStyleBackColor = True
        '
        'btnAddQuantity
        '
        Me.btnAddQuantity.BackgroundImage = Global.POS.My.Resources.Resources.add
        Me.btnAddQuantity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddQuantity.Enabled = False
        Me.btnAddQuantity.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnAddQuantity.Location = New System.Drawing.Point(622, 10)
        Me.btnAddQuantity.Name = "btnAddQuantity"
        Me.btnAddQuantity.Size = New System.Drawing.Size(41, 34)
        Me.btnAddQuantity.TabIndex = 66
        Me.btnAddQuantity.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'btnReceipts
        '
        Me.btnReceipts.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnReceipts.Enabled = False
        Me.btnReceipts.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReceipts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReceipts.Image = Global.POS.My.Resources.Resources.receipt
        Me.btnReceipts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReceipts.Location = New System.Drawing.Point(726, 8)
        Me.btnReceipts.Name = "btnReceipts"
        Me.btnReceipts.Size = New System.Drawing.Size(136, 60)
        Me.btnReceipts.TabIndex = 87
        Me.btnReceipts.Text = "Αποδειξεις"
        Me.btnReceipts.UseVisualStyleBackColor = False
        '
        'btnPos3
        '
        Me.btnPos3.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos3.Location = New System.Drawing.Point(457, 356)
        Me.btnPos3.Name = "btnPos3"
        Me.btnPos3.Size = New System.Drawing.Size(107, 67)
        Me.btnPos3.TabIndex = 88
        Me.btnPos3.UseVisualStyleBackColor = False
        '
        'btnPos2
        '
        Me.btnPos2.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos2.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos2.Location = New System.Drawing.Point(344, 356)
        Me.btnPos2.Name = "btnPos2"
        Me.btnPos2.Size = New System.Drawing.Size(107, 67)
        Me.btnPos2.TabIndex = 89
        Me.btnPos2.UseVisualStyleBackColor = False
        '
        'btnPos7
        '
        Me.btnPos7.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos7.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos7.Location = New System.Drawing.Point(344, 429)
        Me.btnPos7.Name = "btnPos7"
        Me.btnPos7.Size = New System.Drawing.Size(107, 67)
        Me.btnPos7.TabIndex = 90
        Me.btnPos7.UseVisualStyleBackColor = False
        '
        'btnPos12
        '
        Me.btnPos12.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos12.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos12.Location = New System.Drawing.Point(344, 502)
        Me.btnPos12.Name = "btnPos12"
        Me.btnPos12.Size = New System.Drawing.Size(107, 67)
        Me.btnPos12.TabIndex = 91
        Me.btnPos12.UseVisualStyleBackColor = False
        '
        'btnPos17
        '
        Me.btnPos17.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos17.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos17.Location = New System.Drawing.Point(344, 575)
        Me.btnPos17.Name = "btnPos17"
        Me.btnPos17.Size = New System.Drawing.Size(107, 67)
        Me.btnPos17.TabIndex = 92
        Me.btnPos17.UseVisualStyleBackColor = False
        '
        'btnPos16
        '
        Me.btnPos16.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos16.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos16.Location = New System.Drawing.Point(231, 575)
        Me.btnPos16.Name = "btnPos16"
        Me.btnPos16.Size = New System.Drawing.Size(107, 67)
        Me.btnPos16.TabIndex = 93
        Me.btnPos16.UseVisualStyleBackColor = False
        '
        'btnPos11
        '
        Me.btnPos11.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos11.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos11.Location = New System.Drawing.Point(231, 502)
        Me.btnPos11.Name = "btnPos11"
        Me.btnPos11.Size = New System.Drawing.Size(107, 67)
        Me.btnPos11.TabIndex = 94
        Me.btnPos11.UseVisualStyleBackColor = False
        '
        'btnPos6
        '
        Me.btnPos6.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos6.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos6.Location = New System.Drawing.Point(231, 429)
        Me.btnPos6.Name = "btnPos6"
        Me.btnPos6.Size = New System.Drawing.Size(107, 67)
        Me.btnPos6.TabIndex = 95
        Me.btnPos6.UseVisualStyleBackColor = False
        '
        'btnPos1
        '
        Me.btnPos1.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos1.Location = New System.Drawing.Point(231, 356)
        Me.btnPos1.Name = "btnPos1"
        Me.btnPos1.Size = New System.Drawing.Size(107, 67)
        Me.btnPos1.TabIndex = 96
        Me.btnPos1.UseVisualStyleBackColor = False
        '
        'btnPos15
        '
        Me.btnPos15.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos15.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos15.Location = New System.Drawing.Point(118, 575)
        Me.btnPos15.Name = "btnPos15"
        Me.btnPos15.Size = New System.Drawing.Size(107, 67)
        Me.btnPos15.TabIndex = 97
        Me.btnPos15.UseVisualStyleBackColor = False
        '
        'btnPos10
        '
        Me.btnPos10.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos10.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos10.Location = New System.Drawing.Point(118, 502)
        Me.btnPos10.Name = "btnPos10"
        Me.btnPos10.Size = New System.Drawing.Size(107, 67)
        Me.btnPos10.TabIndex = 98
        Me.btnPos10.UseVisualStyleBackColor = False
        '
        'btnPos5
        '
        Me.btnPos5.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos5.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos5.Location = New System.Drawing.Point(118, 429)
        Me.btnPos5.Name = "btnPos5"
        Me.btnPos5.Size = New System.Drawing.Size(107, 67)
        Me.btnPos5.TabIndex = 99
        Me.btnPos5.UseVisualStyleBackColor = False
        '
        'btnPos14
        '
        Me.btnPos14.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos14.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos14.Location = New System.Drawing.Point(5, 575)
        Me.btnPos14.Name = "btnPos14"
        Me.btnPos14.Size = New System.Drawing.Size(107, 67)
        Me.btnPos14.TabIndex = 101
        Me.btnPos14.UseVisualStyleBackColor = False
        '
        'btnPos9
        '
        Me.btnPos9.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos9.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos9.Location = New System.Drawing.Point(5, 502)
        Me.btnPos9.Name = "btnPos9"
        Me.btnPos9.Size = New System.Drawing.Size(107, 67)
        Me.btnPos9.TabIndex = 102
        Me.btnPos9.UseVisualStyleBackColor = False
        '
        'btnPos4
        '
        Me.btnPos4.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos4.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos4.Location = New System.Drawing.Point(5, 429)
        Me.btnPos4.Name = "btnPos4"
        Me.btnPos4.Size = New System.Drawing.Size(107, 67)
        Me.btnPos4.TabIndex = 103
        Me.btnPos4.UseVisualStyleBackColor = False
        '
        'btnPos8
        '
        Me.btnPos8.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos8.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos8.Location = New System.Drawing.Point(457, 429)
        Me.btnPos8.Name = "btnPos8"
        Me.btnPos8.Size = New System.Drawing.Size(107, 67)
        Me.btnPos8.TabIndex = 104
        Me.btnPos8.UseVisualStyleBackColor = False
        '
        'btnPos13
        '
        Me.btnPos13.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos13.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos13.Location = New System.Drawing.Point(457, 502)
        Me.btnPos13.Name = "btnPos13"
        Me.btnPos13.Size = New System.Drawing.Size(107, 67)
        Me.btnPos13.TabIndex = 105
        Me.btnPos13.UseVisualStyleBackColor = False
        '
        'btnPos18
        '
        Me.btnPos18.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos18.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos18.Location = New System.Drawing.Point(457, 575)
        Me.btnPos18.Name = "btnPos18"
        Me.btnPos18.Size = New System.Drawing.Size(107, 67)
        Me.btnPos18.TabIndex = 106
        Me.btnPos18.UseVisualStyleBackColor = False
        '
        'btnPos19
        '
        Me.btnPos19.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos19.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos19.Location = New System.Drawing.Point(5, 648)
        Me.btnPos19.Name = "btnPos19"
        Me.btnPos19.Size = New System.Drawing.Size(107, 67)
        Me.btnPos19.TabIndex = 110
        Me.btnPos19.UseVisualStyleBackColor = False
        '
        'btnPos20
        '
        Me.btnPos20.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos20.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos20.Location = New System.Drawing.Point(118, 648)
        Me.btnPos20.Name = "btnPos20"
        Me.btnPos20.Size = New System.Drawing.Size(107, 67)
        Me.btnPos20.TabIndex = 109
        Me.btnPos20.UseVisualStyleBackColor = False
        '
        'btnPos21
        '
        Me.btnPos21.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos21.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos21.Location = New System.Drawing.Point(231, 648)
        Me.btnPos21.Name = "btnPos21"
        Me.btnPos21.Size = New System.Drawing.Size(107, 67)
        Me.btnPos21.TabIndex = 108
        Me.btnPos21.UseVisualStyleBackColor = False
        '
        'btn5percent
        '
        Me.btn5percent.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn5percent.Enabled = False
        Me.btn5percent.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btn5percent.Location = New System.Drawing.Point(5, 356)
        Me.btn5percent.Name = "btn5percent"
        Me.btn5percent.Size = New System.Drawing.Size(107, 67)
        Me.btn5percent.TabIndex = 112
        Me.btn5percent.Text = "Φ.Π.Α 5%"
        Me.btn5percent.UseVisualStyleBackColor = False
        '
        'btn19percent
        '
        Me.btn19percent.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btn19percent.Enabled = False
        Me.btn19percent.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btn19percent.Location = New System.Drawing.Point(118, 356)
        Me.btn19percent.Name = "btn19percent"
        Me.btn19percent.Size = New System.Drawing.Size(107, 67)
        Me.btn19percent.TabIndex = 111
        Me.btn19percent.Text = "Φ.Π.Α 19%"
        Me.btn19percent.UseVisualStyleBackColor = False
        '
        'btnPos22
        '
        Me.btnPos22.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos22.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos22.Location = New System.Drawing.Point(344, 648)
        Me.btnPos22.Name = "btnPos22"
        Me.btnPos22.Size = New System.Drawing.Size(107, 67)
        Me.btnPos22.TabIndex = 113
        Me.btnPos22.UseVisualStyleBackColor = False
        '
        'btnPos23
        '
        Me.btnPos23.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPos23.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPos23.Location = New System.Drawing.Point(457, 648)
        Me.btnPos23.Name = "btnPos23"
        Me.btnPos23.Size = New System.Drawing.Size(107, 67)
        Me.btnPos23.TabIndex = 114
        Me.btnPos23.UseVisualStyleBackColor = False
        '
        'frmPOSEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1014, 739)
        Me.Controls.Add(Me.btnPos23)
        Me.Controls.Add(Me.btnPos22)
        Me.Controls.Add(Me.btn5percent)
        Me.Controls.Add(Me.btn19percent)
        Me.Controls.Add(Me.btnPos19)
        Me.Controls.Add(Me.btnPos20)
        Me.Controls.Add(Me.btnPos21)
        Me.Controls.Add(Me.btnPos18)
        Me.Controls.Add(Me.btnPos13)
        Me.Controls.Add(Me.btnPos8)
        Me.Controls.Add(Me.btnPos4)
        Me.Controls.Add(Me.btnPos9)
        Me.Controls.Add(Me.btnPos14)
        Me.Controls.Add(Me.btnPos5)
        Me.Controls.Add(Me.btnPos10)
        Me.Controls.Add(Me.btnPos15)
        Me.Controls.Add(Me.btnPos1)
        Me.Controls.Add(Me.btnPos6)
        Me.Controls.Add(Me.btnPos11)
        Me.Controls.Add(Me.btnPos16)
        Me.Controls.Add(Me.btnPos17)
        Me.Controls.Add(Me.btnPos12)
        Me.Controls.Add(Me.btnPos7)
        Me.Controls.Add(Me.btnPos2)
        Me.Controls.Add(Me.btnPos3)
        Me.Controls.Add(Me.btnReceipts)
        Me.Controls.Add(Me.btnHold)
        Me.Controls.Add(Me.btnKronosSearch)
        Me.Controls.Add(Me.btnKronos)
        Me.Controls.Add(Me.btnPayments)
        Me.Controls.Add(Me.btnClearBarcode)
        Me.Controls.Add(Me.chkBoxReturnProduct)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnCash)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDot)
        Me.Controls.Add(Me.btnPayment)
        Me.Controls.Add(Me.btnVisa)
        Me.Controls.Add(Me.btnDiscount)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnBackspace)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnRemoveQuantity)
        Me.Controls.Add(Me.btnAddQuantity)
        Me.Controls.Add(Me.txtBoxQuantity)
        Me.Controls.Add(Me.lblQuantity)
        Me.Controls.Add(Me.lblBarcode)
        Me.Controls.Add(Me.txtBoxBarcode)
        Me.Controls.Add(Me.txtBoxManualAmt)
        Me.Controls.Add(Me.btn8)
        Me.Controls.Add(Me.btn9)
        Me.Controls.Add(Me.btn4)
        Me.Controls.Add(Me.btn5)
        Me.Controls.Add(Me.btn6)
        Me.Controls.Add(Me.number1)
        Me.Controls.Add(Me.btn2)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.btn0)
        Me.Controls.Add(Me.btn7)
        Me.Controls.Add(Me.dgvReceipt)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1024, 768)
        Me.MinimumSize = New System.Drawing.Size(1022, 722)
        Me.Name = "frmPOSEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS"
        CType(Me.dgvReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvReceipt As System.Windows.Forms.DataGridView
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents number1 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents txtBoxManualAmt As System.Windows.Forms.TextBox
    Friend WithEvents btnDiscount As System.Windows.Forms.Button
    Friend WithEvents btnVisa As System.Windows.Forms.Button
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents txtBoxQuantity As System.Windows.Forms.TextBox
    Friend WithEvents btnAddQuantity As System.Windows.Forms.Button
    Friend WithEvents btnRemoveQuantity As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtBoxDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxTotalAmt As System.Windows.Forms.TextBox
    Friend WithEvents lblReturn As System.Windows.Forms.Label
    Friend WithEvents lblPayment As System.Windows.Forms.Label
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents lblTotalAmt As System.Windows.Forms.Label
    Friend WithEvents txtBoxPaymentAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxReturnAmt As System.Windows.Forms.TextBox
    Friend WithEvents btnBackspace As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnPayment As System.Windows.Forms.Button
    Friend WithEvents btnDot As System.Windows.Forms.Button
    Friend WithEvents btnCash As System.Windows.Forms.Button
    Friend WithEvents txtBoxTotalWithDiscount As System.Windows.Forms.TextBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkBoxReturnProduct As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents btnClearBarcode As System.Windows.Forms.Button
    Friend WithEvents btnPayments As System.Windows.Forms.Button
    Friend WithEvents btnCancelDiscount As System.Windows.Forms.Button
    Friend WithEvents btnCancelPayment As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents productSerno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents unitprice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isKronos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents itemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents issueNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents deliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnKronos As System.Windows.Forms.Button
    Friend WithEvents btnKronosSearch As System.Windows.Forms.Button
    Friend WithEvents btnHold As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnReceipts As System.Windows.Forms.Button
    Friend WithEvents btnPos3 As System.Windows.Forms.Button
    Friend WithEvents btnPos2 As System.Windows.Forms.Button
    Friend WithEvents btnPos7 As System.Windows.Forms.Button
    Friend WithEvents btnPos12 As System.Windows.Forms.Button
    Friend WithEvents btnPos17 As System.Windows.Forms.Button
    Friend WithEvents btnPos16 As System.Windows.Forms.Button
    Friend WithEvents btnPos11 As System.Windows.Forms.Button
    Friend WithEvents btnPos6 As System.Windows.Forms.Button
    Friend WithEvents btnPos1 As System.Windows.Forms.Button
    Friend WithEvents btnPos15 As System.Windows.Forms.Button
    Friend WithEvents btnPos10 As System.Windows.Forms.Button
    Friend WithEvents btnPos5 As System.Windows.Forms.Button
    Friend WithEvents btnPos14 As System.Windows.Forms.Button
    Friend WithEvents btnPos9 As System.Windows.Forms.Button
    Friend WithEvents btnPos4 As System.Windows.Forms.Button
    Friend WithEvents btnPos8 As System.Windows.Forms.Button
    Friend WithEvents btnPos13 As System.Windows.Forms.Button
    Friend WithEvents btnPos18 As System.Windows.Forms.Button
    Friend WithEvents btnPos19 As System.Windows.Forms.Button
    Friend WithEvents btnPos20 As System.Windows.Forms.Button
    Friend WithEvents btnPos21 As System.Windows.Forms.Button
    Friend WithEvents btn5percent As System.Windows.Forms.Button
    Friend WithEvents btn19percent As System.Windows.Forms.Button
    Friend WithEvents btnPos22 As System.Windows.Forms.Button
    Friend WithEvents btnPos23 As System.Windows.Forms.Button
End Class

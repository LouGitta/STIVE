﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataGridView1 = New DataGridView()
        deleteButtonColumn = New DataGridViewButtonColumn()
        editButtonColumn = New DataGridViewButtonColumn()
        Dim margin As Padding = New Padding(100, 500, 100, 250)
        Me.Margin = margin
        Label1 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {deleteButtonColumn, editButtonColumn})
        DataGridView1.Location = New Point(13, 75)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(776, 285)
        DataGridView1.TabIndex = 0
        DataGridView1.Margin = margin
        ' 
        ' deleteButtonColumn
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.Red
        deleteButtonColumn.DefaultCellStyle = DataGridViewCellStyle1
        deleteButtonColumn.HeaderText = "Supprimer"
        deleteButtonColumn.Name = "deleteButtonColumn"
        deleteButtonColumn.Text = "Supprimer"
        deleteButtonColumn.UseColumnTextForButtonValue = True
        ' 
        ' editButtonColumn
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.Orange
        editButtonColumn.DefaultCellStyle = DataGridViewCellStyle2
        editButtonColumn.HeaderText = "Modifier"
        editButtonColumn.Name = "editButtonColumn"
        editButtonColumn.Text = "Modifier"
        editButtonColumn.UseColumnTextForButtonValue = True
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 20.0F)
        Label1.Location = New Point(172, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(657, 33)
        Label1.TabIndex = 1
        Label1.Text = "Gestion des stocks"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(13, 447)
        Button1.Name = "Button1"
        Button1.Size = New Size(776, 33)
        Button1.TabIndex = 2
        Button1.Text = "Ajouter un vin"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(202, 20)
        Button2.Name = "Button2"
        Button2.Size = New Size(121, 33)
        Button2.TabIndex = 3
        Button2.Text = "Actualiser"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(13, 20)
        Button3.Margin = New Padding(100)
        Button3.Name = "Button3"
        Button3.Size = New Size(166, 33)
        Button3.TabIndex = 4
        Button3.Text = "AccÃ©der aux commandes"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(930, 494)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label1)
        Controls.Add(DataGridView1)
        Name = "Form1"
        Text = "Form1"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents deleteButtonColumn As DataGridViewButtonColumn
    Friend WithEvents editButtonColumn As DataGridViewButtonColumn
End Class
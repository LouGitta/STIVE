
'Requise par le Concepteur Windows Form


    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UtilisateursForm
        Inherits System.Windows.Forms.Form

        'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

        'Requise par le Concepteur Windows Form
        Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        editButtonColumn = New DataGridViewButtonColumn()
        deleteButtonColumn = New DataGridViewButtonColumn()
        DataGridViewButtonColumn1 = New DataGridViewButtonColumn()
        DataGridViewButtonColumn2 = New DataGridViewButtonColumn()
        DataGridView1 = New DataGridView()
        DataGridViewButtonColumn3 = New DataGridViewButtonColumn()
        DataGridViewButtonColumn4 = New DataGridViewButtonColumn()
        Label1 = New Label()
        AccueilBouton = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' editButtonColumn
        ' 
        editButtonColumn.HeaderText = "Modifier"
        editButtonColumn.Name = "editButtonColumn"
        editButtonColumn.Text = "Modifier"
        editButtonColumn.UseColumnTextForButtonValue = True
        ' 
        ' deleteButtonColumn
        ' 
        deleteButtonColumn.HeaderText = "Supprimer"
        deleteButtonColumn.Name = "deleteButtonColumn"
        deleteButtonColumn.Text = "Supprimer"
        deleteButtonColumn.UseColumnTextForButtonValue = True
        ' 
        ' DataGridViewButtonColumn1
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = Color.Red
        DataGridViewButtonColumn1.DefaultCellStyle = DataGridViewCellStyle5
        DataGridViewButtonColumn1.HeaderText = "Supprimer"
        DataGridViewButtonColumn1.Name = "DeleteButtonColumn"
        DataGridViewButtonColumn1.Text = "Supprimer"
        DataGridViewButtonColumn1.UseColumnTextForButtonValue = True
        ' 
        ' DataGridViewButtonColumn2
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = Color.Orange
        DataGridViewButtonColumn2.DefaultCellStyle = DataGridViewCellStyle6
        DataGridViewButtonColumn2.HeaderText = "Modifier"
        DataGridViewButtonColumn2.Name = "EditButtonColumn"
        DataGridViewButtonColumn2.Text = "Modifier"
        DataGridViewButtonColumn2.UseColumnTextForButtonValue = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {DataGridViewButtonColumn3, DataGridViewButtonColumn4})
        DataGridView1.Location = New Point(44, 99)
        DataGridView1.Margin = New Padding(100, 500, 100, 250)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(913, 400)
        DataGridView1.TabIndex = 1
        AddHandler DataGridView1.CellContentClick, AddressOf DataGridView1_CellContentClick
        ' 
        ' DataGridViewButtonColumn3
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = Color.Red
        DataGridViewButtonColumn3.DefaultCellStyle = DataGridViewCellStyle7
        DataGridViewButtonColumn3.HeaderText = "Supprimer"
        DataGridViewButtonColumn3.Name = "DataGridViewButtonColumn3"
        DataGridViewButtonColumn3.Text = "Supprimer"
        DataGridViewButtonColumn3.UseColumnTextForButtonValue = True
        ' 
        ' DataGridViewButtonColumn4
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = Color.Orange
        DataGridViewButtonColumn4.DefaultCellStyle = DataGridViewCellStyle8
        DataGridViewButtonColumn4.HeaderText = "Modifier"
        DataGridViewButtonColumn4.Name = "DataGridViewButtonColumn4"
        DataGridViewButtonColumn4.Text = "Modifier"
        DataGridViewButtonColumn4.UseColumnTextForButtonValue = True
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 20.0F)
        Label1.Location = New Point(149, 31)
        Label1.Name = "Label1"
        Label1.Size = New Size(657, 33)
        Label1.TabIndex = 2
        Label1.Text = "Gestion des utilisateurs"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' AccueilBouton
        ' 
        AccueilBouton.Font = New Font("Segoe UI", 15.0F)
        AccueilBouton.Location = New Point(741, 31)
        AccueilBouton.Name = "AccueilBouton"
        AccueilBouton.Size = New Size(101, 52)
        AccueilBouton.TabIndex = 58
        AccueilBouton.Text = "Retour"
        AccueilBouton.UseVisualStyleBackColor = True
        ' 
        ' UtilisateursForm
        ' 
        ClientSize = New Size(1085, 575)
        Controls.Add(AccueilBouton)
        Controls.Add(Label1)
        Controls.Add(DataGridView1)
        Name = "UtilisateursForm"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub


    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents deleteButtonColumn As DataGridViewButtonColumn
    Friend WithEvents editButtonColumn As DataGridViewButtonColumn
    Friend WithEvents DataGridViewButtonColumn1 As DataGridViewButtonColumn
    Friend WithEvents DataGridViewButtonColumn2 As DataGridViewButtonColumn
    Friend WithEvents DataGridViewButtonColumn3 As DataGridViewButtonColumn
    Friend WithEvents DataGridViewButtonColumn4 As DataGridViewButtonColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents AccueilBouton As Button
End Class

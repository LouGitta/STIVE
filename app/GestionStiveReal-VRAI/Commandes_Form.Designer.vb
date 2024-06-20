<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Commandes_Form
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        dataGridViewCommandes = New DataGridView()
        Label1 = New Label()
        Accueil = New Button()
        Button1 = New Button()
        CType(dataGridViewCommandes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dataGridViewCommandes
        ' 
        dataGridViewCommandes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGridViewCommandes.Location = New Point(12, 82)
        dataGridViewCommandes.Margin = New Padding(100, 100, 100, 350)
        dataGridViewCommandes.Name = "dataGridViewCommandes"
        dataGridViewCommandes.Size = New Size(1279, 469)
        dataGridViewCommandes.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.0F)
        Label1.Location = New Point(302, 28)
        Label1.Margin = New Padding(100, 110, 103, 100)
        Label1.Name = "Label1"
        Label1.Size = New Size(194, 25)
        Label1.TabIndex = 1
        Label1.Text = "Commandes en cours"
        ' 
        ' Accueil
        ' 
        Accueil.Font = New Font("Segoe UI", 15.0F)
        Accueil.Location = New Point(12, 11)
        Accueil.Name = "Accueil"
        Accueil.Size = New Size(85, 56)
        Accueil.TabIndex = 56
        Accueil.Text = "Accueil"
        Accueil.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 15.0F)
        Button1.Location = New Point(547, 12)
        Button1.Name = "Button1"
        Button1.Size = New Size(143, 56)
        Button1.TabIndex = 57
        Button1.Text = "Actualiser"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Commandes_Form
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1126, 629)
        Controls.Add(Button1)
        Controls.Add(Accueil)
        Controls.Add(Label1)
        Controls.Add(dataGridViewCommandes)
        Name = "Commandes_Form"
        Text = "Commandes"
        WindowState = FormWindowState.Maximized
        CType(dataGridViewCommandes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dataGridViewCommandes As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Accueil As Button
    Friend WithEvents Button1 As Button
End Class

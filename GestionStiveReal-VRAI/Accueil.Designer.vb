<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Accueil
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
        Button1 = New Button()
        TextBoxApiUrl = New TextBox()
        Button3 = New Button()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(398, 178)
        Button1.Name = "Button1"
        Button1.Size = New Size(149, 86)
        Button1.TabIndex = 0
        Button1.Text = "Se connecter"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBoxApiUrl
        ' 
        TextBoxApiUrl.Location = New Point(22, 130)
        TextBoxApiUrl.Name = "TextBoxApiUrl"
        TextBoxApiUrl.Size = New Size(327, 23)
        TextBoxApiUrl.TabIndex = 2
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(398, 129)
        Button3.Name = "Button3"
        Button3.Size = New Size(151, 23)
        Button3.TabIndex = 3
        Button3.Text = "Enregistrer lien api"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Accueil
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button3)
        Controls.Add(TextBoxApiUrl)
        Controls.Add(Button1)
        Name = "Accueil"
        Text = "Accueil"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBoxApiUrl As TextBox
    Friend WithEvents Button3 As Button
End Class

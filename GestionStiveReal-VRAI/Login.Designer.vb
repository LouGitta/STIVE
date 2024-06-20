<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        emailTextBox = New TextBox()
        passwordTextBox = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        loginButton = New Button()
        AccueilBouton = New Button()
        SuspendLayout()
        ' 
        ' emailTextBox
        ' 
        emailTextBox.Location = New Point(211, 195)
        emailTextBox.Name = "emailTextBox"
        emailTextBox.Size = New Size(227, 23)
        emailTextBox.TabIndex = 0
        ' 
        ' passwordTextBox
        ' 
        passwordTextBox.Location = New Point(211, 250)
        passwordTextBox.Name = "passwordTextBox"
        passwordTextBox.Size = New Size(227, 23)
        passwordTextBox.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(157, 203)
        Label1.Name = "Label1"
        Label1.Size = New Size(36, 15)
        Label1.TabIndex = 2
        Label1.Text = "Email"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(116, 258)
        Label2.Name = "Label2"
        Label2.Size = New Size(77, 15)
        Label2.TabIndex = 3
        Label2.Text = "Mot de passe"
        ' 
        ' loginButton
        ' 
        loginButton.Font = New Font("Segoe UI", 11F)
        loginButton.Location = New Point(211, 310)
        loginButton.Name = "loginButton"
        loginButton.Size = New Size(227, 69)
        loginButton.TabIndex = 4
        loginButton.Text = "Accéder à la gestion des stocks"
        loginButton.UseVisualStyleBackColor = True
        ' 
        ' AccueilBouton
        ' 
        AccueilBouton.Font = New Font("Segoe UI", 15F)
        AccueilBouton.Location = New Point(12, 12)
        AccueilBouton.Name = "AccueilBouton"
        AccueilBouton.Size = New Size(110, 76)
        AccueilBouton.TabIndex = 56
        AccueilBouton.Text = "Retour"
        AccueilBouton.UseVisualStyleBackColor = True
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(750, 565)
        Controls.Add(AccueilBouton)
        Controls.Add(loginButton)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(passwordTextBox)
        Controls.Add(emailTextBox)
        Name = "Login"
        Text = "Accueil"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents emailTextBox As TextBox
    Friend WithEvents passwordTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents loginButton As Button
    Friend WithEvents Accueil As Button
    Friend WithEvents AccueilBouton As Button
End Class

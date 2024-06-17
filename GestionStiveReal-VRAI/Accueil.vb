Public Module Config
    Public BaseApiUrl As String = "http://localhost/stive/api"
End Module
Public Class Accueil

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Login.Show()
    End Sub

    Private Sub Accueil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxApiUrl.Text = Config.BaseApiUrl
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim test As String
        test = Config.BaseApiUrl & "/produit"
        If (TextBoxApiUrl.Text.EndsWith("/")) Then
            MsgBox(TextBoxApiUrl.Text)
            MsgBox("veuillez enlever le / à la fin de l'url")
            MsgBox(test)

        Else
            Config.BaseApiUrl = TextBoxApiUrl.Text
            MsgBox(Config.BaseApiUrl)
        End If



    End Sub

    Private Sub TextBoxApiUrl_TextChanged(sender As Object, e As EventArgs) Handles TextBoxApiUrl.TextChanged

    End Sub
End Class
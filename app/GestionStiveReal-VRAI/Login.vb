Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Login
    Public Property JwtToken As String

    Private Async Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
        Dim mail As String = emailTextBox.Text
        Dim mdp As String = passwordTextBox.Text


        Dim credentials As New Dictionary(Of String, String) From {
            {"mail", mail},
            {"mdp", mdp}
        }

        Dim jsonData As String = JsonConvert.SerializeObject(credentials)


        Dim apiUrl As String = ""
        apiUrl = Config.BaseApiUrl & "/utilisateur"
        Using client As New HttpClient()
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            client.DefaultRequestHeaders.Add("action-type", "login")
            client.DefaultRequestHeaders.Add("app-type", "adminapp")
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                Dim responseData As JObject = JObject.Parse(responseContent)

                ' Check if the response status is "success"
                If responseData("status") IsNot Nothing AndAlso responseData("status").ToString() = "success" Then
                    ' Check if the token is present
                    If responseData("token") IsNot Nothing Then
                        Dim token As String = responseData("token").ToString()
                        Config.JwtToken = token
                        ' My.Settings.Save()
                        Dim Form1 As New Form1()
                        Form1.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Token manquant dans la réponse de l'API.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Identifiants incorrects.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Erreur lors de la connexion à l'API.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class


Public Class Utilisateur
    Public Property id As Integer
    Public Property mail As String
    Public Property mdp As String
    Public Property is_admin As Integer
End Class

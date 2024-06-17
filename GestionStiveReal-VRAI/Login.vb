Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class Login
    Private Async Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click
        Dim mail As String = emailTextBox.Text
        Dim mdp As String = passwordTextBox.Text


        Dim credentials As New Dictionary(Of String, String) From {
            {"mail", mail},
            {"mdp", mdp}
        }

        Dim jsonData As String = JsonConvert.SerializeObject(credentials)


        Dim apiUrl As String = ""
        apiUrl = Config.BaseApiUrl & "/produit"
        Using client As New HttpClient()
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                Dim responseData As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(responseContent)

                If responseData.ContainsKey("success") AndAlso responseData("success") = "true" Then
                    If responseData.ContainsKey("token") Then
                        Dim token As String = responseData("token")
                        ' Me.JwtToken = token
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

Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class Commandes_Form
    Private Async Sub Commandes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  Actualiser()
        Await ChargerCommandes()
    End Sub

    Private Sub Actualiser()
        Me.WindowState = FormWindowState.Maximized

        Dim panel As New Panel()
        panel.Dock = DockStyle.Fill
        panel.Padding = New Padding(100)

        Me.Controls.Add(panel)

        Dim dataGridViewCommandes As New DataGridView()
        dataGridViewCommandes.Name = "dataGridViewCommandes"
        dataGridViewCommandes.Dock = DockStyle.Fill
        dataGridViewCommandes.AutoGenerateColumns = False
        InitialiserColonnesDataGridView(dataGridViewCommandes)

        panel.Controls.Add(dataGridViewCommandes)
    End Sub

    Private Sub InitialiserColonnesDataGridView(dataGridView As DataGridView)
        dataGridView.Columns.Clear()
        dataGridView.Columns.Add("id", "ID")
        dataGridView.Columns.Add("reference_commande", "Référence Commande")
        dataGridView.Columns.Add("date_commande", "Date Commande")
        dataGridView.Columns.Add("utilisateur_id", "Utilisateur ID")
        dataGridView.Columns.Add("quantite_commande", "Quantité Commande")
        dataGridView.Columns.Add("prix_commande", "Prix Commande")
        dataGridView.Columns.Add("statut", "Statut")
    End Sub

    Private Async Function ChargerCommandes() As Task
        Dim apiUrl As String = Config.BaseApiUrl & "/commande"
        Dim dataGridViewCommandes As DataGridView = CType(Me.Controls.Find("dataGridViewCommandes", True)(0), DataGridView)

        Using client As New HttpClient()
            Try
                client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Config.JwtToken)

                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                If response.IsSuccessStatusCode Then
                    Dim jsonString As String = Await response.Content.ReadAsStringAsync()
                    Dim commandes As List(Of Commande) = JsonConvert.DeserializeObject(Of List(Of Commande))(jsonString)

                    For Each cmd As Commande In commandes
                        If (DateTime.Now - cmd.date_commande).TotalMinutes > 5 Then
                            If cmd.statut <> "Livrée" Then
                                cmd.statut = "Livrée"
                                Await MettreAJourStatutCommande(cmd)
                            End If
                        Else
                            cmd.statut = "En cours"
                        End If
                    Next

                    dataGridViewCommandes.DataSource = commandes
                Else
                    MsgBox("Erreur lors de la récupération des commandes")
                End If
            Catch ex As Exception
                MsgBox("Une erreur s'est produite : " & ex.Message)
            End Try
        End Using
    End Function

    Private Async Function MettreAJourStatutCommande(cmd As Commande) As Task
        Dim apiUrl As String = Config.BaseApiUrl & $"/commande/{cmd.id}"
        Dim jsonData As String = JsonConvert.SerializeObject(New With {.statut = cmd.statut})

        Using client As New HttpClient()
            Try
                Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.PutAsync(apiUrl, content)
                If Not response.IsSuccessStatusCode Then

                End If
            Catch ex As Exception
                MsgBox("Erreur" & ex.Message)
            End Try
        End Using
    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Accueil_Click(sender As Object, e As EventArgs) Handles Accueil.Click
        Hide()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Actualiser()

    End Sub
End Class

Public Class Commande
    Public Property id As Integer
    Public Property reference_commande As String
    Public Property date_commande As Date
    Public Property utilisateur_id As Integer
    Public Property quantite_commande As Integer
    Public Property prix_commande As Double
    Public Property statut As String
End Class
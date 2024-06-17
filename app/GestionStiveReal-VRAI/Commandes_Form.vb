Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class Commandes_Form
    Private Async Sub Commandes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Actualiser()
        Await ChargerCommandes()
    End Sub

    Private Sub Actualiser()
        Me.WindowState = FormWindowState.Maximized

        Dim panel As New Panel()
        panel.Dock = DockStyle.Fill
        panel.Padding = New Padding(100)

        Me.Controls.Add(panel)

        dataGridViewCommandes.Dock = DockStyle.Fill
        dataGridViewCommandes.AutoGenerateColumns = False
        InitialiserColonnesDataGridView()

        panel.Controls.Add(dataGridViewCommandes)
    End Sub

    Private Sub InitialiserColonnesDataGridView()
        dataGridViewCommandes.Columns.Clear()
        dataGridViewCommandes.Columns.Add("id", "ID")
        dataGridViewCommandes.Columns.Add("reference_commande", "Référence Commande")
        dataGridViewCommandes.Columns.Add("date_commande", "Date Commande")
        dataGridViewCommandes.Columns.Add("utilisateur_id", "Utilisateur ID")
        dataGridViewCommandes.Columns.Add("quantite_commande", "Quantité Commande")
        dataGridViewCommandes.Columns.Add("prix_commande", "Prix Commande")
        dataGridViewCommandes.Columns.Add("statut", "Statut")
    End Sub

    Private Async Function ChargerCommandes() As Task
        Dim apiUrl As String = Config.BaseApiUrl & "/commande"

        Using client As New HttpClient()
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
                MsgBox("Erreur")
            End If
        End Using
    End Function

    Private Async Function MettreAJourStatutCommande(cmd As Commande) As Task
        Dim apiUrl As String = Config.BaseApiUrl & $"/commande/{cmd.id}"
        Dim jsonData As String = JsonConvert.SerializeObject(New With {.statut = cmd.statut})

        Using client As New HttpClient()
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PutAsync(apiUrl, content)
            If Not response.IsSuccessStatusCode Then
                MsgBox($"Erreur  {cmd.id}.")
            End If
        End Using
    End Function


End Class

Public Class cmd
    Public Property id As Integer
    Public Property reference_commande As String
    Public Property date_commande As Date
    Public Property utilisateur_id As Integer
    Public Property quantite_commande As Integer
    Public Property prix_commande As Double
    Public Property statut As String
End Class

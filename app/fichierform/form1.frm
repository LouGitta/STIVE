Imports System.Data.OleDb
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        DataGridView1.Dock = DockStyle.Fill

        Dim margin As New Padding(100, 500, 100, 250)
        Me.Margin = margin


        InitialiserColonnesDataGridView()


        RefreshData()
        AdjustButtonPosition()
    End Sub

    Private Sub InitialiserColonnesDataGridView()

        DataGridView1.Columns.Clear()


        Dim deleteButtonColumn As New DataGridViewButtonColumn()
        deleteButtonColumn.HeaderText = "Supprimer"
        deleteButtonColumn.Name = "DeleteButtonColumn"
        deleteButtonColumn.Text = "Supprimer"
        deleteButtonColumn.UseColumnTextForButtonValue = True
        deleteButtonColumn.DefaultCellStyle.BackColor = Color.Red
        DataGridView1.Columns.Insert(0, deleteButtonColumn)


        Dim editButtonColumn As New DataGridViewButtonColumn()
        editButtonColumn.HeaderText = "Modifier"
        editButtonColumn.Name = "EditButtonColumn"
        editButtonColumn.Text = "Modifier"
        editButtonColumn.UseColumnTextForButtonValue = True
        editButtonColumn.DefaultCellStyle.BackColor = Color.Orange
        DataGridView1.Columns.Insert(1, editButtonColumn)


        DataGridView1.Columns.Add("id", "ID")
        DataGridView1.Columns.Add("nom", "Nom")
        DataGridView1.Columns.Add("prix", "Prix")
        DataGridView1.Columns.Add("description", "Description")
        DataGridView1.Columns.Add("annee", "Année")
        DataGridView1.Columns.Add("quantite_stock", "Quantité en stock")
        DataGridView1.Columns.Add("reference", "Référence")
        DataGridView1.Columns.Add("fournisseur", "Fournisseur")
        DataGridView1.Columns.Add("info", "Info")
        DataGridView1.Columns.Add("maison", "Maison")
        DataGridView1.Columns.Add("famille", "Famille")
        DataGridView1.Columns.Add("region", "Région")
        DataGridView1.Columns.Add("image", "Image")
    End Sub

    Private Sub AdjustButtonPosition()
        Dim bottomMargin As Integer = 10
        Dim buttonLocation As New Point(Me.ClientSize.Width - Button1.Width - 10, Me.ClientSize.Height - Button1.Height - bottomMargin)
        Button1.Location = buttonLocation
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim ajoutVinForm As New AjoutVin()
        ajoutVinForm.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AdjustButtonPosition()
        RefreshData()
    End Sub

    Private Async Sub RefreshData()
        Dim apiUrl As String = "http://localhost/api/"

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
            If response.IsSuccessStatusCode Then
                Dim jsonString As String = Await response.Content.ReadAsStringAsync()
                Dim produits As List(Of Produit) = JsonConvert.DeserializeObject(Of List(Of Produit))(jsonString)
                DataGridView1.DataSource = produits
            Else
                MessageBox.Show("Une erreur est survenue lors du chargement des données depuis l'API.")
            End If
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DataGridView1.Columns("DeleteButtonColumn").Index Then
                Dim confirmationResult As DialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cet enregistrement ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmationResult = DialogResult.Yes Then
                    Dim idToDelete As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("id").Value)
                    DeleteRecord(idToDelete)
                End If
            ElseIf e.ColumnIndex = DataGridView1.Columns("EditButtonColumn").Index Then
                Dim confirmationResult As DialogResult = MessageBox.Show("Voulez-vous vraiment modifier cet enregistrement ?", "Confirmation de modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmationResult = DialogResult.Yes Then
                    Dim idToEdit As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("id").Value)
                    OpenModifierProduitForm(idToEdit)
                End If
            End If
        End If
    End Sub

    Private Sub OpenModifierProduitForm(id As Integer)
        Me.Hide()
        Dim modifierProduitForm As New modifierProduit(id)
        modifierProduitForm.ShowDialog()
        Me.Show()
    End Sub

    Private Async Sub DeleteRecord(id As Integer)
        Dim apiUrl As String = $"localhost/stive/api/produit/{id}"

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.DeleteAsync(apiUrl)

            If response.IsSuccessStatusCode Then
                RefreshData()
                MessageBox.Show("Ligne supprimée avec succès.")
            Else
                MessageBox.Show("Erreur lors de la suppression de la ligne.")
            End If
        End Using
    End Sub
End Class


Public Class Produit
    Public Property id As Integer
    Public Property nom As String
    Public Property prix As Decimal
    Public Property description As String
    Public Property annee As Integer
    Public Property quantite_stock As Integer
    Public Property reference As String
    Public Property fournisseur As String
    Public Property info As String
    Public Property maison As String
    Public Property famille As String
    Public Property region As String
    Public Property image As String
End Class


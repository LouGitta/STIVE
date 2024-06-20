Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net.Http

Public Class UtilisateursForm
    Private Sub Utilisateurs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        RefreshData()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DataGridView1.Columns("DeleteButtonColumn").Index Then
                Dim confirmationResult = MessageBox.Show("Voulez-vous vraiment supprimer cet enregistrement ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmationResult = DialogResult.Yes Then
                    Dim idToDelete = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("id").Value)
                    DeleteRecord(idToDelete)
                End If
            ElseIf e.ColumnIndex = DataGridView1.Columns("EditButtonColumn").Index Then
                Dim confirmationResult = MessageBox.Show("Voulez-vous vraiment modifier cet enregistrement ?", "Confirmation de modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmationResult = DialogResult.Yes Then
                    Dim idToEdit = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("id").Value)
                    'OpenModifierProduitForm(idToEdit)
                End If
            End If
        End If
    End Sub

    Private Async Sub RefreshData()
        Dim apiUrl As String = Config.BaseApiUrl & "/utilisateur"
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Config.JwtToken)
            Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
            If response.IsSuccessStatusCode Then
                Dim jsonString As String = Await response.Content.ReadAsStringAsync()
                Dim users As List(Of User) = JsonConvert.DeserializeObject(Of List(Of User))(jsonString)
                DataGridView1.DataSource = users
                AddActionButtons()
            Else
                MessageBox.Show("Une erreur est survenue lors du chargement des donnÃ©es depuis l'API.")
            End If
        End Using
    End Sub

    Private Sub AddActionButtons()
        If Not DataGridView1.Columns.Contains("DeleteButtonColumn") Then
            Dim deleteButtonColumn As New DataGridViewButtonColumn()
            deleteButtonColumn.Name = "DeleteButtonColumn"
            deleteButtonColumn.HeaderText = "Supprimer"
            deleteButtonColumn.Text = "Supprimer"
            deleteButtonColumn.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Add(deleteButtonColumn)
        End If

        If Not DataGridView1.Columns.Contains("EditButtonColumn") Then
            Dim editButtonColumn As New DataGridViewButtonColumn()
            editButtonColumn.Name = "EditButtonColumn"
            editButtonColumn.HeaderText = "Modifier"
            editButtonColumn.Text = "Modifier"
            editButtonColumn.UseColumnTextForButtonValue = True
            DataGridView1.Columns.Add(editButtonColumn)
        End If
    End Sub

    Private Async Sub DeleteRecord(id As Integer)
        Dim apiUrl As String = Config.BaseApiUrl & $"/utilisateur/?id={id}"
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Config.JwtToken)
            Dim response As HttpResponseMessage = Await client.DeleteAsync(apiUrl)
            If response.IsSuccessStatusCode Then
                Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                Dim responseData As JObject = JObject.Parse(responseContent)
                MessageBox.Show(responseData("message").ToString())
                RefreshData()
            Else
                MessageBox.Show("Erreur lors de la suppression de la ligne.")
            End If
        End Using
    End Sub

    Private Sub OpenModifierProduitForm(id As Integer)
        Me.Hide()
        ' Dim modifierProduitForm As New ModifierProduitForm(id)
        'modifierProduitForm.ShowDialog()
        Me.Show()
    End Sub

    Private Sub AccueilBouton_Click(sender As Object, e As EventArgs) Handles AccueilBouton.Click
        Me.Hide()
        Form1.Show()
    End Sub
End Class

Public Class User
    Public Property id As Integer
    Public Property nom As String
    Public Property prenom As String
    Public Property mdp As String
    Public Property mail As String
    Public Property is_client As Boolean
    Public Property is_admin As Boolean
End Class
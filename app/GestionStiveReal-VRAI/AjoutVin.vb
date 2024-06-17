Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class AjoutVin
    Private Sub AjoutVin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Dim margin As New Padding(100, 500, 100, 250)
        Me.Margin = margin

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                AddHandler CType(ctrl, TextBox).TextChanged, AddressOf TextBox_TextChanged
            End If
        Next

        Button1.BackColor = Color.Red
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs)
        Dim allFilled As Boolean = True
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                If String.IsNullOrWhiteSpace(ctrl.Text) Then
                    allFilled = False
                    Exit For
                End If
            End If
        Next

        If allFilled Then
            Button1.BackColor = Color.Green
        Else
            Button1.BackColor = Color.Red
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                If String.IsNullOrWhiteSpace(ctrl.Text) Then
                    MessageBox.Show("Veuillez remplir toutes les cases.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If
        Next


        Dim vin As New Dictionary(Of String, String) From {
            {"nom", NomTextBox.Text},
            {"prix", PrixTextBox.Text},
            {"description", DescriptionTextBox.Text},
            {"annee", AnneeTextBox.Text},
            {"quantite_stock", QuantiteStockTextBox.Text},
            {"reference", ReferenceTextBox.Text},
            {"fournisseur", FournisseurTextBox.Text},
            {"info", InfoTextBox.Text},
            {"maison", MaisonTextBox.Text},
            {"famille", FamilleTextBox.Text},
            {"region", RegionTextBox.Text},
            {"image", ImageTextBox.Text}
        }


        Dim jsonData As String = JsonConvert.SerializeObject(vin)


        Using content As New MultipartFormDataContent()
            If Not String.IsNullOrEmpty(ImageTextBox.Text) Then
                Dim imageContent As ByteArrayContent = New ByteArrayContent(System.IO.File.ReadAllBytes(ImageTextBox.Text))
                imageContent.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg")
                content.Add(imageContent, "image", System.IO.Path.GetFileName(ImageTextBox.Text))
            End If

            Dim jsonContent As New StringContent(jsonData, Encoding.UTF8, "application/json")
            content.Add(jsonContent, "data")


            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsync(Config.BaseApiUrl & "/produit", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Produit ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Erreur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        End Using
    End Sub

    Private Sub ImporterImageButton_Click(sender As Object, e As EventArgs) Handles ImporterImageButton.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            openFileDialog.Title = "Choisis une image"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ImageTextBox.Text = openFileDialog.FileName
            End If
        End Using
    End Sub

    Private Sub Accueil_Click_1(sender As Object, e As EventArgs) Handles Accueil.Click
        Me.Hide()
        Form1.Show()
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

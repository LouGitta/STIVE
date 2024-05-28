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

        ' Créer un objet pour les données
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


        Using client As New HttpClient()
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("localhost/stive/api/produit", content) 'Remplir url api entre guillemets

            If response.IsSuccessStatusCode Then
                MessageBox.Show("youhou", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("bip boup ca marche pas", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using




        'MessageBox.Show("Données en JSON : " & Environment.NewLine & jsonData, "JSON", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ImporterImageButton_Click(sender As Object, e As EventArgs) Handles ImporterImageButton.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            openFileDialog.Title = "Select an Image"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                ImageTextBox.Text = openFileDialog.FileName
            End If
        End Using
    End Sub

    Private Sub Accueil_Click(sender As Object, e As EventArgs)
        Me.Hide()
        Form1.Show()
    End Sub
End Class

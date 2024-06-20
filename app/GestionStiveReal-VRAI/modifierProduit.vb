Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Runtime.CompilerServices
Imports System.Text

Public Class modifierProduit

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
        'Public Property image As String

    End Class



    Dim _recordId As Integer
    Private Sub modifierProduit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        InitializeComponent()




        PreRemplirZonesDeTexte()
        ' MsgBox(_recordId)
    End Sub

    Public Sub New(ByVal recordId As Integer)
        _recordId = recordId
    End Sub

    Private Sub PreRemplirZonesDeTexte()
        Dim produit As Produit = ObtenirProduitDeApi(_recordId)
        If produit IsNot Nothing Then
            idTextBox.Text = produit.id.ToString()
            NomTextBox.Text = produit.nom
            PrixTextBox.Text = produit.prix.ToString()
            DescriptionTextBox.Text = produit.description
            AnneeTextBox.Text = produit.annee.ToString()
            QuantiteStockTextBox.Text = produit.quantite_stock.ToString()
            ReferenceTextBox.Text = produit.reference
            FournisseurTextBox.Text = produit.fournisseur
            InfoTextBox.Text = produit.info
            MaisonTextBox.Text = produit.maison
            FamilleTextBox.Text = produit.famille
            RegionTextBox.Text = produit.region
            ' Ne pas inclure ImageTextBox.Text = produit.image
        End If
    End Sub

    Private Function ObtenirProduitDeApi(recordId As Integer) As Produit
        Dim apiUrl As String = Config.BaseApiUrl & $"/produit/?id={recordId}"

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.GetAsync(apiUrl).Result
            If response.IsSuccessStatusCode Then

                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                MessageBox.Show(jsonString)
                Dim produit As Produit = JsonConvert.DeserializeObject(Of Produit)(jsonString)
                Return produit
            Else
                MsgBox(apiUrl)
                MessageBox.Show("Erreur lors de la récupération des données de l'enregistrement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End If
        End Using
    End Function


    Private Function MettreAJourProduitDansApi(nouvellesDonnees As Produit) As Boolean
        Dim apiUrl As String = Config.BaseApiUrl & $"/produit/?id={_recordId}"
        Using client As New HttpClient()
            MsgBox(apiUrl)
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Config.JwtToken)
            ' Ne pas inclure .image dans le contenu JSON
            Dim contenu As New StringContent(JsonConvert.SerializeObject(nouvellesDonnees), Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = client.PatchAsync(apiUrl, contenu).Result

            If response.IsSuccessStatusCode Then
                Dim Messageresponse As String = response.Content.ReadAsStringAsync().Result
                MessageBox.Show("modif marche normalement !!.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MsgBox(Messageresponse)
                Return True
            Else
                Dim Messageresponse As String = response.Content.ReadAsStringAsync().Result
                MessageBox.Show("aie aie aie", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MsgBox(Messageresponse)
                Return False

            End If
        End Using
    End Function



    Private Sub Accueil_Click(sender As Object, e As EventArgs) Handles Accueil.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub BoutonValider_Click_1(sender As Object, e As EventArgs) Handles BoutonValider.Click
        Dim nouvellesDonnees As New Produit With {
            .id = Convert.ToDecimal(idTextBox.Text),
       .nom = NomTextBox.Text,
       .prix = Convert.ToDecimal(PrixTextBox.Text),
       .description = DescriptionTextBox.Text,
       .annee = Convert.ToInt32(AnneeTextBox.Text),
       .quantite_stock = Convert.ToInt32(QuantiteStockTextBox.Text),
       .reference = ReferenceTextBox.Text,
       .fournisseur = FournisseurTextBox.Text,
       .info = InfoTextBox.Text,
       .maison = MaisonTextBox.Text,
       .famille = FamilleTextBox.Text,
       .region = RegionTextBox.Text
   }
        ' Ne pas inclure .image = ImageTextBox.Text


        MettreAJourProduitDansApi(nouvellesDonnees)


        Close()
    End Sub
End Class

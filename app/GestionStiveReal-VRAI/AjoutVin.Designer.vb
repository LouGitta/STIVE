<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AjoutVin
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        FileSystemWatcher1 = New IO.FileSystemWatcher()
        Button1 = New Button()
        NomTextBox = New TextBox()
        PrixTextBox = New TextBox()
        DescriptionTextBox = New TextBox()
        AnneeTextBox = New TextBox()
        QuantiteStockTextBox = New TextBox()
        FamilleTextBox = New TextBox()
        MaisonTextBox = New TextBox()
        InfoTextBox = New TextBox()
        FournisseurTextBox = New TextBox()
        ReferenceTextBox = New TextBox()
        RegionTextBox = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label11 = New Label()
        ImporterImageButton = New Button()
        Label12 = New Label()
        ImageTextBox = New TextBox()
        Accueil = New Button()
        CType(FileSystemWatcher1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' FileSystemWatcher1
        ' 
        FileSystemWatcher1.EnableRaisingEvents = True
        FileSystemWatcher1.SynchronizingObject = Me
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Red
        Button1.Font = New Font("Segoe UI", 15F)
        Button1.Location = New Point(61, 418)
        Button1.Name = "Button1"
        Button1.Size = New Size(831, 45)
        Button1.TabIndex = 1
        Button1.Text = "Enregistrer un nouveau vin"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' NomTextBox
        ' 
        NomTextBox.Font = New Font("Segoe UI", 15F)
        NomTextBox.Location = New Point(238, 74)
        NomTextBox.Margin = New Padding(3, 3, 100, 3)
        NomTextBox.Name = "NomTextBox"
        NomTextBox.Size = New Size(153, 34)
        NomTextBox.TabIndex = 2
        ' 
        ' PrixTextBox
        ' 
        PrixTextBox.Font = New Font("Segoe UI", 15F)
        PrixTextBox.Location = New Point(238, 124)
        PrixTextBox.Margin = New Padding(3, 3, 100, 3)
        PrixTextBox.Name = "PrixTextBox"
        PrixTextBox.Size = New Size(153, 34)
        PrixTextBox.TabIndex = 3
        ' 
        ' DescriptionTextBox
        ' 
        DescriptionTextBox.Font = New Font("Segoe UI", 15F)
        DescriptionTextBox.Location = New Point(238, 174)
        DescriptionTextBox.Margin = New Padding(3, 3, 100, 3)
        DescriptionTextBox.Name = "DescriptionTextBox"
        DescriptionTextBox.Size = New Size(153, 34)
        DescriptionTextBox.TabIndex = 4
        ' 
        ' AnneeTextBox
        ' 
        AnneeTextBox.Font = New Font("Segoe UI", 15F)
        AnneeTextBox.Location = New Point(238, 224)
        AnneeTextBox.Margin = New Padding(3, 3, 100, 3)
        AnneeTextBox.Name = "AnneeTextBox"
        AnneeTextBox.Size = New Size(153, 34)
        AnneeTextBox.TabIndex = 5
        ' 
        ' QuantiteStockTextBox
        ' 
        QuantiteStockTextBox.Font = New Font("Segoe UI", 15F)
        QuantiteStockTextBox.Location = New Point(238, 273)
        QuantiteStockTextBox.Margin = New Padding(3, 3, 100, 3)
        QuantiteStockTextBox.Name = "QuantiteStockTextBox"
        QuantiteStockTextBox.Size = New Size(153, 34)
        QuantiteStockTextBox.TabIndex = 6
        ' 
        ' FamilleTextBox
        ' 
        FamilleTextBox.Font = New Font("Segoe UI", 15F)
        FamilleTextBox.Location = New Point(719, 224)
        FamilleTextBox.Margin = New Padding(3, 3, 100, 3)
        FamilleTextBox.Name = "FamilleTextBox"
        FamilleTextBox.Size = New Size(153, 34)
        FamilleTextBox.TabIndex = 11
        ' 
        ' MaisonTextBox
        ' 
        MaisonTextBox.Font = New Font("Segoe UI", 15F)
        MaisonTextBox.Location = New Point(719, 175)
        MaisonTextBox.Margin = New Padding(3, 3, 100, 3)
        MaisonTextBox.Name = "MaisonTextBox"
        MaisonTextBox.Size = New Size(153, 34)
        MaisonTextBox.TabIndex = 10
        ' 
        ' InfoTextBox
        ' 
        InfoTextBox.Font = New Font("Segoe UI", 15F)
        InfoTextBox.Location = New Point(719, 126)
        InfoTextBox.Margin = New Padding(3, 3, 100, 3)
        InfoTextBox.Name = "InfoTextBox"
        InfoTextBox.Size = New Size(153, 34)
        InfoTextBox.TabIndex = 9
        ' 
        ' FournisseurTextBox
        ' 
        FournisseurTextBox.Font = New Font("Segoe UI", 15F)
        FournisseurTextBox.Location = New Point(719, 74)
        FournisseurTextBox.Margin = New Padding(3, 3, 100, 3)
        FournisseurTextBox.Name = "FournisseurTextBox"
        FournisseurTextBox.Size = New Size(153, 34)
        FournisseurTextBox.TabIndex = 8
        ' 
        ' ReferenceTextBox
        ' 
        ReferenceTextBox.Font = New Font("Segoe UI", 15F)
        ReferenceTextBox.Location = New Point(238, 322)
        ReferenceTextBox.Margin = New Padding(3, 3, 100, 3)
        ReferenceTextBox.Name = "ReferenceTextBox"
        ReferenceTextBox.Size = New Size(153, 34)
        ReferenceTextBox.TabIndex = 7
        ' 
        ' RegionTextBox
        ' 
        RegionTextBox.Font = New Font("Segoe UI", 15F)
        RegionTextBox.Location = New Point(719, 273)
        RegionTextBox.Margin = New Padding(3, 3, 100, 3)
        RegionTextBox.Name = "RegionTextBox"
        RegionTextBox.Size = New Size(153, 34)
        RegionTextBox.TabIndex = 12
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 15F)
        Label1.Location = New Point(164, 80)
        Label1.Name = "Label1"
        Label1.Size = New Size(52, 28)
        Label1.TabIndex = 13
        Label1.Text = "nom"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 15F)
        Label2.Location = New Point(171, 132)
        Label2.Name = "Label2"
        Label2.Size = New Size(45, 28)
        Label2.TabIndex = 14
        Label2.Text = "prix"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 15F)
        Label3.Location = New Point(152, 230)
        Label3.Name = "Label3"
        Label3.Size = New Size(64, 28)
        Label3.TabIndex = 16
        Label3.Text = "annee"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 15F)
        Label4.Location = New Point(106, 180)
        Label4.Name = "Label4"
        Label4.Size = New Size(110, 28)
        Label4.TabIndex = 15
        Label4.Text = "description"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 15F)
        Label5.Location = New Point(124, 328)
        Label5.Name = "Label5"
        Label5.Size = New Size(92, 28)
        Label5.TabIndex = 18
        Label5.Text = "reference"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 15F)
        Label6.Location = New Point(131, 276)
        Label6.Name = "Label6"
        Label6.Size = New Size(85, 28)
        Label6.TabIndex = 17
        Label6.Text = "quantite"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 15F)
        Label7.Location = New Point(651, 132)
        Label7.Name = "Label7"
        Label7.Size = New Size(46, 28)
        Label7.TabIndex = 20
        Label7.Text = "info"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 15F)
        Label8.Location = New Point(589, 80)
        Label8.Name = "Label8"
        Label8.Size = New Size(108, 28)
        Label8.TabIndex = 19
        Label8.Text = "fournisseur"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 15F)
        Label9.Location = New Point(627, 230)
        Label9.Name = "Label9"
        Label9.Size = New Size(70, 28)
        Label9.TabIndex = 22
        Label9.Text = "famille"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 15F)
        Label10.Location = New Point(622, 181)
        Label10.Name = "Label10"
        Label10.Size = New Size(75, 28)
        Label10.TabIndex = 21
        Label10.Text = "maison"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 15F)
        Label11.Location = New Point(628, 273)
        Label11.Name = "Label11"
        Label11.Size = New Size(69, 28)
        Label11.TabIndex = 24
        Label11.Text = "region"
        ' 
        ' ImporterImageButton
        ' 
        ImporterImageButton.Font = New Font("Segoe UI", 15F)
        ImporterImageButton.Location = New Point(480, 316)
        ImporterImageButton.Name = "ImporterImageButton"
        ImporterImageButton.Size = New Size(217, 40)
        ImporterImageButton.TabIndex = 25
        ImporterImageButton.Text = "Importer Image"
        ImporterImageButton.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI", 15F)
        Label12.Location = New Point(495, 322)
        Label12.Name = "Label12"
        Label12.Size = New Size(0, 28)
        Label12.TabIndex = 26
        ' 
        ' ImageTextBox
        ' 
        ImageTextBox.Font = New Font("Segoe UI", 15F)
        ImageTextBox.Location = New Point(719, 316)
        ImageTextBox.Margin = New Padding(3, 3, 100, 3)
        ImageTextBox.Name = "ImageTextBox"
        ImageTextBox.Size = New Size(153, 34)
        ImageTextBox.TabIndex = 27
        ' 
        ' Accueil
        ' 
        Accueil.Font = New Font("Segoe UI", 15F)
        Accueil.Location = New Point(20, 29)
        Accueil.Name = "Accueil"
        Accueil.Size = New Size(110, 79)
        Accueil.TabIndex = 55
        Accueil.Text = "Accueil"
        Accueil.UseVisualStyleBackColor = True
        ' 
        ' AjoutVin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(932, 689)
        Controls.Add(Accueil)
        Controls.Add(ImageTextBox)
        Controls.Add(Label12)
        Controls.Add(ImporterImageButton)
        Controls.Add(Label11)
        Controls.Add(Label9)
        Controls.Add(Label10)
        Controls.Add(Label7)
        Controls.Add(Label8)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(Label3)
        Controls.Add(Label4)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(RegionTextBox)
        Controls.Add(FamilleTextBox)
        Controls.Add(MaisonTextBox)
        Controls.Add(InfoTextBox)
        Controls.Add(FournisseurTextBox)
        Controls.Add(ReferenceTextBox)
        Controls.Add(QuantiteStockTextBox)
        Controls.Add(AnneeTextBox)
        Controls.Add(DescriptionTextBox)
        Controls.Add(PrixTextBox)
        Controls.Add(NomTextBox)
        Controls.Add(Button1)
        Name = "AjoutVin"
        Padding = New Padding(58)
        CType(FileSystemWatcher1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents NomTextBox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents RegionTextBox As TextBox
    Friend WithEvents ReferenceTextBox As TextBox
    Friend WithEvents FournisseurTextBox As TextBox
    Friend WithEvents InfoTextBox As TextBox
    Friend WithEvents MaisonTextBox As TextBox
    Friend WithEvents FamilleTextBox As TextBox
    Friend WithEvents QuantiteStockTextBox As TextBox
    Friend WithEvents AnneeTextBox As TextBox
    Friend WithEvents DescriptionTextBox As TextBox
    Friend WithEvents PrixTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ImporterImageButton As Button
    Friend WithEvents ImageTextBox As TextBox
    Friend WithEvents Accueil As Button
End Class

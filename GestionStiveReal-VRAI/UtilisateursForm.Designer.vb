<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UtilisateursForm
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AccueilBouton = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(44, 99)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(913, 400)
        Me.DataGridView1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 20.0F)
        Me.Label1.Location = New System.Drawing.Point(149, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(657, 33)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Gestion des utilisateurs"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' 
        ' AccueilBouton
        ' 
        Me.AccueilBouton.Font = New System.Drawing.Font("Segoe UI", 15.0F)
        Me.AccueilBouton.Location = New System.Drawing.Point(741, 31)
        Me.AccueilBouton.Name = "AccueilBouton"
        Me.AccueilBouton.Size = New System.Drawing.Size(101, 52)
        Me.AccueilBouton.TabIndex = 58
        Me.AccueilBouton.Text = "Retour"
        Me.AccueilBouton.UseVisualStyleBackColor = True
        ' 
        ' UtilisateursForm
        ' 
        Me.ClientSize = New System.Drawing.Size(1085, 575)
        Me.Controls.Add(Me.AccueilBouton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "UtilisateursForm"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AccueilBouton As System.Windows.Forms.Button
End Class
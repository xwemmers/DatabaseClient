Imports DatabaseClient.DatabaseClient

Public Class Form1

    'Change Tracking
    ' Wat is de scope van db?
    Dim db As New AdventureWorksEntities


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim results = db.Products.ToList()

        dgv.DataSource = results

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim query = From p In db.Products
                    Where p.Color = "Red"
                    Select p

        'Dit toont de SQL die op basis van de LINQ query gegenereerd wordt
        'De SQL wordt naar de DB gestuurd en daar uitgevoerd
        MessageBox.Show(query.ToString())

        Dim results = query.ToList()

        dgv.DataSource = results

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        db.SaveChanges()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        dgv.DataSource = db.ProductCategories.ToList
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim query = From p In db.Products
                    Select p.ProductID, p.Name, p.ListPrice, p.Color, p.ModifiedDate

        dgv.DataSource = query.ToList

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim cat As New ProductCategory
        cat.Name = "Fietsen van Xander UTC"
        cat.ModifiedDate = DateTime.UtcNow
        cat.rowguid = Guid.NewGuid

        db.ProductCategories.Add(cat)
        db.SaveChanges()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'GetProductsByColor is een stored procedure die op de database aan de serverkant is gedefinieerd

        dgv.DataSource = db.GetProductsByColor("Blue")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim query = From p In db.Products
                    Select p.Name, p.Color, p.Size, CategoryName = p.ProductCategory.Name

        'In de query zie je in SQL dat de JOIN wordt gegenereerd
        MessageBox.Show(query.ToString())

        dgv.DataSource = query.ToList


    End Sub
End Class

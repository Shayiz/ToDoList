Imports System.Data.SqlClient
Imports System.Data

Imports System.Data.SqlClient.SqlException


Public Class WebForm3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("~/login.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not (String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text)) Then
            If TextBox2.Text.Equals(TextBox3.Text) Then
                Dim con As SqlConnection
                Dim cmd As SqlCommand
                Dim result As String


                Try
                    con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
                    cmd = con.CreateCommand
                    con.Open()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@uname", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text)
                    cmd.Parameters.Add("@res", SqlDbType.Int).Direction = ParameterDirection.Output
                    cmd.CommandText = "signup"
                    cmd.ExecuteNonQuery()

                    result = cmd.Parameters.Item(2).Value.ToString
                    con.Close()
                    If Convert.ToInt32(result) = 0 Then
                        Response.Write("<script>alert('Registered Succesfully')</script>")

                        Response.Redirect("~/login.aspx")
                    ElseIf Convert.ToInt32(result) = 1 Then
                        Response.Write("<script>alert('Username already exist')</script>")


                    End If
                Catch ex As Exception
                    Response.Write("<script>alert('" & ex.Message & "')</script>")

                End Try

            Else
                Response.Write("<script>alert('Password has different values')</script>")

            End If
        Else
            Response.Write("<script>alert('Please provide all the details')</script>")

        End If
    End Sub


End Class
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlClient.SqlException

Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Add("pid", Convert.ToInt32(0))

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If (String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text)) Then
            Response.Write("<script>alert('Please provide username and password')</script>")

        Else
            Dim con As SqlConnection
            Dim cmd As SqlCommand
            Dim result As String


            Try
                con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
                cmd = con.CreateCommand
                con.Open()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@uname", TextBox1.Text)
                cmd.Parameters.AddWithValue("@pass", TextBox2.Text)
                cmd.Parameters.Add("@uid", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.CommandText = "loginCheck"
                cmd.ExecuteNonQuery()

                result = cmd.Parameters.Item(2).Value.ToString
                con.Close()

                If String.IsNullOrEmpty(result) Then

                    Response.Write("<script>alert('Invalid username or password')</script>")
                Else

                    Session("pid") = Convert.ToInt32(result)
                    Session.Add("uname", TextBox1.Text)
                    Response.Redirect("~/HomePage.aspx")


                End If

            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")

            End Try


        End If


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("~/signup.aspx")
    End Sub
End Class
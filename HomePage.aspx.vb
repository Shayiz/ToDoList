Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlClient.SqlException

Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim dl As ArrayList = New ArrayList
    Dim ind As Integer


    Function showList()
        ListBox1.Items.Clear()

        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Dim reader As SqlDataReader


        Try

            con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
            cmd = con.CreateCommand

            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(Session.Item("pid")))

            cmd.CommandText = "showList"
            reader = cmd.ExecuteReader()
            dl.Clear()

            Do While reader.Read()
                ListBox1.Items.Add(reader.GetString(0))
                dl.Add(reader.GetString(1))

            Loop
            con.Close()

        Catch ex As Exception
            MsgBox("Not able to read data from server" & ex.Message)



        End Try
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("uname") Is Nothing And Session.Item("pid") Is Nothing Then
            Response.Redirect("~/login.aspx")
        Else
            showList()

            Label1.Text = Session.Item("uname").ToString

        End If


    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/login.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim item As ListItem = New ListItem(TextBox1.Text)
            If Not ListBox1.Items.Contains(item) Then


                Dim con As SqlConnection
                Dim cmd As SqlCommand
                Try
                    con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
                    cmd = con.CreateCommand
                    con.Open()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(Session.Item("pid")))
                    cmd.Parameters.AddWithValue("@work", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@desc", TextBox2.Text)
                    cmd.CommandText = "addList"
                    cmd.ExecuteNonQuery()
                    con.Close()
                    showList()
                    TextBox1.Text = ""
                    TextBox2.Text = ""

                Catch ex As Exception
                    Response.Write("<script>alert('" & ex.Message & "')</script>")

                End Try
            Else

                Response.Write("<script>alert('Item Already Exist')</script>")
            End If
        Else

            Response.Write("<script>alert('Please provide the work in textbox')</script>")
        End If
    End Sub

    Protected Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged






    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim item As ListItem = New ListItem(TextBox1.Text)
            If ListBox1.Items.Contains(item) Then
                Dim con As SqlConnection
                Dim cmd As SqlCommand
                Try
                    con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
                    cmd = con.CreateCommand
                    con.Open()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(Session.Item("pid")))
                    cmd.Parameters.AddWithValue("@work", TextBox1.Text)

                    cmd.CommandText = "delWork"
                    cmd.ExecuteNonQuery()
                    con.Close()
                    showList()
                    TextBox1.Text = ""
                    TextBox2.Text = ""

                Catch ex As Exception
                    Response.Write("<script>alert('" & ex.Message & "')</script>")
                End Try
            Else

                Response.Write("<script>alert('Item not in List')</script>")
            End If
        Else

            Response.Write("<script>alert('Please provide the work in text box to delete')</script>")

        End If

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim item As ListItem = New ListItem(TextBox1.Text)
            If ListBox1.Items.Contains(item) Then

                Try

                    TextBox2.Text = dl(ListBox1.Items.IndexOf(item))

                Catch ex As Exception
                    Response.Write("<script>alert('" & ex.Message & "')</script>")
                End Try
            Else
                Response.Write("<script>alert('Item not in List')</script>")
            End If
        Else

            Response.Write("<script>alert('Please provide the work in text box')</script>")

        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim con As SqlConnection
        Dim cmd As SqlCommand
        Try
            con = New SqlConnection("Data Source=DESKTOP-9SPQSF9\WEBILLEARN;Initial Catalog=ToDoList;Integrated Security=True")
            cmd = con.CreateCommand
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(Session.Item("pid")))
            cmd.CommandText = "clearAll"
            cmd.ExecuteNonQuery()
            con.Close()
            showList()

        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
    End Sub
End Class
﻿Imports System.Data.SqlClient
Imports System.Data.DataTable

Public Class frmSanPham
    Dim db As New DataTable
    Dim chuoiketnoi As String = "workstation id=binhtsps03740.mssql.somee.com;packet size=4096;user id=zlequydonz_SQLLogin_1;pwd=jfnduudn9v;data source=binhtsps03740.mssql.somee.com;persist security info=False;initial catalog=binhtsps03740"
    Dim conn As SqlConnection = New SqlConnection(chuoiketnoi)
    Private Sub btnThem_Click(sender As Object, e As EventArgs) Handles btnThem.Click
        reset()
    End Sub
    Private Sub LoadData()
        Dim conn As SqlConnection = New SqlConnection(chuoiketnoi)
        conn.Open()
        Dim refesh As SqlDataAdapter = New SqlDataAdapter("select MaSP as 'Mã SP' ,TenSP as 'Tên Sản phẩm', Soluong as 'Số lượng', Dongia as 'Đơn giá', Soluong * Dongia as 'Thành tiền' from SANPHAM1", conn)
        db.Clear()
        refesh.Fill(db)
        DataGridView1.DataSource = db.DefaultView
        conn.Close()
    End Sub
    Private Sub reset()
        txtDongia.Text = ""
        txtMaSP.Text = ""
        txtSoluong.Text = ""
        txtTenSP.Text = ""
        txtMaSP.Focus()
    End Sub
    Private Sub frmSanPham_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub btnLuu_Click(sender As Object, e As EventArgs) Handles btnLuu.Click
        If txtMaSP.Text = "" Then
            MessageBox.Show("Chua nhap mã sản phẩm")
            txtMaSP.Focus()
        ElseIf txtTenSP.Text = "" Then
            MessageBox.Show("Chua nhap Tên sản phẩm")
            txtTenSP.Focus()
        ElseIf txtSoluong.Text = "" Then
            MessageBox.Show("Chua nhap Số lượng")
            txtSoluong.Focus()
        ElseIf txtDongia.Text = "" Then
            MessageBox.Show("Chua nhap đơn giá")
            txtDongia.Focus()
        Else
            Dim conn As SqlConnection = New SqlConnection(chuoiketnoi)
            Dim query As String = "insert into SANPHAM1 values(@MASP,@TENSP,@SOLUONG,@DONGIA)"
            Dim save As SqlCommand = New SqlCommand(query, conn)
            conn.Open()
            save.Parameters.AddWithValue("@MASP", txtMaSP.Text)
            save.Parameters.AddWithValue("@TENSP", txtTenSP.Text)
            save.Parameters.AddWithValue("@SOLUONG", txtSoluong.Text)
            save.Parameters.AddWithValue("@DONGIA", txtDongia.Text)
            save.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("Lưu thành công")
            LoadData()
        End If
    End Sub

    Private Sub btnXoa_Click(sender As Object, e As EventArgs) Handles btnXoa.Click
        If txtMaSP.Text = "" Then
            MessageBox.Show("Nhap MaSP cần xóa")
            txtMaSP.Focus()
        Else
            Dim delquery As String = "delete from SANPHAM1 where MaSP=@MASP"
            Dim delete As SqlCommand = New SqlCommand(delquery, conn)
            Dim resulft As DialogResult = MessageBox.Show("Bạn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resulft = Windows.Forms.DialogResult.Yes Then
                conn.Open()
                delete.Parameters.AddWithValue("@MASP", txtMaSP.Text)
                delete.ExecuteNonQuery()
                conn.Close()
                MessageBox.Show("Xóa thành công")
                LoadData()
            End If
        End If
    End Sub

    Private Sub btnSua_Click(sender As Object, e As EventArgs) Handles btnSua.Click
        If btnSua.Text = "Sửa" Then
            txtMaSP.ReadOnly = True
            btnSua.Text = "Update"
            txtTenSP.Focus()
        ElseIf btnSua.Text = "Update" Then
            Dim conn As SqlConnection = New SqlConnection(chuoiketnoi)
            Dim query As String = "update SANPHAM1 set TenSP=@TENSP, Soluong=@SOLUONG, Dongia=@DONGIA where MaSP=@MASP"
            Dim save As SqlCommand = New SqlCommand(query, conn)
            conn.Open()
            save.Parameters.AddWithValue("@MASP", txtMaSP.Text)
            save.Parameters.AddWithValue("@TENSP", txtTenSP.Text)
            save.Parameters.AddWithValue("@SOLUONG", txtSoluong.Text)
            save.Parameters.AddWithValue("@DONGIA", txtDongia.Text)
            save.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("Update thành công")
            txtMaSP.ReadOnly = False
            btnSua.Text = "Sửa"
            LoadData()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim click As Integer = DataGridView1.CurrentCell.RowIndex
        txtMaSP.Text = DataGridView1.Item(0, click).Value
        txtTenSP.Text = DataGridView1.Item(1, click).Value
        txtSoluong.Text = DataGridView1.Item(2, click).Value
        txtDongia.Text = DataGridView1.Item(3, click).Value

    End Sub
End Class
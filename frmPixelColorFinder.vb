Public Class frmPixelColorFinder
    Dim myImage As Bitmap
    Private Sub btnOpenImage_Click(sender As Object, e As EventArgs) Handles btnOpenImage.Click
        Dim open As New OpenFileDialog
        open.Title = "Open"
        open.Filter = "Bitmap Files|*.bmp|JPEG|*.jpg|GIF|*.gif|PNG|*.png|All files (*.*)|*.*"

        If open.ShowDialog() = Windows.Forms.DialogResult.OK Then
            myImage = New Bitmap(open.FileName, True)
            picBox.BorderStyle = BorderStyle.None
            picBox.Image = myImage
            ' picBox.Image = Nothing
        End If

        'messageBox with image width and height
        Dim imageSizeInfo As String = $"Width: {myImage.Width} pixels {vbNewLine}Height: {myImage.Height} pixels"
        MessageBox.Show(imageSizeInfo, "Image Properties", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try

            Dim columnIndex As Integer = Integer.Parse(txtColumnIndex.Text)
            Dim rowIndex As Integer = Integer.Parse(txtRowIndex.Text)

            'see if indices are in image bounds
            If columnIndex < 0 OrElse columnIndex >= myImage.Width OrElse rowIndex < 0 OrElse rowIndex >= myImage.Height Then
                MessageBox.Show("Please specify a valid row/column index!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            'locate and get color of pixel
            Dim pixelColor As Color = myImage.GetPixel(columnIndex, rowIndex)
            Dim r As Integer = pixelColor.R
            Dim g As Integer = pixelColor.G
            Dim b As Integer = pixelColor.B

            'display RGB values in messagebox
            Dim colorInfo As String = $"R:{r} G:{g} B:{b}"
            MessageBox.Show(colorInfo, "Pixel Color", MessageBoxButtons.OK)

        Catch ex As Exception
            'check if picture box is empty
            If picBox.Image Is Nothing Then
                MessageBox.Show("Please select an image!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            'check if column or row index textboxes are empty
            If String.IsNullOrEmpty(txtColumnIndex.Text) OrElse String.IsNullOrEmpty(txtRowIndex.Text) Then
                MessageBox.Show("Please specify a row/column index!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.ObjectModel

Namespace GridControlSample
    <POCOViewModel> _
    Public Class ViewModel
        Public Overridable Property Items() As ObservableCollection(Of Object)
        Public Sub New()
            Items = New ObservableCollection(Of Object)()
            For i As Integer = 0 To 29
                Dim item As New SampleItem() With {.Id = i, .Name = "Item " & (i Mod 3).ToString(), .Time = Date.Now.AddDays(i)}
                Items.Add(item)
            Next i
        End Sub
    End Class
    Public Class SampleItem
        Public Property Id() As Integer
        Public Property Name() As String
        Public Property Time() As Date
    End Class
End Namespace
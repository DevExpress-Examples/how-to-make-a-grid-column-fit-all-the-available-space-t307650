Imports Microsoft.VisualBasic
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input

Namespace GridControlSample
	Public Class SampleSource
		Implements INotifyPropertyChanged
		Private items_Renamed As ObservableCollection(Of Object)
		Public Property Items() As ObservableCollection(Of Object)
			Get
				Return items_Renamed
			End Get
			Set(ByVal value As ObservableCollection(Of Object))
				If items_Renamed Is value Then
					Return
				End If
				items_Renamed = value
				RaisePropertyChanged("Items")
			End Set
		End Property
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Public Sub New()
			InitItems()
		End Sub
		Private Sub InitItems()
			Items = New ObservableCollection(Of Object)()
			For i As Integer = 0 To 29
				Dim item As New SampleItem() With {.Id = i, .Name = "item " & (i Mod 3).ToString(), .Time = DateTime.Now.AddDays(i)}
				Items.Add(item)
			Next i
		End Sub
		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			If PropertyChangedEvent Is Nothing Then
				Return
			End If
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class

	Public Class SampleItem
		Implements INotifyPropertyChanged
		Private id_Renamed As Integer
		Private name_Renamed As String
		Private time_Renamed As DateTime
		Public Property Id() As Integer
			Get
				Return id_Renamed
			End Get
			Set(ByVal value As Integer)
				If id_Renamed = value Then
					Return
				End If
				id_Renamed = value
				RaisePropertyChanged("Id")
			End Set
		End Property
		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				If name_Renamed = value Then
					Return
				End If
				name_Renamed = value
				RaisePropertyChanged("Name")
			End Set
		End Property
		Public Property Time() As DateTime
			Get
				Return time_Renamed
			End Get
			Set(ByVal value As DateTime)
				If time_Renamed = value Then
					Return
				End If
				time_Renamed = value
				RaisePropertyChanged("Time")
			End Set
		End Property
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub RaisePropertyChanged(ByVal fieldName As String)
			If PropertyChangedEvent Is Nothing Then
				Return
			End If
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(fieldName))
		End Sub
	End Class
End Namespace

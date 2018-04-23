Imports Microsoft.VisualBasic
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Input

Namespace GridControlSample
	Partial Public Class MainWindow
		Inherits Window
		Private source As New SampleSource()
		Public Sub New()
			InitializeComponent()
			DataContext = Me.source
		End Sub
	End Class
End Namespace

Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Native
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace GridControlSample
	Public Class ColumnsLayoutHelper
		Public Shared Function GetAutoWidthColumnFieldName(ByVal obj As DependencyObject) As String
			Return CStr(obj.GetValue(AutoWidthColumnFieldNameProperty))
		End Function
		Public Shared Sub SetAutoWidthColumnFieldName(ByVal obj As DependencyObject, ByVal value As String)
			obj.SetValue(AutoWidthColumnFieldNameProperty, value)
		End Sub
        Public Shared ReadOnly AutoWidthColumnFieldNameProperty As DependencyProperty = DependencyProperty.RegisterAttached("AutoWidthColumnFieldName", GetType(String), GetType(ColumnsLayoutHelper), New PropertyMetadata(Nothing, AddressOf AutoWidthColumnFieldNamePropertyChangedCallback))
		Private Shared Sub AutoWidthColumnFieldNamePropertyChangedCallback(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			If TypeOf obj Is TableView Then
				Dim view = TryCast(obj, TableView)
				If (Not String.IsNullOrEmpty(CStr(e.NewValue))) Then
					view.LayoutCalculatorFactory = New MyGridTableViewLayoutCalculatorFactory()
				Else
					view.LayoutCalculatorFactory = New GridTableViewLayoutCalculatorFactory()
				End If
			End If
		End Sub
	End Class

	Public Class MyGridTableViewLayoutCalculatorFactory
		Inherits GridTableViewLayoutCalculatorFactory
		Public Overrides Function CreateCalculator(ByVal viewInfo As GridViewInfo, ByVal autoWidth As Boolean) As ColumnsLayoutCalculator
			Return New MyColumnsLayoutCalculator(viewInfo)
		End Function
	End Class

	Public Class MyColumnsLayoutCalculator
		Inherits ColumnsLayoutCalculator
		Public Sub New(ByVal viewInfo As GridViewInfo)
			MyBase.New(viewInfo)
		End Sub
		Protected Overrides Sub CalcActualLayoutCore(ByVal arrangeWidth As Double, ByVal layoutAssigner As LayoutAssigner, ByVal showIndicator As Boolean, ByVal needRoundingLastColumn As Boolean, ByVal ignoreDetailButtons As Boolean)
			MyBase.CalcActualLayoutCore(arrangeWidth, layoutAssigner, showIndicator, needRoundingLastColumn, ignoreDetailButtons)

			Dim grid = TryCast(Me.ViewInfo.Grid, GridControl)
			Dim columnsOnHeadersPanel = GetColumnsOnHeadersPanel(grid)

			Dim autoWidthColumnFieldName = ColumnsLayoutHelper.GetAutoWidthColumnFieldName(grid.View)
			Dim autoWidthColumn = columnsOnHeadersPanel.FirstOrDefault(Function(c) c.FieldName = autoWidthColumnFieldName)
			If autoWidthColumn Is Nothing Then
				Return
			End If

			Dim visibleColumnsTotalWidth As Double = 0.0
			For Each column In columnsOnHeadersPanel
				visibleColumnsTotalWidth += layoutAssigner.GetWidth(column)
			Next column

			Dim delta As Double = arrangeWidth - visibleColumnsTotalWidth
			Dim newWidth As Double = layoutAssigner.GetWidth(autoWidthColumn) + delta
			autoWidthColumn.Width = newWidth
		End Sub
		Private Function GetColumnsOnHeadersPanel(ByVal grid As GridControl) As IEnumerable(Of GridColumn)
			Dim res = grid.Columns.Where(Function(c) c.Visible)
			Dim shouldExcludeGroupedColumns As Boolean = (TypeOf grid.View Is TableView) AndAlso Not(TryCast(grid.View, TableView)).ShowGroupedColumns
			If shouldExcludeGroupedColumns Then
				res = res.Where(Function(c) c.GroupIndex = -1)
			End If
			Return res
		End Function
	End Class
End Namespace

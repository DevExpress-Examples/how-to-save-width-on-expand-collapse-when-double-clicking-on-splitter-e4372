Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Namespace DXSample
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub
	End Class

	Public Class SaveWidthContentControl
		Inherits ContentControl

		Public Shared ReadOnly ContentWidthProperty As DependencyProperty = DependencyProperty.Register("ContentWidth", GetType(Double), GetType(SaveWidthContentControl), New UIPropertyMetadata(Double.NaN, New PropertyChangedCallback(AddressOf OnContentWidthChanged)))

		Private Shared Sub OnContentWidthChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			CType(d, SaveWidthContentControl).OnContentWidthChanged()
		End Sub
		Protected Overridable Sub OnContentWidthChanged()
			SetContentWidth(ContentWidth)
		End Sub

		Public Property ContentWidth() As Double
			Get
				Return CDbl(GetValue(ContentWidthProperty))
			End Get
			Set(ByVal value As Double)
				SetValue(ContentWidthProperty, value)
			End Set
		End Property

		Private Sub SetContentWidth(ByVal width As Double)
			Dim controlFromContent As FrameworkElement = TryCast(Content, FrameworkElement)
			If controlFromContent Is Nothing Then
				Return
			End If

			controlFromContent.Width = width
		End Sub

		Protected Overrides Sub OnPropertyChanged(ByVal e As DependencyPropertyChangedEventArgs)
			MyBase.OnPropertyChanged(e)
			If e.Property.Name = "Width" Then
				Dim newWidth As Double = CDbl(e.NewValue)
				If (newWidth <> 0) AndAlso (Not Double.IsNaN(newWidth)) Then
					SetContentWidth(newWidth)
					Width = Double.NaN
				End If
			ElseIf e.Property.Name = "Content" Then
				SetContentWidth(ContentWidth)
			End If
		End Sub
	End Class
End Namespace

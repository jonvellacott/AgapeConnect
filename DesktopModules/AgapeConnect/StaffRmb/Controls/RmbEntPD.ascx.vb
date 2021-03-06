﻿
Partial Class controls_RmbEntPD
    Inherits Entities.Modules.PortalModuleBase
    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim FileName As String = System.IO.Path.GetFileNameWithoutExtension(Me.AppRelativeVirtualPath)
        If Not (Me.ID Is Nothing) Then
            'this will fix it when its placed as a ChildUserControl 
            Me.LocalResourceFile = Me.LocalResourceFile.Replace(Me.ID, FileName)
        Else
            ' this will fix it when its dynamically loaded using LoadControl method 
            Me.LocalResourceFile = Me.LocalResourceFile & FileName & ".ascx.resx"
            Dim Locale = System.Threading.Thread.CurrentThread.CurrentCulture.Name
            Dim AppLocRes As New System.IO.DirectoryInfo(Me.LocalResourceFile.Replace(FileName & ".ascx.resx", ""))
            If Locale = PortalSettings.CultureCode Then
                'look for portal varient
                If AppLocRes.GetFiles(FileName & ".ascx.Portal-" & PortalId & ".resx").Count > 0 Then
                    Me.LocalResourceFile = Me.LocalResourceFile.Replace("resx", "Portal-" & PortalId & ".resx")
                End If
            Else

                If AppLocRes.GetFiles(FileName & ".ascx." & Locale & ".Portal-" & PortalId & ".resx").Count > 0 Then
                    'lookFor a CulturePortalVarient
                    Me.LocalResourceFile = Me.LocalResourceFile.Replace("resx", Locale & ".Portal-" & PortalId & ".resx")
                ElseIf AppLocRes.GetFiles(FileName & ".ascx." & Locale & ".resx").Count > 0 Then
                    'look for a CultureVarient
                    Me.LocalResourceFile = Me.LocalResourceFile.Replace("resx", Locale & ".resx")
                ElseIf AppLocRes.GetFiles(FileName & ".ascx.Portal-" & PortalId & ".resx").Count > 0 Then
                    'lookFor a PortalVarient
                    Me.LocalResourceFile = Me.LocalResourceFile.Replace("resx", "Portal-" & PortalId & ".resx")
                End If
            End If
        End If
    End Sub



    Public Property Comment() As String
        Get
            Return tbDesc.Text
        End Get
        Set(ByVal value As String)
            tbDesc.Text = value
        End Set
    End Property
    Public Property theDate() As Date
        Get
            Return CDate(dtDate.Text)
        End Get
        Set(ByVal value As Date)
            If value = Nothing Then
                dtDate.Text = Today.ToShortDateString
            Else
                dtDate.Text = value
            End If

        End Set
    End Property
    Public Property VAT() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
         


        End Set
    End Property

    Public Property Amount() As Double
        Get
            Return CDbl(tbAmount.Text)
        End Get
        Set(ByVal value As Double)
            tbAmount.Text = value.ToString("n2", New CultureInfo("en-US"))
        End Set
    End Property
    Public Property Taxable() As Boolean
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    Public Property Spare1() As String ' No of People
        Get
            Return DropDownList1.SelectedValue
        End Get
        Set(ByVal value As String)
            If value = "" Then
                DropDownList1.SelectedValue = 1
            Else
                DropDownList1.SelectedValue = value
            End If
        End Set
    End Property
    Public Property Spare2() As String ' No of Days
        Get
            Return DropDownList3.SelectedValue
        End Get
        Set(ByVal value As String)
            If value = "" Then
                DropDownList3.SelectedValue = 1
            Else
                DropDownList3.SelectedValue = value
            End If
        End Set
    End Property
    Public Property Spare3() As String ' Per Diem Rate
        Get
            Return DropDownList2.SelectedValue
        End Get
        Set(ByVal value As String)
            If value = "" Then
                DropDownList2.SelectedIndex = 0
            Else
                DropDownList2.SelectedValue = value
            End If
        End Set
    End Property
    Public Property Spare4() As String
        Get
            Return Nothing
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property Spare5() As String
        Get
            Return Nothing
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property Receipt() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
    Public Property ErrorText() As String
        Get
            Return ""
        End Get
        Set(ByVal value As String)
            ErrorLbl.Text = value
        End Set
    End Property
    Public Function ValidateForm(ByVal userId As Integer) As Boolean
        If tbDesc.Text = "" Then
            ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("Description.Error", LocalResourceFile)
            Return False
        End If
        Try
            Dim theDate As Date = dtDate.Text
            If theDate > Today Then
                ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("OldDate.Error", LocalResourceFile)
                Return False
            End If
        Catch ex As Exception
            ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("Date.Error", LocalResourceFile)
            Return False
        End Try

        Try
            Dim theAmount As Double = Double.Parse(tbAmount.Text, New CultureInfo("en-US").NumberFormat)
            If theAmount > CDbl(lblMaxAmt.Text) Then
                ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("Limit.Error", LocalResourceFile)
                Return False
            End If
            If theAmount <= 0 Then
                ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("Amount.Error", LocalResourceFile)
                Return False
            End If
           
        Catch ex As Exception
            ErrorLbl.Text = DotNetNuke.Services.Localization.Localization.GetString("Amount.Error", LocalResourceFile)
            Return False
        End Try
        ErrorLbl.Text = ""
        Return True
    End Function
    Public Sub Initialize(ByVal Settings As Hashtable)
        DropDownList2.Items(0).Value = Settings("EntDay")
        DropDownList2.Items(1).Value = Settings("EntBreakfast")
        DropDownList2.Items(2).Value = Settings("EntLunch")
        DropDownList2.Items(3).Value = Settings("EntDinner")
        DropDownList2.Items(4).Value = Settings("EntOvernight")


    End Sub
    Protected Sub UpdatePanel1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatePanel1.PreRender
        Try

            Dim cur = StaffBrokerFunctions.GetSetting("Currency", PortalId)
            lblMaxAmt.Text = (DropDownList1.SelectedValue * DropDownList2.SelectedValue * DropDownList3.SelectedValue).ToString("0.00")
            lblCur.Text = cur
        Catch ex As Exception

        End Try

    End Sub
End Class



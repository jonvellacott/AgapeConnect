Imports DotNetNuke
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Math
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Collections.Specialized
Imports System.Linq
Imports Stories
Namespace DotNetNuke.Modules.Stories


    Partial Class AddEditStory
        Inherits Entities.Modules.PortalModuleBase


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Settings("Aspect") <> "" Then
                acImage1.Aspect = Double.Parse(Settings("Aspect"), New CultureInfo(""))
            End If




            If Not Page.IsPostBack Then



                Dim mc As New DotNetNuke.Entities.Modules.ModuleController


                Dim allTabs = mc.GetAllTabsModulesByModuleID(ModuleId)

                Dim channels As New Dictionary(Of Integer, String)

                For Each row As DotNetNuke.Entities.Modules.ModuleInfo In allTabs
                    'Check permissions.
                    If DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(row) Then
                        Dim name As String = row.ModuleControl.ControlTitle
                        If String.IsNullOrEmpty(name) Then
                            name = row.ParentTab.TabName
                        End If
                        channels.Add(row.TabModuleID, name)
                    End If
                Next

                ddlChannels.DataSource = channels
                ddlChannels.DataTextField = "Value"
                ddlChannels.DataValueField = "Key"
                ddlChannels.DataBind()



                Dim d As New StoriesDataContext





                If Me.UserInfo.IsSuperUser And IsEditable() Then
                    'SuperPowers.Visible = True
                End If
                PagePanel.Visible = True
                NotFoundLabel.Visible = False
                If Request.QueryString("StoryID") <> "" Then



                    StoryIdHF.Value = Request.QueryString("StoryId")

                    Dim r = (From c In d.AP_Stories Where c.StoryId = Request.QueryString("StoryID")).First

                    If channels.Where(Function(x) x.Key = r.TabModuleId).Count > 0 Then
                        ddlChannels.SelectedValue = r.TabModuleId
                    Else
                        'Look for tab (which you don't have permission for
                        Dim theTab = From c As DotNetNuke.Entities.Modules.ModuleInfo In allTabs Where c.TabModuleID = r.TabModuleId
                        If theTab.Count > 0 Then
                            Dim name As String = theTab.First.ModuleControl.ControlTitle
                            If String.IsNullOrEmpty(name) Then
                                name = theTab.First.ParentTab.TabName
                            End If

                            ddlChannels.Items.Add(New ListItem(r.TabModuleId, name))
                        End If

                        'just use the current tab
                        ddlChannels.SelectedValue = TabModuleId

                    End If




                    Headline.Text = r.Headline
                    ' imgbtnPrint.OnClientClick = "window.open('/DesktopModules/FullStory/PrintStory.aspx?StoryId=" & Request.QueryString("StoryId") & "', '_blank'); "
                    If Me.UserInfo.IsSuperUser Then

                        If Not Page.IsPostBack Then



                            Dim BoostDate As String
                            If r.EditorBoost <= Date.Now Then
                                BoostLabel.Text = "Not currently boosted."
                            Else
                                BoostDate = r.EditorBoost.Value.ToShortDateString()
                                BoostLabel.Text = "Boosted until " & BoostDate
                            End If
                            Editable.Checked = r.Editable

                        End If

                    End If

                    StoryText.Text = r.StoryText
                    Author.Text = r.Author
                    tbLocation.Text = r.Latitude.Value.ToString(New CultureInfo("")) & ", " & r.Longitude.Value.ToString(New CultureInfo(""))

                    StoryDate.Text = r.StoryDate.ToString("dd MMM yyyy")


                    ' Dim thePhoto = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(r.PhotoId)
                    '  StoryImage.ImageUrl = DotNetNuke.Services.FileSystem.FileManager.Instance.GetUrl(thePhoto)
                    acImage1.FileId = r.PhotoId
                    PhotoIdHF.Value = r.PhotoId
                Else

                    If String.IsNullOrEmpty(Session("Long")) Or String.IsNullOrEmpty(Session("Lat")) Then
                        Dim ls As New LookupService(Server.MapPath("~/App_Data/GeoLiteCity.dat"), LookupService.GEOIP_STANDARD)
                        ' Dim l As Location = ls.getRegion(Request.ServerVariables("remote_addr"))

                        Dim l As Location = ls.getLocation("80.193.180.102")   '(Solihill)
                        Session("Long") = l.longitude
                        Session("Lat") = l.latitude
                    End If

                    Dim lg As Double = Session("Long")
                    Dim lt As Double = Session("Lat")
                    tbLocation.Text = lt.ToString(New CultureInfo("")) & ", " & lg.ToString(New CultureInfo(""))

                    StoryDate.Text = Today.ToString("dd MMM yyyy")
                    StoryText.Text = "Enter your news here..."

                    Author.Text = UserInfo.DisplayName
                    ddlChannels.SelectedValue = TabModuleId
                End If

            End If





        End Sub








        Protected Sub CancelPowerBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelPowerBtn.Click
            'Dim d As New FullStoryDataContext
            'Dim r = From c In d.Agape_Main_Story_Stories Where c.StoryId = CInt(Request.QueryString("StoryID"))
            'If r.Count > 0 Then
            '    TeamList.DataBind()
            '    MinistryList.DataBind()
            '    MainSite.Checked = r.First.MainSite
            '    AscTeamDDL.SelectedValue = r.First.TeamId
            '    Dim BoostDate As String
            '    If r.First.EditorBoost <= Date.Now Then
            '        BoostLabel.Text = "Not currently boosted."
            '    Else
            '        BoostDate = r.First.EditorBoost.Value.ToShortDateString()
            '        BoostLabel.Text = "Boosted until " & BoostDate
            '    End If
            '    Editable.Checked = r.First.Editable
            '    For Each row As ListItem In TeamList.Items
            '        Dim TeamMeta = d.Agape_Main_Story_RetrieveTeamMeta(row.Value, Request.QueryString("StoryID"))
            '        For Each row2 In TeamMeta
            '            row.Selected = row2.Checked
            '        Next
            '    Next
            '    For Each row3 As ListItem In MinistryList.Items
            '        Dim MinMeta = d.Agape_Main_Story_RetrieveMinistryMeta(row3.Value, Request.QueryString("StoryID"))
            '        For Each row4 In MinMeta
            '            row3.Selected = row4.Checked
            '        Next
            '    Next
            'End If

        End Sub

        Protected Sub UpdatePowerBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatePowerBtn.Click
            'Try

            '    Dim d As New FullStoryDataContext
            '    Dim q = From c In d.Agape_Main_Story_Stories Where c.StoryId = CInt(Request.QueryString("StoryID"))
            '    Dim Exister As Boolean = False
            '    For Each row In q
            '        If row.TeamId = AscTeamDDL.SelectedValue Then
            '            Exister = True
            '        End If
            '    Next
            '    If Exister = False Then
            '        Dim u = From c In d.Agape_Main_Story_Stories Where c.StoryId = CInt(Request.QueryString("StoryID"))
            '        u.First.TeamId = AscTeamDDL.SelectedValue
            '        d.SubmitChanges()
            '    End If


            '    Dim s = From c In d.Agape_Main_Story_TeamMetas Where c.StoryId = CInt(Request.QueryString("StoryID"))
            '    d.Agape_Main_Story_TeamMetas.DeleteAllOnSubmit(s)
            '    d.SubmitChanges()
            '    For Each row As ListItem In TeamList.Items
            '        Dim MetaMult As Agape_Main_Story_TeamMeta = New Agape_Main_Story_TeamMeta
            '        MetaMult.StoryId = CInt(Request.QueryString("StoryID"))
            '        If row.Selected = True Then
            '            MetaMult.TeamId = row.Value
            '            d.Agape_Main_Story_TeamMetas.InsertOnSubmit(MetaMult)
            '        End If
            '        d.SubmitChanges()
            '    Next

            '    Dim t = From c In d.Agape_Main_Story_MinistryMetas Where c.StoryId = CInt(Request.QueryString("StoryID"))
            '    d.Agape_Main_Story_MinistryMetas.DeleteAllOnSubmit(t)
            '    For Each thing As ListItem In MinistryList.Items
            '        Dim MetaMult1 As Agape_Main_Story_MinistryMeta = New Agape_Main_Story_MinistryMeta
            '        MetaMult1.StoryId = CInt(Request.QueryString("StoryID"))
            '        If thing.Selected = True Then
            '            MetaMult1.MinistryId = thing.Value
            '            d.Agape_Main_Story_MinistryMetas.InsertOnSubmit(MetaMult1)
            '        End If
            '        d.SubmitChanges()
            '    Next
            '    Dim r = From c In d.Agape_Main_Story_Stories Where c.StoryId = CInt(Request.QueryString("StoryID"))
            '    If r.Count > 0 Then
            '        r.First.MainSite = MainSite.Checked
            '        r.First.TeamId = AscTeamDDL.SelectedValue
            '        r.First.Editable = Editable.Checked
            '        d.SubmitChanges()
            '    End If

            'Catch ex As Exception
            '    Response.Write(ex)
            'End Try
        End Sub

        Protected Sub BoostButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BoostButton.Click
            'Dim d As New FullStoryDataContext
            'If (BoostButton.Text = "Boost") Then
            '    Dim q = d.Agape_Main_Story_BoostStory(Request.QueryString("StoryID"))
            '    BoostLabel.Text = "Boosted until " & q.First.EditorBoost.Value.ToShortDateString()
            '    BoostButton.Text = "Un-Boost"
            'Else
            '    Dim q = (From c In d.Agape_Main_Story_Stories Where c.StoryId = Request.QueryString("StoryID") Select c).First
            '    q.EditorBoost = DateAdd(DateInterval.Day, -2, Now)
            '    d.SubmitChanges()


            '    BoostLabel.Text = "Not Boosted"
            '    BoostButton.Text = "Boost"
            'End If


        End Sub
        Protected Sub SendAbuseEmail(ByVal CommentId As Integer)
            'Dim d As New FullStoryDataContext
            'Dim StoryName As String
            'Dim q = From c In d.Agape_Main_Story_Stories Where c.StoryId = Request.QueryString("StoryID") Select c.Headline
            'If q.Count > 0 Then
            '    StoryName = CStr(q.First)
            'Else
            '    StoryName = "There has been an error in the comment system."
            'End If

            'Dim AbuseComment As String
            'Dim r = From c In d.Agape_Main_Story_Comments Where c.StoryId = Request.QueryString("StoryID") And c.CommentId = CommentId Select c.CommentText
            'If r.Count > 0 Then
            '    AbuseComment = CStr(r.First)
            'Else
            '    AbuseComment = ""
            'End If


            'Dim EmailText As String = "<b>Abuse Report</b><br/><br/>"
            'EmailText = EmailText & "Someone has reported the following comment in the <i>" & StoryName & "</i> Story as abusive:<br/><br/>"
            'EmailText = EmailText & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>" & AbuseComment & "</i><br/><br/>"
            'EmailText = EmailText & "The comment is currently invisible, please either reinstate the comment or delete it when you can.<br/><br/>"
            'EmailText = EmailText & "<i>The Agape Website</i>"

            'Dim EmailAddress As String
            'Dim DisplayName1 As String
            'Dim s = From c In d.Agape_Main_Story_Stories Where c.StoryId = Request.QueryString("StoryID") Select c.UserId

            'Dim Super = From c In d.Users Join b In d.UserRoles On c.UserID Equals b.UserID Join a In d.Roles On b.RoleID Equals a.RoleID Where a.RoleName = "Super Editor" Select c.DisplayName, c.Email
            'If s.Count > 0 Then
            '    Dim t = From c In d.Users Where c.UserID = s.First Select c.Email, c.DisplayName
            '    If t.Count > 0 Then
            '        EmailAddress = t.First.Email
            '        DisplayName1 = t.First.DisplayName
            '        DotNetNuke.Services.Mail.Mail.SendMail("donotreply@agape.org.uk", EmailAddress, "", "Agape Website Abusive Comment", EmailText, "", "HTML", "", "", "", "")
            '    Else
            '        For Each editor In Super
            '            DotNetNuke.Services.Mail.Mail.SendMail("donotreply@agape.org.uk", editor.Email, "", "Agape Website Abusive Comment", EmailText, "", "HTML", "", "", "", "")
            '        Next
            '    End If
            'Else
            '    For Each editor In Super
            '        DotNetNuke.Services.Mail.Mail.SendMail("donotreply@agape.org.uk", editor.Email, "", "Agape Website Abusive Comment", EmailText, "", "HTML", "", "", "", "")
            '    Next
            'End If



        End Sub
        Public Function IsDeleteable(ByVal ThisCommentId As Integer) As Boolean
            Dim Out As Boolean = False

            'Dim d As New FullStoryDataContext
            'Dim q = From c In d.Agape_Main_Story_Comments Where c.CommentId = ThisCommentId
            'If q.Count > 0 Then
            '    If q.First.UserId = Me.UserId Then
            '        Out = True
            '    End If
            'End If

            'If IsEditable Then
            '    Out = True
            'End If

            Return Out
        End Function




        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Dim d As New StoriesDataContext


            Dim q = From c In d.AP_Stories Where c.StoryId = Request.QueryString("StoryId")

            If q.Count > 0 Then
                q.First.StoryText = StoryText.Text
                q.First.Headline = Headline.Text
                q.First.Author = Author.Text
                Try
                    Dim geoLoc = tbLocation.Text.Split(",")
                    If geoLoc.Count = 2 Then

                        q.First.Latitude = Double.Parse(geoLoc(0).Replace(" ", ""), New CultureInfo(""))
                        q.First.Longitude = Double.Parse(geoLoc(1).Replace(" ", ""), New CultureInfo(""))

                    End If
                Catch ex As Exception

                End Try


                If acImage1.CheckAspect() Then

                    q.First.PhotoId = acImage1.FileId
                    d.SubmitChanges()
                Else
                    Return
                End If
                Response.Redirect(EditUrl("ViewStory") & "?StoryId=" & Request.QueryString("StoryId"))
            Else
                Dim insert As New AP_Story
                insert.Headline = Headline.Text
                insert.Author = Author.Text
                If acImage1.CheckAspect() Then
                    insert.PhotoId = acImage1.FileId
                Else
                    Return
                End If
                insert.StoryDate = Today
                insert.StoryText = StoryText.Text
                insert.PortalID = PortalId
                insert.Sent = False
                insert.RegionId = 0
                insert.Editable = True
                insert.EditorBoost = Today
                insert.IsVisible = True
                insert.UserId = UserId
                insert.TabId = TabId
                insert.TabModuleId = CInt(ddlChannels.SelectedValue)
                Try
                    Dim geoLoc = tbLocation.Text.Split(",")
                    If geoLoc.Count = 2 Then

                        insert.Latitude = Double.Parse(geoLoc(0).Replace(" ", ""), New CultureInfo(""))
                        insert.Longitude = Double.Parse(geoLoc(1).Replace(" ", ""), New CultureInfo(""))

                    End If
                Catch ex As Exception

                End Try
                d.AP_Stories.InsertOnSubmit(insert)
                d.SubmitChanges()
                Dim feeds = From c In d.AP_Stories_Module_Channels Where c.URL.EndsWith("?channel=" & insert.TabModuleId)

                For Each row In feeds
                    StoryFunctions.RefreshFeed(row.AP_Stories_Module.TabModuleId, row.ChannelId)
                    StoryFunctions.PrecalAllCaches(row.AP_Stories_Module.TabModuleId)
                Next


                Response.Redirect(EditUrl("ViewStory") & "?StoryId=" & insert.StoryId)
            End If


        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            If Request.QueryString("StoryId") = "" Then
                Response.Redirect(NavigateURL())
            Else
                Response.Redirect(EditUrl("ViewStory") & "?StoryId=" & Request.QueryString("StoryId"))

            End If
        End Sub

        Protected Sub acImage1_Updated() Handles acImage1.Updated
            Dim d As New StoriesDataContext
            Dim q = From c In d.AP_Stories Where c.StoryId = Request.QueryString("StoryId")

            If q.Count > 0 Then

                If acImage1.CheckAspect() Then
                    PhotoIdHF.Value = acImage1.FileId
                    q.First.PhotoId = acImage1.FileId
                    d.SubmitChanges()
                    'If Settings("Aspect") <> "" Then
                    '    acImage1.Aspect = Settings("Aspect")
                    'End If
                    ' acImage1.LazyLoad()
                End If

            End If

        End Sub
    End Class
End Namespace
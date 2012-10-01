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
Namespace DotNetNuke.Modules.FullStory


    Partial Class ViewFullStory
        Inherits Entities.Modules.PortalModuleBase
        Public IsBoosted As Boolean = False
        Public IsBlocked As Boolean = False
        Public location As String = ""
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim d As New StoriesDataContext
            Dim r = (From c In d.AP_Stories Where c.StoryId = Request.QueryString("StoryID")).First
            Dim thecache = From c In d.AP_Stories_Module_Channel_Caches Where c.AP_Stories_Module_Channel.AP_Stories_Module.TabModuleId = r.TabModuleId And c.Link.EndsWith("StoryId=" & r.StoryId)
           
            If Not String.IsNullOrEmpty(Request.Form("Boosted")) Then

                If thecache.Count > 0 Then
                    thecache.First.Block = CBool(Request.Form("Blocked"))
                    If Not thecache.First.Block Then

                        If CBool(Request.Form("Boosted")) Then
                            If Not thecache.First.BoostDate Is Nothing Then
                                If thecache.First.BoostDate < Today Then
                                    thecache.First.BoostDate = Today.AddDays(7)

                                End If
                            Else
                                thecache.First.BoostDate = Today.AddDays(7)

                            End If
                        Else
                            thecache.First.BoostDate = Nothing
                        End If
                    Else
                        thecache.First.BoostDate = Nothing
                    End If
                    d.SubmitChanges()
                End If
                Return
            End If



            If String.IsNullOrEmpty(Request.QueryString("StoryID")) Then
                PagePanel.Visible = False
                NotFoundLabel.Visible = True

            Else
                btnEdit.Visible = IsEditable
                btnNew.Visible = IsEditable

                If Me.UserInfo.IsSuperUser And IsEditable() Then
                    'SuperPowers.Visible = True
                End If
                PagePanel.Visible = True
                NotFoundLabel.Visible = False
                StoryIdHF.Value = Request.QueryString("StoryId")




                Headline.Text = r.Headline

                location = r.Latitude.Value.ToString(New CultureInfo("")) & ", " & r.Longitude.Value.ToString(New CultureInfo(""))
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


                If CType(Settings("Justify"), String) <> "" And CType(Settings("Block"), String) <> "" Then
                    If (Settings("Justify") = "Center") Then
                        StoryText.Controls.Add(New LiteralControl("<Style>.Agape_video" & ModuleId & " {float: none; margin: 10px;text-align: center; }</Style>"))
                    ElseIf Settings("Block") = "Block" Then
                        StoryText.Controls.Add(New LiteralControl("<Style>.Agape_video" & ModuleId & " {float: none; margin: 10px;text-align:" & Settings("Justify") & "; }</Style>"))
                    Else
                        StoryText.Controls.Add(New LiteralControl("<Style>.Agape_video" & ModuleId & " {float: " & Settings("Justify") & "; margin: 10px; }</Style>"))

                    End If
                End If



                StoryText.Text = r.StoryText
                Author.Text = r.Author
                StoryDate.Text = r.StoryDate.ToString("d MMM yyyy")
                Dim thePhoto = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(r.PhotoId)
                StoryImage.ImageUrl = DotNetNuke.Services.FileSystem.FileManager.Instance.GetUrl(thePhoto)

                PhotoIdHF.Value = r.PhotoId
                pnlLanguages.Visible = False
                If Not String.IsNullOrEmpty(r.TranslationGroup) Then



                    Dim Translist = From c In d.AP_Stories Where c.TranslationGroup = r.TranslationGroup And c.PortalID = r.PortalID Select c.Language, c.StoryId

                    If Translist.Count > 1 Then
                        pnlLanguages.Visible = True
                        dlLanuages.DataSource = Translist
                        dlLanuages.DataBind()

                    End If


                End If
                'Get Current Channel 

                If thecache.Count > 0 Then
                    If thecache.First.Block Then
                        lblPowerStatus.Text = "This story has been blocked, and won't appear in the channel feed."
                        IsBlocked = True
                    ElseIf Not thecache.First.BoostDate Is Nothing Then
                        If thecache.First.BoostDate >= Today Then
                            IsBoosted = True
                            lblPowerStatus.Text = "Boosted until " & thecache.First.BoostDate.Value.ToString("dd MMM yyyy")

                        End If
                    End If
                End If



            End If





        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
            Response.Redirect(EditUrl("AddEditStory") & "?StoryID=" & Request.QueryString("StoryID"))

        End Sub
        Public Function GetLanguageName(ByVal language As String) As String
            
            Dim thename = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(Function(x) x.Name.ToLower = language.ToLower).Select(Function(x) x.EnglishName & " / " & x.NativeName)
            If thename.Count > 0 Then
                Return thename.First()
            Else
                Return ""
            End If
        End Function
       
        Public Function GetFlag(ByVal language As String) As String
            If String.IsNullOrEmpty(language) Then
                Return ""
            End If
            If language = "en" Then
                language = "en-GB"

            ElseIf language.Length = 2 Then
                language = language.ToLower & "-" & language.ToUpper

            End If


            Dim flagDir = New DirectoryInfo(Server.MapPath("/images/Flags/"))
            If Not flagDir Is Nothing Then

                Dim flags = flagDir.GetFiles().Where(Function(x) x.Name.ToLower.Contains(language.ToLower))

                If flags.Count = 0 Then
                    Return ""  ' couldn't find flag
                Else
                    Return "/images/Flags/" & flags.First.Name

                End If
            Else
                Return ""
            End If

        End Function

       

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

      

       
        Protected Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
            Response.Redirect(EditUrl("AddEditStory"))
        End Sub
    End Class
End Namespace
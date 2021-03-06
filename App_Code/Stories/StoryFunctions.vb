﻿Imports Microsoft.VisualBasic
Imports System.ServiceModel.Syndication
Imports System.Xml
Imports System.Net
Imports DotNetNuke
Namespace Stories
    Class StoryController
        Implements Entities.Modules.ISearchable

        Public Function GetSearchItems(ModInfo As DotNetNuke.Entities.Modules.ModuleInfo) As DotNetNuke.Services.Search.SearchItemInfoCollection Implements DotNetNuke.Entities.Modules.ISearchable.GetSearchItems
            Dim d As New StoriesDataContext
            Dim SearchItemCollection As New Services.Search.SearchItemInfoCollection
            Dim Stories = From c In d.AP_Stories Where c.PortalID = ModInfo.PortalID And c.IsVisible = True

            'From c In d.AP_Stories_Module_Channel_Caches Where c.AP_Stories_Module_Channel.AP_Stories_Module.TabModuleId = ModInfo.TabModuleID()

            For Each row In Stories

                Dim SearchText = (row.Headline & " " & row.StoryText & " " & row.Author)


                Dim SearchItem As Services.Search.SearchItemInfo
                SearchItem = New Services.Search.SearchItemInfo _
                 (row.Headline, _
                Left(StoryFunctions.StripTags(row.StoryText), 500) & "...", _
                row.UserId, _
               row.StoryDate, ModInfo.ModuleID, _
                 "S" & row.StoryId, _
              SearchText, Guid:="StoryId=" & row.StoryId, Image:=row.PhotoId, TabID:=row.TabId)
                SearchItemCollection.Add(SearchItem)
            Next



            Return SearchItemCollection
        End Function

    End Class

End Namespace

Public Class StoryFunctions
    Public Shared Function StripTags(ByVal HTML As String) As String
        ' Removes tags from passed HTML

        Dim pattern As String = "<(.|\n)*?>"
        Dim pattern2 As String = "\[.*?]]\]"
        Dim pattern3 As String = "\[.*?]\]"
        Dim pattern4 As String = "<script[\d\D]*?>[\d\D]*?</script>"
        Dim pattern5 As String = "<style[\d\D]*?>[\d\D]*?</style>"
        Dim s = HTML
        s = Regex.Replace(s, pattern4, String.Empty)
        s = Regex.Replace(s, pattern5, String.Empty)
        s = Regex.Replace(s, pattern, String.Empty)

        Return Regex.Replace(Regex.Replace(s, pattern2, String.Empty), pattern3, String.Empty).Trim()


    End Function


    Private Shared Sub set_if(ByRef setting As Object, ByVal value As Object)
        If value Is Nothing Then
            Return
        Else
            setting = value

        End If
    End Sub

    Public Shared Function GetStoryModule(ByVal TabModuleId As Integer) As Stories.AP_Stories_Module
        Dim d As New Stories.StoriesDataContext

        If d.AP_Stories_Modules.Where(Function(x) x.TabModuleId = TabModuleId).Count = 0 Then
            Dim insert As New Stories.AP_Stories_Module
            insert.TabModuleId = TabModuleId
            insert.FilterType = 0
            insert.MaxDisplayItems = 15

            d.AP_Stories_Modules.InsertOnSubmit(insert)
            d.SubmitChanges()
        End If

        Return (From c In d.AP_Stories_Modules Where c.TabModuleId = TabModuleId).First
    End Function




    Public Shared Sub RefreshFeed(ByVal tabModuleId As Integer, ByVal ChannelId As Integer, Optional ByVal ClearCache As Boolean = False)




        Dim d As New Stories.StoriesDataContext

        If d.AP_Stories_Modules.Where(Function(x) x.TabModuleId = tabModuleId).Count = 0 Then
            Dim insert As New Stories.AP_Stories_Module
            insert.TabModuleId = tabModuleId
            insert.FilterType = 0
            insert.MaxDisplayItems = 15
            d.AP_Stories_Modules.InsertOnSubmit(insert)
            d.SubmitChanges()
        End If

        Dim theModule = (From c In d.AP_Stories_Modules Where c.TabModuleId = tabModuleId).First

        'Dim reader = XmlReader.Create("http://rss.cnn.com/rss/edition.rss")
        ' Dim reader = XmlReader.Create("http://feeds.bbci.co.uk/news/rss.xml")
        ' Dim reader = XmlReader.Create("http://www.agapeeurope.com/?feed=rss2")

        Try





            'Refresh the feed


            If ClearCache Then
                ' d.AP_Stories_Module_Channel_Caches.DeleteAllOnSubmit(theModule.AP_Stories_Module_Channels.Where(Function(x) x.ChannelId = ChannelId).First.AP_Stories_Module_Channel_Caches.Where(Function(x) x.Block <> True And (x.BoostDate Is Nothing Or x.BoostDate < Today)))
                'd.SubmitChanges()
            End If

            Dim theChannel = (From c In theModule.AP_Stories_Module_Channels Where c.ChannelId = ChannelId).First
            Dim reader = XmlReader.Create(theChannel.URL)
            Dim feed = SyndicationFeed.Load(reader)
            'If Not feed.BaseUri Is Nothing Then
            '    set_if(theChannel.URL, feed.BaseUri.AbsoluteUri)
            'End If
            'If Not feed.Title Is Nothing Then
            '    set_if(theChannel.ChannelTitle, Left(feed.Title.Text, 154))
            'End If

            'set_if(theChannel.Language, feed.Language)



            For Each row In feed.Items
                Try


                    Dim existingStory = From c In theChannel.AP_Stories_Module_Channel_Caches Where c.Link = row.Links.First.Uri.AbsoluteUri
                    If existingStory.Count = 0 Then
                        Dim insert As New Stories.AP_Stories_Module_Channel_Cache
                        If Not row.Title Is Nothing Then
                            insert.Headline = Left(row.Title.Text, 154)
                        End If
                        If Not row.Summary Is Nothing Then
                            insert.Description = Left(row.Summary.Text, 500)
                        End If
                        insert.ChannelId = theChannel.ChannelId

                        insert.Link = row.Links.First.Uri.AbsoluteUri
                        insert.Block = False
                        insert.Precal = 0

                        'Story Location
                        If row.ElementExtensions.Where(Function(x) x.OuterName = "lat").Count > 0 And row.ElementExtensions.Where(Function(x) x.OuterName = "long").Count > 0 Then
                            insert.Latitude = row.ElementExtensions.Where(Function(x) x.OuterName = "lat").First.GetObject(Of XElement).Value
                            insert.Longitude = row.ElementExtensions.Where(Function(x) x.OuterName = "long").First.GetObject(Of XElement).Value
                        Else
                            insert.Latitude = theChannel.Latitude
                            insert.Longitude = theChannel.Longitude
                        End If
                        Try


                            If row.ElementExtensions.Where(Function(x) x.OuterName = "translationGroup").Count > 0 Then
                                insert.TranslationGroup = CInt(row.ElementExtensions.Where(Function(x) x.OuterName = "translationGroup").First.GetObject(Of XElement).Value)
                            End If




                            If row.ElementExtensions.Where(Function(x) x.OuterName = "language").Count > 0 Then
                                insert.Langauge = row.ElementExtensions.Where(Function(x) x.OuterName = "language").First.GetObject(Of XElement).Value

                            ElseIf theChannel.AutoDetectLanguage Then

                                Dim req = "https://www.googleapis.com/language/translate/v2/detect?key=AIzaSyBCSoev7-yyoFLIBOcsnbJqcNifaLwOnPc&q=" & HttpUtility.UrlEncode(Left(insert.Description, 80))

                                Dim web As New WebClient()

                                Dim response = web.DownloadString(req)
                                If response.IndexOf("""language"": """) > 0 Then
                                    Dim lang = response.Substring(response.IndexOf("""language"": """) + 13)
                                    If lang.IndexOf(""",") > 0 Then
                                        lang = lang.Substring(0, lang.IndexOf(""","))
                                        If lang.Length >= 2 Then
                                            insert.Langauge = Left(lang, 8)
                                        End If
                                    End If
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                        If insert.Langauge Is Nothing Then
                            insert.Langauge = theChannel.Language
                        End If
                        ' insert.TranslationGroup = row.Id






                        If row.PublishDate = Nothing Then
                            insert.StoryDate = Today
                        Else
                            insert.StoryDate = row.PublishDate.DateTime
                        End If

                        insert.Clicks = 1
                        SetImage(insert, row, theChannel.ImageId)

                        d.AP_Stories_Module_Channel_Caches.InsertOnSubmit(insert)
                    Else
                        If Not row.Title Is Nothing Then
                            existingStory.First.Headline = Left(row.Title.Text, 154)
                        End If
                        If Not row.Summary Is Nothing Then
                            existingStory.First.Description = Left(row.Summary.Text, 500)
                        End If

                        set_if(existingStory.First.StoryDate, row.PublishDate.DateTime)
                        Try

                       
                        If row.ElementExtensions.Where(Function(x) x.OuterName = "translationGroup").Count > 0 Then
                                existingStory.First.TranslationGroup = CInt(row.ElementExtensions.Where(Function(x) x.OuterName = "translationGroup").First.GetObject(Of XElement).Value)
                        End If

                        Catch ex As Exception

                        End Try


                    End If
                Catch ex As Exception
                    'If a story wont load, just skip to the nect one..
                End Try
            Next


            d.SubmitChanges()

        Catch ex As Exception

        End Try



    End Sub

    Public Shared Sub PrecalAllCaches(ByVal TabModuleId As Integer)
        Dim mc As New DotNetNuke.Entities.Modules.ModuleController

        Dim tm = mc.GetTabModule(TabModuleId)
        Dim recentWeight As Double = 0

        If (tm.TabModuleSettings("WeightRecent") <> "") Then
            recentWeight = CDbl(tm.TabModuleSettings("WeightRecent"))
        End If
        Dim d As New Stories.StoriesDataContext
        Dim allStories = From c In d.AP_Stories_Module_Channel_Caches Where c.AP_Stories_Module_Channel.AP_Stories_Module.TabModuleId = TabModuleId
        For Each row In allStories
            row.Precal = Math.Sqrt(row.AP_Stories_Module_Channel.Weight) * ((GetRecencyScore(row.StoryDate) * recentWeight) + GetBoost(row.BoostDate) + 0.5) / 2.5
        Next

        d.SubmitChanges()
    End Sub

    Public Shared Function GetBoost(ByVal boostDate As Date?) As Double
        If boostDate Is Nothing Then
            Return 0
        Else
            Return IIf(boostDate >= Today, 3, 0)

        End If
    End Function

    Public Shared Function GetRecencyScore(ByVal theDate As Date?) As Double
        If theDate Is Nothing Then
            Return 0.3
        End If
        Dim age = DateDiff(DateInterval.Day, CDate(theDate), Today)
        Dim initialBoost = 1
        If age <= 7 Then
            'give an extra boost for the first week
            initialBoost = 2
        End If
        Return Math.Max(0, 1 - (CDbl(age) / 365.0) * initialBoost)




    End Function






    Private Shared Sub SetImage(ByRef theField As Stories.AP_Stories_Module_Channel_Cache, ByVal theRow As SyndicationItem, ByVal ChannelImage As String)
        If theRow.ElementExtensions.Where(Function(x) x.OuterName = "thumbnail").Count > 0 Then
            'First look for a thumbnail in the rss feed element
            theField.ImageId = theRow.ElementExtensions.Where(Function(x) x.OuterName = "thumbnail").First.GetObject(Of XElement).Attribute("url").Value
            theField.ImageWidth = theRow.ElementExtensions.Where(Function(x) x.OuterName = "thumbnail").First.GetObject(Of XElement).Attribute("width").Value
            theField.ImageHeight = theRow.ElementExtensions.Where(Function(x) x.OuterName = "thumbnail").First.GetObject(Of XElement).Attribute("height").Value
        Else
            'Look for an OgImage
            'Try
            '    'Look For an og:Image
            '    Dim reqPage = HttpWebRequest.Create(theField.Link)
            '    reqPage.Method = WebRequestMethods.Http.Head
            '    reqPage.Timeout = 5000
            '    Dim responsePage = reqPage.GetResponse()
            '    theField.ImageId = responsePage.Headers("og:image")
            'Catch ex As Exception
            'End Try

        End If

        If String.IsNullOrEmpty(theField.ImageId) Then
            theField.ImageId = ChannelImage

        ElseIf (Not (theField.ImageWidth Is Nothing Or theField.ImageHeight Is Nothing)) And theField.ImageWidth < 50 Or theField.ImageId.Length > 250 Then
            theField.ImageId = ChannelImage
        End If
        If theField.ImageWidth Is Nothing Or theField.ImageHeight Is Nothing Then
            'lookupURL and get ImageSize
            Dim req = WebRequest.Create(theField.ImageId)
            Dim response = req.GetResponse
            Dim imageStream = response.GetResponseStream
            Dim theImage = System.Drawing.Image.FromStream(imageStream)
            imageStream.Close()
            theField.ImageWidth = theImage.Width
            theField.ImageHeight = theImage.Height

        End If

    End Sub


    Public Shared Function distance(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double) As Double

        Dim theta As Double = lon1 - lon2
        Dim dist As Double = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta))

        dist = Math.Acos(dist)
        dist = rad2deg(dist)
        dist = dist * 1.1515 * 60
        Return dist
        dist = Math.Max(200, dist) ' if distance /200 limit to 200

        '(should be a value between 0 and 2 ish)
        'Now convert to a weight 
        Dim w As Double = 1.0 - dist / 200
        Return (1.0 + w) / 2.0



    End Function

    Private Shared Function deg2rad(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function

    Private Shared Function rad2deg(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function



End Class


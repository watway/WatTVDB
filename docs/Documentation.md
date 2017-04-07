# Using the Library
Add a reference to WatTvdb.dll to you project.

The main api class is _Tvdb_ in the _WatTvdb.V1_ namespace.  The constructor takes your API key.  Instructions to obtain an API key can be found [here](http://thetvdb.com/?tab=apiregister).

{{
WatTvdb.V1.Tvdb api = new Tvdb("apikey");
}}

## Available Methods
### GetMirrors
_[http://thetvdb.com/wiki/index.php/API:mirrors.xml](http://thetvdb.com/wiki/index.php/API:mirrors.xml)_
According to the API documentation this method is actually deprecated, although it still works.  The documentation suggests hard coding the one and only path..._[http://thetvdb.com](http://thetvdb.com)_
{{
TvdbMirrors GetMirrors()
}}

### Get Server Time
The [API documentation](http://thetvdb.com/wiki/index.php?title=Programmers_API) has a suggested set of steps under "Initial Database Processing" for getting started.  It suggests to get the current server time and record this as a baseline time to be used later when querying the API for data changes since this time.
{{
TvdbServerTime GetServerTime()
}}

### Get Languages
Used to get a list of all the languages used in TheTVDB
{{
List<TvdbLanguage> GetLanguages()
}}

### Search Series
This is the main method for searching for Television Series names.
{{
List<TvdbSeriesSeachItem> SearchSeries (string search)
}}

### Series Base Record
_[http://thetvdb.com/wiki/index.php/API:Base_Series_Record](http://thetvdb.com/wiki/index.php/API:Base_Series_Record)_
The base record contains all information available for a Series.  It does not include any banner, season or episode information.  The XMLMirror is obtained from the GetMirrors method above.
{{
TvdbSeriesBase GetSeriesBaseRecord (string XMLMirror, int SeriesId)
}}

### Full Series Record
_[http://thetvdb.com/wiki/index.php/API:Full_Series_Record](http://thetvdb.com/wiki/index.php/API:Full_Series_Record)_
Result contains all available information for a Series, including information on all the episodes.  Note this is quite a large request.  Note the Language parameter is optional, if you pass _null_, the method will automatically use "en".
{{
TvdbSeriesFull GetSeriesFullRecord (string XMLMirror, int SeriesId, string Language)
}}

### Series Banners
_[http://thetvdb.com/wiki/index.php/API:banners.xml](http://thetvdb.com/wiki/index.php/API:banners.xml)_
Contains all the series and season banners for a series.  Possible values for BannerType are _fanart, episode, poster, actors, season, and series_.
{{
List<TvdbBanner> GetSeriesBanners (string XMLMirror, int SeriesId)
}}
**Important Note:**  the path to a banner provided in this method is not the full url, eg _text/80348.jpg_, this path needs to be combined with the url of the banner mirror, and the folder _/banners/_.  This example would become [http://thetvdb.com/banners/text/80348.jpg](http://thetvdb.com/banners/text/80348.jpg).  You must download the file before including in an application, you cannot hot link the image url using <img> for example.  See helper methods GetImageUrl, and GetImage to assist dealing with the banners etc.

### Series Actors
_[http://thetvdb.com/wiki/index.php/API:actors.xml](http://thetvdb.com/wiki/index.php/API:actors.xml)_
Contains all the actors for the series.
{{
List<TvdbActor> GetSeriesActors (string XMLMirror, int SeriesId)
}}

### Get Episode
_[http://thetvdb.com/wiki/index.php/API:Base_Episode_Record](http://thetvdb.com/wiki/index.php/API:Base_Episode_Record)_
Contains the base record for the Episode.  The EpisodeId can be retrieved from the _GetSeriesFullRecord_ method.
{{
TvdbEpisode GetEpisode (string XMLMirror, int EpisodeId, string Language)
}}

### Get Series Episode
Similar to the GetEpisode method except you use the Season and Episode numbers, eg Season 1, and Episode 1, useful if you don't have the EpisodeId.
{{
TvdbEpisode GetSeriesEpisode (string XMLMirror, int SeriesId, int SeasonNum, int EpisodeNum, string Language)
}}

### Get Updates
_[http://thetvdb.com/wiki/index.php/API:Update_Records](http://thetvdb.com/wiki/index.php/API:Update_Records)_
Use this method periodically to get a list of all the series, episodes, and banners records that have been updated in a specific timeframe.
{{
TvdbUpdates GetUpdates (string XMLMirror, TvdbUpdatePeriod Period)
}}

### Get Updates Since
Takes a timestamp and returns the updates since that time, eg use the value returned in GetServerTime.  After using this method and processing the results, make sure you record a new timestamp so you don't retrieve the same updates again.
{{
TvdbUpdateItems GetUpdatesSince (string XMLMirror, Int64 LastTime)
}}

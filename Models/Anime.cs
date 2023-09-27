namespace AnimedleWeb.Models
{
    /// <summary>
    /// Top layer of AniListResults(data from API call)
    /// </summary>
    public class AniListResults
    {
        public AniListData? Data { get; set; }
    }

    /// <summary>
    /// Stores an AniListPage object
    /// </summary>
    public class AniListData
    {
        public AniListPage? Page { get; set; }
    }

    /// <summary>
    /// Stores an AniListPageInfo and a List of AniListMedia
    /// </summary>
    public class AniListPage
    {
        public AniListPageInfo? PageInfo { get; set; }
        public List<AniListMedia>? Media { get; set; }
    }

    /// <summary>
    /// Stores the info of the search page the Media was pulled from
    /// </summary>
    public class AniListPageInfo
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
    }

    /// <summary>
    /// Stores the info of the Media(anime) pulled from AniList
    /// </summary>
    public class AniListMedia
    {
        public int ID { get; set; }
        public AniListTitle? Title { get; set; }
        public int AverageScore { get; set; }
        public int SeasonYear { get; set; }
        public int? Episodes { get; set;}
        public AniListStudio? Studio { get; set; }
        //public List<string>? Genres { get; set; }
    }

    /// <summary>
    /// Stores the different titles of the Media
    /// </summary>
    public class AniListTitle
    {
        public string? Romaji { get; set; }
        public string? English { get; set; }
        public string? Native { get; set; }
    }

    public class AniListStudio
    {
        public List<AniListNode>? nodes { get; set; } 
    }
    public class AniListNode
    { 
        public string? Name { get; set; }
    }
}

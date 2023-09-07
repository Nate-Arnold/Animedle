namespace AnimedleWeb.Models
{
    public class AniListResults
    {
        public AniListData? Data { get; set; }
    }

    public class AniListData
    {
        public AniListPage? Page { get; set; }
    }

    public class AniListPage
    {
        public AniListPageInfo? PageInfo { get; set; }
        public List<AniListMedia>? Media { get; set; }
    }

    public class AniListPageInfo
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
    }

    public class AniListMedia
    {
        public AniListTitle? Title { get; set; }
        public int AverageScore { get; set; }
        public int SeasonYear { get; set; }
        public int? Episodes { get; set;}
    }

    public class AniListTitle
    {
        public string? English { get; set; }
    }
}

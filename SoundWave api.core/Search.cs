namespace SoundWave_api.core
{
    public class RootObject
    {
        public ArtistResponse Artists { get; set; }
        public AlbumResponse Albums { get; set; }
        public TrackResponse Tracks { get; set; }
    }

    public class ArtistResponse
    {
        public List<ArtistData> Items { get; set; }
    }

    public class AlbumResponse
    {
        public List<AlbumData> Items { get; set; }
    }

    public class TrackResponse
    {
        public List<TrackData> Items { get; set; }
    }

    public class ArtistData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Image> Images { get; set; }
    }

    public class AlbumData
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class TrackData
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
    }
}

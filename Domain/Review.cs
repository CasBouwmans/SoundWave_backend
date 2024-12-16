namespace Domain
{
    public class Review
    {
       
        public int Id { get; set; }
        public string TrackId { get; set; }
        public string UserId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }


        //Dit is de klasse die het model van een review beschrijft, inclusief de eigenschappen zoals Id, TrackId, UserId, enzovoort.
    }
}

namespace API_TestExercise.Models
{
    public class FLRModel
    {
        public string? FlrID { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? PeakTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? ClassType { get; set; }
        public string? SourceLocation { get; set; }
        public int? ActiveRegionNum { get; set; }
        public List<LinkedEvent> LinkedEvents { get; set; } = new List<LinkedEvent>();
        public List<Instrument> Instruments { get; set; } = new List<Instrument>();
        public string? Catalog { get; set; }
        public string? Note { get; set; }
        public string? Link { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public int? VersionId { get; set; }
    }
}

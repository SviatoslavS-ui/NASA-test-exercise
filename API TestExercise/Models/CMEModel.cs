namespace API_TestExercise.Models
{
    public class CMEModel
    {
        public string? ActivityID { get; set; }
        public DateTime? StartTime { get; set; }
        public string? SourceLocation { get; set; }
        public int? ActiveRegionNum { get; set; }
        public List<Instrument> Instruments { get; set; } = new List<Instrument>();
        public List<CmeAnalysis> CmeAnalyses { get; set; } = new List<CmeAnalysis>();
        public List<LinkedEvent> LinkedEvents { get; set; } = new List<LinkedEvent>();
        public string? Note { get; set; }
        public string? Catalog { get; set; }
    }

    public class Instrument
    {
        public string? DisplayName { get; set; }
    }

    public class Impact
    {
        public bool IsGlancingBlow { get; set; }
        public string? Location { get; set; }
        public DateTime? ArrivalTime { get; set; }
    }

    public class Enlil
    {
        public DateTime? ModelCompletionTime { get; set; }
        public double? Au { get; set; }
        public DateTime? EstimatedShockArrivalTime { get; set; }
        public double? EstimatedDuration { get; set; }
        public double? RminRe { get; set; }
        public double? Kp18 { get; set; }
        public double? Kp90 { get; set; }
        public double? Kp135 { get; set; }
        public double? Kp180 { get; set; }
        public bool? IsEarthGB { get; set; }
        public List<Impact> ImpactList { get; set; } = new List<Impact>();
        public List<string> CmeIDs { get; set; } = new List<string>();
    }

    public class CmeAnalysis
    {
        public DateTime? Time21_5 { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? HalfAngle { get; set; }
        public double? Speed { get; set; }
        public string? Type { get; set; }
        public bool? IsMostAccurate { get; set; }
        public string? Note { get; set; }
        public int? LevelOfData { get; set; }
        public List<Enlil> EnlilList { get; set; } = new List<Enlil>();
    }

    public class LinkedEvent
    {
        public string? ActivityID { get; set; }
    }
}

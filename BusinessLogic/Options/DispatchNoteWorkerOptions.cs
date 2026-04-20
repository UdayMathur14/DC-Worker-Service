namespace BusinessLogic.Options
{
    public sealed class DispatchNoteWorkerOptions
    {
        public const string SectionName = "DispatchNoteWorker";

        public int PollIntervalSeconds { get; set; } = 60;

        public int BatchSize { get; set; } = 25;

        public string PendingStatus { get; set; } = "PENDING";

        public string ProcessedStatus { get; set; } = "PROCESSED";

        public string FailedStatus { get; set; } = "FAILED";

        public string UpdatedBy { get; set; } = "BackgroundService";
    }
}

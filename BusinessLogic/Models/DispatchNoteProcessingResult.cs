namespace BusinessLogic.Models
{
    public sealed class DispatchNoteProcessingResult
    {
        public int TotalFetched { get; init; }

        public int ProcessedCount { get; init; }

        public int FailedCount { get; init; }
    }
}

namespace DataAccess.Interfaces
{
    public interface IEntity
    {
        decimal Id { get; set; }
        public string? Status { get; set; }
        public DateTime? InactiveDate { get; set; }
        string? CreatedBy { get; set; }
        DateTime CreationDate { get; set; }
        string? LastUpdatedBy { get; set; }
        DateTime LastUpdateDate { get; set; }
    }
}

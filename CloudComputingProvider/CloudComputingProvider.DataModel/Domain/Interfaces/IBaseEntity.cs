namespace CloudComputingProvider.DataModel.Domain.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        byte[] Version { get; set; }
    }
}

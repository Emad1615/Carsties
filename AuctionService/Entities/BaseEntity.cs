namespace AuctionService.Entities
{
    public class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string CreatedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public string DeletedBy { get; private set; } = string.Empty;
        public DateTime? ModifiedAt { get; private set; }
        public string ModifiedBy { get; private set; } = string.Empty;
        public BaseEntity(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
        public void SoftDelete(string deletedBy)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }
        public void SetModified(string modifiedBy)
        {
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
    }
}

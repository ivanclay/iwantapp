namespace IWantApp.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
           Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        DateTime EditedOn { get; set; }
    }
}

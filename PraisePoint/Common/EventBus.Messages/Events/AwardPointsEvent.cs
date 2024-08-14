namespace EventBus.Messages.Events
{
    public class AwardPointsEvent : IntegrationBaseEvent
    {
        //public Guid CompanyId { get; set; } ovo samo u slucaju da nisu jedinstveni usernameovi (to andrijana treba da podesi)

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int Points { get; set; }
    }
}

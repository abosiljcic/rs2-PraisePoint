namespace EventBus.Messages.Events
{
    public class AwardPointsEvent : IntegrationBaseEvent
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        //public Guid CompanyId { get; set; } ovo samo u slucaju da nisu jedinstveni usernameovi (to andrijana treba da podesi)
        public int Points { get;  set; }
    }
}

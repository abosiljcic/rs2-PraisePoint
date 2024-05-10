namespace User.API.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public int PointsNumber { get; set; }
    }
}

namespace IdentityProject.Dtos
{
    public class UserStatisticsDto
    {
        public int TotalIncomingMessages { get; set; }
        public int TotalOutgoingMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int StarredMessages { get; set; }
    }
}

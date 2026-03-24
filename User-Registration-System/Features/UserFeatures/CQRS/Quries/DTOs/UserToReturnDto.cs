namespace User_Registration_System.Features.UserFeatures.CQRS.Quries.DTOs
{
    public class UserToReturnDto
    {
        public Guid Userid { get; set; }

        public DateTime CreatedAt { get; set; } 

        public string UserName { get; set; }

        public string UserEmail { get; set; }


    }
}

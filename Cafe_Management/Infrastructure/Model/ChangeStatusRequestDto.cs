namespace Cafe_Management.Infrastructure.Model
{
    public class ChangeStatusRequestDto
    {
        public int id { get; set; }
        public int status { get; set; }
        public int id_staff { get; set; }
    }
}

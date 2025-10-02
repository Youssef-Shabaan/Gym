
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class Attendance
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("member")]
        public int memberId { get; set; }
        public Member member { get; set; }
        public bool isPresent { get; set; }
    }
}

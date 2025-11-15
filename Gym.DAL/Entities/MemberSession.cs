
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.DAL.Entities
{
    public class MemberSession
    {
        public MemberSession() { }

        public MemberSession(bool isAttended, DateTime bookingDate, string status, decimal price) 
        {
            this.IsAttended = isAttended;
            this.BookingDate = bookingDate;
            this.Status = status;
            this.Price = price;
        }

        public int Id { get; set; }
        public bool? IsAttended { get; set; }      
        public DateTime BookingDate { get; set; }   
        public string Status { get; set; }          
        public decimal? Price { get; set; }


        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public bool Update(MemberSession memberSession)
        {
            this.Price = memberSession.Price;
            this.IsAttended = memberSession.IsAttended;
            this.Status = memberSession.Status;
            this.BookingDate = memberSession.BookingDate;
            return true;
        }
    }

}

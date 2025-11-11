
namespace Gym.DAL.Enums
{
    public enum Gender { Male, Female };
    public enum MemberShipType
    {
        Monthly = 1,
        ThreeMonths,
        SixMonths,
        Yearly
    }
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard,
        DebitCard,
        MobilePayment
    }
    public enum PaymentStatus
    {
        Pending = 1,
        Paid,
        Failed,
        Refunded
    }
    public enum Gateway
    {
        Paymob = 1,
        Fawry,
        Stripe
    }

}
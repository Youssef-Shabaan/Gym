
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
   
    public enum Gateway
    {
        PayPal = 1,
        Fawry,
        Stripe
    }

}
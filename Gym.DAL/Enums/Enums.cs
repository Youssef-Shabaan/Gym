using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Enums
{
    public enum Gender { Male, Female };
    public enum MemberShipType
    {
        PerSession,
        Weekly,
        Monthly,
        Yearly
    }
    public enum PaymentMethod {
        Cash,
        CreditCard,
        DebitCard,
        MobilePayment
    }
    public enum UserType
    {
        Member,
        Trainer,
        Admin
    }

}
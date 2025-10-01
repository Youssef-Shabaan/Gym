using Gym.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class MemberShip
    {
        public MemberShip() { }
        public MemberShip(MemberShipType type, double price)
        {
            MemberShipType = type;
            Price = price;
        }
        public int Id { get; private set; }
        public MemberShipType MemberShipType {  get; private set; }
        public double Price { get; private set; }

        // relation ship ya hussein 
        public IEnumerable<Member> Members { get; private set; }

        public bool UpdateMemberShip(MemberShip ship)
        {
            if(ship == null) return false;
            MemberShipType = ship.MemberShipType;
            Price = ship.Price;
            return true;
        }

    }
}

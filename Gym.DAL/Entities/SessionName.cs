using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class SessionName
    {
        public SessionName() { }
        public SessionName(string name)
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }

    }
}

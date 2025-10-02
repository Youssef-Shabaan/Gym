using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.Entities
{
    public class Session
    {
        public int Id { get; private set; }

        public string? Description { get; private set; }
        public DateTime ScheduleTime { get; private set; }


        // relation ya hussein
        [ForeignKey("_SessionName")]
        public int SessionNameId { get; private set; }
        public SessionName _SessionName { get; private set; }

        [ForeignKey("_Trainer")]
        public int TrainerId { get; set; }
        public Trainer _Trainer { get; private set; }

    }
}

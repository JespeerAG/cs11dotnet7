using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillarsLib
{
    public class Pillar
    {
        public bool Status { get; set; }
        public int Id { get; set; }


        public Pillar(int id, bool status = false)
        {
            this.Id = id;
            this.Status = status;
        }

        public void PillarSwitch()
        {
            this.Status = !this.Status;
        }
    }
}
using System.Collections.Generic;

namespace Factory.Models
{
    public class Machine
    {
        public Machine()
        {
            this.JoinEntries = new HashSet<EngineerMachine>();
        }

        public int MachineId { get; set; }
        public string Name { get; set; }
        // public bool Owned { get; set; }
        public virtual ICollection<EngineerMachine> JoinEntries { get; set; }
        // static public ICollection<Machine> MachineList { get; set; }
    }
}
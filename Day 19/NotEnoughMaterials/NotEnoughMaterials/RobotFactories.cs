using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotEnoughMaterials
{
    public class OreRobotFactory
    {
        public int OreNeeded { get; set; }

        public OreRobotFactory(int mats)
        {
            OreNeeded = mats;
        }
    }

    public class ClayRobotFactory
    {
        public int OreNeeded { get; set; }

        public ClayRobotFactory(int mats)
        {
            OreNeeded = mats;
        }
    }

    public class ObsidianRobotFactory
    {
        public int OreNeeded { get; set; }

        public int ClayNeeded { get; set; }

        public ObsidianRobotFactory(int mats, int clayNeeded)
        {
            OreNeeded = mats;
            ClayNeeded = clayNeeded;
        }
    }

    public class GeodeRobotFactory
    {
        public int OreNeeded { get; set; }

        public int ClayNeeded { get; set; }

        public GeodeRobotFactory(int mats, int clayNeeded)
        {
            OreNeeded = mats;
            ClayNeeded = clayNeeded;
        }
    }
}

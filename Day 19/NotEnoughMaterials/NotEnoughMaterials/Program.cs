// See https://aka.ms/new-console-template for more information
using NotEnoughMaterials;
using System.Text.RegularExpressions;

using var input = new StreamReader("input.txt");
List<BluePrint> bluePrints = new List<BluePrint>();
while(!input.EndOfStream)
{
    var line = input.ReadLine();
    var nums = Regex.Matches(line, @"\d+").Select(s => int.Parse(s.Value)).ToArray();
    bluePrints.Add(
        new BluePrint
        {
            ID = nums[0],
            OreRobotFactory = new OreRobotFactory(nums[1]),
            ClayRobotFactory = new ClayRobotFactory(nums[2]),
            ObsidianRobotFactory = new ObsidianRobotFactory(nums[3], nums[4]),
            GeodeRobotFactory = new GeodeRobotFactory(nums[5], nums[6])
        }
        );
}



public class BluePrint
{
    public int ID { get; set; }
    public OreRobotFactory OreRobotFactory { get; set; }
    public ClayRobotFactory ClayRobotFactory { get; set; }
    public ObsidianRobotFactory ObsidianRobotFactory { get; set; }
    public GeodeRobotFactory GeodeRobotFactory { get; set; }
}

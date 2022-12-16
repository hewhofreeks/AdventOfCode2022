using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

using var input = new StreamReader("input.txt");

var allInput = input.ReadToEnd();

int lowerBounds = 0;
int upperBounds = 4_000_000;

for(int i = 0; i < 4_000_000;i++)
{
    using var inputStream = new StringReader(allInput);

    HashSet<(int x, int y)> positionsWithoutBeacon = new HashSet<(int x, int y)>();

    int rowToCheck = i;

    while (true)
    {
        var line = inputStream.ReadLine();
        if (line == null)
            break;

        var inputNums = Regex.Matches(line, @"[+-]?\d+(\.\d+)?").Select(s => int.Parse(s.Value)).ToArray();

        var sensorX = inputNums[0];
        var sensorY = inputNums[1];

        var beaconX = inputNums[2];
        var beaconY = inputNums[3];

        var delta = (dx: beaconX - sensorX, dy: beaconY - sensorY);
        var totalHeightOfArea = Math.Abs(delta.dy) + Math.Abs(delta.dx);

        var distanceFromRowToCheck = rowToCheck - sensorY;
        var lengthOfRow = 2 * (totalHeightOfArea - Math.Abs(distanceFromRowToCheck)) + 1;

        // if delta crosses row 2,000,000
        if ((sensorY < rowToCheck && sensorY + totalHeightOfArea > rowToCheck) ||
            (sensorY > rowToCheck && sensorY - totalHeightOfArea < rowToCheck))
        {
            var startX = sensorX - (lengthOfRow - 1) / 2;

            // Iterate through row adding values in the row to our map
            for (int x = Math.Max(startX, lowerBounds); x < Math.Min(upperBounds, (startX + lengthOfRow)); x++)
            {
                if ((x, rowToCheck) != (beaconX, beaconY))
                    positionsWithoutBeacon.Add((x, rowToCheck));
            }
        }
        if (positionsWithoutBeacon.Count == 4_000_000)
            break;
    }

    if(positionsWithoutBeacon.Count == 3_999_999)
    {
        for(int x = 0; x< 4_000_000; x++)
        {
            if (!positionsWithoutBeacon.Contains((x, i)))
            {
                Console.WriteLine((x, i));
            }
        }
    }
}


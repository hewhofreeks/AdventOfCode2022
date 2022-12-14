using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DistressSignal
{
    class PacketComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            var packetOne = JsonSerializer.Deserialize<List<dynamic>>(x);
            var packetTwo = JsonSerializer.Deserialize<List<dynamic>>(y);

            return ComparePackets(packetOne, packetTwo) * -1;
        }
        
        public static int ComparePackets(dynamic left, dynamic right)
        {
            var leftAsString = Convert.ToString(left);
            var rightAsString = Convert.ToString(right);
        
            // Both ints
            int leftInt = 0;
            int rightInt = 0;
            if (int.TryParse(leftAsString, out leftInt) && int.TryParse(rightAsString, out rightInt))
            {
                if (leftInt == rightInt)
                    return 0;
        
                return leftInt < rightInt ? 1 : -1;
            }
            else if (int.TryParse(leftAsString, out leftInt))
            {
                var val = ComparePackets(new List<dynamic>() {leftInt}, JsonSerializer.Deserialize<List<dynamic>>(right));
                if (val != 0)
                    return val;
            }
            else if (int.TryParse(rightAsString, out rightInt))
            {
                var val = ComparePackets(JsonSerializer.Deserialize<List<dynamic>>(left), new List<dynamic>() {rightInt});
                if (val != 0)
                    return val;
            }
            else
            {
                var leftArray = left is List<dynamic> ? left : JsonSerializer.Deserialize<List<dynamic>>(left);
                var rightArray = right is List<dynamic> ? right : JsonSerializer.Deserialize<List<dynamic>>(right);
        
                // both arrays
                for (int i = 0; i < Math.Max(leftArray.Count, rightArray.Count); i++)
                {
                    // If we reach the end of the left list first
                    if (i == leftArray.Count && i < rightArray.Count)
                        return 1;
        
                    // If we reach the end of the right list first
                    if (i == rightArray.Count && i < leftArray.Count)
                        return -1;
        
                    var val = ComparePackets(leftArray[i], rightArray[i]);
        
                    if (val != 0)
                        return val;
                }
            }
        
            return 0;
        }
    }
    
    
}
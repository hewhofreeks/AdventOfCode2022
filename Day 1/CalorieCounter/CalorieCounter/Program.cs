using System;
using System.Collections.Generic;
using System.IO;

namespace CalorieCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            using var inputStream = new StreamReader("input.txt");

            PriorityQueue<int, int> calorieMinHeap = new PriorityQueue<int, int>();

            int currentCalCount = 0;
            // Loop through file, adding up each calorie count and adding them to our min-heap
            while (!inputStream.EndOfStream)
            {
                var line = inputStream.ReadLine();

                if (int.TryParse(line, out int calValue))
                {
                    currentCalCount = currentCalCount + calValue;
                }
                
                // If we have finished calculating the elf's total calories, 
                // Add it to our min-heap and dequeue anything that is currently at it's head
                if (string.IsNullOrEmpty(line) && currentCalCount != 0)
                {
                    calorieMinHeap.Enqueue(currentCalCount, currentCalCount);
                    currentCalCount = 0;

                    while (calorieMinHeap.Count > 3)
                    {
                        // Since we do not care about the three lowest
                        calorieMinHeap.Dequeue();
                    }
                }
            }

            var result = 0;
            while (calorieMinHeap.TryDequeue(out int el, out int priority))
            {
                result += priority;
            }
            
            Console.WriteLine($"Top three calories holders: {result}");
        }
    }
}
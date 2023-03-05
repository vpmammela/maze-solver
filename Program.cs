using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string file;
        Console.Write("Select maze to solve (1 or 2): ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                file = "maze-task-first.txt";
                break;
            case "2":
                file = "maze-task-second.txt";
                break;
            default:
                Console.WriteLine("Invalid selection");
                return;
        }

        string[] maze = File.ReadAllLines($"./mazes/{file}");

        // print each line of the maze
        foreach (string line in maze)
        {
            Console.WriteLine(line);
        }
    }
}
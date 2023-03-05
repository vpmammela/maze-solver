using System;
using System.Collections.Generic;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        string file;
        Console.Write("Select maze to solve (1 or 2): ");
        string choice = Console.ReadLine()!;
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

        MazeSolver solver = new MazeSolver(file);

        // Find the shortest path from the start to any of the end positions in the maze
        List<(int x, int y)> path = solver.FindShortestPath();

        // Mark the path on the maze by replacing the positions with '@' character
        foreach (var position in path)
        {
            maze[position.x] = maze[position.x].Substring(0, position.y) + "@" + maze[position.x].Substring(position.y + 1);
        }

        // Print the maze with the marked path on the console
        foreach (string line in maze)
        {
            Console.WriteLine(line);
        }

        // Display a message if the path is not found within 20 or 150 moves
        if (path.Count >= 20)
        {
            Console.WriteLine("Route not found in 20 moves.");
        }
        if (path.Count >= 150)
        {
            Console.WriteLine("Route not found in 150 moves.");
        }

        Console.WriteLine($"Route found with {path.Count} moves");
    }
}
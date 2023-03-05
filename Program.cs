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


        int startX = -1, startY = -1, endX = -1, endY = -1;

        // Find the starting and ending positions in the maze
        for (int i = 0; i < maze.Length; i++)
        {
            for (int j = 0; j < maze[i].Length; j++)
            {
                if (maze[i][j] == '^')
                {
                    startX = i;
                    startY = j;
                }
                else if (maze[i][j] == 'E')
                {
                    endX = i;
                    endY = j;
                }
            }
        }

        if (startX == -1 || startY == -1 || endX == -1 || endY == -1)
        {
            Console.WriteLine("Error: Maze does not contain starting and ending positions.");
            return;
        }

        bool[,] visited = new bool[maze.Length, maze[0].Length];

        // Define the four possible directions of movement
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        // Use a queue to perform a breadth-first search of the maze
        Queue<(int x, int y, int moves, List<(int x, int y)> path)> queue = new Queue<(int x, int y, int moves, List<(int x, int y)> path)>();
        queue.Enqueue((startX, startY, 0, new List<(int x, int y)> { (startX, startY) }));

        bool foundExit = false;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            // Check if the current position is the exit
            if (current.x == endX && current.y == endY)
            {
                Console.WriteLine("Found a route in " + current.moves + " moves:");
                foreach (var position in current.path)
                {
                    maze[position.x] = maze[position.x].Substring(0, position.y) + "@" + maze[position.x].Substring(position.y + 1);
                }
                foundExit = true;
                break;
            }

            // Check if the maximum number of moves has been exceeded
            if (current.moves >= 200)
            {
                Console.WriteLine("Error: Maximum number of moves exceeded.");
                return;
            }

            // Check each possible direction from the current position
            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                // Check if the new position is within the maze bounds and is not a block
                if (newX >= 0 && newX < maze.Length && newY >= 0 && newY < maze[newX].Length && maze[newX][newY] != '#' && !visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    List<(int x, int y)> newPath = new List<(int x, int y)>(current.path);
                    newPath.Add((newX, newY));
                    queue.Enqueue((newX, newY, current.moves + 1, newPath));
                }
            }
        }

        if (!foundExit)
        {
            Console.WriteLine("No route found.");
        }
    }
}
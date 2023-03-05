using System;
using System.Collections.Generic;
using System.IO;

// MazeSolver reads the maze file, finds the start and end positions, and validates the maze
public class MazeSolver
{
    private readonly string[] maze;  // the maze represented as a string array
    private readonly int startX, startY;  // starting position coordinates
    private readonly List<(int x, int y)> endPositions;  // list of all ending positions coordinates
    private readonly bool[,] visited;  // matrix representing visited cells in the maze
    private readonly int[] dx = { 1, -1, 0, 0 };  // array of x-axis direction movements
    private readonly int[] dy = { 0, 0, 1, -1 };  // array of y-axis direction movements

    public MazeSolver(string file)
    {
        maze = File.ReadAllLines($"./mazes/{file}");

        endPositions = new List<(int x, int y)>();
        visited = new bool[maze.Length, maze[0].Length];

        // Find the start and end positions in the maze
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
                    endPositions.Add((i, j));
                }
            }
        }

        // Validate the maze by checking if it contains a start and end positions
        if (startX == -1 || startY == -1 || endPositions.Count == 0)
        {
            throw new InvalidOperationException("Maze does not contain starting and ending positions.");
        }
    }

    // method to find the shortest path from the start to any of the end positions in the maze
    public List<(int x, int y)> FindShortestPath()
    {
        // Create a queue to hold the positions needed to check
        Queue<(int x, int y, int moves, List<(int x, int y)> path)> queue = new Queue<(int x, int y, int moves, List<(int x, int y)> path)>();

        // Enqueue the starting position with a move count of 0 and a path containing only the starting position
        queue.Enqueue((startX, startY, 0, new List<(int x, int y)> { (startX, startY) }));

        // Keep checking positions until the queue is empty
        while (queue.Count > 0)
        {
            // Get the next position from the queue
            var current = queue.Dequeue();

            // If the end position is reached, return the path taken to get there
            if (endPositions.Contains((current.x, current.y)))
            {
                return current.path;
            }

            // If moves exceed maximum of 200 without reaching the end, throw an exception
            if (current.moves >= 200)
            {
                throw new InvalidOperationException("Maximum number of moves exceeded.");
            }

            // Check all four adjacent positions
            for (int i = 0; i < 4; i++)
            {
                // Calculate the position of the adjacent cell
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                // If the adjacent cell is valid (within the maze bounds, not a wall, and not already visited), add it to the queue
                if (newX >= 0 && newX < maze.Length && newY >= 0 && newY < maze[newX].Length && maze[newX][newY] != '#' && !visited[newX, newY])
                {
                    // Mark the adjacent cell as visited
                    visited[newX, newY] = true;

                    // Create a new path that includes the adjacent cell
                    List<(int x, int y)> newPath = new List<(int x, int y)>(current.path);
                    newPath.Add((newX, newY));

                    // Add the adjacent cell to the queue with an incremented move count and the new path
                    queue.Enqueue((newX, newY, current.moves + 1, newPath));
                }
            }
        }

        // If we've checked all possible positions and still haven't found a path to the end, throw an exception
        throw new InvalidOperationException("No route found.");
    }
}

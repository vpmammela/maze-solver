using System;
using System.Collections.Generic;
using System.IO;

public class MazeSolver
{
    private readonly string[] maze;
    private readonly int startX, startY;
    private readonly List<(int x, int y)> endPositions;
    private readonly bool[,] visited;
    private readonly int[] dx = { 1, -1, 0, 0 };
    private readonly int[] dy = { 0, 0, 1, -1 };

    public MazeSolver(string file)
    {
        maze = File.ReadAllLines($"./mazes/{file}");

        endPositions = new List<(int x, int y)>();
        visited = new bool[maze.Length, maze[0].Length];

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

        if (startX == -1 || startY == -1 || endPositions.Count == 0)
        {
            throw new InvalidOperationException("Maze does not contain starting and ending positions.");
        }
    }

    public List<(int x, int y)> FindShortestPath()
    {
        Queue<(int x, int y, int moves, List<(int x, int y)> path)> queue = new Queue<(int x, int y, int moves, List<(int x, int y)> path)>();
        queue.Enqueue((startX, startY, 0, new List<(int x, int y)> { (startX, startY) }));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (endPositions.Contains((current.x, current.y)))
            {
                return current.path;
            }

            if (current.moves >= 200)
            {
                throw new InvalidOperationException("Maximum number of moves exceeded.");
            }

            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                if (newX >= 0 && newX < maze.Length && newY >= 0 && newY < maze[newX].Length && maze[newX][newY] != '#' && !visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    List<(int x, int y)> newPath = new List<(int x, int y)>(current.path);
                    newPath.Add((newX, newY));
                    queue.Enqueue((newX, newY, current.moves + 1, newPath));
                }
            }
        }

        throw new InvalidOperationException("No route found.");
    }
}

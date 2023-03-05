using System;
using System.Collections.Generic;
using System.IO;

public class MazeSolver
{
    private readonly string[] maze;
    private readonly int startX, startY, endX, endY;
    private readonly bool[,] visited;
    private readonly int[] dx = { 1, -1, 0, 0 };
    private readonly int[] dy = { 0, 0, 1, -1 };

    public MazeSolver(string file)
    {
        maze = File.ReadAllLines($"./mazes/{file}");

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
            throw new InvalidOperationException("Maze does not contain starting and ending positions.");
        }

        visited = new bool[maze.Length, maze[0].Length];
    }

    public List<(int x, int y)> FindShortestPath()
    {
        Queue<(int x, int y, int moves, List<(int x, int y)> path)> queue = new Queue<(int x, int y, int moves, List<(int x, int y)> path)>();
        queue.Enqueue((startX, startY, 0, new List<(int x, int y)> { (startX, startY) }));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.x == endX && current.y == endY)
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

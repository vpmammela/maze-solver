### Maze Solver

This is a C# console application that solves a maze by finding the shortest path from the starting position to any of the end positions in the maze.

## Prerequisites

This application requires .NET to be installed on your machine.

## How to Run

1. Clone this repository to your local machine.
2. Open a terminal and navigate to the root directory of the cloned repository.
3. Navigate to the MazeSolver directory using the command: cd MazeSolver.
4. Run the command dotnet run to execute the program.
5. Follow the prompts to select a maze to solve.

## Usage

The application reads the maze from a file located in the mazes directory. You can add new mazes to this directory as long as they are formatted correctly.

When prompted, enter either 1 or 2 to select the maze you want to solve. The program will then display the maze with the shortest path marked by @ characters.
If the path is not found within 20 or 150 moves, the program will display a message indicating that the route was not found.

## How it Works

The MazeSolver class reads the maze file, finds the start and end positions, and validates the maze.
The FindShortestPath method then uses a breadth-first search algorithm to find the shortest path from the start to any of the end positions in the maze.

The Program class prompts the user to select a maze to solve,
reads the maze file using the MazeSolver class,
finds the shortest path using the FindShortestPath method,
and then marks the path on the maze by replacing the positions with @ characters.
Finally, the program prints the maze with the marked path on the console.
/* Program: MazeController
 * Author:  Steven Motz
 * Purpose: This program creates a maze object, gives it information from the user,
 *          shows the maze in the Unity editor, and runs the searches.
 * Date:    10/25/2021
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;

public class MazeController : MonoBehaviour
{
    public Material cubeMaterial;
    public Material outerwallMaterial;
    public Material topOrBottomMaterial;
    public Material searchMaterial;
    public Material startEndMaterial;


    private int maze2Offset = 1000;
    public int intX = 50, intY = 1, intZ = 50;
    MazeClass maze;
    public int intCount = 0;
    Queue<string> queueBFS = new Queue<string>();
    int intQ = 0;
    int intMQ = 0;
    // this function generates and prints the maze
    public void mazeGen(int x, int y, int z, int intDensity)
    {

        intX = x;
        intY = y;
        intZ = z;

        maze = new MazeClass(intX, intY, intZ);
        maze.generateMaze(intDensity);
        showMaze();
    }
    public void mazeStartAndEnd()
    {
        maze.pickStartandEnd();
        showStartEnd();
    }

    // this code starts the DFS recuscion, shows it in the maze, and reports path size to who calls it.
    public int runDFS()
    {
        Stack<int[]> stackDFS = new Stack<int[]>();
        stackDFS.Push(maze.getStartLocation());
        stackDFS = DFS(stackDFS);
        if (((int[])stackDFS.Peek()).SequenceEqual(maze.getEndLocation()))
        {
            // draw in maze
            int count = stackDFS.Count;
            Debug.Log("END, Yes");
            for (int i = 0; i < count; i++)
            {
                int[] nextLoacion = new int[3];
                nextLoacion = (int[])stackDFS.Pop();
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(nextLoacion[0], nextLoacion[1], nextLoacion[2]);
                cube.GetComponent<Renderer>().material = searchMaterial;
                cube.name = "DFS Search Cube";
                cube.isStatic = true;

            }
            return count;
        }
        else
        {
            return -1;
        }
    }

    // This code runs and shows BFS in the maze and send path size to who called it
    public int runBFS()
    {
        BFS(new Queue<string> { }, maze.getStartLocation());

        Debug.Log(queueBFS.Count + " test");

        int count = queueBFS.Count;
        for (int i = 1; i < count; i++)
        {
            // draw in maze
            string[] nextstring = queueBFS.Dequeue().Split(',');
            int[] nextLoacion = new int[3] { int.Parse(nextstring[0]), int.Parse(nextstring[1]), int.Parse(nextstring[2]) };
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(nextLoacion[0] + maze2Offset, nextLoacion[1], nextLoacion[2]);
            cube.GetComponent<Renderer>().material = searchMaterial;
            cube.name = "BFS Search Cube";
            cube.isStatic = true;

        }
        Debug.Log(" max = " + intMQ);
        return count;
    }
    // this BFS function calls it self to find diffrent paths then chooses the shortest.
    public void BFS(Queue<string> queue, int[] current)
    {
        if (intMQ < queue.Count)
        {
            intMQ = queue.Count;
        }

        if (queue.Count < intX * intY * intZ)
        {
            int[] end = maze.getEndLocation();
            queue.Enqueue(current[0] + "," + current[1] + "," + current[2]);
            if (current[0] == end[0] && current[1] == end[1] && current[2] == end[2])
            {
                intQ++;
                if (queueBFS.Count == 0 || queueBFS.Count > queue.Count)
                {
                    queueBFS = new Queue<string>(queue);
                }
            }
            else
            {
                if (current[0] < end[0] && maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2]))
                {
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                }
                else if (current[0] > end[0] && maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2]))
                {
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                }
                else if (current[1] < end[1] && maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2]))
                {
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                }
                else if (current[1] > end[1] && maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2]))
                {
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                }
                else if (current[2] < end[2] && maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1)))
                {
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                }
                else if (current[2] > end[2] && maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1)))
                {
                    if (maze.BFSCheck(current[0], current[1], current[2] - 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] - 1))) // z - 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] - 1 });
                    }
                    if (maze.BFSCheck(current[0] + 1, current[1], current[2]) && !queue.Contains((current[0] + 1) + "," + current[1] + "," + current[2])) // x + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] + 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0] - 1, current[1], current[2]) && !queue.Contains((current[0] - 1) + "," + current[1] + "," + current[2])) // x -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0] - 1, current[1], current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] + 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] + 1) + "," + current[2])) // y + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] + 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1] - 1, current[2]) && !queue.Contains((current[0]) + "," + (current[1] - 1) + "," + current[2])) // y -1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1] - 1, current[2] });
                    }
                    if (maze.BFSCheck(current[0], current[1], current[2] + 1) && !queue.Contains((current[0]) + "," + current[1] + "," + (current[2] + 1))) // z + 1
                    {
                        BFS(new Queue<string>(queue), new int[] { current[0], current[1], current[2] + 1 });
                    }
                }
            }
        }
    }

    // this method will call its self until it finds the end of the maze and sends the path back or it decides the maze cannot be solved 
    public Stack<int[]> DFS(Stack<int[]> stackDFS)
    {
        intCount++;
        int[] nextLoacion = new int[3];
        nextLoacion = maze.DFSMove(((int[])stackDFS.Peek())[0], ((int[])stackDFS.Peek())[1], ((int[])stackDFS.Peek())[2]);
        if (maze.getEndLocation().SequenceEqual((int[])stackDFS.Peek())) // got the end
        {
            return stackDFS;
        }
        else if (nextLoacion.SequenceEqual((int[])stackDFS.Peek()) && !maze.getStartLocation().SequenceEqual((int[])stackDFS.Peek())) // go back a step
        {
            stackDFS.Pop();
            return DFS(stackDFS);
        }
        else if (!nextLoacion.SequenceEqual((int[])stackDFS.Peek())) // go forward
        {
            stackDFS.Push(nextLoacion);
            return DFS(stackDFS);
        }
        else if (maze.getStartLocation().SequenceEqual((int[])stackDFS.Peek())) // no solution
        {
            return stackDFS;
        }
        else
        {
            return stackDFS;
        }


    }

    // this function controls the start and end
    void showStartEnd()
    {
        int[] startLocation = maze.getStartLocation();
        int[] endLocation = maze.getEndLocation();

        // makes start cube
        GameObject startCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startCube.transform.position = new Vector3(startLocation[0], startLocation[1], startLocation[2]);
        startCube.GetComponent<Renderer>().material = startEndMaterial;
        startCube.name = "Start";
        GameObject startCube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startCube2.transform.position = new Vector3(startLocation[0] + maze2Offset, startLocation[1], startLocation[2]);
        startCube2.GetComponent<Renderer>().material = startEndMaterial;
        startCube2.name = "Start2";
        // makes end cube
        GameObject endCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        endCube.transform.position = new Vector3(endLocation[0], endLocation[1], endLocation[2]);
        endCube.GetComponent<Renderer>().material = startEndMaterial;
        endCube.name = "End";
        GameObject endCube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        endCube2.transform.position = new Vector3(endLocation[0] + maze2Offset, endLocation[1], endLocation[2]);
        endCube2.GetComponent<Renderer>().material = startEndMaterial;
        endCube2.name = "End2";
    }
    void makecube(int[] location)
    {
        GameObject startCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startCube.transform.position = new Vector3(location[0], location[1], location[2]);
        startCube.GetComponent<Renderer>().material = searchMaterial;
    }

    // makes the maze cubes and applies materails to them.
    void showMaze()
    {
        int cubeCount = 0;
        for (int i = 0; i < intX + 2; i++)
        {
            for (var j = 0; j < intY + 2; j++)
            {
                for (int k = 0; k < intZ + 2; k++)
                {
                    if (maze.isWall(i, j, k))
                    {

                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, k);
                        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube2.transform.position = new Vector3(i + maze2Offset, j, k);

                        if (maze.isOuterWall(i, j, k))
                        {
                            cube.GetComponent<Renderer>().material = outerwallMaterial;
                            cube2.GetComponent<Renderer>().material = outerwallMaterial;
                        }
                        else if (maze.isTopBottom(i, j, k))
                        {
                            cube.GetComponent<Renderer>().material = topOrBottomMaterial;
                            cube2.GetComponent<Renderer>().material = topOrBottomMaterial;
                        }
                        else
                        {
                            cube.GetComponent<Renderer>().material = cubeMaterial;
                            cube2.GetComponent<Renderer>().material = cubeMaterial;
                        }
                        cubeCount++;
                        cube.name = "cube_" + cubeCount;
                        cube.isStatic = true;
                        cube2.name = "cube2_" + cubeCount;
                        cube2.isStatic = true;
                    }
                }
            }


        }

    }
}

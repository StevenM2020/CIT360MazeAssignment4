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

    public int dencity;

    public int intX = 50, intY = 1, intZ = 30;
    MazeClass maze;
    public int intCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        int[] test1 = new int[3] { 1, 2, 3 };
        int[] test2 = new int[3] { 1, 2, 3 };
        int[] test3 = new int[3] { 1, 2, 0 };

        maze = new MazeClass(intX, intY, intZ);
        maze.generateMaze(dencity);
        showMaze();
        maze.pickStartandEnd();
        showStartEnd();

        Stack<int[]> stackDFS = new Stack<int[]>();
        stackDFS.Push(maze.getStartLocation());
        stackDFS = DFS(stackDFS);
        Debug.Log("stack size: " + stackDFS.Count + "call level: " + intCount);
        if (((int[])stackDFS.Peek()).SequenceEqual(maze.getEndLocation()))
        {
            int count = stackDFS.Count;
            Debug.Log("END, LETS GO!!!!!");
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
        }
        else if (((int[])stackDFS.Peek()).SequenceEqual(maze.getStartLocation()))
        {
            Debug.Log("Start, so close !!!!!");
        }
        else
        {
            Debug.Log("WHYYYYYY!!!!!");
        }

    }
    public Stack<int[]> DFS(Stack<int[]> stackDFS)
    {
        intCount++;
        Debug.Log("call level: " + intCount);
        int[] nextLoacion = new int[3];
       nextLoacion = maze.DFSMove(((int[])stackDFS.Peek())[0], ((int[])stackDFS.Peek())[1], ((int[])stackDFS.Peek())[2]);
        if (maze.getEndLocation().SequenceEqual((int[])stackDFS.Peek()))
        {
            return stackDFS;
        }
        else if(nextLoacion.SequenceEqual((int[])stackDFS.Peek()) && !maze.getStartLocation().SequenceEqual((int[])stackDFS.Peek()))
        {
            stackDFS.Pop();
            return DFS(stackDFS);
        }else if (!nextLoacion.SequenceEqual((int[])stackDFS.Peek()))
        {
            stackDFS.Push(nextLoacion);
            return DFS(stackDFS);
        }
        else if (maze.getStartLocation().SequenceEqual((int[])stackDFS.Peek()))
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
        // makes end cube
        GameObject endCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        endCube.transform.position = new Vector3(endLocation[0], endLocation[1], endLocation[2]);
        endCube.GetComponent<Renderer>().material = startEndMaterial;
        endCube.name = "End";
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
        for (int i = 0; i < intX+2; i++)
        {
            for (var j = 0; j < intY+2; j++)
            {
                for (int k = 0; k < intZ+2; k++)
                {
                    if (maze.isWall(i, j, k))
                    {
                       
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, k);
                        
                        if (maze.isOuterWall(i, j, k))
                        {
                            cube.GetComponent<Renderer>().material = outerwallMaterial;
                        }
                        else if(maze.isTopBottom(i, j, k))
                        {
                            cube.GetComponent<Renderer>().material = topOrBottomMaterial;
                        }
                        else
                        {
                            cube.GetComponent<Renderer>().material = cubeMaterial;
                        }
                        cubeCount++;
                        cube.name = "cube" + cubeCount;
                        cube.isStatic = true;
                        
                    }
                }
            }


            
        }
        
    }
}

/* Program: MazeController
 * Author:  Steven Motz
 * Purpose: This program creates a maze object, gives it information from the user,
 *          shows the maze in the Unity editor, and runs the searches.
 * Date:    10/25/2021
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        maze = new MazeClass(intX, intY, intZ);
        maze.generateMaze(dencity);
       Debug.Log( maze.getMazeAsString());
        showMaze();
        maze.pickStartandEnd();
        showStartEnd();
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

        // makes end cube
        GameObject endCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        endCube.transform.position = new Vector3(endLocation[0], endLocation[1], endLocation[2]);
        endCube.GetComponent<Renderer>().material = startEndMaterial;

    }

    // makes the maze cubes and applies materails to them.
    void showMaze()
    {
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
                        
                    }
                }
            }


            
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public Material cubeMaterial;
    public Material outerwallMaterial;
    public Material topOrBottomMaterial;

    private int intX = 50, intY = 1, intZ = 50;
    MazeClass maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = new MazeClass(intX, intY, intZ);
        maze.generateMaze(80);
       Debug.Log( maze.getMazeAsString());
        showMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

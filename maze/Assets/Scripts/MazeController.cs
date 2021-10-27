using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MazeClass maze = new MazeClass(4,4,4);
        maze.generateMaze(50);
       Debug.Log( maze.getMazeAsString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

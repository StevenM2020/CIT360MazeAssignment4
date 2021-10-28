using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeClass : MonoBehaviour
{

    private int[,,] intMaze;

        // Create a class constructor for the Car class
        public MazeClass(int x,int y,int z)
        {
        intMaze = new int[x+2, y+2, z+2];

    }

    public void generateMaze(int dencity)
    {


        System.Random rnd = new System.Random();
        for ( int i = 0; i < intMaze.GetLength(0) ; i++)
        {
            for ( int j = 0; j < intMaze.GetLength(1); j++)
            {
                for (int k = 0; k < intMaze.GetLength(2); k++)
                { 
                    intMaze[i, j, k] = (j == 0 || j == intMaze.GetLength(1) - 1 ||
                        k == 0 || k == intMaze.GetLength(2) - 1) ? 2 : (i == 0 || i == intMaze.GetLength(0) - 1) 
                        ? 3 : ((int)rnd.Next(100) > dencity ? 1 : 0);
                }
            }
        }
    }
    public string getMazeAsString()
    {
        string str = "";
        for (int i = 0; i < intMaze.GetLength(0); i++) {
            str = str + "starting new level \n";
            for (int j = 0; j < intMaze.GetLength(1); j++)
            {
                for (int k = 0; k < intMaze.GetLength(2); k++)
                {
                    str = str + intMaze[i, j, k];
                }
               str = str + "\n";
            }
        }

        return str;
    }
    public bool isWall(int x, int y, int z)
    {
        return intMaze[x,y,z] == 1 || intMaze[x, y, z] == 2 || intMaze[x, y, z] == 3;
    }
    public bool isOuterWall(int x, int y, int z)
    {
        return intMaze[x, y, z] == 2;
    }
    public bool isTopBottom(int x, int y, int z)
    {
        return intMaze[x, y, z] == 3;
    }
}

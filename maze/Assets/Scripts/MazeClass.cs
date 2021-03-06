/* Program: MazeClass
 * Author:  Steven Motz
 * Purpose: This is used for making a maze object, holding all its data, and methods to modify that data.
 * Date:    10/25/2021
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeClass : MonoBehaviour
{

    private int[,,] intMaze;
    private int[] startLocation;
    private int[] endLocation;
    private int[,,] DFSMaze;
    private int[,,] BFSMaze;
    // Create a class constructor for the maze class
    public MazeClass(int x, int y, int z)
    {
        // sizes the maze and adds room for walls
        intMaze = new int[x + 2, y + 2, z + 2];
        DFSMaze = new int[x + 2, y + 2, z + 2];
        BFSMaze = new int[x + 2, y + 2, z + 2];
    }

    // fills the maze
    public void generateMaze(int dencity)
    {

        System.Random rnd = new System.Random();
        for (int i = 0; i < intMaze.GetLength(0); i++) // x
        {
            for (int j = 0; j < intMaze.GetLength(1); j++) // y
            {
                for (int k = 0; k < intMaze.GetLength(2); k++) // z
                {
                    /* chooses what number to put
                    0 = empty
                    1 = maze wall
                    2 = outer wall
                    3 = top or bottom
                    4 = start
                    5 = end
                    */
                    intMaze[i, j, k] = (i == 0 || i == intMaze.GetLength(0) - 1 ||
                        k == 0 || k == intMaze.GetLength(2) - 1) ? 2 : (j == 0 || j == intMaze.GetLength(1) - 1)
                        ? 3 : ((int)rnd.Next(100) > dencity ? 1 : 0);
                    DFSMaze[i, j, k] = (intMaze[i, j, k] == 1 || intMaze[i, j, k] == 2 || intMaze[i, j, k] == 3 ? 1 : 0);
                    BFSMaze[i, j, k] = (intMaze[i, j, k] == 1 || intMaze[i, j, k] == 2 || intMaze[i, j, k] == 3 ? 1 : 0);
                }
            }
        }
    }
    // converts the maze to a string for debugging
    public string getMazeAsString()
    {
        string str = "";
        for (int i = 0; i < intMaze.GetLength(0); i++)
        { //x
            str = str + "starting new level \n";
            for (int j = 0; j < intMaze.GetLength(1); j++) //y
            {
                for (int k = 0; k < intMaze.GetLength(2); k++) //z
                {
                    str = str + intMaze[i, j, k];
                }
                str = str + "\n";
            }
        }

        return str;
    }
    // checks if that spot in the maze is any kind of wall
    public bool isWall(int x, int y, int z)
    {
        return intMaze[x, y, z] == 1 || intMaze[x, y, z] == 2 || intMaze[x, y, z] == 3;
    }
    // checks if that spot in the maze is an outer wall
    public bool isOuterWall(int x, int y, int z)
    {
        return intMaze[x, y, z] == 2;
    }
    //checks if that spot in the maze is a top or bottom
    public bool isTopBottom(int x, int y, int z)
    {
        return intMaze[x, y, z] == 3;
    }

    //chooses a start and end point randomly
    public void pickStartandEnd()
    {
        bool setStart = false;
        bool setEnd = false;
        System.Random rnd = new System.Random();

        // repeats until it finds and empty spot for start
        while (!setStart)
        {
            int x = rnd.Next(intMaze.GetLength(0) - 1), y = rnd.Next(intMaze.GetLength(1) - 1), z = rnd.Next(intMaze.GetLength(2) - 1);
            if (intMaze[x, y, z] == 0)
            {
                setStart = true;
                startLocation = new int[3] { x, y, z };
                intMaze[x, y, z] = 4;
                DFSMaze[x, y, z] = 4;
                BFSMaze[x, y, z] = 4;

            }

        }
        // repeats until it finds and empty spot for end
        while (!setEnd)
        {
            int x = rnd.Next(intMaze.GetLength(0) - 1), y = rnd.Next(intMaze.GetLength(1) - 1), z = rnd.Next(intMaze.GetLength(2) - 1);
            if (intMaze[x, y, z] == 0)
            {
                setEnd = true;
                endLocation = new int[3] { x, y, z };
                intMaze[x, y, z] = 5;
                //DFSMaze[x, y, z] = 5;
            }
        }
    }

    // gets start cords
    public int[] getStartLocation()
    {
        return startLocation;
    }
    // gets end cords
    public int[] getEndLocation()
    {
        return endLocation;
    }
    public bool BFSCheck(int x, int y, int z)
    {
        if (BFSMaze[x, y, z] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void BFSMove(int x, int y, int z)
    {
        BFSMaze[x, y, z] = 1;
    }
    public int[] DFSMove(int x, int y, int z)
    {
        if (DFSMaze[x + 1, y, z] == 0)
        {
            DFSMaze[x + 1, y, z] = 1;
            return new int[3] { x + 1, y, z };
        }
        else if (DFSMaze[x - 1, y, z] == 0)
        {
            DFSMaze[x - 1, y, z] = 1;
            return new int[3] { x - 1, y, z };
        }
        else if (DFSMaze[x, y + 1, z] == 0)
        {
            DFSMaze[x, y + 1, z] = 1;
            return new int[3] { x, y + 1, z };
        }
        else if (DFSMaze[x, y - 1, z] == 0)
        {
            DFSMaze[x, y - 1, z] = 1;
            return new int[3] { x, y - 1, z };
        }
        else if (DFSMaze[x, y, z + 1] == 0)
        {
            DFSMaze[x, y, z + 1] = 1;
            return new int[3] { x, y, z + 1 };
        }
        else if (DFSMaze[x, y, z - 1] == 0)
        {
            DFSMaze[x, y, z - 1] = 1;
            return new int[3] { x, y, z - 1 };
        }
        else
        {
            return new int[3] { x, y, z };
        }

    }

}

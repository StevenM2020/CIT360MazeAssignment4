using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 *Authors: Ethan Lehutsky, Andy Jackowski, & Steven Motz
 *Purpose: A event handler that links the methods from Maze controller to the GUI interface
 */

public class EventController : MonoBehaviour
{
    int intStage = 0;
    int intDensity = 65;
    int intCameraRotate = 0;
    bool dfsCamera = false;
    bool bfsCamera = true;

    public GameObject stageButton;
    public GameObject stageButtonText;
    public GameObject textX;
    public GameObject textY;
    public GameObject textZ;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public GameObject cameraPos;
    public GameObject cameraPos2;
    public GameObject cameraslider1;
    public GameObject cameraslider2;
    public GameObject debugtext1;
    public GameObject debugtext2;



    private int intDFS, intBFS;


    public void stageButtonController()


    {


        switch (intStage)
        {
            case 0: // maze generation
                if (int.Parse(textX.GetComponent<InputField>().text) > 5 && int.Parse(textY.GetComponent<InputField>().text) >= 1 && int.Parse(textZ.GetComponent<InputField>().text) > 5)
                {
                    if (int.Parse(textX.GetComponent<InputField>().text) * int.Parse(textY.GetComponent<InputField>().text) * int.Parse(textZ.GetComponent<InputField>().text) < 10000)
                    {
                        debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Maze generation started";
                        debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Maze generation started";
                        this.GetComponent<MazeController>().mazeGen(int.Parse(textX.GetComponent<InputField>().text), int.Parse(textY.GetComponent<InputField>().text), int.Parse(textZ.GetComponent<InputField>().text), 70);
                        intStage++;
                        camera1.transform.position = new Vector3(int.Parse(textX.GetComponent<InputField>().text) / 2, int.Parse(textX.GetComponent<InputField>().text) > int.Parse(textZ.GetComponent<InputField>().text) ? int.Parse(textX.GetComponent<InputField>().text) : int.Parse(textZ.GetComponent<InputField>().text), int.Parse(textZ.GetComponent<InputField>().text) / 2);
                        camera2.transform.position = new Vector3(int.Parse(textX.GetComponent<InputField>().text) / 2 + 1000, int.Parse(textX.GetComponent<InputField>().text) > int.Parse(textZ.GetComponent<InputField>().text) ? int.Parse(textX.GetComponent<InputField>().text) : int.Parse(textZ.GetComponent<InputField>().text), int.Parse(textZ.GetComponent<InputField>().text) / 2);
                        cameraPos.transform.position = new Vector3(int.Parse(textX.GetComponent<InputField>().text) / 2, int.Parse(textY.GetComponent<InputField>().text) + 5, int.Parse(textZ.GetComponent<InputField>().text) / 2);
                        cameraPos2.transform.position = new Vector3(int.Parse(textX.GetComponent<InputField>().text) / 2 + 1000, int.Parse(textY.GetComponent<InputField>().text) + 5, int.Parse(textZ.GetComponent<InputField>().text) / 2);
                        debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Maze generation finished, maze size: " + (int.Parse(textX.GetComponent<InputField>().text) * int.Parse(textY.GetComponent<InputField>().text) * int.Parse(textZ.GetComponent<InputField>().text));
                        debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Maze generation finished, maze size: " + (int.Parse(textX.GetComponent<InputField>().text) * int.Parse(textY.GetComponent<InputField>().text) * int.Parse(textZ.GetComponent<InputField>().text));
                        stageButtonText.GetComponent<Text>().text = "Set Start/End";


                    }
                    else
                    {
                        debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Values are too large to compute";
                        debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Values are too large to compute";
                    }
                }
                else
                {
                    debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Maze is too small";
                    debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Maze is too small";
                }
                break;
            case 1: // set start/end
                debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Setting starting point and ending point";
                debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Setting starting point and ending point";
                this.GetComponent<MazeController>().mazeStartAndEnd();
                intStage++;
                stageButtonText.GetComponent<Text>().text = "DFS";
                break;
            case 2: // DFS
                debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Starting DFS";
                intDFS = this.GetComponent<MazeController>().runDFS();
                if (intDFS == -1)
                {
                    debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Could not find a path";
                }
                else
                {
                    debugtext1.GetComponent<Text>().text = debugtext1.GetComponent<Text>().text + "\n" + "Finished DFS, path size: " + intDFS;
                }
                intStage++;
                stageButtonText.GetComponent<Text>().text = "BFS";
                break;
            case 3: // BFS
                if (intDFS == -1)
                {
                    debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Could not find a path";
                }
                else
                {
                    debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Starting BFS";
                    intBFS = this.GetComponent<MazeController>().runBFS();
                    debugtext2.GetComponent<Text>().text = debugtext2.GetComponent<Text>().text + "\n" + "Finished BFS, path size: " + intBFS;
                }
                intStage++;
                stageButtonText.GetComponent<Text>().text = "Restart";
                break;
            case 4: // restart
                SceneManager.LoadScene("Maze");
                break;
        }
    }
    public void reloadScene()
    {
        SceneManager.LoadScene("Maze");
    }

    public void Camera1SliderUpdate()
    {

        cameraPos.transform.eulerAngles = new Vector3(cameraPos.transform.eulerAngles.x, cameraslider1.GetComponent<Slider>().value, cameraPos.transform.eulerAngles.z);
    }



    public void Camera2SliderUpdate()
    {
        cameraPos2.transform.eulerAngles = new Vector3(cameraPos2.transform.eulerAngles.x, cameraslider2.GetComponent<Slider>().value, cameraPos.transform.eulerAngles.z);
    }


    public void Camera1DropdownUpdate(int val)
    {

        camera1.enabled = dfsCamera;
        dfsCamera = !dfsCamera;
    }
    public void Camera2DropdownUpdate(int val)
    {
        camera3.enabled = bfsCamera;
        bfsCamera = !bfsCamera;
    }
    void Start()
    {

        camera3.enabled = false;

    }
    public void exitMaze()
    {
        Application.Quit();
    }

}

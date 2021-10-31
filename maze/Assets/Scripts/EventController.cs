using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 *Authors: Ethan Lehutsky and Steven Motz
 *Purpose: A event handler that links the methods from Maze controller to the GUI interface
 */

public class EventController : MonoBehaviour
{
    int intStage = 0;
    int intDensity = 65;


    public GameObject stageButton;
    public GameObject stageButtonText;
    public GameObject textX;
    public GameObject textY;
    public GameObject textZ;
    public GameObject densitySlider;
    public Camera camera1;

    public Material cubeMaterial;
    public Material outerwallMaterial;
    public Material topOrBottomMaterial;

    private int intDFS, intBFS;
    public void stageButtonController()
    {
        

        switch (intStage)
        {
            case 0: // maze generation
                if(int.Parse(textX.GetComponent<InputField>().text) > 5 && int.Parse(textY.GetComponent<InputField>().text) >= 1 && int.Parse(textZ.GetComponent<InputField>().text) > 5)
                {
                    // both consoles should say maze generation started *************
                    this.GetComponent<MazeController>().mazeGen(int.Parse(textX.GetComponent<InputField>().text), int.Parse(textY.GetComponent<InputField>().text), int.Parse(textZ.GetComponent<InputField>().text), intDensity);
                    intStage++;
                    camera1.transform.position = new Vector3(int.Parse(textX.GetComponent<InputField>().text) / 2, int.Parse(textY.GetComponent<InputField>().text) + 20, int.Parse(textZ.GetComponent<InputField>().text)/2);
                    // both consoles should say maze generation finished, maze size: (x * y * z) **********
                    stageButtonText.GetComponent<Text>().text = "Set Start/End";
                }
                else
                {
                    // both consoles need to say "maze size is too small" ************************************* <- to be notest
                }
                break;
            case 1: // set start/end
                // both consoles should say setting starting point and ending point *************
                this.GetComponent<MazeController>().mazeStartAndEnd();
                intStage++;
                stageButtonText.GetComponent<Text>().text = "DFS";
                break;
            case 2: // DFS
                // DFS console should say starting DFS **********
                intDFS = this.GetComponent<MazeController>().runDFS();
                if(intDFS == -1)
                {
                    // DFS console should say could not find a path or something **********
                }
                else
                {
                    // DFS console should say Finished DFS, path size intDFS **********
                }
                intStage++;
                stageButtonText.GetComponent<Text>().text = "BFS";
                break;
            case 3: // BFS
                if (intDFS == -1)
                {
                    // BFS console should say could not find a path or something **********
                }
                else
                {
                    // BFS console should say starting DFS
                    intBFS = this.GetComponent<MazeController>().runBFS();
                    
                    // BFS console should say Finished DFS, path size intBFS ********** 
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
    public void MazeTransUpdater()
    {
        // not done
    }
    public void wallsTransUpdater()
    {
        // not done
    }
    public void dencitySliderUpdater()
    {
       intDensity = (int)densitySlider.GetComponent<Slider>().value;
    }
    void Start()
    {



    }
   

}

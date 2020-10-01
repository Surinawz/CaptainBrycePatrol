using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    List<string> Instructions = new List<string>();
    private string pageText;
    [SerializeField] private Text tutorialText;
    private int tutorialCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "";
        MakeInstructions();
        InvokeRepeating("TutorialTextUpdate",2f, 5f);
      

    }

    private void MakeInstructions()
    {
        
        Instructions.Add("Welcome to Captain Bryce Patrol");
        Instructions.Add("An Arcaxer Fangame");
        Instructions.Add("Featuring the Adventures of Captain Bryce");
        Instructions.Add("This is in Super Beta Right Now");
        Instructions.Add("But you can Fly and Shoot Stuff");
        Instructions.Add("Ready for Instructions? Feel free to test them out as we go.");
        Instructions.Add("To look around... look around");
        Instructions.Add("Use the Left Control Stick to Move forward and back, and turn");
        Instructions.Add("Use the Right Grip to Lift up");
        Instructions.Add("Use the Left Grip to Lower Down (If gravity isnt fast enough for you)");
        Instructions.Add("Use the right Control Stick to Aim the Cannon");
        Instructions.Add("Use the right trigger to fire Greenaga!");
        Instructions.Add("Hope you got that... entering the test sim!");
    }

    // Update is called once per frame
    void Update()
    {
  
       
    }

    private void TutorialTextUpdate()
    {
        if (tutorialCounter <= Instructions.Count-1)
        {
            tutorialText.text = Instructions[tutorialCounter];
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        tutorialCounter++;

    }
}

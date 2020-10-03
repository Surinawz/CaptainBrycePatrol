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
    

    void Awake()
    {
    
    }

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
        Instructions.Add("But you can Fly and Shoot Hax from the Cannon");
        Instructions.Add("Ready for Instructions? Feel free to test them out as we go.");
        Instructions.Add("To look around... look around");
        Instructions.Add("Use the Left Control Stick to Move forward and back, and turn");
        Instructions.Add("Use the Right Grip to Lift up");
        Instructions.Add("Use the Left Grip to Lower Down \n(If gravity isnt fast enough for you)");
        Instructions.Add("Your Right Hand is a Cannon that focus's the power of Hax");
        Instructions.Add("Use the right trigger to fire some Hax");
        Instructions.Add("Use the A Button to cycle through Hax \nYour Active Hax is on the Bow Display");
        Instructions.Add("You need AP to fire Hax. \nYour available AP is on the botton left of the Bow Display");
        Instructions.Add("Some Hax give you more AP when they are active. Cool right?");
        Instructions.Add("Hope you got that...\n entering the test sim!");
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

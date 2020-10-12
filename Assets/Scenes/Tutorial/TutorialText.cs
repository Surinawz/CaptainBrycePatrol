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
        #region Instructions Text
        Instructions.Add("Welcome to Captain Bryce Patrol");
        Instructions.Add("An Arcaxer Fangame\nFeaturing the Adventures of Captain Bryce");
        Instructions.Add("This is in Super Beta Right Now\nBut you can Fly and Shoot Hax from the Cannon");
        Instructions.Add("Ready for Instructions? Feel free to test them out as we go.");
        Instructions.Add("To look around... look around");
        Instructions.Add("Use the Left Control Stick to Move forward and back, and to turn");
        Instructions.Add("Use the Right Grip to lift up and Fly");
        Instructions.Add("Use the Left Grip to diver Down \n(If gravity isnt fast enough for you)");
        Instructions.Add("Your Right Hand is a Cannon that focuses the power of Hax");
        Instructions.Add("Use the right trigger to fire Hax");
        Instructions.Add("The 4 orbs on the deck are your loadout. \nTouch an orb to activate that Hax");
        Instructions.Add("We will add more Hax in the future and a menu to change your loadout.");
        Instructions.Add("You use up AP to fire Hax. \nYour available AP is on the bottom left of the Bow Display");
        Instructions.Add("Some Hax give you more AP when they are active. Cool right?");
        Instructions.Add(
            "For reasons of eventual story, your left hand is a flashlight.\nPull the left trigger to turn it off and on.");
        Instructions.Add("Hope you got that...\n entering the test sim!");
        Instructions.Add("Kill the toxins and find the entrance to the underground drain.\n It's enormous and coffee colored.");
        #endregion
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

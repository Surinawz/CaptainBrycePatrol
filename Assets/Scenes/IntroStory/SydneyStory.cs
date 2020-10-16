using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


#region Create Class for ScriptLines
public class ScriptLine
{   
    public int Order { get; set; }
    public string Speaker { get; set; }
    public string Saying { get; set; }
    public string Button1Text { get; set; }
    public string Button2Text { get; set; }
    public int? Choice1Destination { get; set; }
    public int? Choice2Destination { get; set; }

    public ScriptLine(int order,string speaker, string saying,string button1Text,string button2Text,int? choice1Destination,int? choice2Destination)
    {
        Order = order;
        Speaker = speaker;
        Saying = saying;
        Button1Text = button1Text;
        Button2Text = button2Text;
        Choice1Destination = choice1Destination;
        Choice2Destination = choice2Destination;
    }

}
#endregion


public class SydneyStory : MonoBehaviour
{

    #region Define variables
    [SerializeField] public GameObject Player; // Used to make sure Dialog Boxes always face the player
    [SerializeField] public GameObject DialogBox; // Used to Toggle Dialog Box Visibility
    [SerializeField] public TextMeshProUGUI speaker; // The textbox showing who is speaking
    [SerializeField] public TextMeshProUGUI saying; // The textbox showing the dialog text
    [SerializeField] public TextMeshProUGUI choice1; // Text on Button 1
    [SerializeField] public TextMeshProUGUI choice2; // Text on Button 2
    [SerializeField] public Button choice1Button; // Used to Enable or disable Button 1
    [SerializeField] public Button choice2Button;// Used to Enable or disable Button 2

    private CharacterController playerControls; // used to disable movement controls when Dialog box is open

    public List<ScriptLine> MissionScript = new List<ScriptLine>(); // Stores Script Lines for iterating
    private int? DialogCurrent = 1; // Stores current Scriptline to display
    private bool dialogTriggered = false; // Stores whether this Conversation has already happened to supress for duration of scene
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Get Player Control Component, make sure dialog box is not visible, Instantiate scriptlines and add to list
        playerControls = Player.GetComponent<CharacterController>();
        HideDialogBox();
        BuildMissionScript();
        #endregion

    }

    private void BuildMissionScript()
    {
        #region Write Script and Add to list for Iteration
        string sydney = "Sydney";
        string bryce = "Bryce";

        /*
         Conversations work by creating a Choose your own adventure style system. Each Script line has 2 choices
         and the class of Scriptline assigns a destination Scriptline to continue the conversation.
         Buttons are nulled if there is no option text present
         */



        MissionScript.Add(new ScriptLine(1, sydney, "Hey Bryce! Is that you out there?",  null, "Yes", null,  2));
        MissionScript.Add(new ScriptLine(2, bryce, "Ahoy there Sydney! Let me come on in.",  null,  "Go in", null, 3));
        MissionScript.Add(new ScriptLine(3, sydney, "OH NO YOU DON'T!",  null,   "???", null, 4));
        MissionScript.Add(new ScriptLine(4, sydney, "I haven't had coffee today. No one sees me before I have coffee.", null,   "Ok?", null, 5));
        MissionScript.Add(new ScriptLine(5, sydney, "Something is wrong with the pipeline. It isn't pumping.", "Maybe...",  "Oh no?!", 6, 13));
        MissionScript.Add(new ScriptLine(6, bryce, "Maybe this is a good thing! I don't need coffee and look at me go.", null,   "Go!", null, 7));
        MissionScript.Add(new ScriptLine(7, sydney, "I will hit you with this coffee mug.",  null,  "*Duck*", null, 8));
        MissionScript.Add(new ScriptLine(8, bryce, "I think we better get this coffee situation sorted out.",  null,   "*Dust off*", null, 9));
        MissionScript.Add(new ScriptLine(9, sydney, "I need you to go investigate.",  "Why me?",  "Of course", 10, 15));
        MissionScript.Add(new ScriptLine(10, sydney, "Because the Arcaxer is busy saving the world and he never summons you anyway.",  "Harsh.",  "He's strong!", 11, 12));
        MissionScript.Add(new ScriptLine(11, bryce, "Now that you mention it...", null, "*frown*", null, 15));
        MissionScript.Add(new ScriptLine(12, bryce, "It's good to see he can handle himself.",  null,   "*Smile*", null, 15));
       
        MissionScript.Add(new ScriptLine(13, bryce, "Oh no! What could possibly cause this?",  null,  "???", null, 14));
        MissionScript.Add(new ScriptLine(14, sydney, "I have no idea. I need you to go investigate.",  "Why me?",  "Of course", 10, 15));

        MissionScript.Add(new ScriptLine(15, sydney, "There is an exhaust up in the mountains that you can use to enter the pipes",  null,   "Exhaust Pipe?", null, 16));
        MissionScript.Add(new ScriptLine(16, sydney, "It is huge. You'll find it. It is stained brown from coffee.",  null,   "You got it!", null, 17));
        MissionScript.Add(new ScriptLine(17, bryce, "Better get back to my ship!",  null,   "Get to it", null, 0));
        MissionScript.Add(new ScriptLine(0, sydney, "", null, "", null, null));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        DialogBox.transform.LookAt(Player.transform.position);
    }

    void ShowDialogBox()
    {
        DialogBox.SetActive(true);
        playerControls.enabled = false;
        
    }

    void HideDialogBox()
    {
        DialogBox.SetActive(false);
        playerControls.enabled = true;
    }

 

    void OnTriggerEnter(Collider collider)
    {
        if (dialogTriggered == false)
        {
            StartDialog();
        }
        
        
    }

    void StartDialog()
    {

     ShowDialogBox();
     DialogCurrent = 1;
     dialogTriggered = true;
     UpdateDialog(DialogCurrent);
    }
    

    void UpdateDialog(int? currentDialog)
    {
        DialogCurrent = currentDialog;
        ScriptLine currentScriptLine = MissionScript.Single(s => s.Order == DialogCurrent);

        print(currentDialog);
        print(currentScriptLine.Speaker);
        print(currentScriptLine.Saying);
        print(choice1Button.enabled);
        print(choice2Button.enabled);

        if (currentDialog == 0)
        {
            HideDialogBox();
            SceneManager.LoadScene(3);
        }
        else
        {
            speaker.text = currentScriptLine.Speaker;
            saying.text = currentScriptLine.Saying;
            choice1Button.enabled = (currentScriptLine.Choice1Destination)!=null;
            choice2Button.enabled = (currentScriptLine.Choice2Destination)!=null;
            choice1.text = currentScriptLine.Button1Text;
            choice2.text = currentScriptLine.Button2Text;

        print(choice1Button.enabled);
        print(choice2Button.enabled);
        }

    }

    public void OnButton1Click()
    {
        
        UpdateDialog(MissionScript.Single(s => s.Order == DialogCurrent).Choice1Destination);
    }

    public void OnButton2Click()
    {
      UpdateDialog(MissionScript.Single(s => s.Order == DialogCurrent).Choice2Destination);
    }


}

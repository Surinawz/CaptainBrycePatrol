using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject DialogBox;

    // Start is called before the first frame update
    void Start()
    {
        HideDialogBox();
    }

    void HideDialogBox()
    {
        DialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGameButton()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}

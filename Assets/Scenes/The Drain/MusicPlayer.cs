using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        int numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        print(numberOfMusicPlayers);
        if (numberOfMusicPlayers > 1)
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

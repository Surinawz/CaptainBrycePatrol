using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPipe1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        #region detect hull hitting next level marker inside Coffee Exhaust Pipe
        if (collider.name == "Hull")
        {
            SceneManager.LoadScene(4);
        }
        #endregion
    }


}

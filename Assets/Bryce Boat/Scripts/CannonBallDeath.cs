using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject,2f);
    }

  
    // Update is called once per frame
    void Update()
    {
        
    }
}

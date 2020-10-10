using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaxOnCollision : MonoBehaviour
{
  
    private Light deathLight;

    // Start is called before the first frame update
    void Start()
    {
        
        deathLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Detect if Collision is an Enemy and if so, flash and destroy
        if (collision.gameObject.tag == "Enemy")
        {
            deathLight.intensity = 30f;
            Destroy(transform.gameObject, .05f);
        }
        #endregion

    }
}

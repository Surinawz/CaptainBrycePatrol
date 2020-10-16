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
            if (gameObject.name == "Periwinkle")
            {
                var meshes = collision.gameObject.GetComponentsInChildren<MeshRenderer>();
                var peri = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
                foreach (MeshRenderer m in meshes )
                {
                    m.material = peri.material;
                }

            }

            Destroy(transform.gameObject, .05f);
        }
        #endregion

    }
}

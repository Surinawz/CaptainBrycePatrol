using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHaxFire : MonoBehaviour
{
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
    GameObject BryceBoat = GameObject.Find("BryceBoat");
        target = BryceBoat.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 50 * Time.deltaTime);

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        if (Vector3.Distance(transform.position,target)<5)
        {
            DestroyImmediate(gameObject);
        }
    }
}

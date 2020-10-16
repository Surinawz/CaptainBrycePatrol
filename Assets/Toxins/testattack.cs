using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testattack : MonoBehaviour
{

    [SerializeField] private GameObject camera;

    private Vector3 PositionAtInstantiation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, camera.transform.position) < 200)
        {
            PositionAtInstantiation = camera.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, PositionAtInstantiation, 20 * Time.deltaTime);
        }
    }
}

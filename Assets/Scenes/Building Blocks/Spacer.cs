using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
[SelectionBase]
public class Spacer : MonoBehaviour
{
    [SerializeField] private int horizontalGridSize;
    [SerializeField] private int verticalGridSize;
    [SerializeField] private int lateralGridSize;
    private Vector3 pipePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SnapEndsTogether();
    }

    void SnapEndsTogether()
    {
        pipePos = new Vector3(
             Mathf.RoundToInt(transform.position.x/horizontalGridSize)*horizontalGridSize,
             Mathf.RoundToInt(transform.position.y/verticalGridSize) * verticalGridSize,
             Mathf.RoundToInt(transform.position.z/lateralGridSize) * lateralGridSize
        );

        transform.position = pipePos;
    }

}

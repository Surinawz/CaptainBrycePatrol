using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{

    #region Assign Variables

    #region Boat Movement Attributes
    [SerializeField] private float boatSpeed=8000; // forward thrust
    [SerializeField] private float turnSpeed=60; // speed of rotation in turns
    [SerializeField] private float lift=5000; // Power of lift and drop
    [SerializeField] private float rotationFromFriction=5; // angle of centrifugal force when turning
    [SerializeField] private float planeForce=-2; // rising angle of bow when moving forward
    [SerializeField] private float liftForce=20; // rising angle of bow when lifting
    [SerializeField] private float floatForce = 50; // upward force due to boyancy on lakes of coffee
    private Rigidbody boatRigidbody; // assigns rigidbody for boat
    private float turning = 0; // instantiate float for turn calculation later
#endregion

    #region Level Objects 
    [SerializeField] private Plane water;
#endregion

    #region Controller Throw Values
    private float horizontalThrow;
    private float verticalThrow;
    private float liftThrow;
    private float dropThrow;
    private float ovrLiftThrow;
    private float ovrDropThrow;
    #endregion

#endregion  

    // Start is called before the first frame update
    void Start()
    {
    #region Assign Components
            boatRigidbody = GetComponent<Rigidbody>();
    #endregion
    }

    // Update is called once per frame
    void Update()
    {
    #region Method for Boat Movement
        ProcessTranslation();
        ProcessRotation();
        ProcessLift();
        ProcessDrop();
       // ProcessFloat();
#endregion
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CoffeeLake")
        {
            boatRigidbody.AddRelativeForce(Vector3.up * floatForce * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "CoffeeLake")
        {
            boatRigidbody.AddRelativeForce(Vector3.up * floatForce * Time.deltaTime);
        }
    }

   
    /*   **Should be handled by Triggers now
    private void ProcessFloat()
    {
    #region Provide lift force to bounce at water level
            if (transform.position.y < 2 && SceneManager.GetActiveScene().buildIndex<2)
            {
                boatRigidbody.AddRelativeForce(Vector3.up*lift*Time.deltaTime);
            }
    #endregion
    }
    */

    private void ProcessLift()
    {
    #region Assign Controller Throw Values
            liftThrow = Input.GetAxis("Jump");
            ovrLiftThrow = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
    #endregion
    #region Activate Lift
            if (liftThrow>0||ovrLiftThrow>0)
            {
                boatRigidbody.AddRelativeForce(Vector3.up * lift * Time.deltaTime);
             };
    #endregion
    }

    private void ProcessDrop()
    {
    #region Assign Controller Throw Values
            dropThrow = Input.GetAxis("Fire1");
            ovrDropThrow = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
    #endregion
    #region Activate Drop
            if (dropThrow > 0 || ovrDropThrow > 0)
            {
                boatRigidbody.AddRelativeForce(Vector3.down * lift * Time.deltaTime);
            };
    #endregion
    }

    private void ProcessTranslation()
    {
    #region Assign Controller Throw Values
            verticalThrow = Input.GetAxis("Vertical");
    #endregion

    #region Activate Thrust
        #region Activate Forward Thrust
            if (verticalThrow > 0)
            {
                boatRigidbody.AddRelativeForce(Vector3.forward * verticalThrow * boatSpeed*Time.deltaTime);
            }
        #endregion
        #region Activate Aft Thrust at half power
            else if (verticalThrow < 0)
            {
                boatRigidbody.AddRelativeForce(Vector3.forward * verticalThrow * boatSpeed* .5f*Time.deltaTime);
            }
        #endregion
    #endregion  
    }

    private void ProcessRotation()
    {
    #region Assign Controller Throw Values
            horizontalThrow = Input.GetAxis("Horizontal");
    #endregion
    #region Assign All Rotation Parameters
            turning += (horizontalThrow * turnSpeed * Time.deltaTime); // Left and Right Turn 
                float turnForce = transform.localRotation.z + (horizontalThrow * rotationFromFriction); // Z Rotation to simulate Centrifugal Force
                liftForce = -boatRigidbody.velocity.y; // Modifier for Bow Angle based on Lift or Drop
                float ridePlane = transform.localRotation.x + (verticalThrow * planeForce) + (liftForce); //Modifier for Bow Angle with Forward Velocity with Lift/Drop
    #endregion
    #region Update Rotation
            transform.localRotation = Quaternion.Euler(ridePlane, turning, turnForce);
    #endregion  
    }
}

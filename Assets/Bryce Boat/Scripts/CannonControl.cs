using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonControl : MonoBehaviour
{
#region Assign Variable
    #region Cannon Attributes
        [SerializeField] private float cannonAimSpeed; // how fast the cannon turns
        [SerializeField] private ParticleSystem cannonFire; // TO DO: Make an explosion on firing
        private Quaternion currentAngle; // stores current angle for lerping
        [SerializeField] private float lerpSpeed; // Sets lerp speed for rotating cannon
    #endregion
    #region Cannon Ball Attributes
        [SerializeField] GameObject cannonBall; // For assigning Cannon Ball Prefab for Instantiation
        Rigidbody cannonBallBody; // Assigns rigid body for cannon Balls after instantiation
        [SerializeField] public float firePower; // Cannon Ball Firing Speed
        public Transform shotPos; // Stores Position and Angle when Instantiating Cannon Ball
    #endregion
    #region Controller Throw Variables
        private float yThrow; //Right Thumbstick up and down
        private float xThrow; //Right Thumbstick left and right
        private float ovrYThrow; //Oculus Specific Right Thumbstick up and down
        private float ovrXThrow; //Oculus Specific Right Thumbstick left and right
        //private float cannonThrow; // Fire 1 button for testing in editor
        private float ovrCannonThrow; // Oculus Specific Right Index Trigger
        private Vector2 rightThumbStick;
    #endregion
#endregion

    // Start is called before the first frame update
    void Start()
    {
        // Instantiating a value for Testing
        currentAngle = transform.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
    #region Cannon Controls
        FireCannon();
        RotateCannon();
    #endregion
    }

    private void RotateCannon()
    {
        #region //Get Current Status of Cannon Angle and Controller Input
        currentAngle = transform.localRotation;
        yThrow += Input.GetAxis("Mouse Y") * cannonAimSpeed;
        xThrow += Input.GetAxis("Mouse X") * cannonAimSpeed;
        #endregion

        #region //Limit Cannon Rotation
        if (currentAngle.eulerAngles.y > 30 && currentAngle.eulerAngles.y < 100)
        {
            xThrow = -cannonAimSpeed;
        }
        if (currentAngle.eulerAngles.y < 330 && currentAngle.eulerAngles.y > 200)
        {
            xThrow = cannonAimSpeed;
        }
        if (currentAngle.eulerAngles.x < 330)
        {
            yThrow = -cannonAimSpeed;
        }
        #endregion

        #region // Update Rotation Destination and Lerp
        Quaternion updatedRotation = Quaternion.Euler(yThrow, xThrow, 0f);
        transform.localRotation = Quaternion.Lerp(currentAngle, updatedRotation, Time.deltaTime * lerpSpeed);
        #endregion

    }

    private void FireCannon()
    {
        #region // Count Existing Projectives and Suppress Fire at 30
        float cannonCallCount = GameObject.FindGameObjectsWithTag("Cannonball").Length;
        if (cannonCallCount > 30)
        {
            return;
        }
        #endregion
        #region // Fire Projectile
        else 
        {
            shotPos.rotation = transform.rotation;
            ovrCannonThrow = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            //cannonThrow = Input.GetAxis("Fire1");
            print(shotPos.position);
            if (ovrCannonThrow > .5)
            {
                GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, shotPos.rotation) as GameObject;
                cannonBallBody = cannonBallCopy.GetComponent<Rigidbody>();
                cannonBallBody.AddForce(transform.forward * firePower);
            }
        }
        #endregion
    }
}




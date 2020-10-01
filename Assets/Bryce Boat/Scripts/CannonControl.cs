using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hax
{
    public string Name { get; set; }
    public GameObject CannonBall { get; set; }
    public float APMaxMultiplier { get; set; }
    public float baseDamage { get; set; }

    public Hax(string name, GameObject cannonball, float apmaxmultiplier, float basedamage)
    {
        Name = name;
        CannonBall = cannonball;
        APMaxMultiplier = apmaxmultiplier;
        baseDamage = basedamage;
    }
}

public class Player
{
    public float APMax { get; set; }
    public List<Hax> Haxlist { get; set; }

    public float EquippedMaxAP(Hax hax)
    {
        return (APMax * hax.APMaxMultiplier)-1;
    }

    public float APAvailable(Hax hax,float haxCast)
    {
        return (APMax * hax.APMaxMultiplier) - haxCast;
    }
}

public class CannonControl : MonoBehaviour
{
    #region Assign Variables
    #region Cannon Attributes
        [SerializeField] private float cannonAimSpeed; // how fast the cannon turns
        [SerializeField] private ParticleSystem cannonFire; // TO DO: Make an explosion on firing
        private Quaternion currentAngle; // stores current angle for lerping
        [SerializeField] private float lerpSpeed; // Sets lerp speed for rotating cannon
    #endregion
    #region Cannon Ball Attributes

        [SerializeField] private GameObject Hax; // For assigning Cannon Ball Prefab for Instantiation
        private Hax cannonBall; // Creates Hax Class that add attributes to projectile game object
        Rigidbody cannonBallBody; // Assigns rigid body for cannon Balls after instantiation
        [SerializeField] public float firePower; // Cannon Ball Firing Speed
        public Transform shotPos; // Stores Position and Angle when Instantiating Cannon Ball       
        [SerializeField] Text HaxChoice; // Text box that shows the active Hax in the cannon
        List<Hax> Haxes = new List<Hax>(); // List of Hax available to Player
        private float cannonCallCount = 0;
        enum HaxList // Assign the Name to the Child Order for Instantiating Children of Hax GameObject
          {
        Greenaga = 0,
        Redaga = 1,
        Lemon = 2
          }
    #endregion
    #region Controller Throw Variables
        private float yThrow; //Right Thumbstick up and down
        private float xThrow; //Right Thumbstick left and right
        private float ovrYThrow; //Oculus Specific Right Thumbstick up and down
        private float ovrXThrow; //Oculus Specific Right Thumbstick left and right
        private bool aButton;
        private float cannonThrow; // Fire 1 button for testing in editor
        private float ovrCannonThrow; // Oculus Specific Right Index Trigger
        private Vector2 rightThumbStick;
    #endregion

    Player player = new Player();
    [SerializeField] Text APDisplay;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        player.APMax = 5;


        currentAngle = transform.localRotation;// Instantiating a value for Testing
    #region Instantiate Available Hax and Set Default Hax
        MakeHaxList();
        cannonBall = Haxes.Single(c => c.Name == "Greenaga");
        UpdateHaxNotifier();
    #endregion
    }

    private void MakeHaxList()
    {
    #region Instantiate Hax
        Hax Greenaga = new Hax("Greenaga",Hax.transform.GetChild((int)HaxList.Greenaga).gameObject,5,10);
        Hax Redaga = new Hax("Redaga",Hax.transform.GetChild((int)HaxList.Redaga).gameObject, 5, 10);
        Hax Lemon = new Hax("A Lemon",Hax.transform.GetChild((int)HaxList.Lemon).gameObject, 1, 10);
    #endregion
    #region Add to List of Available Hax
        Haxes.Add(Greenaga);
        Haxes.Add(Redaga);
        Haxes.Add(Lemon);
    #endregion
    }

    // Update is called once per frame
    void Update()
    {
     #region Cannon Controls
        FireCannon();
        RotateCannon();
        ChangeHax();
        #endregion
     cannonCallCount = GameObject.FindGameObjectsWithTag("Cannonball").Length;
        if (player.APAvailable(cannonBall, cannonCallCount) < 0)
        {
            APDisplay.text = 0.ToString();
        }
        else
        {
            APDisplay.text = player.APAvailable(cannonBall, cannonCallCount).ToString();
        }
        
    }

    private void ChangeHax()
    {
        #region Cycle Through Hax
        if (Input.GetButtonDown("HaxSelect")|| OVRInput.GetDown(OVRInput.Button.One,OVRInput.Controller.RTouch))
        {
            if (cannonBall == Haxes.Single(c=>c.Name=="Greenaga"))
            {
                cannonBall = Haxes.Single(c => c.Name == "Redaga");
            }
            else if (cannonBall == Haxes.Single(c => c.Name == "Redaga"))
            {
                cannonBall = Haxes.Single(c => c.Name == "A Lemon");
            }
            else
            {
                cannonBall = Haxes.Single(c => c.Name == "Greenaga");
            }
            UpdateHaxNotifier();
        }
        #endregion
    }

    private void UpdateHaxNotifier()
    {
    #region Update the Name of Active Hax on the HUD
        HaxChoice.text = cannonBall.CannonBall.name;
        MeshRenderer haxMesh = cannonBall.CannonBall.transform.GetChild(0).GetComponent<MeshRenderer>();
        Material haxMaterial = haxMesh.sharedMaterials[0];
        Color haxColor = haxMaterial.color;
        HaxChoice.color = haxColor;
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
        
        if (cannonCallCount > player.EquippedMaxAP(cannonBall))
        {
            return;
        }
        #endregion
        #region // Fire Projectile
        else 
        {
            shotPos.rotation = transform.rotation;
            ovrCannonThrow = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            cannonThrow = Input.GetAxis("Fire2");
            print(shotPos.position);
            

         if (ovrCannonThrow > .5||cannonThrow>.5)
            {
                GameObject cannonBallCopy = Instantiate(cannonBall.CannonBall, shotPos.position, shotPos.rotation) as GameObject;
                cannonBallBody = cannonBallCopy.GetComponent<Rigidbody>();
                cannonBallBody.AddForce(transform.forward * firePower);
                GameObject.Destroy(cannonBallCopy,2);
            }
        }
        #endregion
    }
}




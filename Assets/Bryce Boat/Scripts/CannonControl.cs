using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


#region Hax Class

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

#endregion

#region Player Class

public class Player
{
    public float APMax { get; set; }
    public List<Hax> Haxlist { get; set; }

    public float EquippedMaxAP(Hax hax)
    {
        return (APMax * hax.APMaxMultiplier) - 1;
    }

    public float APAvailable(Hax hax, float haxCast)
    {
        return (APMax * hax.APMaxMultiplier) - haxCast;
    }
}

#endregion


public class CannonControl : MonoBehaviour
{
    #region Assign Variables


    #region Hax Attributes

    [SerializeField] public GameObject Hax; // For assigning Cannon Ball Prefab for Instantiation
    private Hax cannonBall; // Creates Hax Class that add attributes to projectile game object
    Rigidbody cannonBallBody; // Assigns rigid body for cannon Balls after instantiation
    [SerializeField] public float firePower; // Cannon Ball Firing Speed
    public Transform shotPos; // Stores Position and Angle when Instantiating Cannon Ball       
    [SerializeField] Text HaxChoice; // Text box that shows the active Hax in the cannon
    public List<Hax> Haxes = new List<Hax>(); // List of Hax available to Player
    private float cannonCallCount = 0;

    public enum HaxList // Assign the Name to the Child Order for Instantiating Children of Hax GameObject
    {
        Greenaga = 0,
        Redaga = 1,
        Lemon = 2,
        Stabby = 3
    }

    #endregion

    #region Controller Throw Variables

  
    private bool aButton;
    private bool cannonThrow; // Fire 1 button for testing in editor
    private bool ovrCannonThrow; // Oculus Specific Right Index Trigger
    private Vector2 rightThumbStick;

    #endregion

    #region Update Bow Display
    public Player player = new Player();
    [SerializeField] Text APDisplay;
    [SerializeField] private Text ToxinDisplay;
    [SerializeField] private GameObject EnemiesOnTerrain;
    private int Enemies;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Initialize Player Stats
        player.APMax = 5;
#endregion

        #region Initialize Available Hax and Set Default Hax

        MakeHaxList();
        cannonBall = Haxes.Single(c => c.Name == "Greenaga");
        UpdateHaxNotifier();

        #endregion
    }

    void MakeHaxList()
    {
        #region Initialize Hax

        Hax Greenaga = new Hax("Greenaga", Hax.transform.GetChild((int) HaxList.Greenaga).gameObject, 5, 10);
        Hax Redaga = new Hax("Redaga", Hax.transform.GetChild((int) HaxList.Redaga).gameObject, 5, 10);
        Hax Lemon = new Hax("A Lemon", Hax.transform.GetChild((int)HaxList.Lemon).gameObject, 1, 10);
        Hax Stabby = new Hax("Stabby", Hax.transform.GetChild((int)HaxList.Stabby).gameObject, 1, 10);
        #endregion

        #region Add to List of Available Hax

        Haxes.Add(Greenaga);
        Haxes.Add(Redaga);
        Haxes.Add(Lemon);
        Haxes.Add(Stabby);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        Enemies = EnemiesOnTerrain.transform.childCount;
        ToxinDisplay.text = Enemies.ToString();

        #region Cannon Controls

        FireCannon();

        #region Check for Available AP

        cannonCallCount = GameObject.FindGameObjectsWithTag("Cannonball").Length;
        if (player.APAvailable(cannonBall, cannonCallCount) < 0)
        {
            APDisplay.text = 0.ToString();
        }
        else
        {
            APDisplay.text = player.APAvailable(cannonBall, cannonCallCount).ToString();
        }

        #endregion

        #endregion
    }
    
    void OnTriggerEnter(Collider collision)
    {
        try
        {

            if (collision.tag == "Loadout")
            {
                cannonBall = Haxes.Single(c => c.Name == collision.name);
                UpdateHaxNotifier();
            }
        }
        catch
        {
        }

    }

    void OnTriggerStay(Collider collision)
    {

    }

    void OnTriggerExit(Collider collision)
    {
  
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
    
    private void FireCannon()
    {
        #region Check for Available AP
        if (cannonCallCount > player.EquippedMaxAP(cannonBall))
        {
            return;
        }
        #endregion
        #region Fire Projectile
        else
        {
            shotPos.rotation = transform.rotation;
            ovrCannonThrow = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.RTouch);
            cannonThrow = Input.GetMouseButtonDown(0);

            if (ovrCannonThrow || cannonThrow)
            {
                GameObject cannonBallCopy =
                    Instantiate(cannonBall.CannonBall, shotPos.position, shotPos.rotation) as GameObject;
                cannonBallBody = cannonBallCopy.GetComponent<Rigidbody>();
                cannonBallBody.AddForce(transform.forward * firePower);
                GameObject.Destroy(cannonBallCopy, 2);
            }
        }

        #endregion
    }
}





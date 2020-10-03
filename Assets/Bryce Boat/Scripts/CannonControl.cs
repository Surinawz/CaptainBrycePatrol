using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
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


    #region Cannon Ball Attributes

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
    private float cannonThrow; // Fire 1 button for testing in editor
    private float ovrCannonThrow; // Oculus Specific Right Index Trigger
    private Vector2 rightThumbStick;

    #endregion

    private bool levelDone = false;


    public Player player = new Player();
    [SerializeField] Text APDisplay;
    [SerializeField] private Text ToxinDisplay;
    [SerializeField] private GameObject EnemiesOnTerrain;
    private int Enemies;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        player.APMax = 5;


        #region Instantiate Available Hax and Set Default Hax

        MakeHaxList();
        cannonBall = Haxes.Single(c => c.Name == "Greenaga");
        UpdateHaxNotifier();

        #endregion
    }

    void MakeHaxList()
    {
        #region Instantiate Hax

        Hax Greenaga = new Hax("Greenaga", Hax.transform.GetChild((int) HaxList.Greenaga).gameObject, 5, 10);
        Hax Redaga = new Hax("Redaga", Hax.transform.GetChild((int) HaxList.Redaga).gameObject, 5, 10);
        Hax Lemon = new Hax("A Lemon", Hax.transform.GetChild((int) HaxList.Lemon).gameObject, 1, 10);
        Hax Stabby = new Hax("Stabby", Hax.transform.GetChild((int) HaxList.Stabby).gameObject, 1, 10);

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
        ChangeHax();

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

        #region Detect Win

        if (Enemies == 0 && SceneManager.GetActiveScene().buildIndex > 0)
        {
            Invoke("LevelComplete", 2);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && levelDone == true)
        {
            HaxChoice.text = cannonBall.Name;
            LoadNextLevel();
        }

        #endregion
    }

    #region Methods to Detect Win and Move to Next Level

    private void LevelComplete()
    {
        HaxChoice.text = "All Dead";
        Invoke("LevelDone", 5);
    }

    private void LevelDone()
    {
        levelDone = true;
    }

    private static void LoadNextLevel()
    {
        SceneManager.LoadScene(2);
    }

    #endregion

    private void ChangeHax()
    {
        #region Cycle Through Hax

        if (Input.GetButtonDown("HaxSelect") || OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (cannonBall == Haxes.Single(c => c.Name == "Greenaga"))
            {
                cannonBall = Haxes.Single(c => c.Name == "Redaga");
            }
            else if (cannonBall == Haxes.Single(c => c.Name == "Redaga"))
            {
                cannonBall = Haxes.Single(c => c.Name == "A Lemon");
            }
            else if (cannonBall == Haxes.Single(c => c.Name == "A Lemon"))
            {
                cannonBall = Haxes.Single(c => c.Name == "Stabby");
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



            if (ovrCannonThrow > .5 || cannonThrow > .5)
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





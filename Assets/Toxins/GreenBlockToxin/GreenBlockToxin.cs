using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static CannonControl;

public class GreenBlockToxin : MonoBehaviour
{
    List<Hax> Haxes = new List<Hax>(); // List of All Hax so the enemy knows what can hit it
    [SerializeField] public GameObject Hax; // The Hax Gameobjects
    [SerializeField] public GameObject ThisToxin; // Kind of Redundant
    [SerializeField] private float HP; // Max HP for Enemy
    [SerializeField] private Text HPLeft; // Current HP for Enemy
    private float haxDamageReceived; // the Amount of Damage from the HAX
    [SerializeField] private RuntimeAnimatorController deathScene; // Animation Control to flail wildly
    private Rigidbody thisRigidbody; // Holds Rigidbody for the Enemy
    private Animator thisAnimator; // Holds Animator for the enemy




    // Start is called before the first frame update
    void Start()
    {

        MakeHaxList(); // Instantiate list of all hax
        thisAnimator = ThisToxin.GetComponent<Animator>();
        thisRigidbody = ThisToxin.GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
        #region Update Hitpoints with 0 Floor
        float toxinHP;
        if (HP - haxDamageReceived < 0)
        {
            toxinHP = 0;
        }
        else
        {
            toxinHP = HP - haxDamageReceived;
        }
        #endregion
        #region Detect Death
        HPLeft.text = (toxinHP).ToString()+ " HP";
        if (toxinHP==0)
        {
            HPLeft.text = "guunnngg";
            thisRigidbody.mass = 0;
            thisAnimator.runtimeAnimatorController = deathScene;
            Destroy(ThisToxin, 1.5f);
        }
        #endregion
    }

    void OnCollisionEnter(Collision collision)
    {
        try
        {
            haxDamageReceived +=
                Haxes.SingleOrDefault(hax => hax.Name == collision.gameObject.name.Replace("(Clone)", "")).baseDamage;
        }
        catch
        {

        }
    }

    void MakeHaxList()
    {
        #region Instantiate Hax
        Hax Greenaga = new Hax("Greenaga", Hax.transform.GetChild((int)HaxList.Greenaga).gameObject, 5, 10);
        Hax Redaga = new Hax("Redaga", Hax.transform.GetChild((int)HaxList.Redaga).gameObject, 5, 10);
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
}

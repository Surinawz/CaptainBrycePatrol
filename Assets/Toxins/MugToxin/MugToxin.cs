using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using static CannonControl;

public class MugToxin : MonoBehaviour
{

    List<Hax> Haxes = new List<Hax>(); // List of All Hax so the enemy knows what can hit it
    [SerializeField] public GameObject Hax; // The Hax Gameobjects
    [SerializeField] private float HP; // Max HP for Enemy
    [SerializeField] private Text HPLeft; // Current HP for Enemy
    private Random attackFrequencyRange;
    private float attackFrequency;
    [SerializeField] private GameObject ToxinHax;
    private float haxDamageReceived; // the Amount of Damage from the HAX
    private Rigidbody thisRigidbody;
    private GameObject BryceBoat;



    // Start is called before the first frame update
    void Start()
    {
        attackFrequency = attackFrequencyRange.Next(18, 28) / 10f;
        MakeHaxList(); // Instantiate list of all hax
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        BryceBoat = GameObject.Find("BryceBoat");
        StartCoroutine("CheckBryceDistance");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(BryceBoat.transform.position);
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
        HPLeft.text = (toxinHP).ToString() + " HP";
        if (toxinHP == 0)
        {
            HPLeft.text = "pspss";
            thisRigidbody.mass = 0;
            Destroy(gameObject, 1.5f);
        }
        #endregion

    }

    IEnumerator CheckBryceDistance()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, BryceBoat.transform.position) < 100)
            {
                GameObject.Instantiate(ToxinHax, transform.position, transform.localRotation);
            }
            yield return new WaitForSeconds(attackFrequency);
        }
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
        Hax Periwinkle = new Hax("Periwinkle", Hax.transform.GetChild((int)HaxList.Periwinkle).gameObject, 5, 10);
        Hax Lemon = new Hax("A Lemon", Hax.transform.GetChild((int)HaxList.Lemon).gameObject, 1, 10);
        Hax Stabby = new Hax("Stabby", Hax.transform.GetChild((int)HaxList.Stabby).gameObject, 1, 10);
        #endregion
        #region Add to List of Available Hax
        Haxes.Add(Greenaga);
        Haxes.Add(Periwinkle);
        Haxes.Add(Lemon);
        Haxes.Add(Stabby);
        #endregion
    }


}

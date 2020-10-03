using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static CannonControl;

public class GreenBlockToxin : MonoBehaviour
{
    List<Hax> Haxes = new List<Hax>();
    [SerializeField] public GameObject Hax;
    [SerializeField] public GameObject ThisToxin;
    [SerializeField] private float HP;
    [SerializeField] private Text HPLeft;
    private float haxDamageReceived;
    [SerializeField] private RuntimeAnimatorController deathScene;
    private Rigidbody thisRigidbody;
    private Animator thisAnimator;
    


    // Start is called before the first frame update
    void Start()
    {
        MakeHaxList();
        thisAnimator = ThisToxin.GetComponent<Animator>();
        thisRigidbody = ThisToxin.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float toxinHP;
        if (HP - haxDamageReceived < 0)
        {
            toxinHP = 0;
        }
        else
        {
            toxinHP = HP - haxDamageReceived;
        }

        HPLeft.text = (toxinHP).ToString()+ " HP";
        if (toxinHP==0)
        {
            HPLeft.text = "guunnngg";
            thisRigidbody.mass = 0;
            thisAnimator.runtimeAnimatorController = deathScene;
            Destroy(ThisToxin, 1.5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        haxDamageReceived += Haxes.SingleOrDefault(hax => hax.Name == collision.gameObject.name.Replace("(Clone)", "")).baseDamage;
        print(haxDamageReceived);

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

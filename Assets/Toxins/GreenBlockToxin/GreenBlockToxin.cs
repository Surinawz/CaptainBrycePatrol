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
    private Animator thisAnimator;


    // Start is called before the first frame update
    void Start()
    {
        MakeHaxList();
        thisAnimator = ThisToxin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HPLeft.text = (HP - haxDamageReceived).ToString()+ " HP";
        if (HP-haxDamageReceived<=0)
        {
            thisAnimator.enabled = false;
            Destroy(ThisToxin, 2);
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
        #endregion
        #region Add to List of Available Hax
        Haxes.Add(Greenaga);
        Haxes.Add(Redaga);
        Haxes.Add(Lemon);
        #endregion
    }
}

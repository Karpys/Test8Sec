using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ReplaceWorld : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ship;
    public Animator Anim;
    public OulaManager Oula;
    public TextMeshProUGUI PhaseTxt;
    public void Replace(int Phase)
    {
        Anim.Play("ReplaceFadeAnimator");
        PhaseTxt.text = "Level " + Phase;
    }

    public void ReplaceShip()
    {
        Ship.transform.position = new Vector3(-54.0299988f, 1.10000002f, 0);
        List<Coin> newList = new List<Coin>();
        newList = FindObjectsOfType<Coin>().ToList();
        foreach (Coin coin in newList)
        {
            Destroy(coin.gameObject);
        }
        Oula.RemoveAndReplace();
    }
}

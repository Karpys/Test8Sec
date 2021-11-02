using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int RessourceCount;
    public int HightScore;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI HighScore;
    public int LastRessourceCount;
    public Animator Anim;
    public void Start()
    {
        Load();
        Text.text = RessourceCount.ToString();
        HighScore.text = HightScore.ToString();
        LastRessourceCount = RessourceCount;
    }


    public void Update()
    {
        Text.text = RessourceCount.ToString();
        if (LastRessourceCount + 10 < RessourceCount)
        {
            LastRessourceCount = RessourceCount;
            Save();
            Debug.Log("cc");
        }
    }

    public void PlusOne()
    {
        Anim.Play("AnimUi");
        //ANIMATION TEXT//
        RessourceCount += 1;
        //PLAY SOUND//
    }

    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }

    public void Load()
    {
        RessourceData data = SaveSystem.LoadPlayer();

        RessourceCount = data.RessourceCount;
        HightScore = data.HightScore;
    }
}

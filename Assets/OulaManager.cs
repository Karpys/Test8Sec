using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class OulaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Oula;
    public GameObject SpaceShip;
    public List<GameObject> ListOula = new List<GameObject>();
    public Vector2 MaxMinX;
    public Vector2 MaxMiny;
    public int StartOula;
    private int Counter;
    public RessourceManager Ressource;
    public bool Endless = false;
    public int Phase = 1;


    public int CounterReplace = 30;
    private int ActualWave;
    private int CounterBeforeReplace;

    public GameObject Coin;

    public ReplaceWorld ReplaceManager;
    void Start()
    {
        InstOula();
        PlaceFirstOula();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Endless && Counter >= StartOula)
        {
            //CALL END LEVEL//
        }
    }

    public void InstOula()
    {
        for(int i = 0 ; i < StartOula;i++)
        {
            GameObject Obj = Instantiate(Oula,SpaceShip.transform.position,Oula.transform.rotation);
            Obj.GetComponent<Oula>().Manager = this;
            ListOula.Add(Obj);
        }
    }

    public void Replace(GameObject Obj)
    {

        if (Endless)
        {
            ActualWave += 1;
            if (ActualWave > CounterReplace)
            {
                CounterBeforeReplace += 1;
                if (CounterBeforeReplace >= StartOula)
                {
                    Phase += 1;
                    MaxMiny.x -= 10;
                    MaxMiny.y += 10;
                    MaxMiny.x = Mathf.Clamp(MaxMiny.x, -80, 0);
                    MaxMiny.y = Mathf.Clamp(MaxMiny.y, 0, 80);
                    ReplaceManager.Replace(Phase);
                    ActualWave = 0;
                    CounterBeforeReplace = 0;
                }
            }
            else
            {
                ListOula.Remove(Obj);
                ListOula.Add(Obj);
                ReplaceOne(Obj);
            }
        }
        else
        {
            Counter += 1;
        }

/*        Ressource.RessourceCount += 1;*/
    }

    public void RemoveAndReplace()
    {
        foreach (GameObject Oula in ListOula)
        {
            Destroy(Oula.gameObject);
        }
        ListOula.Clear();
        InstOula();
        PlaceFirstOula();
    }

    public void PlaceCoin(int id)
    {
        Vector3 Direction = ListOula[id].transform.position - ListOula[id-1].transform.position;
        Vector3 DirectionPow;
        for (float i = 0; i < 1; i+=0.1f)
        {
            if (i != 0)
            {
                /*DirectionPow = Direction / i;
                DirectionPow += ListOula[id - 1].transform.position;*/
                Vector3 Pos = Vector3.Slerp(ListOula[id].transform.position, ListOula[id - 1].transform.position,i);
                Instantiate(Coin, Pos, Coin.transform.rotation);
            }
        }
    }

    public void ReplaceOne(GameObject Obj)
    {
        Obj.transform.position = NewVectorOula(ListOula[ListOula.Count - 2]);
        Obj.GetComponent<Oula>().HasPassed = false;
    }

    public void PlaceFirstOula()
    {
        for (int i = 0;i < ListOula.Count; i++)
        {
            if (i == 0)
            {
                ListOula[i].transform.position = NewFirst(SpaceShip);
                ListOula[i].GetComponent<Oula>().HasPassed = false;
            }
            else
            {
                ListOula[i].transform.position = NewVectorOula(ListOula[i - 1]);
                ListOula[i].GetComponent<Oula>().HasPassed = false;
                if (Random.Range(0, 10) > 3)
                {
                    PlaceCoin(i);
                }
            }
        }
    }

    public Vector3 NewVectorOula(GameObject Base)
    {
        Vector3 vec;
        vec = new Vector3(Base.transform.position.x + Random.Range(MaxMinX.x, MaxMinX.y),
            Base.transform.position.y + Random.Range(MaxMiny.x, MaxMiny.y), Base.transform.position.z);
        return vec;
    }
    public Vector3 NewFirst(GameObject Base)
    {
        Vector3 vec;
        vec = new Vector3(Base.transform.position.x + Random.Range(MaxMinX.x, MaxMinX.y),
            Base.transform.position.y, Base.transform.position.z);
        return vec;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public CreateRessource Ressource;
    public bool Endless = false;
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
            ListOula.Remove(Obj);
            ListOula.Add(Obj);
            ReplaceOne(Obj);
        }
        else
        {
            Counter += 1;
        }
        Ressource.CreateRessources();
    }

    public void ReplaceOne(GameObject Obj)
    {
        Obj.transform.position = NewVectorOula(ListOula[ListOula.Count - 2]);
    }

    public void PlaceFirstOula()
    {
        for (int i = 0;i < ListOula.Count; i++)
        {
            if (i == 0)
            {
                ListOula[i].transform.position = NewVectorOula(SpaceShip);
            }
            else
            {
                ListOula[i].transform.position = NewVectorOula(ListOula[i - 1]);
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
}

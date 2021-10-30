using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRessource : MonoBehaviour
{
    // Start is called before the first frame update
    /*public GameObject SpaceShip;
    public Canvas CanvasParent;
    public GameObject Ressource;
    public Vector3 SpaceShipPos;
    public Camera Cam;*/

    public GameObject Target;
    public GameObject SpaceShip;
    private float timer;
    public GameObject Prefab;

    public void CreateRessources()
    {
        Vector3 NewPos = new Vector3(Target.transform.position.x, SpaceShip.transform.position.y,
            SpaceShip.transform.position.z);
        Instantiate(Prefab, NewPos, Prefab.transform.rotation);
    }
}

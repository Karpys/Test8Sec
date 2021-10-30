using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oula : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer Mesh;
    public OulaManager Manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mesh.material.color = Color.white;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            other.GetComponent<SpaceShipController>().Anim.LaunchBarrel();
            other.GetComponent<SpaceShipController>().SpeedAnim.SpeedLaunch();
            StartCoroutine(ReplaceSelf());
        }
    }

    IEnumerator ReplaceSelf()
    {
        yield return new WaitForSeconds(0.5f);
        Manager.Replace(this.gameObject);
    }
}

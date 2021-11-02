using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public RessourceManager Ressource;
    public GameObject Particle;
    void Start()
    {
        Ressource = FindObjectOfType<RessourceManager>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ship"))
        {
            Instantiate(Particle, transform.position, transform.rotation);
            Ressource.PlusOne();
            Destroy(gameObject);
        }
    }
}

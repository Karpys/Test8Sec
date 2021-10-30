using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectInterval : MonoBehaviour
{
    // Start is called before the first frame update
    public float Interval;
    private float Timer;
    public GameObject Particle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Interval)
        {
            Instantiate(Particle, transform.position, transform.rotation);
        }
    }
}

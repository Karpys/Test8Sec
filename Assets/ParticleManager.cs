using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem Ps;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Ps)
        {
            if (!Ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}

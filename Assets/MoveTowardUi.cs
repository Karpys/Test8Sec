using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardUi : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UiTarget;
    public float Speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, UiTarget.transform.position, Speed * Time.deltaTime);
    }
}

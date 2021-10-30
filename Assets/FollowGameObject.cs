using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Follow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Follow.transform.position.x, transform.position.y, transform.position.z);
    }
}

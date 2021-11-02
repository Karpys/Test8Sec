using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsRessource : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Target;
    public float timer;
    void Start()
    {
        Target = FindObjectOfType<RessourceManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 NewPos = Vector3.MoveTowards(transform.position, Target.transform.position, timer);
        NewPos.x = Target.transform.position.x;
        transform.position = NewPos;
        if (timer >= 1)
        {
            Target.GetComponent<RessourceManager>().RessourceCount += 1;
            Destroy(gameObject);
        }
    }
}

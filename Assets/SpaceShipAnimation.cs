using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float BarrelRollDuration;
    public float TimerBarrel;
    public AnimationCurve Curve;
    void Start()
    {
        TimerBarrel = BarrelRollDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerBarrel <= BarrelRollDuration)
        {
            TimerBarrel += Time.deltaTime;
            float NewRot = Mathf.Lerp(0, 1,Curve.Evaluate(TimerBarrel)) * 360;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, NewRot);
        }


    }

    public void LaunchBarrel()
    {
        TimerBarrel = 0;
    }
}

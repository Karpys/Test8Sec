using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve Curve;
    public float Duration;
    public SpaceShipController Controller;
    private float timer;
    public float BaseSpeed;
    public float MaxSpeed;

    public bool HasReset=false;
    void Start()
    {
        timer = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= Duration)
        {
            timer += Time.deltaTime;
            Controller.Speed = Mathf.Lerp(BaseSpeed, MaxSpeed, Curve.Evaluate(timer));
            /*Controller.LookAtForce = 2;
            if (timer > Duration)
            {
                Controller.LookAtForce = 4;
            }*/
        }
    }

    public void SpeedLaunch()
    {
        Controller.CameraAnimator.Play("Dezoom");
        timer = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LookAtPoint;
    [SerializeField] private float LookAtForce = 2.0f;

    public GameObject Visual;

    float TimerRotation;
    [SerializeField] private float RotationDuration;

    public List<ParticleSystem> ParticleToPlayOnChangeDirection;

    public SpaceShipAnimation Anim;

    public float Speed;
    // Update is called once per frame
    void Update()
    {
        if (TimerRotation < RotationDuration)
        {
            TimerRotation += Time.deltaTime;
            float ratio = TimerRotation/RotationDuration; 
            float newRot = 1 - Mathf.Pow((1 - ratio), 3);
            RotateTowardsLookPoint(LookAtPoint,newRot);
        }

        Vector3 Forward = Visual.transform.forward * Speed * Time.deltaTime;
        transform.Translate(Forward);
    }

    public void ResetTimer()
    {
        TimerRotation = 0;
    }

    public void PlayParticles(List<ParticleSystem> Particles)
    {
        foreach (ParticleSystem particle in Particles)
        {
            if (!particle.isEmitting)
            {
                particle.Play();
            }
        }
    }

    public void RotateTowardsLookPoint(GameObject Target,float Speed)
    {
        Vector3 _direction = (Target.transform.position - Visual.transform.position).normalized;

        Quaternion LookRotation = Quaternion.LookRotation(_direction);

        Quaternion newRot = Quaternion.Slerp(Visual.transform.rotation, LookRotation, Speed);

        Vector3 eulerAng = newRot.eulerAngles;

        Visual.transform.eulerAngles = new Vector3(eulerAng.x, Visual.transform.eulerAngles.y, Visual.transform.eulerAngles.z);
    }

    public void SetPositionLookAt(bool Up)
    {
        PlayParticles(ParticleToPlayOnChangeDirection);
        if (Anim.TimerBarrel > Anim.BarrelRollDuration)
        {
            Anim.LaunchBarrel();
        }
        if (Up)
        {
            LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x,LookAtForce,
                LookAtPoint.transform.localPosition.z);
        }
        else
        {
            LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x, -LookAtForce,
                LookAtPoint.transform.localPosition.z);
        }
    }

    public void ResetPositionLookAt()
    {
        LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x, 0,
            LookAtPoint.transform.localPosition.z);
    }
}

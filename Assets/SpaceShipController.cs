using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LookAtPoint; 
    public float LookAtForce = 2.0f;
    public float ActualLookAt = 0f;
    public float LookAtAcc;

    public GameObject Visual;

    float TimerRotation;
    [SerializeField] private float RotationDuration;

    public List<ParticleSystem> ParticleToPlayOnChangeDirection;

    public SpaceShipAnimation Anim;
    public SpaceShipSpeed SpeedAnim;

    public Animator CameraAnimator;

    public AnimationCurve SpeedShip;

    public float Speed;
    // Update is called once per frame
    void Start()
    {
        CameraAnimator = GameObject.FindObjectOfType<CinemachineVirtualCamera>().gameObject.GetComponent<Animator>();
    }

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

    public void SetPositionLookAt(float ratio)
    {

        /*if (State == PlayerTouch.SHIPSTATE.UP)
        {
            ActualLookAt += LookAtAcc * Time.deltaTime;
            
        }
        else if (State == PlayerTouch.SHIPSTATE.DOWN)
        {
            ActualLookAt -= LookAtAcc * Time.deltaTime;
            ActualLookAt = Mathf.Clamp(ActualLookAt, -LookAtForce, LookAtForce);
            LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x, ActualLookAt,
                LookAtPoint.transform.localPosition.z);
        }*/
        ActualLookAt = SpeedShip.Evaluate(ratio);
        ActualLookAt = Mathf.Clamp(ActualLookAt, -LookAtForce, LookAtForce);
        LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x, ActualLookAt,
            LookAtPoint.transform.localPosition.z);
    }

    public void ResetPositionLookAt()
    {
        LookAtPoint.transform.localPosition = new Vector3(LookAtPoint.transform.localPosition.x, 0,
            LookAtPoint.transform.localPosition.z);
    }
}

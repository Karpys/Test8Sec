using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static MathUtilities.MathUtilities;
public class PlayerTouch : MonoBehaviour
{
    public Controls Controller;

    public Controls.GameplayActions Gameplay;


    public TOUCHSTATE TouchState = TOUCHSTATE.IDLE;

    private Vector2 TouchPosition;
    private Vector2 TouchStartPosition;
    private Vector2 UpdateTouchPosition;
    private float HighestActual;
    private float LowestActual;
    public float DeadZone = 10.0f;

    public float ZoneTouch = 300.0f;
    public float TouchCenter;
    public float StartZone;
    public float EndZone;
    public float RatioZone;
    public float Percent;
    public SHIPSTATE ShipState;
    public TextMeshProUGUI Text;

    public GameObject UpdateGameObject;
    public SpaceShipController SpaceShipController;

    private void Awake()
    {
        Controller = new Controls();
        Gameplay = Controller.Gameplay;
    }
    private void OnEnable()
    {
        Controller.Enable();
    }
    private void OnDisable()
    {
        Controller.Disable();
    }

    public void Start()
    {
        Controller.Gameplay.TouchPress.started += ctx => StartTouch(ctx);
        Controller.Gameplay.TouchPress.canceled += ctx => EndTouch(ctx);
        
    }

    public void UpdatePercent(float percent)
    {
        Percent = percent;
        Text.text = Percent.ToString();
    }

    public void Update()
    {
        if (TouchState == TOUCHSTATE.PRESSED)
        {
            UpdateTouchPosition = Vector2.zero;
            TouchState = TOUCHSTATE.UPDATE;
            
        }else if (TouchState == TOUCHSTATE.ENDED)
        {
            TouchState = TOUCHSTATE.IDLE;
            SpaceShipController.ResetPositionLookAt();
            SpaceShipController.ResetTimer();
        }

        if (TouchState == TOUCHSTATE.UPDATE)
        {
            ZoneTouch = Screen.currentResolution.height * Percent;
            TouchPosition = Gameplay.TouchPosition.ReadValue<Vector2>();
            TouchStartPosition = Gameplay.TouchStartPosition.ReadValue<Vector2>();

            TouchCenter = Screen.currentResolution.height/2;

            StartZone = TouchCenter - (ZoneTouch / 2);
            StartZone = Mathf.Clamp(StartZone, 0, Screen.currentResolution.height);
            EndZone = TouchCenter + (ZoneTouch / 2);
            EndZone = Mathf.Clamp(EndZone, 0, Screen.currentResolution.height);
            TouchPosition.y = Mathf.Clamp(TouchPosition.y, StartZone, EndZone);
            RatioZone = (TouchPosition.y - StartZone)/ (EndZone - StartZone);

            if (UpdateTouchPosition == Vector2.zero)
            {
                UpdateTouchPosition = TouchStartPosition;
                ShipState = SHIPSTATE.FIRSTSTATE;
                LowestActual = TouchPosition.y;
                HighestActual = TouchPosition.y;

            }

            /*SpaceShipController.SetPositionLookAt(true);*/
            /*UpdateTouchPosition = TouchPosition;*/

            

            /*if (ShipState == SHIPSTATE.FIRSTSTATE)
            {
                if (TouchPosition.y > HighestActual)
                {
                    HighestActual = TouchPosition.y;
                }

                if (TouchPosition.y + DeadZone < HighestActual)
                {
                    HighestActual = TouchPosition.y;
                    LowestActual = TouchPosition.y;
                    ShipState = SHIPSTATE.DOWN;
                    SpaceShipController.ResetTimer();
                }

                if (TouchPosition.y < LowestActual)
                {
                    LowestActual = TouchPosition.y;
                }

                if (TouchPosition.y - DeadZone > LowestActual)
                {
                    LowestActual = TouchPosition.y;
                    HighestActual = TouchPosition.y;
                    ShipState = SHIPSTATE.UP;
                    SpaceShipController.ResetTimer();
                }
            }

            if (ShipState == SHIPSTATE.UP)
            {
                if (TouchPosition.y >  HighestActual)
                {
                    HighestActual = TouchPosition.y;
                }

                if (TouchPosition.y + DeadZone < HighestActual )
                {
                    HighestActual = TouchPosition.y;
                    LowestActual = TouchPosition.y;
                    ShipState = SHIPSTATE.DOWN;
                    SpaceShipController.ResetTimer();
                }
            }

            if (ShipState == SHIPSTATE.DOWN)
            {
                if (TouchPosition.y < LowestActual)
                {
                    LowestActual = TouchPosition.y;
                }

                if (TouchPosition.y - DeadZone > LowestActual)
                {
                    LowestActual = TouchPosition.y;
                    HighestActual = TouchPosition.y;
                    ShipState = SHIPSTATE.UP;
                    SpaceShipController.ResetTimer();
                }
            }*/

            SpaceShipController.SetPositionLookAt(RatioZone);

        }
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        /*Debug.Log("Touch Started" + Controller.Gameplay.TouchPosition.ReadValue<Vector2>());*/
        TouchState = TOUCHSTATE.PRESSED;
        SpaceShipController.ActualLookAt = 0;
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        /*Debug.Log("Touch Ended" + Controller.Gameplay.TouchPosition.ReadValue<Vector2>());*/
        TouchState = TOUCHSTATE.ENDED;
    }
    public enum TOUCHSTATE
    {
        PRESSED,
        UPDATE,
        ENDED,
        IDLE,
    }

    public enum SHIPSTATE
    {
        UP,
        DOWN,
        IDLE,
        FIRSTSTATE,
    }
}

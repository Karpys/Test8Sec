using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static MathUtilities.MathUtilities;
public class PlayerTouch : MonoBehaviour
{
    public Controls Controller;

    public Controls.GameplayActions Gameplay;

    public GameObject Stick;

    public TOUCHSTATE TouchState = TOUCHSTATE.IDLE;

    private Vector2 TouchPosition;
    private Vector2 TouchStartPosition;
    private Vector2 UpdateTouchPosition;
    private float HighestActual;
    private float LowestActual;
    public float DeadZone = 10.0f;

    public SHIPSTATE ShipState;

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

    public void Update()
    {
        if (TouchState == TOUCHSTATE.PRESSED)
        {
            TouchState = TOUCHSTATE.UPDATE;
        }else if (TouchState == TOUCHSTATE.ENDED)
        {
            TouchState = TOUCHSTATE.IDLE;
            SpaceShipController.ResetPositionLookAt();
            SpaceShipController.ResetTimer();
        }

        if (TouchState == TOUCHSTATE.UPDATE)
        {
            TouchPosition = Gameplay.TouchPosition.ReadValue<Vector2>();
            TouchStartPosition = Gameplay.TouchStartPosition.ReadValue<Vector2>();

            if (UpdateTouchPosition == Vector2.zero)
            {
                UpdateTouchPosition = TouchStartPosition;
                ShipState = SHIPSTATE.UP;
                SpaceShipController.SetPositionLookAt(true);
                
            }

            /*SpaceShipController.SetPositionLookAt(true);*/
            /*UpdateTouchPosition = TouchPosition;*/
            


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
                    SpaceShipController.SetPositionLookAt(false);
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
                    SpaceShipController.SetPositionLookAt(true);
                    SpaceShipController.ResetTimer();
                }
            }

        }
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        /*Debug.Log("Touch Started" + Controller.Gameplay.TouchPosition.ReadValue<Vector2>());*/
        Stick.transform.position = Controller.Gameplay.TouchPosition.ReadValue<Vector2>();
        TouchState = TOUCHSTATE.PRESSED;
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
    }
}

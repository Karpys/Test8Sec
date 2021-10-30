using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerTouch : MonoBehaviour
{
    public Controls Controller;

    public Controls.GameplayActions Gameplay;

    public GameObject Stick;

    public TOUCHSTATE TouchState = TOUCHSTATE.IDLE;

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
        }

        if (TouchState == TOUCHSTATE.UPDATE)
        {
            
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
}

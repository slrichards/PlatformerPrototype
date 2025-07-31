using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
    Player Input Controller
    Move Action, Look Action, Jump Action, Sprint Action

    Only Move Action is enabled currently.
*/
public class PlayerInputController : MonoBehaviour
{
    private InputAction moveAction;
    //private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    //[SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    public Vector2 MoveInput { get; private set; }
    //public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }

    public static PlayerInputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        //lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {

        //checks to see if move button is pressed.
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        //checks to see if move button is no longer pressed.
        moveAction.canceled += context => MoveInput = Vector2.zero;

        //lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        //lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

    }
    //enables an action
    private void OnEnable()
    {
        moveAction.Enable();
        //lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }


    //disables an action
    private void OnDisable()
    {
        moveAction.Disable();
        //lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }
}

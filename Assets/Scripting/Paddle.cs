using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType 
{ 
   Player1,
   Player2 
}

public class Paddle : MonoBehaviour
{
    //Paddle movment variables
    public PlayerType playerType;
    public float speed = 10f;
    public float boundary= 4f;
    [SerializeField] private InputAction inputAction;
    private float moveInput;

    /// <summary>
    /// Initializes the input actions for moving the paddle
    /// Initializes the input action for stopping the paddle movement
    /// </summary>
    void Awake()
    {
        inputAction.performed += OnMovePerformed;
        inputAction.canceled += OnMoveCanceled;
    }
    /// <summary>
    /// Turns on the input action when the script is enabled
    /// </summary>
    void OnEnable()
    {
        inputAction.Enable();
    }
    /// <summary>
    /// Turns off the input action when the script is disabled
    /// </summary>
    void OnDisable()
    {
        inputAction.Disable();
    }

    /// <summary>
    /// Move the paddle based on the input action's value
    /// Uses the new input system to read the value
    /// </summary>

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float> ();
    }
    
    /// <summary>
    /// Moves the paddle back to zero when the input action is canceled
    /// Stops the paddle from moving when the input is released
    /// </summary>
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = 0;
    }

    void Update()
    {
        // Moves the paddle using the float value from the input action and the speed variable
        float movement = moveInput * speed * Time.deltaTime;
        // Translates the paddle's position based on the movement value
        transform.Translate(0f, movement, 0f);
        // Defines the boundary for the paddle's movement and clamps the paddle's position within that boundary
        float clampedY = Mathf.Clamp(transform.position.y, -boundary, boundary);
        //Sets the paddle's position and clamps the y position to the defined boundary
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}

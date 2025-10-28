using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 2f; //Player speed variable.
    private Rigidbody2D playerRigidbody2d; //Rigidbody of the player to apply forces and movement.
    private Vector2 playerDirection; //Direction the player moves.

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f; //Force used to jump.

    [Header("Grounded")]
    [SerializeField] Transform groundCheckPos; //Position from where we check if the player is touching the ground.
    [SerializeField] Vector2 groundCheckSize = new Vector2(0.5f, 0.05f); //Size of the box used to detect the ground.
    [SerializeField] LayerMask groundLayer; //Layer where the ground is.

    void Start()
    {
        playerRigidbody2d = GetComponent<Rigidbody2D>(); //Get the Rigidbody2D component from the player.
    }

    void FixedUpdate()
    {
        playerRigidbody2d.linearVelocity = new Vector2(playerDirection.x * playerSpeed, playerRigidbody2d.linearVelocityY); //Move the player by changing his velocity in X.
    }

    public void Move(InputAction.CallbackContext context) //Function to move the player when the movement input is detected.
    {
        playerDirection = context.ReadValue<Vector2>(); //Read the input value and store it in the direction variable.
    }

    public void Jump(InputAction.CallbackContext context) //Function to make the player jump.
    {
            if (IsGrounded()) //Only can jump if is grounded.
            {
                    if (context.performed) //Check if the jump button is pressed.
                    {
                        
                        playerRigidbody2d.linearVelocity = new Vector2(playerRigidbody2d.linearVelocityX, jumpForce); //Apply the jump force.
                       
                    }
            }
    }


    public bool IsGrounded() //Function to check if the player is touching the ground.
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) //Make a box in the ground check position.
        {
          return true; //If the box touchs the ground layer, return true.
        }
        return false; //If not, return false.
    }

    private void OnDrawGizmos() //Function to draw the ground check box in the editor.
    {
        Gizmos.color = Color.red; //Color of the box.
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize); //Draw the box to visualize the grounded area.
    }
}

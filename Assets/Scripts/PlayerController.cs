using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[RequireComponent(typeof(CharacterController))] // Attribute ensures set component(s) are automatically added to script's target.
*/
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    #region PLAYER
    [Header("MOVEMENT VARIABLES")]

    public float moveSpeed = 6.0f; // Used to set player's movement speed in X and Z coordinates.
    public float jumpStrength = 8.0f; // Used to set player's jump height in Y coordinates.
//    public float gravity = 20.0f; // Used for a constant force pushing the player down.

    [Header("TECHNICAL STUFF")]
/*
    private Vector3 moveDirection = Vector3.zero; // moveDirection's starting input in Vector3 is neutral (0, 0, 0).
    public CharacterController controller; // Creates a space in the script where a CharacterController component can be assigned.
*/
    public Rigidbody rigid;
    public float rayDistance = 1f; // A float for the length (how far) the rayCast's line is drawn 
    private bool isGrounded = false; // A boolean used later to make different rules for when the player is airborne/touching the ground.
    #endregion PLAYER

    #region CAMERA

    #endregion CAMERA

    #endregion Variables

    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(groundRay, out hit, rayDistance))
        {
            return true;
        }
        return false;
    }
/*
    // Use this for initialization; runs before the first/physics update on an object when the script is first active.
    private void Start()
    {
        controller = GetComponent<CharacterController>(); // Tell's the script to retrieve the CharacterController component when the game starts.
    }
*/
/*
    // Update is called once per frame; this is critical for player controls, as it ensures that no player inputs are missed or registered late.
    void Update()
    {
        // The moveDirection variable was defined as a Vector3 with null values (it did nothing). Here, moveDirection fufills its destiny.
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // new X and Z values read from Inputs in Unity.

        moveDirection = transform.TransformDirection(moveDirection); // Finds transform component and moves in the direction of moveDirection.

        moveDirection *= moveSpeed; // Operator gets static Vector3 of moveDirection and multiplies received values of inputs by moveSpeed.

        if(Input.GetButton("Jump") && IsGrounded()) // if 'jump' is pressed, AND the player is grounded.
        {
            moveDirection.y = jumpStrength; // 'moveDirection.y' was defined above as '0'. Now it's equal to jumpStrength.
        }

        // These two lines multiply all player's movement speeds by Time.deltaTime (if the game is paused, all forms of player movement stop).
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
*/
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveDir = new Vector3(inputH, 0f, inputV);
        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

        if (Input.GetButton("Jump") && IsGrounded())
        {
            force.y = jumpStrength;
        }

        rigid.velocity = force;

        // If the user pressed a key (moveDir has values in it other than 0)
        if(moveDir.magnitude > 0)
        {
            // Rotate the player to that moveDir
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

    }

}

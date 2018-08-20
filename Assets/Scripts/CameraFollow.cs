using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations; // Using .Animations because it's the only way to find the <PositionConstraint> component.

[RequireComponent(typeof(PositionConstraint))] // Ensures the Required Component is automatically attached with script if it doesn't already exist.
public class CameraFollow : MonoBehaviour
{
    public Transform target; // Used as player. Refers to assigned object's Transform(s).

    private Vector3 offset; // Used for camera. Refers to it's 3D vector points at the start of the game.

    public PositionConstraint xStraint; // Using a PositionConstraint to constrain the camera's movement on the X Axis.


    // Use this for initialization
    void Start()
    {
        #region UNITY POSITION CONSTRAINT
        // This section uses UnityEngine.Animations' fancy new 'Position Constraint' component to freeze the camera.
        // Uncertain if this solution is intended, or completely unorthodox, but... it works.

        xStraint = GetComponent<PositionConstraint>(); // Fetches and uses the PositionConstraint component.

        // if statement sets up all the core parametres for the PositionConstraint automatically just to ensure nothing is missed.
        if (xStraint)
        {
            xStraint.locked = true; // Locks access to the 'offset' and 'position at rest' settings.
            xStraint.constraintActive = true; // Activates the component's constraint feature.
            xStraint.translationAxis = Axis.X; // Sets the applied constraint effect on component to the X Axis only.
            xStraint.weight = 0f; // Setting weight to 0f sets influence of constraint's effect to full lock.
        }
        #endregion UNITY POSITION CONSTRAINT

        offset = transform.position - target.position; // Subtracts target (player's) position from camera's position to get the difference (offset).
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // Moves the camera with the player's position, maintaining the camera's offset through addition.
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(PositionConstraint))]
public class CameraFollow : MonoBehaviour
{
    public Transform target; // Used as player. Refers to assigned object's Transform(s).

    private Vector3 offset; // Used for camera. Refers to it's 3D vector points at the start of the game.

    public PositionConstraint xStraint;
    bool constraintActive = true;


    // Use this for initialization
    void Start()
    {
        xStraint = GetComponent<PositionConstraint>();
        if(xStraint)
        {
            xStraint.locked = true;
            xStraint.constraintActive = true;
            xStraint.translationAxis = Axis.X;
            xStraint.weight = 0f;
        }
        offset = transform.position - target.position; // Subtracts target (player's) position from camera's position to get the difference (offset).
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // Moves the camera with the player's position, maintaining the camera's offset through addition.
    }

    
}

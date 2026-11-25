/***************************************************************
*file: DebugOrbitMovement.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/21/2026
*
*purpose: Extremely rudimentary enemy AI. Just moves the enemy in an ellipse.
*
****************************************************************/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugOrbitMovement : MonoBehaviour
{
    private Vector3 pivotPoint = Vector3.zero;
    private float speed = 0.5f;

    [Header("Ellipse Dimensions")]
    public float radX = 15f;
    public float radY = 3f;

    private float currAngle = 0f;
    private EnemyElementalEffects elementalEffects;

    // function: Start
    // Purpose: Called on the frame when this script is enabled.
    //          Initializes elementalEffects.
    void Start()
    {
        elementalEffects = GetComponent<EnemyElementalEffects>();
    }

    // function: Update
    // Purpose: repositions the enemy based on its current angle, where the pivot point is, and the radius of the ellipse.
    //          checks if enemy is frozen, returns early if so.
    void Update()
    {
        if (elementalEffects != null && elementalEffects.IsFrozen)
        {
            return;
        }

        currAngle += speed * Time.deltaTime;

        float x = pivotPoint.x + MathF.Cos(currAngle) * radX;
        float y = pivotPoint.y + MathF.Sin(currAngle) * radY;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}

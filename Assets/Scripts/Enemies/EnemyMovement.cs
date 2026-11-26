/***************************************************************
*file: EnemyMovement.cs
*author: Ethan Buff
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/24/2026
*
*purpose: Script responsible for moving enemies. Attached to each
*         enemy. References the flow field for its cell for direction
*
****************************************************************/
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // These being serialized is TEMPorary, the spawner should give them
    [SerializeField] private GridMap gridMap;
    [SerializeField] private EnemyData enemyData;

    private FlowField flowField;
    private Rigidbody2D rb;

    // function: Awake
    // purpose: Called on the first frame that this script is active
    //   Gets flow field from the gridMap and the rigidbody to move
    void Start()
    {
        flowField = gridMap.GetFlowField(enemyData.name);
        rb = GetComponent<Rigidbody2D>();
    }


    // function: FixedUpdate
    // purpose: Called at a constant rate while script is active.
    //    When able to move, checks the flowField for where it should go
    void FixedUpdate()
    {
        Vector2Int coords = gridMap.WorldToCell(this.transform.position);
        (int x, int y) dirI = AI.DirectionHelp.DirToInts(flowField.DirectionAt(coords[0], coords[1]));
        Vector2 direction = new Vector2(0.0f + (float)dirI.x, 0.0f + (float)dirI.y);
        float speedMod = 1/gridMap.DifficultyAt(coords.x, coords.y);
        rb.linearVelocity = direction * enemyData.speed *speedMod;
    }
}

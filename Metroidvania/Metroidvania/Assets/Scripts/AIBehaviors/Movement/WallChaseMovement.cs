using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wall movement differs from aerial movement by anchoring the enemy to a point//
//The movement is the same, with the exception that the enemy must stay within an area//
public class WallChaseMovement : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;
    
    public EnemyBehavior parent;
    
    public float moveSpeed;
    public float moveRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

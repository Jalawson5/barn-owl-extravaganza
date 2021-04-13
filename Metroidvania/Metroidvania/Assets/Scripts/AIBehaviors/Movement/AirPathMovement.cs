using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPathMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] path;
    public EnemyBehavior parent;
    
    private Vector3[] positions;
    private Vector3 targetPos;
    private int index;
    

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        positions = new Vector3[path.Length];
        for(int i = 0; i < path.Length; i++)
        {
            positions[i] = path[i].position;
        }
        
        targetPos = positions[index];
        
        parent.SetDirection(-1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            
        if(Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            index++;
            if(index >= path.Length)
                index = 0;
                
            targetPos = positions[index];
        }
        
        if(targetPos.x < transform.position.x)
        {
            parent.SetDirection(-1);
        }
        
        else if(targetPos.x > transform.position.x)
        {
            parent.SetDirection(1);
        }
    }
}

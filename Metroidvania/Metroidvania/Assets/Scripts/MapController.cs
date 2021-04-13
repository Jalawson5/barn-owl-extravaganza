using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public MapNode current; //Map Node the player is currently on//
    public float moveSpeed; //Speed for the player to move on the map//
    
    private MapNode target; //Next Map Node to move to//
    private bool isTravelling; //Is the player currently moving?//
    private GameObject[] currentPath; //Path to the target Map Node//
    private Vector3 targetPos; //Target position to reach the next node in the current path//
    private int index; //Index of the path array//

    // Start is called before the first frame update
    void Start()
    {
        isTravelling = false;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTravelling)
        {
            if(Input.GetKeyDown("up") && current.upNode != null)
            {
                target = current.upNode;
                currentPath = current.upPath;
                index = 0;
                targetPos = currentPath[0].transform.position;
                isTravelling = true;
            }
            
            else if(Input.GetKeyDown("down") && current.downNode != null)
            {
                target = current.downNode;
                currentPath = current.downPath;
                index = 0;
                targetPos = currentPath[0].transform.position;
                isTravelling = true;
            }
            
            else if(Input.GetKeyDown("left") && current.leftNode != null)
            {
                target = current.leftNode;
                currentPath = current.leftPath;
                index = 0;
                targetPos = currentPath[0].transform.position;
                isTravelling = true;
            }
            
            else if(Input.GetKeyDown("right") && current.rightNode != null)
            {
                target = current.rightNode;
                currentPath = current.rightPath;
                index = 0;
                targetPos = currentPath[0].transform.position;
                isTravelling = true;
            }
            
            else if(Input.GetKeyDown("z"))
            {
                SceneManager.LoadScene(current.sceneName);
            }
        }
        
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        
            if(Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                index++;
                if(index >= currentPath.Length)
                {
                    index = 0;
                    isTravelling = false;
                    current = target;
                    target = null;
                    return;
                }
                
                targetPos = currentPath[index].transform.position;
            }
        }
    }
}

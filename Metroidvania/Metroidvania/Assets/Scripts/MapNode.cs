using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public string nodeName;
    public string sceneName;
    
    public MapNode upNode;
    public MapNode downNode;
    public MapNode leftNode;
    public MapNode rightNode;
    
    public GameObject[] upPath;
    public GameObject[] downPath;
    public GameObject[] leftPath;
    public GameObject[] rightPath;
    
    void Start()
    {
        InitPaths();
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    //void InitPaths()                                                           //
    //Checks all assigned paths to determine which direction they need to move in//
    //If the end of the path is closer than the beginning, reverse the path      //
    ///////////////////////////////////////////////////////////////////////////////
    private void InitPaths()
    {
        if(upNode != null && Vector3.Distance(transform.position, upPath[0].transform.position) > Vector3.Distance(transform.position, upPath[upPath.Length - 1].transform.position))
            Array.Reverse(upPath);
            
        if(downNode != null && Vector3.Distance(transform.position, downPath[0].transform.position) > Vector3.Distance(transform.position, downPath[downPath.Length - 1].transform.position))
            Array.Reverse(downPath);
            
        if(leftNode != null && Vector3.Distance(transform.position, leftPath[0].transform.position) > Vector3.Distance(transform.position, leftPath[leftPath.Length - 1].transform.position))
            Array.Reverse(leftPath);
            
        if(rightNode != null && Vector3.Distance(transform.position, rightPath[0].transform.position) > Vector3.Distance(transform.position, rightPath[rightPath.Length - 1].transform.position))
            Array.Reverse(rightPath);
    }
}

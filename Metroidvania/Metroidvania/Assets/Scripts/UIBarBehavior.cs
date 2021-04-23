using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarBehavior : MonoBehaviour
{
    private float targetScale;
    private float scaleTime;
    private bool isScaling;
    private float vel = 0f;
    
    public void Start()
    {
        targetScale = 1f;
        scaleTime = 0.2f;
        isScaling = false;
    }
    
    public void Update()
    {
        if(isScaling)
        {
            float newScale = Mathf.SmoothDamp(transform.localScale.x, targetScale, ref vel, scaleTime);
            transform.localScale = new Vector3(newScale, 1f, 0);
            
            if(transform.localScale.x == targetScale)
                isScaling = false;
        }
    }

    public void AdjustBar(int current, int max)
    {
        targetScale = (float)current/max;
        isScaling = true;
    }
}

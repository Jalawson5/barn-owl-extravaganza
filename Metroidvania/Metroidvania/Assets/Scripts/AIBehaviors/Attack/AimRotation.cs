using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = targetPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = targetTransform.position - transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private DialogueEntry dialogue;
    
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up") && Vector2.Distance(transform.position, player.position) <= 1f)
        {
            TypewriterController.instance.TypeDialogue(dialogue);
        }
    }
}

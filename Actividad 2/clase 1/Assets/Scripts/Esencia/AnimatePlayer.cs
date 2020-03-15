using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private bool up, down;
    void Start()
    {
        animator = GetComponent<Animator>();
        up = false;
        down = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !up)
        {
            up = true;
            animator.SetBool("isUp", true);            
        } else if ((!Input.GetKeyDown(KeyCode.UpArrow) || !Input.GetKeyDown(KeyCode.W)) && up)
        {
            up = false;
            animator.SetBool("isUp", false);
        }

        if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.D)) && !down)
        {
            down = true;
            animator.SetBool("isDown", true);
        }
        else if ((!Input.GetKeyUp(KeyCode.DownArrow) || !Input.GetKeyUp(KeyCode.D)) && down)
        {
            down = false;
            animator.SetBool("isDown", false);
        }
    }

}

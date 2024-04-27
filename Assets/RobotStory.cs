using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStory : MonoBehaviour
{
    public Animator animator;

    public void Idle()
    {
        animator.SetBool("isMoving", false);
    }

    public void Walking()
    {
        animator.SetBool("isMoving", true);
    }
}

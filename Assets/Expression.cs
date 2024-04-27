using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Expression : MonoBehaviour
{
    public Animator animator;

    public void Clapping()
    {
        animator.SetBool("isClapping", true);
    }

    public void Thinking()
    {
        animator.SetBool("isThinking", true);
    }

    public void Talking()
    {
        animator.SetBool("isTalking", true);
    }

    public void Waving()
    {
        animator.SetBool("isWaving", true);
    }
}

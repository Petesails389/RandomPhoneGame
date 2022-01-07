using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Pause()
    {
        animator.speed = 0;
    }
    public void Play()
    {
        animator.speed = 1;
    }
    public void Kill()
    {
        animator.SetBool("Dead", true);
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
    }
    public void Land()
    {
        animator.SetTrigger("Land");
    }
}

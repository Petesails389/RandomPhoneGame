using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public bool grounded = true;
    bool wasGrounded = true;

    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider2D colider;
    [SerializeField] AnimationController animationCon;

    void Start()
    {
        colider = gameObject.GetComponent(typeof(Collider2D)) as Collider2D;
    }

    void Update()
    {
        wasGrounded = grounded;
        grounded = colider.IsTouchingLayers(layerMask);
        if(!wasGrounded && grounded){
            animationCon.Land();
        }
    }
}
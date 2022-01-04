using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public bool grounded = true;

    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider2D colider;

    void Start()
    {
        colider = gameObject.GetComponent(typeof(Collider2D)) as Collider2D;
    }

    void Update()
    {
        grounded = colider.IsTouchingLayers(layerMask);
    }
}
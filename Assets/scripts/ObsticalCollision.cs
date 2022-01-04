using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalCollision : MonoBehaviour
{
    public GameObject systemController;

    [SerializeField] LayerMask layerMask;
    [SerializeField] BoxCollider2D colider;

    void Awake()
    {
        systemController = GameObject.Find("SystemController");
        colider = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
    }

    void Update()
    {
        if(colider.IsTouchingLayers(layerMask)){
            (systemController.GetComponent(typeof(EnvironmentController)) as EnvironmentController).endGame();
        }
    }
}

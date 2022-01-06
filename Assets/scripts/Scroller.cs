using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    EnvironmentController controller;
    
    [SerializeField] float speedPercentage;
    [SerializeField] float spawnHeight;
    [SerializeField] GameObject selfPrefab;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("SystemController").GetComponent(typeof(EnvironmentController)) as EnvironmentController;
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.paused){
            gameObject.transform.Translate(new Vector3(- controller.currentSpeed * Time.deltaTime * (speedPercentage/100f), 0,0),Space.World);
            if(gameObject.transform.position.x < -15){
                Instantiate(selfPrefab, new Vector3(15, spawnHeight, 0), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void Spawn()
    {
        Instantiate(selfPrefab, new Vector3(-15, spawnHeight, 0), Quaternion.identity);
        Instantiate(selfPrefab, new Vector3(-5, spawnHeight, 0), Quaternion.identity);
        Instantiate(selfPrefab, new Vector3(5, spawnHeight, 0), Quaternion.identity);
        Instantiate(selfPrefab, new Vector3(15, spawnHeight, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}

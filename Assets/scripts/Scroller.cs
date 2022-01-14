using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    EnvironmentController controller;
    
    [SerializeField] float speedPercentage;
    [SerializeField] float spawnHeight;
    [SerializeField] float width=10;
    [SerializeField] bool isObstacle;
    [SerializeField] GameObject selfPrefab;

    public bool spawnedNew = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("SystemController").GetComponent(typeof(EnvironmentController)) as EnvironmentController;
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = gameObject.transform.position.x;
        if(!controller.paused){
            gameObject.transform.Translate(new Vector3(- controller.currentSpeed * Time.deltaTime * (speedPercentage/100f), 0,0),Space.World);
            if(positionX < (8 - width/2) && !isObstacle && !spawnedNew){
                Instantiate(selfPrefab, new Vector3(positionX + width, spawnHeight, 0), Quaternion.identity);
                spawnedNew = true;
            }
            if(positionX < (-8 - width/2)){
                Destroy(gameObject);
            }
        }
    }

    public void Spawn()
    {
        width = width-0.2f;
        float nextSpawn = 8+width;
        while(nextSpawn > -8){
            nextSpawn -= width;
            (Instantiate(selfPrefab, new Vector3(nextSpawn, spawnHeight, 0), Quaternion.identity).GetComponent(typeof(Scroller)) as Scroller).spawnedNew = true;
        }
        Instantiate(selfPrefab, new Vector3(8 + width, spawnHeight, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}

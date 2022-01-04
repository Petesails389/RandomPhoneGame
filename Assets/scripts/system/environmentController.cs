using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentController : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedIncPercent;

    [SerializeField] GameObject platformPrefab;

    List<GameObject> platforms = new List<GameObject>();    
    float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed;

        //spawns in platforms to start
        platforms.Add(Instantiate(platformPrefab, new Vector3(-15, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(-5, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(5, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(15, -5, 0), Quaternion.identity));
    }

    void Update()
    {
        //variable setup
        if(currentSpeed < maxSpeed){
            currentSpeed += currentSpeed * Time.deltaTime * (speedIncPercent/100);
        }

        //sort out the platform movement and recycling
        foreach(GameObject platform in platforms){
            //float newX = platform.transform.position.x ;
            platform.transform.Translate(new Vector3(- currentSpeed * Time.deltaTime, 0,0),Space.World);
        }
        if(platforms[0].transform.position.x < -15){
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
            platforms.Add(Instantiate(platformPrefab, new Vector3(15, -5, 0), Quaternion.identity));
        }

        //spawns and destroys obsticals

    }
}

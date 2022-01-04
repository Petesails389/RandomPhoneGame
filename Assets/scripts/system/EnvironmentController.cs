using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedIncPercent;

    [SerializeField] float spawnDist;
    [SerializeField] float spawnVariability;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject player;

    bool paused = false;
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> obstacles = new List<GameObject>();
    float currentSpeed;
    float spawnTimer;

    void Start()
    {
        currentSpeed = baseSpeed;
        spawnTimer = spawnDist*2f;

        //spawns in platforms to start
        platforms.Add(Instantiate(platformPrefab, new Vector3(-15, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(-5, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(5, -5, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(15, -5, 0), Quaternion.identity));
    }

    void Update()
    {
        if(paused == false){
            //variable setup
            if(currentSpeed < maxSpeed){
                currentSpeed += currentSpeed * Time.deltaTime * (speedIncPercent/100);
            }

            //sort out the platform movement and recycling
            foreach(GameObject platform in platforms){
                platform.transform.Translate(new Vector3(- currentSpeed * Time.deltaTime, 0,0),Space.World);
            }
            if(platforms[0].transform.position.x < -15){
                Destroy(platforms[0]);
                platforms.RemoveAt(0);
                platforms.Add(Instantiate(platformPrefab, new Vector3(15, -5, 0), Quaternion.identity));
            }

            //Obstacle controll
            if(spawnTimer <= 0f){
                obstacles.Add(Instantiate(obstaclePrefab, new Vector3(15, -4, 0), Quaternion.identity));
                spawnTimer = Random.Range(-spawnVariability,spawnVariability) + spawnDist;
            }
            else{
                spawnTimer -= currentSpeed * Time.deltaTime;
            }

            foreach(GameObject obstacle in obstacles){
                obstacle.transform.Translate(new Vector3(- currentSpeed * Time.deltaTime, 0,0),Space.World);
            }
            if(obstacles.Count > 0){
                if(obstacles[0].transform.position.x < -15){
                        Destroy(obstacles[0], 1);
                        obstacles.RemoveAt(0);
                }
            }
        }
    }

    public void pausePlay(){
        if(paused){
            paused = false;
            (player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D).simulated = true;

        }
        else{
            paused = true;
            (player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D).simulated = false;
        }
    }

    public void endGame()
    {
        (gameObject.GetComponent(typeof(UIController)) as UIController).deathUItoggle();
        paused = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject scoreUI;

    bool paused = false;
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> obstacles = new List<GameObject>();
    float currentSpeed;
    float spawnTimer;
    Text scoreText;
    float score;

    void Start()
    {
        currentSpeed = baseSpeed;
        spawnTimer = spawnDist;

        score = 0f;
        scoreText = scoreUI.GetComponent(typeof(Text)) as Text;
        scoreText.text = ((int) Math.Floor(score)).ToString();

        //spawns in platforms to start
        platforms.Add(Instantiate(platformPrefab, new Vector3(-15, -4, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(-5, -4, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(5, -4, 0), Quaternion.identity));
        platforms.Add(Instantiate(platformPrefab, new Vector3(15, -4, 0), Quaternion.identity));
    }

    void Update()
    {
        if(paused == false){
            score += Time.deltaTime * currentSpeed;
            scoreText.text = ((int) Math.Floor(score)).ToString();

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
                platforms.Add(Instantiate(platformPrefab, new Vector3(15, -4, 0), Quaternion.identity));
            }

            //Obstacle controll
            if(spawnTimer <= 0f){
                obstacles.Add(Instantiate(obstaclePrefab, new Vector3(15, -3, 0), Quaternion.identity));
                spawnTimer = UnityEngine.Random.Range(-spawnVariability,spawnVariability) + spawnDist;
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
            (player.GetComponent(typeof(AnimationController)) as AnimationController).Play();

        }
        else{
            paused = true;
            (player.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D).simulated = false;
            (player.GetComponent(typeof(AnimationController)) as AnimationController).Pause();
        }
    }

    public void endGame()
    {
        (gameObject.GetComponent(typeof(UIController)) as UIController).deathUItoggle();
        paused = true;
    }
}

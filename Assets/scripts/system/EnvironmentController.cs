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
    [SerializeField] GameObject wallNormalPrefab;
    [SerializeField] GameObject player;
    [SerializeField] GameObject scoreUI;

    public bool paused = false;
    public float currentSpeed;

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

        //spawns in platforms and walls to start
        (Instantiate(platformPrefab, new Vector3(0,0,0), Quaternion.identity).GetComponent(typeof(Scroller)) as Scroller).Spawn();
        (Instantiate(wallNormalPrefab, new Vector3(0,0,0), Quaternion.identity).GetComponent(typeof(Scroller)) as Scroller).Spawn();
    }

    void Update()
    {
        if(paused == false){
            //variable setup
            score += Time.deltaTime * currentSpeed;
            scoreText.text = ((int) Math.Floor(score)).ToString();

            if(currentSpeed < maxSpeed){
                currentSpeed += currentSpeed * Time.deltaTime * (speedIncPercent/100);
            }

            //Obstacle controll
            if(spawnTimer <= 0f){
                Instantiate(obstaclePrefab, new Vector3(15, -3, 0), Quaternion.identity);
                spawnTimer = UnityEngine.Random.Range(-spawnVariability,spawnVariability) + spawnDist;
            }
            else{
                spawnTimer -= currentSpeed * Time.deltaTime;
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
        (player.GetComponent(typeof(AnimationController)) as AnimationController).Kill();
        paused = true;
    }
}

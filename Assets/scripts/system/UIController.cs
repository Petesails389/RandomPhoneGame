using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseCanvas;
    
    private EnvironmentController controller;

    void Start()
    {
        controller = gameObject.GetComponent(typeof(EnvironmentController)) as EnvironmentController;
    }

    public void pauseUIToggle()
    {
        if(pauseCanvas.activeSelf){
            pauseCanvas.SetActive(false);
            controller.pausePlay();
        }
        else{
            pauseCanvas.SetActive(true);
            controller.pausePlay();
        }
    }
    public void deathUItoggle()
    {
        pauseButton.SetActive(false);
        if(pauseCanvas.activeSelf == false){
            pauseCanvas.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject[] DeathObjects;
    GameObject[] WinObjects;
    ShipController shipController;
    EnemyShipController enemyController;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        DeathObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
        WinObjects = GameObject.FindGameObjectsWithTag("ShowOnWin");
        HidePaused();
        HideDeath();
        HideWin();

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MovementTesting"))
        {
            shipController = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<ShipController>();
            enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyShipController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1 && shipController.alive == true)
            {
                Time.timeScale = 0;
                ShowPaused();
            }
            else if (Time.timeScale == 0 && shipController.alive == true)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                HidePaused();
            }
        }
        if(Time.timeScale == 1 && shipController.alive == false)
        {
            Time.timeScale = 0;
            ShowDeath();
        }
        if (Time.timeScale == 1 && enemyController.alive == false)
        {
            Time.timeScale = 0;
            Debug.Log("should win");
            ShowWin();
        }
    }
    //to load or restart a scene use LoadSceneOnClick instead

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }
    public void DeathControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowDeath();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideDeath();
        }
    }
    public void WinControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowWin();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideWin();
        }
    }

    //shows objects with ShowOnPause tag
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        Cursor.visible = true;
    }
    //hides objects with ShowOnPause tag
    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        Cursor.visible = false;
    }
    public void HideDeath()
    {
        foreach (GameObject g in DeathObjects)
        {
            g.SetActive(false);
        }
        Cursor.visible = false;
    }
    public void ShowDeath()
    {
        foreach (GameObject g in DeathObjects)
        {
            g.SetActive(true);
        }
        Cursor.visible = true;
    }
    public void HideWin()
    {
        foreach (GameObject g in WinObjects)
        {
            g.SetActive(false);
        }
        Cursor.visible = false;
    }
    public void ShowWin()
    {
        foreach (GameObject g in WinObjects)
        {
            g.SetActive(true);
        }
        Cursor.visible = true;
    }
}

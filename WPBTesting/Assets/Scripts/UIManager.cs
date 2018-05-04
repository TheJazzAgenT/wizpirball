using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject[] DeathObjects;
    GameObject[] WinObjects;
    ShipController shipController;
    ShipController enemyController;

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

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("FixedArena") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("V. SplitScreen"))
        {
            shipController = GameObject.FindGameObjectWithTag("Ship_P1").GetComponent<ShipController>();
            enemyController = GameObject.FindGameObjectWithTag("Ship_P2").GetComponent<ShipController>();
        }
        if(shipController == null)
        {
            Debug.Log("issue with player controller");
        }
        if(enemyController == null)
        {
            Debug.Log("issue with enemyController");
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
                Debug.Log("pausing?");
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
            //Time.timeScale = 0;
            Invoke("ShowDeath", 3);
        }
        if (Time.timeScale == 1 && enemyController.alive == false)
        {
            
            Debug.Log("should win");
            Invoke("ShowWin", 3);
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
            //Debug.Log("displaying pauseobject");
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
        Debug.Log("hiding cursor");
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
        Time.timeScale = 0;
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
        Time.timeScale = 0;
        foreach (GameObject g in WinObjects)
        {
            g.SetActive(true);
        }
        Cursor.visible = true;
    }
}

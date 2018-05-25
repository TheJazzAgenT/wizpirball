using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManegerMenu : MonoBehaviour
{
    //this exists just to make sure time scale is on when heading back to menu

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //to load or restart a scene use LoadSceneOnClick instead
}

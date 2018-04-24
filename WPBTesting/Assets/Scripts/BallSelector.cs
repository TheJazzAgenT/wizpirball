using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelector : MonoBehaviour
{
    public GameObject[] balls; // 0-A, 1-B, 2-X, 3-Y
    public int player; // 2 for player 2, 1 for player 1

    void Start()
    {
        balls[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        balls[1].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        balls[2].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        balls[3].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
    }

    void Update()
    {
        if (Input.GetButtonDown("A_" + player))
        {
            Debug.Log("Player numbber: " + player);
        }
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("A_" + player))
        {
            balls[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            balls[1].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[2].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[3].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B_" + player))
        {
            balls[0].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            balls[2].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[3].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("X_" + player))
        {
            balls[0].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[1].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            balls[3].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("Y_" + player))
        {
            balls[0].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[1].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[2].GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            balls[3].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}

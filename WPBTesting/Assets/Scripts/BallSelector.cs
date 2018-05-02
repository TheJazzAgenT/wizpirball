using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelector : MonoBehaviour
{
    public GameObject[] ballsContainer; // 0-A, 1-B, 2-X, 3-Y
    public Sprite[] Images;
    public int player; // 2 for player 2, 1 for player 1

    private Image[] balls;
    private int[] Loadout;

    void Start()
    {
        balls = new Image[ballsContainer.Length];
        for (int i = 0; i < ballsContainer.Length; i++)
        {
            balls[i] = ballsContainer[i].GetComponent<Image>();
        }

        if (GameObject.Find("InfoStorage") != null)
        {
            Loadout = GameObject.Find("InfoStorage").GetComponent<InfoStore>().GetLoadout(player);
        }
        else
        {
            Loadout = new int[] { 0, 1, 2 };
        }

        balls[1].sprite = Images[Loadout[0]];
        balls[2].sprite = Images[Loadout[1]];
        balls[3].sprite = Images[Loadout[2]];

        balls[0].color = new Color32(255, 255, 255, 255);
        balls[1].color = new Color32(100, 100, 100, 100);
        balls[2].color = new Color32(100, 100, 100, 100);
        balls[3].color = new Color32(100, 100, 100, 100);
    }

    void Update()
    {
        if (Input.GetButtonDown("A_" + player))
        {
            Debug.Log("Player numbber: " + player);
        }
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("A_" + player))
        {
            balls[0].color = new Color32(255, 255, 255, 255);
            balls[1].color = new Color32(100, 100, 100, 100);
            balls[2].color = new Color32(100, 100, 100, 100);
            balls[3].color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B_" + player))
        {
            balls[0].color = new Color32(100, 100, 100, 100);
            balls[1].color = new Color32(255, 255, 255, 255);
            balls[2].color = new Color32(100, 100, 100, 100);
            balls[3].color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("X_" + player))
        {
            balls[0].color = new Color32(100, 100, 100, 100);
            balls[1].color = new Color32(100, 100, 100, 100);
            balls[2].color = new Color32(255, 255, 255, 255);
            balls[3].color = new Color32(100, 100, 100, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("Y_" + player))
        {
            balls[0].color = new Color32(100, 100, 100, 100);
            balls[1].color = new Color32(100, 100, 100, 100);
            balls[2].color = new Color32(100, 100, 100, 100);
            balls[3].color = new Color32(255, 255, 255, 255);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	static public int maxHealth = 200;
    public bool alive;
    public bool isHit;
    public bool isDummy;
    public bool dummyHit = false;

    private int curHealth = 200;
    private float delay = 0.001f;

    [SerializeField]
    private BarScript bar;

    void Start()
    {
        //Cursor.visible = true;
        curHealth = maxHealth;
        alive = true;
        isHit = false;
    }
    void Update()
    {
        if (curHealth < 1)
        {
            CommitSudoku();
        }
    }

    public void TakeDamage(int amount)
    {
        if (!dummyHit)
        {
            dummyHit = true;
        }
        if(isHit == false && !isDummy)
        {
            isHit = true;
            curHealth -= amount;
            bar.fillAmount = curHealth;
            Debug.Log("ship hit");
            Invoke("ResetHit", delay);
        }
    }

    private void CommitSudoku()
    {
        if (!isDummy)
        {
            alive = false;
            GetComponent<ShipFixedPathing>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void ResetHit()
    {
        isHit = false;
    }
}
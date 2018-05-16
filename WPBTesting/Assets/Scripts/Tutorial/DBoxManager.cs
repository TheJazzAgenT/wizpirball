﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DBoxManager : MonoBehaviour {

    public GameObject dBox;
    public Text body;
    public Text boxTitle;

    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;

    private bool isActive = false;
    private bool cancelTyping = false;
    private float textScrollSpeed = 0.05f;

    private string title = "Coach Z: ";
    private string[] dialogues;
    private bool[] dialogueStatusP1 = new bool[] { true, false, false, false, false, false, false };
    private bool[] dialogueStatusP2 = new bool[] { true, false, false, false, false, false, false };
    private bool[] dialogueDisplayed = new bool[] { false, false, false, false, false, false, false };
    private int curDialogue = 0;

    // Use this for initialization
    void Start () {
        //ShowDialogue("Ayy Lmao", "I got 99 problems but dialogue boxes aint one");
        theSourceFile = new FileInfo("Assets/Dialogues.txt");
        reader = theSourceFile.OpenText();
        string text = " "; // assigned to allow first line to be read below
        text = reader.ReadLine();
        dialogues = new string[int.Parse(text)];
        for(int i = 0; i < dialogues.Length;i++)
        {
            text = reader.ReadLine();
            dialogues[i] = text;
            //Console.WriteLine(text);
            Debug.Log(text);
        }
        ShowNextDialogue();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive && (Input.GetButtonDown("A_1") || Input.GetButtonDown("A_2") || Input.GetKeyDown(KeyCode.Space)))
        {
            //Debug.Log("sudoku");
            dBox.SetActive(false);
            cancelTyping = true;
            isActive = false;
        }

        //Debug.Log(curDialogue + " : " + dialogueDisplayed[curDialogue - 1] + " : " + dialogueStatus[curDialogue]);
        if (dialogueDisplayed[curDialogue - 1] && dialogueStatusP1[curDialogue] && dialogueStatusP2[curDialogue])
        {
            ShowNextDialogue();
        }
        Debug.Log("current: " + curDialogue + " : " + dialogues.Length);
        if (curDialogue == dialogues.Length - 1)
        {
            StartCoroutine(LoadSceneOnDelay(20.0f));
        }
    }

    public void ReadyDialogue(int dialogueNum, int player)
    {
        if (player == 1)
        {
            dialogueStatusP1[dialogueNum] = true;
        }
        else if (player == 2)
        {
            dialogueStatusP2[dialogueNum] = true;
        }
        else
        {
            Debug.LogWarning("Invalid player number given");
        }
    }

    private void ShowNextDialogue()
    {
        StopCoroutine("ScrollText");
        StartCoroutine(ScrollText(dialogues[curDialogue]));
        boxTitle.text = title;
        dialogueDisplayed[curDialogue] = true;
        curDialogue++;
    }

    private IEnumerator ScrollText(string theText)
    {
        int index = 0;
        while(!cancelTyping && theText.Length > index)
        {
            body.text = theText.Remove(index);
            index++;
            yield return new WaitForSeconds(textScrollSpeed);
        }
        body.text = theText;
        cancelTyping = false;
    }

    private IEnumerator LoadSceneOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<LoadSceneOnClick>().LoadByIndex(3);
    }
}
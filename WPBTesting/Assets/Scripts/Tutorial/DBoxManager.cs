using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DBoxManager : MonoBehaviour
{

    public GameObject dBox;
    public Text body;
    public Text boxTitle;
    public RawImage[] buttonImgs;
    public GameObject[] healthBars;
    public GameObject[] manaBars;
    public GameObject Enemy;

    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;

    private bool isActive = false;
    private bool cancelTyping = false;
    private bool nextSceneTriggered = false;
    private float textScrollSpeed = 0.02f;

    private string title = "Coach Z: ";
    private string[] dialogues;
    private bool[] dialogueStatusP1;
    private bool[] dialogueStatusP2;
    private bool[] dialogueDisplayed;
    private bool[] buttonDisplay;
    private int curDialogue = 0;
    private int curButton = -1;
    private Fade fader;
    private GameObject Continue;

    // Use this for initialization
    void Start()
    {
        fader = GameObject.Find("BlackScreen").GetComponent<Fade>();
        //fader.DoFade();
        //ShowDialogue("Ayy Lmao", "I got 99 problems but dialogue boxes aint one");
        theSourceFile = new FileInfo("Assets/Dialogues.txt");
        reader = theSourceFile.OpenText();
        string text = " "; // assigned to allow first line to be read below
        text = reader.ReadLine();
        int numLines = int.Parse(text);
        dialogues = new string[numLines];
        dialogueStatusP1 = new bool[numLines];
        dialogueStatusP2 = new bool[numLines];
        dialogueDisplayed = new bool[numLines];
        buttonDisplay = new bool[numLines];
        string[] splits;
        Continue = GameObject.FindGameObjectWithTag("continueButton");
        Continue.SetActive(false);
        for (int i = 0; i < dialogues.Length; i++)
        {
            text = reader.ReadLine();
            splits = text.Split(new char[] { ' ' }, 3);
            dialogues[i] = splits[2];
            dialogueStatusP1[i] = splits[0] == "true";
            dialogueStatusP2[i] = splits[0] == "true";
            dialogueDisplayed[i] = false;
            buttonDisplay[i] = splits[1] == "true";
            Debug.Log(buttonDisplay[i]);
            //Console.WriteLine(text);
            //Debug.Log(text);
        }

        /*foreach (RawImage img in buttonImgs)
        {
            img.gameObject.SetActive(true);
        }*/
        ShowNextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && (Input.GetButtonDown("A_1") || Input.GetButtonDown("A_2") || Input.GetKeyDown(KeyCode.Space)))
        {
            //Debug.Log("sudoku");
            dBox.SetActive(false);
            cancelTyping = true;
            isActive = false;
        }

        if (curDialogue == dialogues.Length && !nextSceneTriggered)
        {
            StartCoroutine(LoadSceneOnDelay(10.0f));
            StartCoroutine(FadeOnDelay(7.0f));
            nextSceneTriggered = true;
        }
        //Debug.Log(curDialogue + " : " + dialogueDisplayed[curDialogue - 1] + " : " + dialogueStatus[curDialogue]);
        else if (!nextSceneTriggered && dialogueDisplayed[curDialogue - 1] && dialogueStatusP1[curDialogue] && dialogueStatusP2[curDialogue])
        {
            ShowNextDialogue();
        }
        //Debug.Log("current: " + curDialogue + " : " + dialogues.Length);

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

        if (buttonDisplay[curDialogue])
        {
            curButton++;
            buttonImgs[curButton].gameObject.SetActive(true);
            if (curButton > 0)
            {
                buttonImgs[curButton - 1].gameObject.SetActive(false);
            }
            if (curButton == 1)
            {
                curButton++;
                buttonImgs[curButton].gameObject.SetActive(true);
            }
            if (curButton == 3)
            {
                buttonImgs[curButton - 2].gameObject.SetActive(false);
            }
        }
        else
        {
            buttonImgs[curButton].gameObject.SetActive(false);
        }
        Continue.SetActive(false);
        if (curDialogue == 1)
        {
            Continue.SetActive(true);
            foreach (GameObject manaBar in manaBars)
            {
                manaBar.GetComponent<PulseImage>().StartPulse();
            }
        }
        if (curDialogue == 2)
        {
            foreach (GameObject manaBar in manaBars)
            {
                manaBar.GetComponent<PulseImage>().StopPulse();
            }
            foreach (GameObject healthBar in healthBars)
            {
                healthBar.GetComponent<PulseImage>().StartPulse();
            }
            Continue.SetActive(true);
        }
        if (curDialogue == 3)
        {
            foreach (GameObject healthBar in healthBars)
            {
                healthBar.GetComponent<PulseImage>().StopPulse();
            }
            Continue.SetActive(true);
        }
        if (curDialogue == 6)
        {
            Enemy.SetActive(true);
        }

        dialogueDisplayed[curDialogue] = true;
        curDialogue++;
    }

    private IEnumerator ScrollText(string theText)
    {
        int index = 0;
        while (!cancelTyping && theText.Length > index)
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
    private IEnumerator FadeOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fader.DoFade(3.0f, false);
    }
}

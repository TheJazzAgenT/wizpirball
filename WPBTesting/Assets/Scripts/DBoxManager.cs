using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBoxManager : MonoBehaviour {

    public GameObject dBox;
    public Text body;
    public Text boxTitle;

    private bool isActive = false;
    private bool cancelTyping = false;
    private float textScrollSpeed = 0.1f;

	// Use this for initialization
	void Start () {
        ShowDialogue("Ayy Lmao", "I got 99 problems but dialogue boxes aint one");
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive && (Input.GetButtonDown("X_1") || Input.GetButtonDown("X_2") || Input.GetKeyDown(KeyCode.Space)))
        {
            //Debug.Log("sudoku");
            dBox.SetActive(false);
            cancelTyping = true;
            isActive = false;
        }
	}

    public void ShowDialogue(string title, string dialogue)
    {
        if (!isActive)
        {
            dBox.SetActive(true);
            isActive = true;
            StartCoroutine(ScrollText(dialogue));
            //body.text = dialogue;
            boxTitle.text = title;
        }

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
}

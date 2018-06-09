using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnStart : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected = false;

    // Use this for initialization
    void Start()
    {
        if (buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
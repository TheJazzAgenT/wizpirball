using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelector : MonoBehaviour {

    public int level;

    GameObject MapA;
    GameObject MapB;
    GameObject description;

    Text descript;
    // Use this for initialization
    void Start()
    {
        MapA = GameObject.Find("MapAPic");
        MapB = GameObject.Find("MapBPic");
        description = GameObject.Find("Map_Descript");

        descript = description.GetComponent<Text>();

        descript.text = " ";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonHover(Button button)
    {
        if (button.name == "Map1Button")//horseshoe island thing
        {
            MapA.transform.SetAsLastSibling();
            descript.text = " island shaped like a horseshoe, the edge of the Empires control";
        }
        else if (button.name == "Map2Button")//canyons of doom
        {
            MapB.transform.SetAsLastSibling();
            descript.text = " Canyons that many have tried to navigate and failed, then again they never thought to use magic";
        }
    }

}

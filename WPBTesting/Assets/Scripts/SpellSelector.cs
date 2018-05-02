using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SpellSelector : MonoBehaviour {

    public int level;
    public Sprite[] SpellImages;
    public GameObject SlotXP1;
    public GameObject SlotYP1;
    public GameObject SlotBP1;
    public GameObject SlotXP2;
    public GameObject SlotYP2;
    public GameObject SlotBP2;

    private Text descriptP1;
    private int ActiveSpellP1;
    private Text descriptP2;
    private int ActiveSpellP2;
    private int[] FinalChoicesP1 = new int[] { -1, -1, -1 };
    private int[] FinalChoicesP2 = new int[] { -1, -1, -1 };
    private Dictionary<string, int> buttonToNumP1;
    private Dictionary<string, int> buttonToNumP2;
    private string[] descriptions = new string[5];
    private bool[] ready = new bool[] { false, false };
    // Use this for initialization
    void Start()
    {
        descriptP1 = GameObject.Find("SpellDescriptionP1").GetComponent<Text>();
        descriptP1.text = " ";
        descriptP2 = GameObject.Find("SpellDescriptionP2").GetComponent<Text>();
        descriptP2.text = " ";
        buttonToNumP1 = new Dictionary<string, int>()
            {
                {"FireP1", 0 },
                {"IceP1", 1 },
                {"LightningP1", 2},
                {"VampireP1", 3},
                {"MIRVP1", 4}
            };
        buttonToNumP2 = new Dictionary<string, int>()
            {
                {"FireP2", 0 },
                {"IceP2", 1 },
                {"LightningP2", 2},
                {"VampireP2", 3},
                {"MIRVP2", 4}
            };
        descriptions[0] = "Shits on fire, yo";
        descriptions[1] = "Ice, Ice, Baby";
        descriptions[2] = "Magnetizes your baseballs";
        descriptions[3] = "Steal life from your opponent";
        descriptions[4] = "Splits and splits and splits and splits";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("X_1") || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotXP1.GetComponent<Image>().sprite = SpellImages[ActiveSpellP1];
            FinalChoicesP1[0] = ActiveSpellP1;
        }
        if (Input.GetButtonDown("Y_1") || Input.GetKeyDown(KeyCode.Alpha3))
        {
            SlotYP1.GetComponent<Image>().sprite = SpellImages[ActiveSpellP1];
            FinalChoicesP1[1] = ActiveSpellP1;
        }
        if (Input.GetButtonDown("B_1") || Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlotBP1.GetComponent<Image>().sprite = SpellImages[ActiveSpellP1];
            FinalChoicesP1[2] = ActiveSpellP1;
        }
        if (Input.GetButtonDown("X_2") || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotXP2.GetComponent<Image>().sprite = SpellImages[ActiveSpellP2];
            FinalChoicesP2[0] = ActiveSpellP2;
        }
        if (Input.GetButtonDown("Y_2") || Input.GetKeyDown(KeyCode.Alpha3))
        {
            SlotYP2.GetComponent<Image>().sprite = SpellImages[ActiveSpellP2];
            FinalChoicesP2[1] = ActiveSpellP2;
        }
        if (Input.GetButtonDown("B_2") || Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlotBP2.GetComponent<Image>().sprite = SpellImages[ActiveSpellP2];
            FinalChoicesP2[2] = ActiveSpellP2;
        }
        if(ready[0] && ready[1])
        {
            if (!FinalChoicesP1.Contains(-1) || !FinalChoicesP2.Contains(-1))
            {
                var setP1 = new HashSet<int>(FinalChoicesP1);
                var setP2 = new HashSet<int> (FinalChoicesP2);
                if (setP1.Count == FinalChoicesP1.Length && setP2.Count == FinalChoicesP2.Length)
                {
                    SceneManager.LoadScene(level);
                }
            }
        }
    }

    public void ButtonClickP1(Button button)
    {
        ActiveSpellP1 = buttonToNumP1[button.name];
        descriptP1.text = descriptions[ActiveSpellP1];
    }
    public void ButtonClickP2(Button button)
    {
        ActiveSpellP2 = buttonToNumP2[button.name];
        descriptP2.text = descriptions[ActiveSpellP2];
    }
    public void ReadyUp(int player)
    {
        ready[player] = !ready[player];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScrollSelector(int sceneIndex)
    {
        GameObject.FindGameObjectWithTag("ScrollSelectPanel").GetComponent<SpellSelector>().level = sceneIndex;
    }
}

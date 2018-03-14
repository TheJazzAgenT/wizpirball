using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;


public class FlashIndicator : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer Flash;
    private Color colorField;// = Flash.GetComponent<SpriteRenderer>().color;

    public float width = 3;
    public float height = 3;
    public GameObject start;
    public GameObject target;// = GetComponentInParent<EnemyCharController>().playerShip;

    //public Vector3 position = new Vector3(10, 5, 0);

    private bool oscillate;
    private float oscSpeed = .05f;
    private Vector3 init;
    private Vector3 scaleTransf;

    // Use this for initialization

    //private IEnumerator coroutine; //will be used as a coroutine for the oscillator


    void Start () {
        //Vector3 scale = new Vector3(width, height, 1f);
        //transform.localScale *= (DistTransform(start, target) / 20);
        init = transform.localScale;
        scaleTransf = transform.localScale;
        oscillate = true;
        colorField = Flash.GetComponent<SpriteRenderer>().color;
        //coroutine = Blink(1);
        //target = Player;
        target = GameObject.FindGameObjectWithTag("PlayerShip");//getCom  <EnemyCharController>().playerShip;
    }
	
	// Update is called once per frame
	void Update () {
        colorField = Flash.GetComponent<SpriteRenderer>().color;
        //Debug.Log(colorField);
        float transf = 100 / 255;
        
        if (colorField.a < transf)
        {
            oscillate = false;
        }
        if (colorField.a == 1)
        {
            oscillate = true;
        }
        if (oscillate)
        {
            colorField.a = colorField.a - oscSpeed;
            Flash.GetComponent<SpriteRenderer>().color = colorField;
            //Debug.Log("here");

        }
        if (oscillate == false)
        {
            colorField.a = colorField.a + oscSpeed;
            Flash.GetComponent<SpriteRenderer>().color = colorField;
            //Debug.Log("now here");
        }
        if (target != null)
        {
            transform.LookAt(target.GetComponent<Transform>().position);
        }
        scaleTransf.x = DistTransform(start, target);
        //Debug.Log(scaleTransf.x);
        scaleTransf.y = DistTransform(start, target);
        transform.localScale = scaleTransf;
        //transform.localScale = (DistTransform(start, target));


    }
    private float DistTransform(GameObject One, GameObject Two)
    {
        //Debug.Log("getting dist");
        Vector3 positionOne = One.GetComponent<Transform>().position;
        Vector3 positionTwo = Two.GetComponent<Transform>().position;
        
        return Vector3.Distance(positionOne, positionTwo);
    }
    //private IEnumerator Blink(float speed)
    /*IEnumerator Blink(float timeS)
    {

    }*/
    /*private float RotateTo(GameObject One, GameObject Two)
    {

        return 0;
    }*/
}

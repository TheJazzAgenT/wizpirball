using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	[SerializeField]
	private BarScript bar;

	static public int maxHealth = 100;
	[SerializeField]
    static public int curHealth = 100;

    public bool alive;
    public Vector3 COM;
    public AudioClip crashSound;

    private Rigidbody rb;
    private Transform m_COM;
    float movementFactor;
    float steerFactor;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        //Cursor.visible = true;
        curHealth = maxHealth;
        alive = true;
    }
    void Update()
    {

        if (curHealth < 1)
        {
            //Destroy(gameObject);//breaks stuff if kept in
            alive = false;
        }
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
		bar.fillAmount = curHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "WATER")
        {
            audioSource.PlayOneShot(crashSound);
        }
    }
}
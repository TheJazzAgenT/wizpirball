using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	
    public Camera ShipCamera;
    public float speed = 0.02f;
    public float steerSpeed = 0.7f;
    public float movementThreshold = 10.0f;
    public float maxSpeed = 5;
    public float maxTurnSpeed = 5;
    public float steerThreshold = 5.0f;
    
	[SerializeField]
	private BarScript bar;

	static public int maxHealth = 100;
	[SerializeField]
    static public int curHealth = 100;

    public bool alive;
    public float damage = 20;//not sure if this should be here or in player controller, just leaving this here though
    public Vector3 COM;
    public AudioClip crashSound;

    private Rigidbody rb;
    private Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
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
        Cursor.visible = true;
        curHealth = maxHealth;
        alive = true;
    }
    void Update()
    {
        if (ShipCamera.enabled)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }

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
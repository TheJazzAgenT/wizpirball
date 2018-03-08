using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    //public Camera PlayerCamera;
    public float speed = 0.02f;
    //public float BulletSpeed = 6.0f;
    public GameObject spawnPoint;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform batAimer;
    public AudioClip batSound;
    //public float turnSpeed = 50;
    public float batDelay = 2.0f;
    public GameObject[] Bullets;
    //TEMP
    //public Vector3 aimer;

    private float fireDelay = 0.8f;
    [SerializeField]
    private GameObject myShip;
    float verticalInput;
    float horizontalInput;
    float timestamp;
    bool canRespawn = true;
    Animator anim;
    Animator legsAnim;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        legsAnim = GetComponentInChildren<Animator>();
		//anim.SetBool("Idle", true);
        anim.SetBool("Moving", false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= timestamp)
        {
            timestamp = Time.time + batDelay;
            anim.SetBool("Moving", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Hitting", true);
            Invoke("Fire", fireDelay);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletPrefab = Bullets[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletPrefab = Bullets[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletPrefab = Bullets[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            bulletPrefab = Bullets[3];
        }
    }
    void FixedUpdate () {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * speed, 0.0f, verticalInput * speed);
        if (verticalInput != 0 || horizontalInput != 0)
        {
            legsAnim.SetTrigger("Moving");
            anim.SetBool("Moving", true);
            anim.SetBool("Idle", false);

        }
        else
        {
            //legsAnim.SetTrigger("Stopping");
            //anim.SetBool("Idle", true);
            anim.SetBool("Moving", false);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "WATER" && canRespawn)
        {
            canRespawn = false;
            Invoke("respawnPlayer", 5);
        }
    }
    void respawnPlayer()
    {
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = spawnPoint.transform.rotation;
        canRespawn = true;
    }
    void Fire()
    {
        audioSource.PlayOneShot(batSound, 1.0f);
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        Vector3 boatVelocity = myShip.GetComponent<ShipFixedPathing>().getShipVelocity();
        bullet.GetComponent<Rigidbody>().velocity = batAimer.forward * Ballistics.bulletSpeed + boatVelocity;
        anim.SetBool("Hitting", false);
        //bullet.GetComponent<Rigidbody>().velocity = VelocityFinder.BallisticVel(transform.position, aimer, 45f);
        // not destroying bullet yet, letting it go free
        // Destroy the bullet after 2 seconds
        // Destroy(bullet, 2.0f);
    }
}
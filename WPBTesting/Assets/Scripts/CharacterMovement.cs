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
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= timestamp)
        {
            timestamp = Time.time + batDelay;
            anim.SetTrigger("isHitting");
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
        if (verticalInput != 0 || horizontalInput !=0)
        {
            anim.SetTrigger("isMoving");
        }
        else
        {
            //anim.SetTrigger("");
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
        //bullet.GetComponent<Rigidbody>().velocity = VelocityFinder.BallisticVel(transform.position, aimer, 45f);
        // not destroying bullet yet, letting it go free
        // Destroy the bullet after 2 seconds
        // Destroy(bullet, 2.0f);
    }
}
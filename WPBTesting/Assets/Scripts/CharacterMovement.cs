using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    //public Camera PlayerCamera;
    public float speed = 0.02f;
    //public float BulletSpeed = 6.0f;
    public GameObject spawnPoint;
    public GameObject bulletPrefab;
    public GameObject ShieldPrefab;//need editing
    //public Transform[] shieldSpawns;
    public Transform bulletSpawn;
    public Transform batAimer;
    public AudioClip batSound;
    //public float turnSpeed = 50;
    public float batDelay = 2.0f;
    public GameObject[] Bullets;
    //TEMP
    //public Vector3 aimer;
    public GameObject[] shields;//0,1-back, 2-left 3-right 4,5-front
    GameObject LeftBarr;//left shield
    GameObject RightBarr;
    GameObject FrontBarr;
    GameObject BackBarr;

    public int mana;//starts at 100
    int manaCost;//this is cost for spells
    int shieldCost = 20;
    //add costs in this script
    //normal ball - no cost
    //fire ball - 15
    //ice ball - 25
    //lightning ball - 40
    //shields - 20
    private IEnumerator manaRegen;

    private float fireDelay = 0.8f;
    [SerializeField]
    private GameObject myShip;
    float verticalInput;
    float horizontalInput;
    float timestamp;
    bool canRespawn = true;
    Animator anim;
    //Animator legsAnim;
    private AudioSource audioSource;

    [SerializeField]
    private BarScript bar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        //legsAnim = GetComponentInChildren<Animator>();
		//anim.SetBool("Idle", true);
        anim.SetBool("Moving", false);
        LeftBarr = GameObject.Find("ShieldActivatePoint1");
        RightBarr = GameObject.Find("ShieldActivatePoint2");
        FrontBarr = GameObject.Find("ShieldActivatePoint3");
        BackBarr = GameObject.Find("ShieldActivatePoint4");
        manaCost = 0;
        manaRegen = Regen();
        StartCoroutine(manaRegen);
    }
    // Update is called once per frame
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetAxis("RightTrigger") > 0) && Time.time >= timestamp)
        {
            Debug.Log("TRIGGERED");
            timestamp = Time.time + batDelay;
            /*anim.SetBool("Moving", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Hitting", true);*/
            Invoke("Fire", fireDelay);
			anim.Play ("Armature|CharacterHittingOneHand");
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("A"))
        {
            //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAA");
            bulletPrefab = Bullets[0];//normal
            manaCost = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B"))
        {
            bulletPrefab = Bullets[1];//fire
            manaCost = 15;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("X"))
        {
            bulletPrefab = Bullets[2];//ice
            manaCost = 25;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("Y"))
        {
            bulletPrefab = Bullets[3];//lightning
            manaCost = 35;
        }
    }
    void FixedUpdate () {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * speed, 0.0f, verticalInput * speed);
        if (verticalInput != 0 || horizontalInput != 0)
        {
            //legsAnim.SetTrigger("Moving");
            anim.SetBool("Moving", true);
            anim.SetBool("Idle", false);

        }
        else
        {
            //legsAnim.SetTrigger("Stopping");
            anim.SetBool("Idle", true);
            anim.SetBool("Moving", false);
        }

    }
    IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!(mana>=100))
            {
                mana += 2;
                bar.fillAmount = mana;
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "WATER" && canRespawn)
        {
            canRespawn = false;
            Invoke("respawnPlayer", 4);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.transform.tag == "ShieldActivator" && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("RightBumper")))
        {
            //Debug.Log("shield activate");
            //var shield = (GameObject)Instantiate(ShieldPrefab, col.transform.position, col.transform.rotation);//needs editing
            if (col.gameObject == BackBarr)//1-left, 2-right, 3-front, 4-back
            {
                if(mana > shieldCost)
                {
                    shields[0].GetComponent<ShieldController>().Blocks = 3;
                    shields[1].GetComponent<ShieldController>().Blocks = 3;
                    shields[0].SetActive(true);
                    shields[1].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
            if (col.gameObject == LeftBarr)//1-left, 2-right, 3-front, 4-back
            {
                if(mana > shieldCost){
                    shields[2].GetComponent<ShieldController>().Blocks = 3;
                    shields[2].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
            if (col.gameObject == RightBarr)//1-left, 2-right, 3-front, 4-back
            {
                if (mana > shieldCost)
                {
                    shields[3].GetComponent<ShieldController>().Blocks = 3;
                    shields[3].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
            if (col.gameObject == FrontBarr)//1-left, 2-right, 3-front, 4-back
            {
                if (mana > shieldCost)
                {
                    shields[4].GetComponent<ShieldController>().Blocks = 3;
                    shields[5].GetComponent<ShieldController>().Blocks = 3;
                    shields[4].SetActive(true);
                    shields[5].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
        }
    }
    void respawnPlayer()
    {
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = spawnPoint.transform.rotation;
        canRespawn = true;
    }
    public void Fire()
    {
        if (mana > manaCost)
        {
            audioSource.PlayOneShot(batSound, 1.0f);
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);
            mana -= manaCost;
            bar.fillAmount = mana;

            // Add velocity to the bullet
            Vector3 boatVelocity = myShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            bullet.GetComponent<Rigidbody>().velocity = batAimer.forward * Ballistics.bulletSpeed + boatVelocity;
            anim.SetBool("Hitting", false);
            //bullet.GetComponent<Rigidbody>().velocity = VelocityFinder.BallisticVel(transform.position, aimer, 45f);
            // not destroying bullet yet, letting it go free
            // Destroy the bullet after 2 seconds
            // Destroy(bullet, 2.0f);
        }
        else
        {
            Debug.Log("not enough mana");
        }
    }
}
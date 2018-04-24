using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed = 0.02f; // Player movement speed
    public GameObject spawnPoint; // Where player spawns if they fall off the ship
    public int playerNum; // Player 1 is 1, Player 2 is 2
    public bool stunned = false;
    public GameObject ShieldPrefab;// need editing
    public Transform bulletSpawn;
    public GameObject bulletPrefab; // Gets overwritten once the player changes ball types
    public GameObject[] Bullets; // Array of possible magic balls

    public Transform batAimer;
    public AudioClip batSound;
    public float batDelay = 2.0f;

    public int mana; // starts at 100

    // This dictionary maps a universal set of numbers to player specific inputs. It gets defined in Start().
    public Dictionary<int, string> playerInput;

    public GameObject[] shields; // 0,1-back, 2-left 3-right 4,5-front
    GameObject LeftBarr; // left shield
    GameObject RightBarr;
    GameObject FrontBarr;
    GameObject BackBarr;

    int manaCost; // This is cost for spells
    int shieldCost = 20;
    // add costs in this script
    // normal ball - 0
    // fire ball - 15
    // ice ball - 25
    // lightning ball - 40
    // poly ball - 25
    // MIRV ball - 20
    // Vamp Ball - 35
    // Smoke Ball - 15
    // shields - 20
    private IEnumerator manaRegen;

    private float fireDelay = 0.8f;

    float verticalInput;
    float horizontalInput;
    float timestamp;
    bool canRespawn = true;
    Animator anim;
    //Animator legsAnim;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject myShip;
    [SerializeField]
    private BarScript bar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = spawnPoint.transform.rotation;

        anim = GetComponent<Animator>();
        anim.SetBool("Moving", false);

        LeftBarr = GameObject.Find("ShieldActivatePoint1");
        RightBarr = GameObject.Find("ShieldActivatePoint2");
        FrontBarr = GameObject.Find("ShieldActivatePoint3");
        BackBarr = GameObject.Find("ShieldActivatePoint4");

        manaCost = 0;
        manaRegen = Regen();
        StartCoroutine(manaRegen);

        //Set input dictionary appropriate to player
        if (playerNum == 1)
        {
            playerInput = new Dictionary<int, string>()
            {
                {0, "X_1" },
                {1, "Y_1" },
                {2, "A_1" },
                {3, "B_1" },
                {4, "RightStickX" },
                {5, "RightStickY" },
                {6, "Horizontal" },
                {7, "Vertical" },
                {8, "LeftBumper" },
                {9, "RightBumper" },
                {10, "LeftTrigger" },
                {11, "RightTrigger" }
            };
        }
        else if (playerNum == 2)
        {
            playerInput = new Dictionary<int, string>()
            {
                {0, "X_2" },
                {1, "Y_2" },
                {2, "A_2" },
                {3, "B_2" },
                {4, "RightStickX_2" },
                {5, "RightStickY_2" },
                {6, "Horizontal_2" },
                {7, "Vertical_2" },
                {8, "LeftBumper_2" },
                {9, "RightBumper_2" },
                {10, "LeftTrigger_2" },
                {11, "RightTrigger_2" }
            };
        }
        else
        {
            Debug.LogWarning("Player Number not set!");
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetAxis(playerInput[11]) > 0) && Time.time >= timestamp)
        {
            Debug.Log("TRIGGERED");
            timestamp = Time.time + batDelay;
            if (!stunned)
            {
                Invoke("Fire", fireDelay);
                anim.Play("Armature|CharacterHittingOneHand");
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown(playerInput[2]))
        {
            bulletPrefab = Bullets[0]; // normal
            manaCost = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown(playerInput[3]))
        {
            bulletPrefab = Bullets[1]; // fire
            manaCost = 15;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown(playerInput[0]))
        {
            bulletPrefab = Bullets[2]; // ice
            manaCost = 25;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown(playerInput[1]))
        {
            bulletPrefab = Bullets[3]; // lightning
            manaCost = 35;
        }
        /*if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetButtonDown(playerInput[1]))
        {
            bulletPrefab = Bullets[4]; // poly
            manaCost = 35;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetButtonDown(playerInput[1]))
        {
            bulletPrefab = Bullets[5]; // MIRV
            manaCost = 20;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetButtonDown(playerInput[1]))
        {
            bulletPrefab = Bullets[6]; // Vamp
            manaCost = 35;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetButtonDown(playerInput[1]))
        {
            bulletPrefab = Bullets[7]; // Smoke
            manaCost = 15;
        }*/
    }
    void FixedUpdate () {
        verticalInput = Input.GetAxis(playerInput[7]);
        horizontalInput = Input.GetAxis(playerInput[6]);
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
        if (col.transform.tag == "ShieldActivator" && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(playerInput[9])))
        {
            //var shield = (GameObject)Instantiate(ShieldPrefab, col.transform.position, col.transform.rotation);//needs editing
            if (col.gameObject == BackBarr) // 1-left, 2-right, 3-front, 4-back
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
            if (col.gameObject == LeftBarr) //1-left, 2-right, 3-front, 4-back
            {
                if(mana > shieldCost){
                    shields[2].GetComponent<ShieldController>().Blocks = 3;
                    shields[2].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
            if (col.gameObject == RightBarr) //1-left, 2-right, 3-front, 4-back
            {
                if (mana > shieldCost)
                {
                    shields[3].GetComponent<ShieldController>().Blocks = 3;
                    shields[3].SetActive(true);
                    mana -= shieldCost;
                    bar.fillAmount = mana;
                }
            }
            if (col.gameObject == FrontBarr) //1-left, 2-right, 3-front, 4-back
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
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<PlayerSelector>().SetPlayer(playerNum == 1 ? "P1" : "P2");
            mana -= manaCost;
            bar.fillAmount = mana;

            // Add velocity to the bullet
            Vector3 boatVelocity = myShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            bullet.GetComponent<Rigidbody>().velocity = batAimer.forward * Ballistics.bulletSpeed + boatVelocity;
            anim.SetBool("Hitting", false);
        }
        else
        {
            Debug.Log("not enough mana");
        }
    }
}
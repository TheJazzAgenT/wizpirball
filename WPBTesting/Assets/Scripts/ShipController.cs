using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	static public int maxHealth = 100;
	[SerializeField]
    static public int curHealth = 100;

    public bool alive;
    public AudioClip crashSound;

    private AudioSource audioSource;
    [SerializeField]
    private BarScript bar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //Cursor.visible = true;
        curHealth = maxHealth;
        alive = true;
    }
    void Update()
    {
        if (curHealth < 1)
        {
            CommitSudoku();
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

    private void CommitSudoku()
    {
        alive = false;
        GetComponent<ShipFixedPathing>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
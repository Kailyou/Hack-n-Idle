using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    // References
    private Rigidbody2D rb2d;
    private Animator animator;
    //private AudioSource audioSource;

    // Config
    public float moveSpeed = 2;
	private bool playerMoving;
	private bool diagonal;
	private Vector2 lastMove;

    // Status
    [HideInInspector]
    public bool locked    = false; 
	public bool attacking = false;
    
    // Melee Attack
    public int meleeDamage = 1;
	private float attackCooldownTime_sword = 0.6f;
    private float next_attack_sword = 0f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	private void Update()
    {
		Handle_User_Input_Movement();
    }

	private void Handle_User_Input_Movement()
	{
		if (!locked) 
		{
			playerMoving   = false;

			float hor = Input.GetAxisRaw("Horizontal");
			float ver = Input.GetAxisRaw("Vertical");

			// Check if player is moving directional
			if (hor > 0.5f && ver > 0.5f
			    || hor > 0.5f && ver < -0.5f
			    || hor < -0.5f && ver < -0.5f
			    || hor < -0.5f && ver > 0.5f)
			{
				diagonal = true;
			} 
			else 
			{
				diagonal = false;
			}
			
			// Horizontal Movement
			if (hor > 0.5f || hor < -0.5f || ver > 0.5f || ver < -0.5f)
			{
				playerMoving   = true;
			}

			if(Input.GetButtonDown("Fire1") && Time.time > next_attack_sword)
			{
				animator.SetTrigger("Attacking_Sword");
				attacking = true;
				next_attack_sword = Time.time + attackCooldownTime_sword;
			}

			// set attack to false
			if (attacking && Time.time > next_attack_sword) 
			{
				attacking = false;
			}
								
			animator.SetFloat("MoveX", hor);
			animator.SetFloat("MoveY", ver);
			animator.SetFloat("LastMoveX", lastMove.x);
			animator.SetFloat("LastMoveY", lastMove.y);
			animator.SetBool("PlayerMoving", playerMoving);
		}
	}

	private void FixedUpdate()
	{
		if (!locked) 
		{
			float hor = Input.GetAxisRaw ("Horizontal");
			float ver = Input.GetAxisRaw ("Vertical");

			// Stop move while attacking
			if (attacking) 
			{
				Debug.Log ("test");
				rb2d.velocity = new Vector2 (0, 0);
			} 
			else 
			{
				// Set player's speed to zero while not moving
				if (hor == 0 && ver == 0) 
				{
					rb2d.velocity = new Vector2 (0f, 0f);
				}
				else 
				{
					if (!diagonal) 
					{
						rb2d.velocity = new Vector2 (hor * moveSpeed, ver * moveSpeed);
					}
					else
					{
						rb2d.velocity = new Vector2(hor * (moveSpeed/2), ver * (moveSpeed));
					}

					lastMove = new Vector2 (hor, ver);
				}
			}
		}
	}
}
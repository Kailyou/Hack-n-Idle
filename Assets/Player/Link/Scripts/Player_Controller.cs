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
	private bool playerMovingHorVer;
	private bool playerMovingDiagonal;
	private Vector2 lastMove;

    // Status
    [HideInInspector]
    public bool locked = false; 
    
    // Melee Attack
    public int meleeDamage = 1;
    private float attackCooldownTime_melee = 0.25f;
    private float next_attack_melee = 0f;

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
		Debug.Log (rb2d.velocity);

		Handle_User_Input_Movement();
    }

	private void Handle_User_Input_Movement()
	{
		if (!locked) 
		{
			playerMovingHorVer   = false;
			playerMovingDiagonal = false;

			float hor = Input.GetAxisRaw("Horizontal");
			float ver = Input.GetAxisRaw("Vertical");

			// Check if player is moving directional
			if ((hor > 0.5f && ver > 0.5f)
				|| (hor > 0.5f && ver < -0.5f)
				|| (hor < -0.5f && ver < -0.5f)
				|| (hor < -0.5f && ver > 0.5f))
			{
				playerMovingHorVer   = false;
				playerMovingDiagonal = true;
			}

			// Horizontal Movement
			if (hor > 0.5f || hor < -0.5f)
			{
				if (!playerMovingDiagonal)
				{
					playerMovingHorVer   = true;
					playerMovingDiagonal = false;
				}
			}

			// Vertical Movement
			if (ver > 0.5f || ver < -0.5f)
			{
				if (!playerMovingDiagonal)
				{
					playerMovingHorVer = true;
					playerMovingDiagonal = false;
				}
			}

			animator.SetFloat("MoveX", hor);
			animator.SetFloat("MoveY", ver);
			animator.SetBool("PlayerMovingHorVer", playerMovingHorVer);
			animator.SetBool("PlayerMovingDiagonal", playerMovingDiagonal);
			animator.SetFloat("LastMoveX", lastMove.x);
			animator.SetFloat("LastMoveY", lastMove.y);
		}
	}

	private void FixedUpdate()
	{
		if (!locked) 
		{
			float hor = Input.GetAxisRaw ("Horizontal");
			float ver = Input.GetAxisRaw ("Vertical");

			// Set player's speed to zero while not moving
			if (hor == 0 && ver == 0) 
			{
				rb2d.velocity = new Vector2 (0f, 0f);
			}
			else 
			{
				if (!playerMovingDiagonal) 
				{
					rb2d.velocity = new Vector2 (hor * moveSpeed, ver * moveSpeed);
				}
				else
				{
					rb2d.velocity = new Vector2(hor * (moveSpeed/2), ver * (moveSpeed/2));
				}

				lastMove = new Vector2 (hor, ver);
			}
		}
	}

	public void Handle_User_Input_Attack()
	{
		// Melee Attack
		if (Input.GetButtonDown ("Fire1") && Time.time > next_attack_melee)
		{
			animator.SetTrigger ("attacking_melee");
			next_attack_melee = Time.time + attackCooldownTime_melee;
		}
	}
}
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
	void Update()
    {
		handleUpdate();

    }

	public void handleUpdate()
	{
		playerMoving = false;

		if (!locked) 
		{
			float hor = Input.GetAxisRaw("Horizontal");
			float ver = Input.GetAxisRaw("Vertical");

			if (hor > -0.5 && hor < 0.5 && ver > -0.5 && ver > 0.5)
			{
				playerMoving = false;
			}

			if (hor > 0.5f || hor < -0.5f)
			{
				transform.Translate(new Vector3(hor * moveSpeed * Time.deltaTime, 0f, 0f));
				lastMove = new Vector2 (hor, 0f);
				playerMoving = true;
			}

			if (ver > 0.5f || ver < -0.5f)
			{
				transform.Translate(new Vector3(0f, ver * moveSpeed * Time.deltaTime, 0f));
				lastMove = new Vector2 (0f, ver);
				playerMoving = true;
			}

			animator.SetFloat("MoveX", hor);
			animator.SetFloat("MoveY", ver);
			animator.SetBool("PlayerMoving", playerMoving);
			animator.SetFloat("LastMoveX", lastMove.x);
			animator.SetFloat("LastMoveY", lastMove.y);
		}
	}

	public void FixedUpdate()
	{
		
	}
}
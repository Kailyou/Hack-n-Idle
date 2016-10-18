using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    // References
    private Rigidbody2D rb2d;
    private Animator animator;
    //private AudioSource audioSource;

    // Config
    public float moveSpeed = 7;

    // Status
    [HideInInspector]
    public bool do_lock; 
    
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
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", hor);
        animator.SetFloat("MoveY", ver);

        /*
        if (hor > -0.5f && hor < 0.5f && ver > -0.5f && ver < 0.5f)
        {
            animator.SetBool("PlayerMoving", false);
        }
        else
        {
            animator.SetBool("PlayerMoving", true);
        }
        */
        
        if (hor > 0.5f || hor < -0.5f)
        {
            transform.Translate(new Vector3(hor * moveSpeed * Time.deltaTime,
                                            0f,
                                            0f));
        }

        if (ver > 0.5f || ver < -0.5f)
        {
            transform.Translate(new Vector3(0f,
                                            ver * moveSpeed * Time.deltaTime,
                                            0f));
        }
    }

    void FixedUpdate()
    {
     
    }

    // Flips the player
    public void Flip()
    {
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
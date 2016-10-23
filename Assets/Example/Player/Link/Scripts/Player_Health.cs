using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour {

	// Config
	public int maxHealthPoints = 10;
	//public Image healthGui;

	// Status
	private float currentHealthPoints;
	private bool isDamageable	= true;
	private bool isDead 		= false;

	// References
	private Animator animator;
	private Player_Controller playerController;


	public void Start ()
	{
		// Get references
		animator = GetComponent<Animator> ();
		playerController = GetComponent<Player_Controller> ();

		currentHealthPoints	= PlayerPrefs.GetFloat ("currentHealthPoints");
	}


	/**
	 * This function is used to apply damage to the player.
	 * When the player is alive, he will lose the damage amount as health.
	 * The player will become invincible for one second once he received damage.
	 * If the player died, his die animation (if existed) will be played.
	 */
	public void ApplyDamage (float damage)
	{
		if (isDamageable) 
		{
			isDamageable = false;

			if (!isDead)
			{
				currentHealthPoints = Mathf.Max (0, currentHealthPoints-damage);

				if (currentHealthPoints == 0) 
				{
					isDead = true;
					Dying ();
				} 
				else 
				{
					playHurtAnimation ();
					Invoke ("ResetIsDamageAble", 1);
				}
			}
		}
	}


	/**
	 * Resets the Invincible status of the player
	 */
	private void ResetIsDamageAble ()
	{
		isDamageable = true;
	}


	/**
	 * Refreshes health points of the player by a given amount.
	 */
	public void AddHealth (float extraHealth)
	{
		currentHealthPoints += extraHealth;
		currentHealthPoints = Mathf.Min (currentHealthPoints, maxHealthPoints);
	}


	/**
	 * This function can be used to let thep layer die instantly from outside
	 */
	public void DieNow ()
	{
		currentHealthPoints = 0;
		isDead = true;
		Dying ();
	}


	/**
	 * plays dieing animation and deactivates the player's control.
	 * Restarts the level if the player is not game over
	 * restarts the whole game if the player is game over
	 */
	private void Dying ()
	{
		animator.SetTrigger("Death");
		playerController.enabled = false;
	}


	/** 
	 * Triggers the hurt animation in the animator
	 */
	private void playHurtAnimation ()
	{
		animator.SetTrigger("hurt");
	}
}

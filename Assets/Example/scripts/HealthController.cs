using UnityEngine;
using System.Collections;

/**
 * Health Controller Class
 * 
 */
public class HealthController : MonoBehaviour
{
    // Config
    public float HP_max = 100f;
    public GameObject healthBar;
    public MonoBehaviour controller;
	public float invincibleTime;
    
	// Status
	private float HP_current;
	private bool isDamageable	= true;
	private bool isDead = false;
    

    public void Start()
    {
        HP_current = HP_max;
        InvokeRepeating("decreaseHP", 0.2f, 0.2f);
    }

    void decreaseHP()
    {
        HP_current -= 2;

        UpdateHealthBar();
    }

    /**
	 This function is used to apply damage.If the target is alive and is damageable, it's hp will be decreased by the given amount.
	 */
    public void ApplyDamage (int damage)
	{
		if (isDamageable) {
			isDamageable = false;

			if (!isDead) {
                changeCurrentHealth(damage);
                UpdateHealthBar();

                if (HP_current == 0) {
					Dying ();
				} else {
					Invoke ("ResetIsDamageAble", invincibleTime);
				}
			}
		}
	}


	/**
	 * Changes the maximum health value.
	 */
	public void changeMaxHealth (int value)
	{
		HP_max += value;
	}


	/**
	 * Changes the curent health value. If the new value would be less than 0, set current HP to 0 instead of.
	 */
	public void changeCurrentHealth (int value)
	{
        HP_current = Mathf.Max(0, HP_current - value);
        UpdateHealthBar();
    }


	/**
	 * Resets the Invincible status
	 */
	private void ResetIsDamageAble ()
	{
		isDamageable = true;
	}


	// Deactivate the controller to disable further actions.
	private void Dying ()
	{
		isDead = true;
		controller.enabled = false;
	}

    // Updates the health bar
    private void UpdateHealthBar()
    {
        float HP_calculated = HP_current / HP_max;

        // MyHealth value between 0-1, calculated by max health and current help
        HP_calculated = Mathf.Max(0, HP_calculated);

        healthBar.transform.localScale = new Vector3(HP_calculated, 
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z
                                                     );
    }
}

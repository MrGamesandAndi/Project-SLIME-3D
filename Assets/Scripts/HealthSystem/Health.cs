using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	#region Variables
	public float startingHealth = 100f;
	public float HealthPoints
	{
		get { return healthPoints; }
		set
		{
			healthPoints = Mathf.Clamp(value, 0f, 100f);

			if (healthPoints <= 0f)
			{
				Die();
			}
		}
	}

	public float healthPoints = 100f;
	private Image healthBar;
	public bool isDead = false;
	public AudioClip deathSfx;

	private CharacterController3D player;

	#endregion

	#region Basic Methods
	private void Awake()
	{
		healthPoints = startingHealth;
		healthBar = GameObject.Find("Health_Bar").GetComponent<Image>();
	}

	private void Start()
	{
		player = FindObjectOfType<CharacterController3D>();
	}
	#endregion

	#region Custom Methods
	public float TakeDamage(float quantity)
	{
		if(healthPoints > 0 && !player.isUsingPower)
		{
			float hpRemoved = healthPoints -= quantity * Time.deltaTime;
			healthBar.rectTransform.localScale = new Vector3(hpRemoved / 100, 1, 1);
			return hpRemoved;
		}
		else if (player.isUsingPower)
		{
			return 0;
		}

		Die();
		return 0;
	}

	public void Die()
	{
		if (!isDead)
		{
			isDead = true;
			AudioManager.Instance.StopMusic();
			AudioManager.Instance.PlaySfx(deathSfx, 2f);
		}
	}

	public bool PlayerIsDead()
	{
		if(healthPoints > 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	#endregion
}

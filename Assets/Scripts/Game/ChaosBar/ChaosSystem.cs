//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public static HealthSystem Instance;

	public Image currentChaosBar;
	public float chaosPoint = 0f;

	public float maxChaosPoint = 100f;

	//==============================================================
	// Awake
	//==============================================================
	void Awake()
	{
		Instance = this;
	}
	
	//==============================================================
	// Awake
	//==============================================================
  	void Start()
	{
		UpdateChaosBar();
	}

	//==============================================================
	// Chaos Logic
	//==============================================================
	private void UpdateChaosBar()
	{
		float ratio = chaosPoint / maxChaosPoint;
		currentChaosBar.rectTransform.localPosition = new Vector3(currentChaosBar.rectTransform.rect.width * ratio - currentChaosBar.rectTransform.rect.width, 0, 0);
	}

	public void Machin(float Damage)
	{
		chaosPoint += Damage;
		chaosPoint = chaosPoint < 0 ? 0 : (chaosPoint > maxChaosPoint ? maxChaosPoint : chaosPoint);

		UpdateChaosBar();
	}

	public void SetMaxChaos(float max)
	{
		maxChaosPoint = (int)max;

		UpdateChaosBar();
	}
}

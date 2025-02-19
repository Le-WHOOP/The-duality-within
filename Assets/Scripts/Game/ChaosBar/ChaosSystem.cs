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
using System;

public class ChaosSystem : MonoBehaviour
{
	public static ChaosSystem Instance;

	public Image currentChaosBar;
	public float chaosPoint = 0f;

	public float maxChaosPoint = 100f;

	//==============================================================
	// Awake
	//==============================================================
	void Awake()
	{
        Instance = this;
        GetMaxChaos();
	}
	
	//==============================================================
	// Awake
	//==============================================================
  	void Start()
	{
		UpdateChaosBar();
	}

	/// <summary>
	/// Sets the max chaos according to Hyde's difficulty
	/// </summary>
    private void GetMaxChaos()
    {
		Difficulty hydeDifficulty = GameSettings.SwapRoles ? GameSettings.Player1Difficulty : GameSettings.Player2Difficulty;
		switch (hydeDifficulty)
		{
			case Difficulty.Easy:
				maxChaosPoint = 50f;
				break;
			case Difficulty.Medium:
				maxChaosPoint = 100f;
				break;
			case Difficulty.Hard:
				maxChaosPoint = 200f;
				break;
		}
    }

    //==============================================================
    // Chaos Logic
    //==============================================================
    private void UpdateChaosBar()
	{
		float ratio = chaosPoint / maxChaosPoint;
		currentChaosBar.rectTransform.localPosition = new Vector3(currentChaosBar.rectTransform.rect.width * ratio - currentChaosBar.rectTransform.rect.width, 0, 0);
	}

	public void RaiseChaos(float value)
	{
		chaosPoint += value;
		chaosPoint = chaosPoint < 0 ? 0 : (chaosPoint > maxChaosPoint ? maxChaosPoint : chaosPoint);

		UpdateChaosBar();

		if (chaosPoint >= maxChaosPoint)
			GameController.Instance.EndGame();
	}

	public void SetMaxChaos(float max)
	{
		maxChaosPoint = (int)max;

		UpdateChaosBar();
	}
}

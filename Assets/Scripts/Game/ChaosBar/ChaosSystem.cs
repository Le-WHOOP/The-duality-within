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
using System.Collections.Generic;

public class ChaosSystem : MonoBehaviour
{
	public static ChaosSystem Instance;

    [SerializeField]
    private List<InteractableChaos> _allChaosActions;

    // Associates a chaotic action with wether it was done
    private readonly Dictionary<InteractableChaos, bool> _chaosStatus = new();

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
        GetMaxChaos();
		UpdateChaosBar();
	}

	/// <summary>
	/// Sets the max chaos according to Hyde's difficulty
	/// </summary>
    private void GetMaxChaos()
    {
		Difficulty hydeDifficulty = GameSettings.SwapRoles ? GameSettings.Player1Difficulty : GameSettings.Player2Difficulty;
		int chaoticActionsNumber = 0;
		switch (hydeDifficulty) // TODO update values
		{
			case Difficulty.Easy:
				chaoticActionsNumber = 4;
				break;
			case Difficulty.Medium:
				chaoticActionsNumber = 8;
				break;
			case Difficulty.Hard:
				chaoticActionsNumber = 16;
				break;
		}

        // select actions randomly
        System.Random random = new();
        List<InteractableChaos> selectedChaos = new(_allChaosActions);
		selectedChaos.Sort((a, b) => random.Next(100) - random.Next(100)); // shuffle
		for (int i = 0; i < chaoticActionsNumber; i++)
		{
			_chaosStatus.Add(selectedChaos[i], false);
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

	public void RaiseChaos(InteractableChaos chaoticAction)
	{
		if (_chaosStatus == null || !_chaosStatus.ContainsKey(chaoticAction))
		{
            throw new Exception("an invalid chaotic action is being executed");
        }

		if (!_chaosStatus[chaoticAction])
		{
			_chaosStatus[chaoticAction] = true;
			chaosPoint += chaoticAction.ChaosValue;
			chaosPoint = chaosPoint < 0 ? 0 : (chaosPoint > maxChaosPoint ? maxChaosPoint : chaosPoint);

			UpdateChaosBar();
			// TODO animation
		}
	}

	public void SetMaxChaos(float max)
	{
		maxChaosPoint = (int)max;

		UpdateChaosBar();
	}
}

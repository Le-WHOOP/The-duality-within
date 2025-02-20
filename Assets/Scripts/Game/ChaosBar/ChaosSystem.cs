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
using System.Linq;

public class ChaosSystem : MonoBehaviour
{
	[SerializeField]
	public static ChaosSystem Instance;

    [SerializeField]
	private GameObject _NPCs;

    private List<InteractableChaos> _allChaosActions;

    // Associates a chaotic action with wether it was done
    private readonly Dictionary<InteractableChaos, bool> _chaosStatus = new();

	[SerializeField]
	private SpriteRenderer currentChaosBar;

	[SerializeField]
	private float chaosPoint = 0f;

	[SerializeField]
	private float maxChaosPoint = 100f;

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
		GetAllChaosActions();
        GetMaxChaos();
		UpdateChaosBar();
	}

	private void GetAllChaosActions()
	{
		_allChaosActions = _NPCs.GetComponentsInChildren<InteractableChaos>().ToList();
		Debug.Log(_allChaosActions.Count);
		// TODO: boxes 
		// TODO: shops
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
		currentChaosBar.transform.localScale = new Vector3(currentChaosBar.transform.localScale.x * ratio - currentChaosBar.transform.localScale.x, 0, 0);
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
			chaosPoint = chaosPoint > maxChaosPoint ? maxChaosPoint : chaosPoint;

			UpdateChaosBar();
			// TODO animation
		}
		if (chaosPoint >= maxChaosPoint)
			GameController.Instance.EndGame();
	}

	public void SetMaxChaos(float max)
	{
		maxChaosPoint = (int)max;

		UpdateChaosBar();
	}
}

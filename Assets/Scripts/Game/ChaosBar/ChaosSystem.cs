using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
	private Slider currentChaosBar;

	private float chaosPoint = 0f;

	private float maxChaosPoint = 0f;

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
				chaoticActionsNumber = 5;
				break;
			case Difficulty.Medium:
				chaoticActionsNumber = 10;
				break;
			case Difficulty.Hard:
				chaoticActionsNumber = 20;
				break;
		}

        // select actions randomly
        System.Random random = new();
        List<InteractableChaos> selectedChaos = new(_allChaosActions);
		selectedChaos.Sort((a, b) => random.Next(100) - random.Next(100)); // shuffle

		int i = 0;
		for (; i < chaoticActionsNumber; i++)
		{
			selectedChaos[i].transform.Find("Interactable").gameObject.SetActive(true);
			_chaosStatus.Add(selectedChaos[i], false);
			maxChaosPoint += selectedChaos[i].ChaosValue;
		}

		maxChaosPoint = (int)Math.Floor(maxChaosPoint * 0.8f);

		// deactivate the triggers of inactive chaotic actions
		for (; i < selectedChaos.Count; i++)
		{
			selectedChaos[i].GetComponents<Collider2D>().First(collider => collider.isTrigger).enabled = false;
		}
    }

    //==============================================================
    // Chaos Logic
    //==============================================================
    private void UpdateChaosBar()
	{
		float ratio = chaosPoint / maxChaosPoint;
		
		currentChaosBar.value = ratio;
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

			chaoticAction.transform.Find("Interactable").gameObject.SetActive(false);
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

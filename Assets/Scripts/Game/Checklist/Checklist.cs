using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checklist : MonoBehaviour
{
    public static Checklist Instance;

    [SerializeField]
    private List<InteractableIngredient> _allIngredients;

    // Associates an ingredient with wether it was collected
    private readonly Dictionary<InteractableIngredient, bool> _checklist = new ();

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Instance = this;
        // initialize the inventory according to the difficulty
        InitializeChecklist();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    /// <summary>
    /// fill the checklist randomly according to the difficulty
    /// </summary>
    private void InitializeChecklist()
    {
        // get the total number of non-alcohol ingredients according to the difficulty
        int totalIngredients = GetTotalIngredients();
        
        // separate all ingredients into 2 lists: alcohol and the rest
        List<InteractableIngredient> alcohols = _allIngredients.Where((ingredient) => ingredient.Type == InteractableIngredient.IngredientType.ALCOHOL).ToList();
        List<InteractableIngredient> otherIngredients = _allIngredients.Where((ingredient) => ingredient.Type != InteractableIngredient.IngredientType.ALCOHOL).ToList();
        
        // choose 1 alcohol randomly
        System.Random random = new();
        InteractableIngredient alcohol = alcohols[random.Next(alcohols.Count)];
        _checklist.Add(alcohol, false);

        // choose the ingredients
        otherIngredients.Sort((a, b) => random.Next(100) - random.Next(100)); // shuffle
        for (int i = 0; i < totalIngredients; i++)
        {
            _checklist.Add(otherIngredients[i], false);
        }
    }

    /// <summary>
    /// Determines the number of ingredients to collect according to the player's difficulty
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private int GetTotalIngredients()
    {
        Difficulty jekyllDifficulty = GameSettings.SwapRoles ? GameSettings.Player2Difficulty : GameSettings.Player1Difficulty;
        switch (jekyllDifficulty)
        {
            case Difficulty.Easy:
                return 3;
            case Difficulty.Medium:
                return 6;
            case Difficulty.Hard:
                return 11;
            default:
                throw new Exception("wtf is this difficulty?");
        }
    }

    public void Collect(InteractableIngredient ingredient)
    {
        if (ingredient == null || !_checklist.ContainsKey(ingredient))
        {
            throw new Exception("an invalid ingredient is being collected");
        }
        _checklist[ingredient] = true;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO handle display
    }
}

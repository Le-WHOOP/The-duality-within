using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
    public static Checklist Instance;

    [SerializeField]
    private List<InteractableIngredient> _allIngredients;

    // Associates an ingredient with wether it was collected
    private readonly Dictionary<InteractableIngredient, bool> _checklist = new ();

    private GameObject _inventory;
    [SerializeField]
    private GameObject _item;
    [SerializeField]
    private GameObject _textJekyll;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize the inventory according to the difficulty
        InitializeChecklist();

        _inventory = gameObject;

        foreach (InteractableIngredient item in _checklist.Keys)
        {
            GameObject newitem = Instantiate(_item);
            newitem.name = item.IngredientSprite.name;
            newitem.transform.parent = _inventory.transform;
            newitem.transform.localScale = new Vector3(1,1,1);
            newitem.transform.Find("Ingredient").gameObject.GetComponent<Image>().sprite = item.IngredientSprite;
        }
    }

    /// <summary>
    /// Fill the checklist randomly according to the difficulty
    /// </summary>
    private void InitializeChecklist()
    {
        // Get the total number of non-alcohol ingredients according to the difficulty
        int totalIngredients = GetTotalIngredients();

        // Separate all ingredients into 2 lists: alcohol and the rest
        List<InteractableIngredient> alcohols = _allIngredients.Where((ingredient) => ingredient.Type == InteractableIngredient.IngredientType.Alcohol).ToList();
        List<InteractableIngredient> otherIngredients = _allIngredients.Where((ingredient) => ingredient.Type != InteractableIngredient.IngredientType.Alcohol).ToList();

        // Choose 1 alcohol randomly
        System.Random random = new();
        InteractableIngredient alcohol = alcohols[random.Next(alcohols.Count)];
        _checklist.Add(alcohol, false);

        // choose the ingredients
        otherIngredients.Sort((a, b) => random.Next(100) - random.Next(100)); // shuffle
        for (int i = 0; i < totalIngredients; i++)
        {
            _checklist.Add(otherIngredients[i], false);
        }

        foreach (InteractableIngredient ingredient in _allIngredients)
        {
            ingredient.gameObject.SetActive(true);
            if (!_checklist.ContainsKey(ingredient))
                ingredient.SetEmpty();
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
                return 9;
            default:
                throw new Exception("wtf is this difficulty?");
        }
    }

    /// <summary>
    /// Changes the status of the ingredient
    /// </summary>
    /// <param name="ingredient"></param>
    /// <exception cref="Exception"></exception>
    public bool Collect(InteractableIngredient ingredient)
    {
        if (ingredient == null || !_checklist.ContainsKey(ingredient))
        {
            throw new Exception("an invalid ingredient is being collected");
        }

        if (!_checklist[ingredient])
        {
            _checklist[ingredient] = true;
            _inventory.transform.Find(ingredient.IngredientSprite.name).Find("Checkmark").gameObject.SetActive(true);

            if (IsChecklistComplete())
            {
                _textJekyll.SetActive(true);
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if all ingredients have been collected
    /// </summary>
    /// <returns></returns>
    public bool IsChecklistComplete()
    {
        return _checklist.Values.Where(collected => !collected).Count() == 0;
    }
}

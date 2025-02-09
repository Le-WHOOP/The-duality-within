using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Ingredient[] inventory_;
    private int inventorySize_;
    private int maxChaos_;
    private Player leftPlayer_;
    private Player rightPlayer_;

    public Game()
    {
        Initialize();
    }

    /*
     * Sets the values of base game variables according to GameSettings information
     */
    private void Initialize()
    {
        if (!GameSettings.SwapRoles) // jekyll is played by the left player
        {
            InitializeInventory(GameSettings.Player1Difficulty);
            SetMaxChaos(GameSettings.Player2Difficulty);
        }
        else // jekyll is played by the right player
        {
            InitializeInventory(GameSettings.Player2Difficulty);
            SetMaxChaos(GameSettings.Player1Difficulty);
        }

        // TODO choose randomly plants
        // TODO choose randomly the position of those plants
        // TODO choose randomly an alcohol
        // => use Enums to classify Ingredients ? Some non-alcohol ingredients cannot be found in nature how do we determine their position?
    }

    /*
     * The amount of chaos needed for Hyde to win depends on the Difficulty.
     */
    private void SetMaxChaos(Difficulty hydeDifficulty)
    {
        switch (hydeDifficulty) // TODO: replace placeholder values
        {
            case Difficulty.Easy:
                maxChaos_ = 30;
                break;
            case Difficulty.Medium:
                maxChaos_ = 60;
                break;
            case Difficulty.Hard:
                maxChaos_ = 120;
                break;
        }
    }

    /*
    * The amount of ingredients needed for Jekyll to win depends on the Difficulty.
    * Sets the inventory size and initializes the invertory.
    */
    private void InitializeInventory(Difficulty jekyllDifficulty)
    { 
        // change the size of the inventory according to the difficulty
        switch (jekyllDifficulty) // TODO: replace placeholder sizes
        {
            case Difficulty.Easy:
                inventorySize_ = 3;
                break;
            case Difficulty.Medium:
                inventorySize_ = 6;
                break;
            case Difficulty.Hard:
                inventorySize_ = 11;
                break;
        }

        inventory_ = new Ingredient[inventorySize_];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

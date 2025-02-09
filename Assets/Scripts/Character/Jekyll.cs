using UnityEngine;

public class Jekyll : Character
{
    private int inventoryIndex_ = 0;

    /*
     * Verifies if there is an ingredient to collect and adds said ingredient to the game inventory
     * If this is an interaction with the Alambic and inventoryIndex == inventorySize (in Game.cs), the game is finished
     * 
     * Returns wether the game is finished
     */
    public override bool ExecuteSpecialAction()
    {
        throw new System.NotImplementedException();
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

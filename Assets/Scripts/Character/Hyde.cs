using UnityEngine;

public class Hyde : Character
{
    private int chaos_ = 0;

    /*
     * Verifies if the bad action can be executed and changes the total chaos created.
     * If chaos >= maxChaos (present in Game.cs), the game is finished.
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

using UnityEngine;

public class Player : MonoBehaviour
{
    private Character character_;
    private bool isLeftPlayer_;
    private bool isPlaying_; // to know if key presses have to be taken into account
    //private key binding machin truc // TODO handle key presses

    public Player(Character character, bool isLeftPlayer)
    {
        character_ = character;
        isLeftPlayer_ = isLeftPlayer;
        isPlaying_ = character is Jekyll; // Jekyll starts
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * handle collisions with :
     * - Ingredients (Jekyll)
     * - npc (Hyde)
     * - places [ Apothicary, Drug Store ] + Laboratory (to enter) for Jekyll
     */
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityPlayerController : PlayerController
{
    public List<InteractableController> Interactions { get; } = new List<InteractableController>();

    public CityPlayerController() : base(Personnality.Jekyll) { }

    [Header("UI")]
    [SerializeField]
    private GameObject _chaosbar;
    [SerializeField]
    private GameObject _inventory;

    protected override void Start()
    {
        base.Start();
        _chaosbar.SetActive(_personnality == Personnality.Hyde);
        _inventory.SetActive(_personnality == Personnality.Jekyll);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction)
            // This interaction can only be used if available to the current personnality controlling the player
            && interaction.IsAvailableTo(_personnality))
            Interactions.Add(interaction);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction))
            Interactions.Remove(interaction);
    }

    public override void Move(float x, float y)
    {
        base.Move(x, y);

        // The player is trying to walk do set their new direction
        if (x != 0 || y != 0)
        {
            _activeAnimator.SetFloat("x", x);
            _activeAnimator.SetFloat("y", y);
            _activeAnimator.SetBool("isWalking", true);
        }
        // If x and y are 0: the player transitions to idle BUT still needs to face their original direction
        // If we put x and y at 0 then the player is always idling facing down
        else
        {
            _activeAnimator.SetBool("isWalking", false);
        }
    }

    /// <summary>
    /// Attempts to interact with a nearby interactable object, if one is within range.
    /// </summary>
    public void Interact()
    {
        // Since Unity overrides the null operator, we can't use the null propagation (? operator)
        InteractableController interaction = Interactions.FirstOrDefault();
        if (interaction != null) {
            interaction.Interact(this);
        }
    }

    public override void SwapPersonnality()
    {
        base.SwapPersonnality();
        _chaosbar.SetActive(_personnality == Personnality.Hyde);
        _inventory.SetActive(_personnality == Personnality.Jekyll);

        // Remove all interactions that are no longer accessible because of the personnality change
        // TODO If the player is already inside the trigger of an interaction that was for the other player when the
        // personnality swaps, this interaction will not be added
        foreach (InteractableController interaction in Interactions.Where(interaction => !interaction.IsAvailableTo(_personnality)).ToArray())
            Interactions.Remove(interaction);
    }


    /// <summary>
    /// returns the current personnality of the playerController
    /// </summary>
    public Personnality GetCurrentPersonnality()
    {
        return _personnality;
    }
}

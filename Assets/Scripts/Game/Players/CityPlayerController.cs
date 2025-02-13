using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityPlayerController : PlayerController
{
    private Personnality _personnality;

    public List<InteractableController> Interactions { get; } = new List<InteractableController>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction)
            // This interaction can only be used if TODO comment
            && interaction.IsAvailableTo(_personnality))
            Interactions.Add(interaction);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction))
            Interactions.Remove(interaction);
    }

    /// <summary>
    /// Attempts to interact with a nearby interactable object, if one is within range.
    /// </summary>
    public void Interact()
    {
        // Since Unity overrides the null operator, we can't use the null propagation (? operator)
        InteractableController interaction = Interactions.FirstOrDefault();
        if (interaction != null)
            interaction.Interact();
    }

    public void SwapPersonnality()
    {
        _personnality = _personnality == Personnality.Hyde ? Personnality.Jekyll : Personnality.Hyde;

        // Remove all interactions that are no longer accessible because of the personnality change
        // TODO If the player is already inside the trigger of an interaction that was for the other player when the
        // personnality swaps, this interaction will not be added
        foreach (InteractableController interaction in Interactions.Where(interaction => !interaction.IsAvailableTo(_personnality)))
            Interactions.Remove(interaction);
    }
}

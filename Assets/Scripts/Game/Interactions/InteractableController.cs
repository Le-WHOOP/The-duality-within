using UnityEngine;

public abstract class InteractableController : MonoBehaviour
{
    // Who can use this interaction ?
    private readonly Personnality availableTo;

    protected InteractableController(Personnality availableTo)
    {
        this.availableTo = availableTo;
    }

    /// <summary>
    /// Check if the given personnality can use this interaction
    /// </summary>
    /// <param name="personnality">The personnality</param>
    /// <returns><see langword="true"/> if this personnality can use this interaction. <see langword="false"/> otherwise</returns>
    public bool IsAvailableTo(Personnality personnality)
    {
        return availableTo.HasFlag(personnality);
    }

    /// <summary>
    /// Use the interaction
    /// </summary>
    public abstract void Interact(CityPlayerController player);
}

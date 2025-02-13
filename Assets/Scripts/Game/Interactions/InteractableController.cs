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
    /// TODO
    /// </summary>
    /// <param name="personnality"></param>
    /// <returns></returns>
    public bool IsAvailableTo(Personnality personnality)
    {
        return availableTo.HasFlag(personnality);
    }

    /// <summary>
    /// TODO
    /// </summary>
    public abstract void Interact();
}

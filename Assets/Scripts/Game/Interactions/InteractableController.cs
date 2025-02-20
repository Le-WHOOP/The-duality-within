using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public abstract class InteractableController : MonoBehaviour
{
    // Who can use this interaction ?
    private readonly Personnality availableTo;

    private AudioSource _audioSource;

    protected InteractableController(Personnality availableTo)
    {
        this.availableTo = availableTo;
    }

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
    public virtual void Interact(CityPlayerController player)
    {
        _audioSource.Play();
    }
}

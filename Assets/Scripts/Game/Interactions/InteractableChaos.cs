using UnityEngine;

public class InteractableChaos : InteractableController
{
    [SerializeField]
    private float _chaosValue;

    [SerializeField]
    [Tooltip("Leave empty if this item doesn't have a flammable interaction")]
    private GameObject _flameObject;

    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        ChaosSystem.Instance.RaiseChaos(_chaosValue);

        // Show fire animation
        _flameObject.SetActive(true);
        // Wait a bit and deactivate the GameObject
        Invoke("deactivateGameObject", 0.5f);
    }

    private void deactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}

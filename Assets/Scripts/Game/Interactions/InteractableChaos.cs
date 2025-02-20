using UnityEngine;

public class InteractableChaos : InteractableController
{
    public float ChaosValue = 1f;

    [SerializeField]
    [Tooltip("Leave empty if this item doesn't have a flammable interaction")]
    private GameObject _flameObject;

    public InteractableChaos() : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        ChaosSystem.Instance.RaiseChaos(this);

        if (_flameObject != null)
        {
            // Show fire animation
            _flameObject.SetActive(true);
            // Wait a bit and deactivate the GameObject
            Invoke(nameof(DeactivateGameObject), 0.5f);
        }
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}

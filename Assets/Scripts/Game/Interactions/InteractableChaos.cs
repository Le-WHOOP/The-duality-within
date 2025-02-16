using UnityEngine;

public class InteractableChaos : InteractableController
{
    [SerializeField]
    private float _chaosValue;

    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        ChaosSystem.Instance.RaiseChaos(_chaosValue);
    }
}

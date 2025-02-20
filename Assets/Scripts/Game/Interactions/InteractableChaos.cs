using UnityEngine;

public class InteractableChaos : InteractableController
{
    
    public float ChaosValue = 1f;

    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        ChaosSystem.Instance.RaiseChaos(this);
    }
}

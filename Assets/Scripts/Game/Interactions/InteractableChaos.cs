using UnityEngine;

public class InteractableChaos : InteractableController
{
    public float ChaosValue;

    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        if (player.GetCurrentPersonnality() == Personnality.Hyde)
        {
            ChaosSystem.Instance.RaiseChaos(this);
        }
    }
}

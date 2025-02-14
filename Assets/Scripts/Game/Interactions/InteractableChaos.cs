public class InteractableChaos : InteractableController
{
    public InteractableChaos(Personnality availableTo) : base(Personnality.Hyde) { }

    public override void Interact(CityPlayerController player)
    {
        // TODO Add chaos to the chaos bar
        throw new System.NotImplementedException();
    }
}

public class InteractableBrewingStant : InteractableController
{
    public InteractableBrewingStant(Personnality availableTo) : base(Personnality.Jekyll) { }

    public override void Interact()
    {
        if (Checklist.Instance.IsChecklistComplete())
        {
            // TODO Finish game
        }
    }
}

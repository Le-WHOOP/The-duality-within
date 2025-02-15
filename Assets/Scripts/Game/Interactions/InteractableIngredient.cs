public class InteractableIngredient : InteractableController
{
    public InteractableIngredient(Personnality availableTo) : base(Personnality.Jekyll) { }

    public override void Interact()
    {
        // TODO Pick up the ingredient
        throw new System.NotImplementedException();
    }
}

using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private MenuController menuController;

    void Start()
    {
        menuController = GetComponentInParent<MenuController>();
    }
}

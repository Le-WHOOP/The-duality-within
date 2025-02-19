using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    [SerializeField]
    private FadeScene fadeScene;

    public void RestartButton() 
    {
        StartCoroutine(fadeScene.LoadScene("GameScene"));
    }

    public void MenuButton()
    {
        StartCoroutine(fadeScene.LoadScene("MenuScene"));
    }
}

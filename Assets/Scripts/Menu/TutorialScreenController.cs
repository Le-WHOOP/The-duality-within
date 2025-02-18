using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreenController : ScreenController
{
    [Header("Player 1")]
    [SerializeField]
    private TextMeshProUGUI p1NameText;

    [SerializeField]
    private TextMeshProUGUI p1Description;

    [Header("Player 2")]
    [SerializeField]
    private TextMeshProUGUI p2NameText;

    [SerializeField]
    private TextMeshProUGUI p2Description;

    [Header("Scene")]
    [SerializeField]
    private FadeScene fadeScene;

    public void StartGame()
    {
        StartCoroutine(fadeScene.LoadScene("GameScene"));
    }

    void OnEnable()
    {
        if (GameSettings.SwapRoles) {
            // Swaping Jekyll & Hyde and the image
            string tmpName = p1NameText.text;
            string tmpDesc = p1Description.text;

            p1NameText.text = p2NameText.text;
            p2NameText.text = tmpName;

            p1Description.text = p2Description.text;
            p2Description.text = tmpDesc;
        }
    }
}
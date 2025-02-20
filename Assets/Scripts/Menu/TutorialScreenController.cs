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

    [SerializeField]
    private Image p1Sprite;

    [Header("Player 2")]
    [SerializeField]
    private TextMeshProUGUI p2NameText;

    [SerializeField]
    private TextMeshProUGUI p2Description;

    [SerializeField]
    private Image p2Sprite;

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
            // Swaping Jekyll & Hyde
            string tmpName = p1NameText.text;
            string tmpDesc = p1Description.text;
            Sprite tmpSprite = p1Sprite.sprite;

            p1NameText.text = p2NameText.text;
            p2NameText.text = tmpName;

            p1Description.text = p2Description.text;
            p2Description.text = tmpDesc;

            p1Sprite.sprite = p2Sprite.sprite;
            p2Sprite.sprite = tmpSprite;
        }
    }
}
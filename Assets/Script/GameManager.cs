using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Data")]
    public int maxGifts = 5;
    public int stolenGifts = 0;
    public int santaPoints = 0;

    [Header("UI Elements")]
    public RawImage[] giftIcons;
    public TextMeshProUGUI pointsText;
    public GameObject gameOverPanel;

    [Header("Santa Animator")]
    public Animator santaAnimator; // assign Santa's Animator here

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM();
    }

    public void GiftStolen()
    {
        stolenGifts = Mathf.Min(stolenGifts + 1, maxGifts);
        UpdateUI();

        if (stolenGifts >= maxGifts)
        {
            GameOver();
        }
    }

    public void AddPoint(int points = 1)
    {
        santaPoints += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < giftIcons.Length; i++)
        {
            if (giftIcons[i] != null)
                giftIcons[i].enabled = (i < (maxGifts - stolenGifts));
        }

        if (pointsText != null)
            pointsText.text = "Points: " + santaPoints;
    }

    private void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [Header("Points")]
    public TMP_Text pointsText;
    public PlayfabManager PlayfabManager;

    [Header("Leaderboard and Game Over Screen Switch")]
    public GameObject GameOver;
    public GameObject Leaderboard;

    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton() {
        SceneManager.LoadScene("WaveAttack");
    }

    public void LeaderboardButton() {
        PlayfabManager.GetLeaderboard();
        GameOver.SetActive(false);
        Leaderboard.SetActive(true);
    }

    public void BackButton() {
        GameOver.SetActive(true);
        Leaderboard.SetActive(false);
    }
}

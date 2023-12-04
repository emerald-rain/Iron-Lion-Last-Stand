using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;

    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton() {
        SceneManager.LoadScene("WaveAttack");
    }

    public void LeaderboardButton() {

    }
}

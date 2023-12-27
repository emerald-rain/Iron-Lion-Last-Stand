using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to UI components
    [Header("GameOver Screen")]
    public GameOverScreen GameOverScreen;
    public ScoreManager ScoreManager;

    // Manages interactions with Playfab for score handling
    [Header("Playfab Score Sending")]
    public PlayfabManager PlayfabManager;

    // Controls the playback of the game over video
    [Header("Play GameOver Video")]
    public VidPlayer vidPlayer;

    // Hides bullets during the GameOver screen
    [Header("Hide Bullets on GameOver Screen")]
    public Transform reloadSystem;

    // Manages background soundtracks
    [Header("Background Soundtracks")]
    public SoundEffectsPlayer soundEffectsPlayer;

    void Start()
    {
        // Start playing background music
        soundEffectsPlayer.PlayInRow();
    }

    public void GameOver()
    { 
        // Hide ammo and play a loss video
        reloadSystem.gameObject.SetActive(false);
        vidPlayer.PlayVideo();

        // Get the final score and update the GameOver screen
        int score = ScoreManager.GetScore();
        GameOverScreen.Setup(score);

        // Send the player's score to Playfab leaderboard
        PlayfabManager.SendLeaderboard(score); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {
    /// <summary>
    /// Manages game play of the game
    /// </summary>

    public static GamePlayManager instance = null;

    public GameObject player;
    public GameObject asteroidSprite;
    public GameObject bulletSprite;

    private List<GameObject> asteroids = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();

    private float asteroidsRandomChange = .03f;  // Chance of spawning an asteroid

    public Text playerScoreText;
    private int playerScore = 0;

    public Text timeLeftText;
    private float timeLeft = 100;

    private float lastupdate = 0;

    private void Awake() {
        if (instance == null) { instance = this; } 
        else { Destroy(gameObject); }
    }

    private void Update() {
        playerScoreText.text = "Score: " + playerScore; // Updates score
        timeLeftText.text = "Time Left: " + timeLeft; // Updates score
    }

    private void FixedUpdate() {
        if (lastupdate == 10) {
            timeLeft -= 1f;
            if (timeLeft == 0) {
                Restart();
            }
            lastupdate = 0;
        }
        if (Random.value <= asteroidsRandomChange) { // Apple is being made
            CreateAsteroid();
        }
        lastupdate++;
    }

    private void Restart() {                        // Restart Game
        SceneChanger.instance.ChangeScene("Game Over"); // Change Scene
        HighScoreManager.instance.NewHighScore(playerScore); // Update High Scores

        for (int i = 0; i < asteroids.Count; i++) {
            Destroy(asteroids [i]);             // Destroy all apple objects
        }
        asteroids.Clear();                     // Clear list

        for (int i = 0; i < bullets.Count; i++) {
            Destroy(bullets [i]);             // Destroy all apple objects
        }
        bullets.Clear();                     // Clear list

        playerScore = 0;                    // Reset score
        lastupdate = 0;
        player.transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));

    }

    public void PlayerHit() { 
        SFXManager.instance.SoundEffectPlay();        // Play sound effect
        Restart();                    // Restart game
    }

    public void PlayerScored(GameObject asteroid) {
        SFXManager.instance.SoundEffectPlay();        // Play sound effect
        playerScore += 10;
        RemoveAsteroid(asteroid);
    }

    public void RemoveAsteroid(GameObject asteroid) {
        asteroids.Remove(asteroid);            // Remove apple from list
        Destroy(asteroid);             // Destroy all apple objects
    }

    public void CreateAsteroid() {
        asteroids.Add(Asteroid.Create(asteroidSprite));
    }

    public void CreateBullet(GameObject player) {
        bullets.Add(Bullet.Create(bulletSprite, player));
    }

}

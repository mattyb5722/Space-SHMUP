using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class GamePlayManager : MonoBehaviour {
    /// <summary>
    /// Manages game play of the game
    /// </summary>

    public static GamePlayManager instance = null;

    public GameObject player;
    public GameObject singleAsteroidSprite;
    public GameObject multiAsteroidSprite;
    public GameObject bulletSprite;
    public GameObject laserSprite;
    public GameObject laserUpgradeSprite;

    private List<Enemy> enemies = new List<Enemy>();
    private List<Bullet> bullets = new List<Bullet>();
    private List<GameObject> upgrades = new List<GameObject>();

    private float asteroidsRandomChange = .03f;     // Chance of spawning an asteroid

    public Text playerScoreText;
    private int playerScore = 0;

    public Text timeLeftText;
    private float timeLeft = 60;                    // Seconds left in the game

    private float lastUpdate = 0;                   // Time since last counter decrease

    private void Awake() {
        if (instance == null) { instance = this; } 
        else { Destroy(gameObject); }
    }

    private void Update() {
        playerScoreText.text = "Score: " + playerScore; // Updates score
        timeLeftText.text = "Time Left: " + timeLeft; // Updates score
    }

    private void FixedUpdate() {
        if (lastUpdate == 50) {                     // Happens once per secound
            timeLeft -= 1f;                         // Decrease game counter
            if (timeLeft == 0) {                    // If the game is over
                Restart();
            }
            lastUpdate = 0;                         // Reset the once per secound counter
        }
        if (Random.value <= asteroidsRandomChange) { // Spawn a standard asteroid
            CreateSingleAsteroid();
        }
        if (lastUpdate == 0 && timeLeft % 10 == 0) { // Spawn a multi asteroid
            CreateMultiAsteroid();
        }
        if (lastUpdate == 0 && timeLeft % 20 == 0) { // Spawn a laser upgrade
            CreateLaserUpgrade();
        }

        foreach (Bullet temp in bullets.ToArray()) { // Move bullets
            temp.Move();
        }
        foreach (Enemy temp in enemies.ToArray()) { // Move enemies
            temp.Move();
        }

        lastUpdate++;                               // Update the once per secound counter
    }

    // Restart the game
    private void Restart() {
        SceneChanger.instance.ChangeScene("Game Over"); // Change Scene
        HighScoreManager.instance.NewHighScore(playerScore); // Update High Scores
        
        for (int i = 0; i < enemies.Count; i++) {   // Fore each enemy
            Destroy(enemies [i].thisObject);        // Destroy enemy
        }
        enemies = new List<Enemy>();                // Clear list

        for (int i = 0; i < bullets.Count; i++) {   // For each bullet
            Destroy(bullets [i].thisObject);        // Destroy bullet
        }
        bullets = new List<Bullet>();               // Clear list

        for (int i = 0; i < upgrades.Count; i++) {  // For each upgrade pack
            Destroy(upgrades [i]);                  // Destroy upgrade pack
        }
        upgrades = new List<GameObject>();          // Clear list

        playerScore = 0;                            // Reset score
        lastUpdate = 0;                             // Reset timer
        player.transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));

    }

    // When the player is hit
    public void PlayerHit() {
        SFXManager.instance.SoundEffectPlay();      // Play sound effect
        Restart();                                  // Restart game
    }

    // When the player scores
    public void PlayerScored() {
        SFXManager.instance.SoundEffectPlay();      // Play sound effect
        playerScore += 10;                          // Incroment score
    }

    // Remove a buller from the game
    public void RemoveBullet(Bullet bullet) {
        bullets.Remove(bullet);                     // Remove bullet from list
        Destroy(bullet.thisObject);                 // Destroy bullet
    }

    // Spawn Standard enemy with a random position around the boarder
    public void CreateSingleAsteroid() {
        enemies.Add(new SingleAsteroid(singleAsteroidSprite));
    }

    // Spawn Standard enemy with a certain position
    public void CreateSingleAsteroid(Vector3 position) {  
        enemies.Add(new SingleAsteroid(singleAsteroidSprite, position));
    }

    // Spawn a multi enemy with a random position around the boarder
    public void CreateMultiAsteroid() {
        enemies.Add(new MultiAsteroid(multiAsteroidSprite));
    }

    // Spawn a single bullet
    public void SingleShot(GameObject player) {
        Bullet temp = new SingleShot(bulletSprite, player);
        bullets.Add(temp);
    }

    // Spawn a laser
    public void LaserShot(GameObject player) {
        Bullet temp = new Laser(laserSprite, player);
        bullets.Add(temp);
    }

    // Spawn a laser upgrade pack
    public void CreateLaserUpgrade() {
        upgrades.Add(LaserUpgrade.instance.Create(laserUpgradeSprite));
    }
}

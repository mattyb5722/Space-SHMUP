using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {

    /// <summary>
    /// This class handles collisions for asteroids 
    /// </summary>

    public GameObject explosion;                            // Explosion particle affect

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {         // Hit by a standard bullet
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play(); // Play explosion sound Effect
            Destroy(collision.gameObject);                  // Destroy bullet

            if (gameObject.tag == "Single Asteroid") {      // Asteroid is a standard asteroid
                GamePlayManager.instance.PlayerScored();    // Incroment score
                Destroy(gameObject);                        // Destroy asteroid
            } else if (gameObject.tag == "Multi Asteroid") { // Asteroid is a multi asteroid
                GamePlayManager.instance.PlayerScored();    // Incorment score
                Destroy(gameObject);                        // Destory asteroid
                for (int i = 0; i < 5; i++) {               // Spawn 5 standard asteroids
                    GamePlayManager.instance.CreateSingleAsteroid(gameObject.transform.position);
                }
            }
        } else if (collision.gameObject.tag == "Laser") {   // Hit by a laser
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play(); // Play explosion sound Effect

            if (gameObject.tag == "Single Asteroid") {      // Asteroid is a standard asteroid
                GamePlayManager.instance.PlayerScored();    // Incroment score
                Destroy(gameObject);                        // Destroy asteroid
            } else if (gameObject.tag == "Multi Asteroid") { // Asteroid is a multi asteroid
                GamePlayManager.instance.PlayerScored();    // Incorment score
                Destroy(gameObject);                        // Destory asteroid
                for (int i = 0; i < 5; i++) {               // Spawn 5 standard asteroids
                    GamePlayManager.instance.CreateSingleAsteroid(gameObject.transform.position);
                }
            }
        } else if (collision.gameObject.tag == "Player") {  // Hit the Player
            GamePlayManager.instance.PlayerHit();           // Game Over
        }
    }
}

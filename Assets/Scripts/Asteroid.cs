using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    private static float boarderX = 8f; // Edge of the screen
    private static float boarderY = 4f; // Edge of the screen

    public GameObject explosion;

    public static GameObject Create(GameObject sprite) {
        GameObject temp = Instantiate(sprite);              // Make new asteroid

        float scale = Random.Range(.25f, .75f);             // Change scale of asteroid
        temp.transform.localScale = new Vector3(scale, scale, scale);
        float side = Random.value;                          // Where the asteroid spawns
        float rotation = 0;

        if (side > .75) {                                   // Top of the Sceen
            float location = Random.Range(-1 * boarderX, boarderX);
            temp.transform.position = new Vector3(location, -1 * boarderY, 0f);
            rotation = Random.Range(-90f, 90f);
        } else if (side > .5) {                             // Bottom of the Screen
            float location = Random.Range(-1 * boarderX, boarderX);
            temp.transform.position = new Vector3(location, boarderY, 0f);
            rotation = Random.Range(90f, 270f);
        } else if (side > .25) {                            // Right side of the Screen
            float location = Random.Range(-1 * boarderY, boarderY);
            temp.transform.position = new Vector3(-1 * boarderX, location, 0f);
            rotation = Random.Range(-180f, 0f);
        } else {                                            // Left side of the Screen
            float location = Random.Range(-1 * boarderY, boarderY);
            temp.transform.position = new Vector3(boarderX, location, 0f);
            rotation = Random.Range(0, 180f);
        }
        temp.transform.Rotate(Vector3.forward, rotation, Space.World);
        temp.SetActive(true);               // Make the asteroid visible
        return temp;
    }

    private void FixedUpdate() {
        transform.position += transform.up * .01f;

        if (transform.position.x < -1 * boarderX) {         // Left border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.x > boarderX) {       // Right border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.y < -1 * boarderY) {  // Bottom border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.y > boarderY) {       // Top border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {         // Hit by a bullet
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play(); // Play explosion sound Effect
            Destroy(collision.gameObject);                  // Destroy Asteroid
            GamePlayManager.instance.PlayerScored(this.gameObject);
        } else if (collision.gameObject.tag == "Player") {  // Hit the Player
            GamePlayManager.instance.PlayerHit();           // Game Over
        }
    }
}

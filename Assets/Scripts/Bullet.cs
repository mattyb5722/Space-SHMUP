using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private static float boarderX = 8f; // Edge of the screen
    private static float boarderY = 5f; // Edge of the screen

    public static GameObject Create(GameObject sprite, GameObject player) {
        GameObject temp = Instantiate(sprite);          // Make new Bullet
        // Set Bullet's position and rotation to Player's
        temp.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        temp.SetActive(true);                           // Make the Bullet visible
        return temp;
    }

    private void FixedUpdate() {
        transform.position += transform.up * .1f;

        if (transform.position.x < -1 * boarderX) {      // Left border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.x > boarderX) {   // Right border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.y < -1 * boarderY) { // Bottom border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        } else if (transform.position.y > boarderY) {   // Top border of the screen
            GamePlayManager.instance.RemoveAsteroid(this.gameObject);
        }
    }
}

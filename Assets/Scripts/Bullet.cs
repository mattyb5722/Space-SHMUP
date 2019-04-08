using UnityEngine;

namespace Assets.Scripts {
    public abstract class Bullet : MonoBehaviour {
        /// <summary>
        /// This class is a bullet object class. This class contains the speed of the bullet, 
        /// the screen bounds of the bullet and the gameobject assosiated with each bullet.
        /// </summary>

        public float boarderX;     // Edge of the screen
        public float boarderY;     // Edge of the screen

        public GameObject thisObject;   // Game Object of the Enemy

        public float speed;             // Speed of the Enemy

        public void Move() {
            if (thisObject != null) {
                thisObject.transform.position += thisObject.transform.up * speed;

                if (thisObject.transform.position.x < -1 * boarderX) {      // Left border of the screen
                    GamePlayManager.instance.RemoveBullet(this);
                } else if (thisObject.transform.position.x > boarderX) {    // Right border of the screen
                    GamePlayManager.instance.RemoveBullet(this);
                } else if (thisObject.transform.position.y < -1 * boarderY) { // Bottom border of the screen
                    GamePlayManager.instance.RemoveBullet(this);
                } else if (thisObject.transform.position.y > boarderY) {    // Top border of the screen
                    GamePlayManager.instance.RemoveBullet(this);
                }
            }
        }
    }

    public class SingleShot : Bullet {

        public SingleShot(GameObject sprite, GameObject player) {
            boarderX = 8f;                                  // Set bound size
            boarderY = 5f;                                  // Set bound size

            speed = .1f;                                    // Set speed
            thisObject = Instantiate(sprite);               // Make new bullet

            // Set Bullet's position and rotation to Player's
            thisObject.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);

            thisObject.SetActive(true);                     // Make the bullet visible
        }
    }

    public class Laser : Bullet {

        public Laser(GameObject sprite, GameObject player) {
            boarderX = 13f;                                 // Set bound size
            boarderY = 10f;                                 // Set bound size

            speed = .5f;                                    // Set speed
            thisObject = Instantiate(sprite);               // Make new laser

            // Set Bullet's position and rotation to Player's
            thisObject.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
            thisObject.transform.position += thisObject.transform.up * 5;

            thisObject.SetActive(true);                     // Make the laser visible
        }
    }
}
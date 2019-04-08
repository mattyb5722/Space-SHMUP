using UnityEngine;

namespace Assets.Scripts {

    public abstract class Enemy : MonoBehaviour {

        /// <summary>
        /// This class is an enemy object class. This class contains the speed of the enemy, 
        /// the screen bounds of the enemies and the gameobject assosiated with each enemy.
        /// </summary>

        public float boarderX = 8f;     // Edge of the screen
        public float boarderY = 4f;     // Edge of the screen

        public GameObject thisObject;   // Game Object of the Enemy

        public float speed;             // Speed of the Enemy

        public void Move() {            // Movement for a standard aseroid
            if (thisObject != null) {
                thisObject.transform.position += thisObject.transform.up * speed;   // Change location

                // Destroy asteroid if it is off the screen
                if (thisObject.transform.position.x < -1 * boarderX) {         // Left border of the screen
                    Destroy(thisObject);
                } else if (thisObject.transform.position.x > boarderX) {       // Right border of the screen
                    Destroy(thisObject);
                } else if (thisObject.transform.position.y < -1 * boarderY) {  // Bottom border of the screen
                    Destroy(thisObject);
                } else if (thisObject.transform.position.y > boarderY) {       // Top border of the screen
                    Destroy(thisObject);
                }
            }
        }

        // Set Location and Position
        public void RandomLoactionAndRotation() {
            float side = Random.value;                      // Where the asteroid spawns
            float rotation = 0;

            if (side > .75) {                               // Top of the Sceen
                float location = Random.Range(-1 * boarderX, boarderX);
                thisObject.transform.position = new Vector3(location, -1 * boarderY, 0f);
                rotation = Random.Range(-90f, 90f);
            } else if (side > .5) {                         // Bottom of the Screen
                float location = Random.Range(-1 * boarderX, boarderX);
                thisObject.transform.position = new Vector3(location, boarderY, 0f);
                rotation = Random.Range(90f, 270f);
            } else if (side > .25) {                        // Right side of the Screen
                float location = Random.Range(-1 * boarderY, boarderY);
                thisObject.transform.position = new Vector3(-1 * boarderX, location, 0f);
                rotation = Random.Range(-180f, 0f);
            } else {                                        // Left side of the Screen
                float location = Random.Range(-1 * boarderY, boarderY);
                thisObject.transform.position = new Vector3(boarderX, location, 0f);
                rotation = Random.Range(0, 180f);
            }
            thisObject.transform.Rotate(Vector3.forward, rotation, Space.World);
        }

    }

    // Standard Single Asteroid
    public class SingleAsteroid : Enemy {

        // Constructor not give a location to place the Asteroid
        public SingleAsteroid(GameObject sprite) {
            speed = .01f;                                   // Set Speed
            thisObject = Instantiate(sprite);               // Make new asteroid

            float scale = Random.Range(.25f, .5f);          // Change scale of asteroid
            thisObject.transform.localScale = new Vector3(scale, scale, scale);
            RandomLoactionAndRotation();                    // Set Location and Position

            thisObject.SetActive(true);                     // Make the asteroid visible
        }

        // Constructor given a location for the asteroid
        public SingleAsteroid(GameObject sprite, Vector3 position) {
            speed = .01f;                                   // Set speed of new asteroid
            thisObject = Instantiate(sprite);               // Make new asteroid

            float scale = Random.Range(.25f, .5f);          // Change scale of asteroid
            thisObject.transform.localScale = new Vector3(scale, scale, scale);
            thisObject.transform.position = position;       // Set position of new asteroid
            thisObject.transform.Rotate(Vector3.forward, Random.Range(0f, 360f), Space.World);

            thisObject.SetActive(true);                     // Make the asteroid visible
        }
    }

    // Big asteroid that breaks into smaller asteroid when destroyed
    public class MultiAsteroid : Enemy {
        public MultiAsteroid(GameObject sprite) {
            speed = .01f;                                   // Set speed of new asteroid
            thisObject = Instantiate(sprite);               // Make new asteroid

            thisObject.transform.localScale = new Vector3(.5f, .5f, .5f);
            RandomLoactionAndRotation();                    // Set Location and Position

            thisObject.SetActive(true);                     // Make the asteroid visible
        }
    }
}

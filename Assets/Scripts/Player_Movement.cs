using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    /// <summary>
    /// Manages the player's movement
    /// </summary>
    private float boarderX = 7.25f; // Edge of the screen
    private float boarderY = 4.25f; // Edge of the screen

    private int fireRate = 50;
    private int lastfire;

    private ParticleSystem leftEngine;
    private ParticleSystem rightEngine;

    private Rigidbody2D body;

    private void Awake() {
        leftEngine = GetComponentsInChildren<ParticleSystem>() [0];
        rightEngine = GetComponentsInChildren<ParticleSystem>() [1];
        lastfire = fireRate;
        body = GetComponent<Rigidbody2D>();
}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)){       // Moving forward
            leftEngine.Play();
            rightEngine.Play();
            body.AddForce(transform.up * 1.5f);
        }
        if (Input.GetKeyUp(KeyCode.W)){
            leftEngine.Stop();
            rightEngine.Stop();
        }

        if (Input.GetKey(KeyCode.S)){         // Moving backwards
            leftEngine.Play();
            rightEngine.Play();
            body.AddForce(transform.up * -1.5f);
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            leftEngine.Stop();
            rightEngine.Stop();
        }

        if (Input.GetKey(KeyCode.A)) {          // Rotate clockwise
            rightEngine.Play();
            transform.Rotate(Vector3.forward, 5f, Space.World);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            rightEngine.Stop();
        }

        if (Input.GetKey(KeyCode.D)) {          // Rotate counter clockwise
            leftEngine.Play();
            transform.Rotate(Vector3.forward, -5f, Space.World);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            leftEngine.Stop();
        }

        if (Input.GetKey(KeyCode.Space)) {      // Shoot
            if (lastfire > fireRate) {
                GamePlayManager.instance.CreateBullet(gameObject);
                lastfire = 0;
            }
        }
        lastfire++;

        if (transform.position.x < -1* boarderX) {      // Left border of the screen
            transform.position = new Vector3(-1* boarderX, transform.position.y, 0f);
        } else if (transform.position.x > boarderX) {   // Right border of the screen
            transform.position = new Vector3(boarderX, transform.position.y, 0f);
        }
        if (transform.position.y < -1 * boarderY) {      // Bottom border of the screen
            transform.position = new Vector3(transform.position.x, -1 * boarderY, 0f);
        } else if (transform.position.y > boarderY) {   // Top border of the screen
            transform.position = new Vector3(transform.position.x, boarderY, 0f);
        }
    }
}

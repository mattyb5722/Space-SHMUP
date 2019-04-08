using UnityEngine;

public class LaserUpgrade : MonoBehaviour {

    public static LaserUpgrade instance = null;

    private float boarderX = 7f; // Edge of the screen
    private float boarderY = 4f; // Edge of the screen

    private void Awake() {
        if (instance == null) { instance = this; } 
        else { Destroy(gameObject); }
    }

    public GameObject Create(GameObject laserUpgradeSprite) {
        float x = Random.Range(-1* boarderX, boarderX);
        float y = Random.Range(-1* boarderY, boarderY);

        GameObject temp = Instantiate(laserUpgradeSprite);

        temp.transform.position = new Vector3(x, y, 0);
        temp.SetActive(true);               // Make the asteroid visible
        return temp;
    }
}

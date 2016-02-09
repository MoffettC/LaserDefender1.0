using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 0.7f;
    public float spawnDelay = 2f;

    private bool movingRight = false;
    //private bool movingLeft = false;
    private float xmax;
    private float xmin;

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(-.05f, 0f, distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1.08f, 0f, distanceToCamera));
        xmax = rightEdge.x;
        xmin = leftEdge.x;

        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
        //SpawnUntilFull();
	}
	
    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

	// Update is called once per frame
	void Update () {
	    if (movingRight){
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        } else {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        float rightEdgeOfFormation = (float)(transform.position.x + (0.5 * width));
        float leftEdgeOfFormation = (float) (transform.position.x - (0.5 * width));
        if (leftEdgeOfFormation <= xmin) {
            movingRight = true;
        } else if (rightEdgeOfFormation >= xmax) {
            movingRight = false;
        }

        if (AllMembersDead()) {
            SpawnUntilFull();
        }
    }

    bool AllMembersDead() {
        foreach(Transform childPositionGameObject in transform) {
           if (childPositionGameObject.childCount > 0) {
                return false;
            }
        }
        return true;
    }

    //void SpawnEnemies() {
    //    foreach (Transform child in transform) {
    //        GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
    //        enemy.transform.parent = child;
    //    }
    //}

    void SpawnUntilFull() {
        Transform freePosition = NextFreePosition(); //assigning free position to a variable
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    Transform NextFreePosition() {
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount == 0) {
                return childPositionGameObject;
            }
        }
        return null;
    }
}

using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

    public GameObject enemyLaser;
    public float health = 150;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public float projectileSpeed = 10f;

    public AudioClip guns;
    public AudioClip death;

    private ScoreKeeper scoreKeeper;

    void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void Update() {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability) {
            Fire();
        }
    }

    void Fire() {
        //Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
        GameObject missile = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f);
        AudioSource.PlayClipAtPoint(guns, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.getDamage();
            missile.Hit();
            if (health <= 0) {
                AudioSource.PlayClipAtPoint(death, transform.position);
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
            }
        }
    }


}

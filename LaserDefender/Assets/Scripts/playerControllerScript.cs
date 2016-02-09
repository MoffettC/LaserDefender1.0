using UnityEngine;
using System.Collections;

public class playerControllerScript : MonoBehaviour {

    public GameObject playerLaser;
    public float speed = 5.0f;
    public float padding = 1f;
    public float fireRate = 1f;
    public float projectileSpeed = 100.0f;
    public float health = 100f;

    public AudioClip guns;
    public AudioClip death;

    float xmin;
    float xmax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
    }
	
    void Fire() {
        Vector3 offset = new Vector3(0, 1f, 0);
        GameObject beam = Instantiate(playerLaser, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
        AudioSource.PlayClipAtPoint(guns, transform.position);
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.LeftArrow)){
            //transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f); duplicate
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f); duplicate
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax); //restrict player to gamespace
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);


        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        } 
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Fire");
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.getDamage();
            missile.Hit();
            if (health <= 0) {
                AudioSource.PlayClipAtPoint(death, transform.position);
                Destroy(gameObject);
                LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                man.LoadLevel("Win");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletController : MonoBehaviour
{

    private Rigidbody2D rb;
    public PlayerController pc;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Shoot(Vector2 direction, float force) {
        rb.AddForce(direction * force);
    }
    
    public void Break(float time) {
        Destroy(gameObject, time);
    }

    void OnTriggerEnter2D(Collider2D other) { // for enemy bullet
        if(gameObject.tag == "enemybullet" && other.gameObject.tag == "Player") {
            Destroy(other.gameObject); //kill player
            Destroy(gameObject); // die
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnCollisionEnter2D(Collision2D other) { // player bullet
        if (other.gameObject.tag == "Enemy") {
            pc.AddScore();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerController : MonoBehaviour
{
    public PlayerController pc;

    private void Awake() {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D other) { // on enemy collision with trigger, go to GameOver
        if (other.gameObject.tag == "Enemy") {
            pc.RemoveHealth(other.gameObject);
        }
    }
}

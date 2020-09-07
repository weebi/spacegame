using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) { // on enemy collision with trigger, go to GameOver
        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject); // die
            SceneManager.LoadScene("GameOver");
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveInputHor;
    public float moveInputVer;
    public float speed;
    private Rigidbody2D rb;
    public GameObject laserPrefab;
    private Text txtScore;
    public int score;
    private Vector2 bottomLeft;
    private Vector2 topRight;
    private float height;
    private int highscore;
    
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        txtScore = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        height = transform.localScale.y;

        if(PlayerPrefs.GetInt("highscore") <= 0) {
            PlayerPrefs.SetInt("highscore", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // oikea nuoli = 1, vasen nuoli = 1
        moveInputHor = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(rb.velocity.x, moveInputVer * speed); // do stuff

        // oikea nuoli = 1, vasen nuoli = 1
        moveInputVer = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveInputHor * speed, rb.velocity.y); // do stuff

        // change to .GetKey() for speeed
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot(); // pew
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.03f, 0.97f);
        pos.y = Mathf.Clamp(pos.y, 0.03f, 0.97f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, rb.position + Vector2.up * 0.25f, Quaternion.identity);
        BulletController projectile = laser.GetComponent<BulletController>();
        projectile.Shoot(new Vector2(0,1), 250);
        projectile.Break(2);
    }

    public void AddScore()
    {
        this.score += 1;
        this.txtScore.text = "Score: " + score;
        
        PlayerPrefs.SetInt("score", this.score);
        highscore = PlayerPrefs.GetInt("highscore");
        if(score > highscore) {
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    void OnCollisionEnter2D(Collision2D other) { // on collision with enemy
        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject); //kill enemy
            Destroy(gameObject); // die
            SceneManager.LoadScene("GameOver");
        }
    }
}

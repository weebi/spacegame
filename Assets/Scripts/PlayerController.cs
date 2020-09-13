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
    private Text txtLives;
    public int score;
    private Vector2 bottomLeft;
    private Vector2 topRight;
    private float height;
    private int highscore;
    public int lives;
    private bool cheat;
    
    // Start is called before the first frame update
    void Start()
    {
        cheat = false;
        QualitySettings.vSyncCount = 1; // enable vSync
        //Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        txtScore = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        txtLives = GameObject.FindWithTag("LivesText").GetComponent<Text>();

        // set screen boundaries
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
        moveInputHor = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(rb.velocity.x, moveInputVer * speed); // move on the horizontal axis

        moveInputVer = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveInputHor * speed, rb.velocity.y); // move on the vertical axis

        // enable "cheat mode" = fast shooting by pressing C, toggle off by pressing it again
        if (Input.GetKeyDown(KeyCode.C)) {
            if(cheat) cheat = false;
            else cheat = true;
        }

        if(cheat == true) {
            if (Input.GetKey(KeyCode.Space)) Shoot(); // brrr
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) Shoot(); // pew
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { // quit by pressing esc
            if(Time.timeScale == 0) {
                Time.timeScale = 1;
            } else {
                Time.timeScale = 0;
            } 
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
        projectile.Break(2); // destroy bullet after two seconds
    }

    public void AddScore()
    {
        this.score += 1;
        this.txtScore.text = "Score: " + score;
        
        PlayerPrefs.SetInt("score", this.score);
    }

    public void RemoveScore() { // -1 score if over 0
        if(score > 0) {
            this.score -= 1;
            this.txtScore.text = "Score: " + score;
            PlayerPrefs.SetInt("score", this.score);
        }
    }

    public void RemoveHealth(GameObject obj) {
        if(lives > 0){
            lives--;
            txtLives.text = "Lives: " + lives;

            // if on last life, red text
            if(lives == 0) txtLives.color = Color.red;
            if(obj.tag != "Player") Destroy(obj); // destroy gameobject if not player, lazy fix
            RemoveScore();
        } else {
            Destroy(obj); //kill enemy
            Destroy(gameObject); // die
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnCollisionEnter2D(Collision2D other) { // -1 on collision with enemy
        if (other.gameObject.tag == "Enemy") RemoveHealth(other.gameObject);
    }
}

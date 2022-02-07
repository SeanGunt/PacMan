using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; 

public class PacManController : MonoBehaviour
{   
    public float speedMultiplier;
    private Vector2 direction;
    public Text livesText;
    public Text scoreText;
    private int lives = 3;
    private int score = 0;
    private float powerupTimer = 5.0f;
    bool powered;
    Animator animator;
    AudioSource audioSource;
    Vector2 startPos;
    public AudioClip pickup;
    public AudioClip loseMusic;
    public GameObject pellets;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = score.ToString();
        
        startPos = this.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 90.0f);
        }
        else if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180.0f);
        }

        // Pickup power.
        if (powered)
        {
            Powered();
        }

        // Lose requirement.
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }

        // Win requirement.
        if (!GameObject.FindWithTag("Dot"))
        {
            SceneManager.LoadScene(3);
        }
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + direction.x * Time.deltaTime * speedMultiplier , 
        transform.position.y + direction.y * Time.deltaTime * speedMultiplier);
    }  

    //  Code when you pickup a powerup.
    private void Powered()
    {
        powerupTimer -= Time.deltaTime;
        EnemyAI.target = GameObject.FindWithTag("Ghost1").transform;
        EnemyAI.enemySpriteRenderer.color = EnemyAI.myBlue;
            if (powerupTimer <= 0)
            {
                powered = false;
                powerupTimer = 5.0f;
                EnemyAI.enemySpeed = 1000f;
                EnemyAI.target = GameObject.FindWithTag("PacMan").transform;
                EnemyAI.enemySpriteRenderer.color = EnemyAI.myRed;
                EnemyAI.boxCollider2d.enabled = true;
            }
    }

    // Resets positions of both the ghost and pacman.
    public void ResetPos()
    {
        this.transform.position = startPos;
        GameObject.FindWithTag("Enemy").transform.position = EnemyAI.startPos;
        direction = Vector2.zero;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dot")
        {
            score += 10;
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }
        if (other.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            EnemyAI.enemySpeed = 300f;
            powered = true;
            audioSource.PlayOneShot(pickup);
        }
        if (other.tag == "Enemy" && !powered)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            ResetPos();
        }
        if (other.tag == "Enemy" && powered)
        {
            EnemyAI.enemySpeed = 2000f;
            EnemyAI.boxCollider2d.enabled = false;
            powerupTimer = 5.0f;
            score += 50;
            scoreText.text = score.ToString();
        }
        if (other.tag == "TelLeft")
        {
            this.transform.position = new Vector2(14.0f, 0.0f);
        }
        if (other.tag == "TelRight")
        {
            this.transform.position = new Vector2(-14.0f, 0.0f);
        }
    }
}

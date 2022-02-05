using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = score.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }

        if (powered)
        {
            Powered();
        }
        if (lives <= 0)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                        SceneManager.LoadScene(1);
            }
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + direction.x * Time.deltaTime * speedMultiplier , 
        transform.position.y + direction.y * Time.deltaTime * speedMultiplier);
    }  

    private void Powered()
    {
        powerupTimer -= Time.deltaTime;
        EnemyAI.target = GameObject.FindWithTag("Ghost1").transform;
        EnemyAI.enemySpriteRenderer.color = EnemyAI.myBlue;
            if (powerupTimer <= 0)
            {
                powered = false;
                powerupTimer = 5.0f;
                EnemyAI.enemySpeed = 900f;
                EnemyAI.target = GameObject.FindWithTag("PacMan").transform;
                EnemyAI.enemySpriteRenderer.color = EnemyAI.myRed;
            }
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
        }
        if (other.tag == "Enemy" && !powered)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
        }
        if (other.tag == "Enemy" && powered)
        {
            EnemyAI.enemySpeed = 2000f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacManController : MonoBehaviour
{   
    public float speedMultiplier;
    private Vector2 direction;
    public Text livesText;
    public Text gameOverText;
    public Text scoreText;
    private int lives = 3;
    private int score = 0;
    Rigidbody2D rigidbody2d;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = score.ToString();
        gameOverText.text = "";
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
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + direction.x * Time.deltaTime * speedMultiplier , 
        transform.position.y + direction.y * Time.deltaTime * speedMultiplier);
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesText.text = "Lives: " + lives.ToString();
        }
        if (lives <= 0)
        {
            gameOverText.text = "You lose!";
            Destroy(this.gameObject);
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
    }
}

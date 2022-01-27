using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacManController : MonoBehaviour
{   
    public float speedMultiplier;

    Rigidbody2D rigidbody2d;
    private Vector2 direction;
    public Text livesText;
    public Text winText;

    private int lives;

    Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        livesText.text = "";
        winText.text = "";

        lives = 3;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + direction.x * speedMultiplier, transform.position.y + direction.y * speedMultiplier);
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
        }

         livesText.text = "Lives: " + lives.ToString();
        if (lives <=0)
        {
            winText.text = "You lose!";
        }

        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}

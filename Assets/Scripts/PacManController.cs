using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed;
    float horizontal;
    float vertical;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        // Code that makes Pac-man move //
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            rigidbody2d.MovePosition(new Vector2 (position.x + speed * horizontal * Time.deltaTime , position.y));
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            rigidbody2d.MovePosition(new Vector2 (position.x , position.y + speed * vertical * Time.deltaTime));
        }
    }  
}

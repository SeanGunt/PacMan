using UnityEngine;

public class BoltBase : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float homeTimer = 5.0f;
    public float chaseTimer = 60.0f;
    public float scatterTimer = 15.0f;
    public float homeDirectionTimer = -1.0f;
    public float homeSpeed = 0.20f;
    int homeDirection = 1;
    public static bool home;
    public static bool chase;
    public static bool scatter;
    public static SpriteRenderer enemySpriteRenderer;
    public static Color myBlue;
    public static Color myWhite;
    public static Vector2 startPos;
    public static BoxCollider2D boxCollider2d;

    void Awake()
    {
        home = true;
        chase = false;
        scatter = false;

        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();

        myBlue = new Color(0.25f,0.25f,0.75f,1f);
        myWhite = Color.white;
        enemySpriteRenderer.color = myWhite;

        startPos = this.transform.position;
    }
    public void Home()
    {   
        BoltAI.enemySpeed = 0f;
        homeDirectionTimer -= Time.deltaTime;

        if (homeDirectionTimer <= 0)
        {
            homeDirection = -homeDirection;
            homeDirectionTimer = 1.0f;
        }
        Vector2 position = rigidbody2d.position;
        
        position.x = position.x + Time.deltaTime * homeSpeed * homeDirection;

        homeTimer -= Time.deltaTime;

        if (homeTimer <= 0)
        {
            BoltAI.enemySpeed = 1000.0f;
            homeTimer = 7.0f;
            home = false;
            scatter = true;
        }

        rigidbody2d.MovePosition(position);
    }
    void Scatter()
    {
        scatterTimer -= Time.deltaTime;
        BoltAI.target = GameObject.FindWithTag("BoltScatter").transform;
        if (scatterTimer <= 0)
        {
            BoltAI.enemySpeed = 1000.0f;
            scatterTimer = 15.0f;
            chase =  true;
            scatter = false;
        }
    }
    void Chase()
    {
        BoltAI.target = GameObject.FindWithTag("PacMan").transform;
        chaseTimer -= Time.deltaTime;
        if (chaseTimer <= 0)
        {
            BoltAI.enemySpeed = 1000.0f;
            chaseTimer = 20.0f;
            chase = false;
            scatter = true;
        }
    }
    void FixedUpdate()
    {
        if (home)
        {
            Home();
        }

        if (scatter)
        {
            Scatter();
        }

        if (chase)
        {
            Chase();
        }
    }
}

using UnityEngine;

public class FlakeyBase : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float homeTimer = 7.5f;
    public float chaseTimer = 20.0f;
    public float scatterTimer = 8.0f;
    public float homeDirectionTimer = 1.0f;
    public float homeSpeed = 1.0f;
    public static float frightenedTimer = 5.0f;
    public static int homeDirection = 1;
    public static bool home;
    public static bool chase;
    public static bool scatter;
    public static bool frightened;
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
        frightened = false;

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
        FlakeyAI.enemySpeed = 0f;

        scatter = false;
        chase = false;

        enemySpriteRenderer.color = myWhite;

        homeDirectionTimer -= Time.deltaTime;

        if (homeDirectionTimer <= 0)
        {
            homeDirection = -homeDirection;
            homeDirectionTimer = 1.0f;
        }
        Vector2 position = rigidbody2d.position;
        
        position.y = position.y + Time.deltaTime * homeSpeed * homeDirection;

        homeTimer -= Time.deltaTime;

        if (homeTimer <= 0)
        {
            homeDirectionTimer = 1.0f;
            homeDirection = 1;
            FlakeyAI.enemySpeed = 850.0f;
            homeTimer = 7.5f;
            home = false;
            scatter = true;
        }

        rigidbody2d.MovePosition(position);
    }
    void Scatter()
    {
        FlakeyAI.target = GameObject.FindWithTag("FlakeyScatter").transform;

        scatterTimer -= Time.deltaTime;

        if (scatterTimer <= 0)
        {
            FlakeyAI.enemySpeed = 850.0f;
            scatterTimer = 8.0f;
            chase =  true;
            scatter = false;
        }
    }
    void Chase()
    {
        FlakeyAI.target = GameObject.FindWithTag("PacMan").transform;

        chaseTimer -= Time.deltaTime;

        if (chaseTimer <= 0)
        {
            FlakeyAI.enemySpeed = 850.0f;
            chaseTimer = 20.0f;
            chase = false;
            scatter = true;
        }
    }
    void Frightened()
    {
        chase = false;
        scatter = false;

        frightenedTimer -= Time.deltaTime;

        FlakeyAI.target = GameObject.FindWithTag("FlakeyStart").transform;
        enemySpriteRenderer.color = myBlue;

        if (frightenedTimer <= 0)
        {
            enemySpriteRenderer.color = myWhite;;
            FlakeyAI.enemySpeed = 850.0f;
            frightened = false;
            chase = true;
            frightenedTimer = 5.0f;
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
        if (frightened)
        {
            Frightened();
        }
    }
}

using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public static Transform target;
    public static float enemySpeed = 1000f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rigidbody2d;
    public static BoxCollider2D boxCollider2d;
    public static SpriteRenderer enemySpriteRenderer;
    public static Color myRed;
    public static Color myBlue;
    public static Vector2 startPos;

    void Awake()
    {
        target = GameObject.FindWithTag("PacMan").transform;

        seeker = GetComponent<Seeker>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        myRed = new Color(1f,0.25f,0.15f,1f);
        myBlue = new Color(0.25f,0.25f,0.75f,1f);
        enemySpriteRenderer.color = myRed;

        startPos = this.transform.position;

        InvokeRepeating("UpdatePath", 0f, 0.25f);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
        }
        
    }
    void FixedUpdate()
    {
        if (path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        } 

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
        Vector2 force = direction * enemySpeed * Time.deltaTime;

        float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

        rigidbody2d.AddForce(force);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}

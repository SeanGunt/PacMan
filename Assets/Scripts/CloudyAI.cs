using UnityEngine;
using Pathfinding;

public class CloudyAI : MonoBehaviour
{
    public static Transform target;
    public static float enemySpeed = 900f;
    float nextWaypointDistance = 1.2f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        target = GameObject.FindWithTag("PacMan").transform;
        seeker = GetComponent<Seeker>();
        rigidbody2d = GetComponent<Rigidbody2D>();
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
        {
            return;
        }

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

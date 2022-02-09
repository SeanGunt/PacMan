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
    AudioSource audioSource;
    Vector2 startPos;
    public AudioClip pickup;
    public AudioClip loseMusic;
    GameObject pellets;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        
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

        CloudyAI.target = GameObject.FindWithTag("CloudyStart").transform;
        DripAI.target = GameObject.FindWithTag("DripStart").transform;
        FlakeyAI.target = GameObject.FindWithTag("FlakeyStart").transform;
        BoltAI.target = GameObject.FindWithTag("BoltStart").transform;

        CloudyBase.enemySpriteRenderer.color = CloudyBase.myBlue;
        DripBase.enemySpriteRenderer.color = DripBase.myBlue;
        FlakeyBase.enemySpriteRenderer.color = FlakeyBase.myBlue;
        BoltBase.enemySpriteRenderer.color = BoltBase.myBlue;

        if (powerupTimer <= 0)
        {
            powered = false;
            powerupTimer = 5.0f;

            CloudyAI.enemySpeed = 1000f;
            CloudyAI.target = GameObject.FindWithTag("PacMan").transform;
            CloudyBase.enemySpriteRenderer.color = CloudyBase.myWhite;
            CloudyBase.boxCollider2d.enabled = true;

            DripAI.enemySpeed = 1000f;
            DripAI.target = GameObject.FindWithTag("PacMan").transform;
            DripBase.enemySpriteRenderer.color = DripBase.myWhite;
            DripBase.boxCollider2d.enabled = true;

            FlakeyAI.enemySpeed = 1000f;
            FlakeyAI.target = GameObject.FindWithTag("PacMan").transform;
            FlakeyBase.enemySpriteRenderer.color = FlakeyBase.myWhite;
            FlakeyBase.boxCollider2d.enabled = true;

            BoltAI.enemySpeed = 1000f;
            BoltAI.target = GameObject.FindWithTag("PacMan").transform;
            BoltBase.enemySpriteRenderer.color = BoltBase.myWhite;
            BoltBase.boxCollider2d.enabled = true;
        }
    }

    // Resets positions of both the ghosts and pacman.
    public void ResetPos()
    {
        this.transform.position = startPos;

        GameObject.FindWithTag("Cloudy").transform.position = CloudyBase.startPos;
        GameObject.FindWithTag("Drip").transform.position = DripBase.startPos;
        GameObject.FindWithTag("Bolt").transform.position = BoltBase.startPos;
        GameObject.FindWithTag("Flakey").transform.position = FlakeyBase.startPos;

        direction = Vector2.zero;
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dot")
        {
            score += 10;
            scoreText.text = "Score: " + score.ToString();
            audioSource.PlayOneShot(pickup);
            Destroy(other.gameObject);
        }

        if (other.tag == "PowerUp")
        {
            Destroy(other.gameObject);

            CloudyAI.enemySpeed = 300f;
            DripAI.enemySpeed = 300f;
            FlakeyAI.enemySpeed = 300f;
            BoltAI.enemySpeed = 300f;

            powered = true;
            audioSource.PlayOneShot(pickup);
        }

        if (other.tag == "Cloudy" && !powered || other.tag == "Drip" && !powered || other.tag == "Bolt" && !powered || other.tag == "Flakey" && !powered)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            ResetPos();
        }

        if (other.tag == "Cloudy" && powered)
        {
            CloudyAI.enemySpeed = 2000f;
            CloudyBase.boxCollider2d.enabled = false;
            powerupTimer = 5.0f;
            score += 50;
            scoreText.text = score.ToString();
        }

        if (other.tag == "Drip" && powered)
        {
            DripAI.enemySpeed = 2000f;
            DripBase.boxCollider2d.enabled = false;
            powerupTimer = 5.0f;
            score += 50;
            scoreText.text = score.ToString();
        }

        if (other.tag == "Bolt" && powered)
        {
            BoltAI.enemySpeed = 2000f;
            BoltBase.boxCollider2d.enabled = false;
            powerupTimer = 5.0f;
            score += 50;
            scoreText.text = score.ToString();
        }

        if (other.tag == "Flakey" && powered)
        {
            FlakeyAI.enemySpeed = 2000f;
            FlakeyBase.boxCollider2d.enabled = false;
            powerupTimer = 5.0f;
            score += 50;
            scoreText.text = score.ToString();
        }

        if (other.tag == "TelLeft")
        {
            this.transform.position = new Vector2(GameObject.FindWithTag("TelRight").transform.position.x - 0.50f, GameObject.FindWithTag("TelRight").transform.position.y);
        }

        if (other.tag == "TelRight")
        {
            this.transform.position = new Vector2(GameObject.FindWithTag("TelLeft").transform.position.x + 1.0f, GameObject.FindWithTag("TelLeft").transform.position.y);
        }
    }
}

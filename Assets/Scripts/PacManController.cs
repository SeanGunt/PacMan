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

        if (lives <= 0)
        {
            SceneManager.LoadScene(3);
        } 

        if (!GameObject.FindWithTag("Dot") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        if (!GameObject.FindWithTag("Dot") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(4);
        }
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + direction.x * Time.deltaTime * speedMultiplier , 
        transform.position.y + direction.y * Time.deltaTime * speedMultiplier);
    }  
    public void ResetPos()
    {
        this.transform.position = startPos;

        GameObject.FindWithTag("Cloudy").transform.position = CloudyBase.startPos;
        GameObject.FindWithTag("Drip").transform.position = DripBase.startPos;
        GameObject.FindWithTag("Bolt").transform.position = BoltBase.startPos;
        GameObject.FindWithTag("Flakey").transform.position = FlakeyBase.startPos;

        CloudyBase.home = true;
        DripBase.home = true;
        FlakeyBase.home = true;
        BoltBase.home = true;

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
            audioSource.PlayOneShot(pickup);

            CloudyBase.frightened = true;
            BoltBase.frightened = true;
            DripBase.frightened = true;
            FlakeyBase.frightened = true;

            CloudyAI.enemySpeed = 300f;
            DripAI.enemySpeed = 300f;
            FlakeyAI.enemySpeed = 300f;
            BoltAI.enemySpeed = 300f;
        }

        if (other.tag == "Cloudy" && !CloudyBase.frightened || other.tag == "Drip" && !DripBase.frightened 
            || other.tag == "Bolt" && !BoltBase.frightened || other.tag == "Flakey" && !FlakeyBase.frightened)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            ResetPos();
        }

        if (other.tag == "Cloudy" && CloudyBase.frightened)
        {
            GameObject.FindWithTag("Cloudy").transform.position = CloudyBase.startPos;
            CloudyBase.home = true;
            CloudyBase.frightened = false;
            CloudyBase.frightenedTimer = 5.0f;
            CloudyBase.enemySpriteRenderer.color = CloudyBase.myWhite;
            CloudyBase.homeDirection = 1;
            score += 50;
            scoreText.text = "Score: " + score.ToString();
        }

        if (other.tag == "Drip" && DripBase.frightened)
        {
            GameObject.FindWithTag("Drip").transform.position = DripBase.startPos;
            DripBase.home = true;
            DripBase.frightened = false;
            DripBase.frightenedTimer = 5.0f;
            DripBase.enemySpriteRenderer.color = DripBase.myWhite;
            DripBase.homeDirection = 1;
            score += 50;
            scoreText.text = "Score: " + score.ToString();
        }

        if (other.tag == "Bolt" && BoltBase.frightened)
        {
            GameObject.FindWithTag("Bolt").transform.position = BoltBase.startPos;
            BoltBase.home = true;
            BoltBase.frightened = false;
            BoltBase.frightenedTimer = 5.0f;
            BoltBase.enemySpriteRenderer.color = BoltBase.myWhite;
            BoltBase.homeDirection = 1;
            score += 50;
            scoreText.text = "Score: " + score.ToString();
        }

        if (other.tag == "Flakey" && FlakeyBase.frightened)
        {
            GameObject.FindWithTag("Flakey").transform.position = FlakeyBase.startPos;
            FlakeyBase.home = true;
            FlakeyBase.frightened = false;
            FlakeyBase.frightenedTimer = 5.0f;
            FlakeyBase.enemySpriteRenderer.color = FlakeyBase.myWhite;
            FlakeyBase.homeDirection = 1;
            score += 50;
            scoreText.text = "Score: " + score.ToString();
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

using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    #region Variables

    public float speed;
    public float timerTime;
    public GameObject racket1;
    public GameObject racket2;
    public Text score;
    public Text timer;
    bool timerIsRunning = false;
    int player1;
    int player2;
    float originalSpeed;
    float originalTime;
    Vector3 timerPos;
    Vector2 ballDir;
    Vector3 racket1Pos;
    Vector3 racket2Pos; 

    #endregion

    #region Hooks

    void Awake()
    {

    }

    void Start()
    {
        timerIsRunning = true;
        originalSpeed = speed;
        originalTime = timerTime;
        racket1Pos = racket1.transform.position;
        racket2Pos = racket2.transform.position;
        timerPos = timer.transform.position;
        ResetRound();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timerTime > 0)
            {
                timerTime -= Time.deltaTime;
                DisplayTime(timerTime);
            }
            else
            {
                timerTime = 0;
                timerIsRunning = false;
                StartRound();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        speed += 10;
        if (col.gameObject.name == "Player1")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            Vector2 dir = new Vector2(1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * speed * Time.deltaTime;
            Debug.Log(dir.ToString() + " - " + speed.ToString() + " - " + Time.deltaTime.ToString());
        }

        if (col.gameObject.name == "Player2")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            Vector2 dir = new Vector2(-1, y).normalized;
             
            GetComponent<Rigidbody2D>().velocity = dir * speed * Time.deltaTime;
            Debug.Log(dir.ToString() + " - " + speed.ToString() + " - " + Time.deltaTime.ToString());
        }

        if (col.gameObject.name == "LeftWall")
        {
            player2++;
            UpdateScore();
            ResetRound();
        }
        else if (col.gameObject.name == "RightWall")
        {
            player1++;
            UpdateScore();
            ResetRound();
        }
    }

    #endregion


    #region Functions

    void ResetRound()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().position = new Vector2(0, 0);

        racket1.transform.position = racket1Pos;
        racket2.transform.position = racket2Pos;
        timerTime = originalTime;
        timerIsRunning = true;
        timer.transform.position = timerPos;
    }

    void StartRound()
    {
        speed = originalSpeed;
        timerTime = originalTime;
        float x = Random.Range(0, 2) * 2 - 1;
        float y = Random.Range(0, 2) * 2 - 1;
        ballDir = new Vector2(x, y);
        GetComponent<Rigidbody2D>().velocity = ballDir * speed * (Time.deltaTime * 10);
        Debug.Log(ballDir.ToString() + " - " + speed.ToString() + " - " + Time.deltaTime.ToString());
    }

    void DisplayTime(float timerTime)
    {
        int seconds = Mathf.FloorToInt(timerTime % 60);
        if (seconds < 0)
        {
            timer.transform.position = new Vector3(0, 1000, 0);
        }
        timer.text = seconds.ToString();
    }

    void StartTimer()
    {

    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void UpdateScore()
    {
        score.text = player1.ToString() + "     :     " + player2.ToString();
    }

    #endregion
}

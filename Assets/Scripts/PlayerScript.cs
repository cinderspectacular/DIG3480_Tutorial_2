using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text win;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    private int scoreValue = 0;
    private int lifeNum = 3;
    private bool teleportOnce = false;
    private bool gameOver = false;
    public AudioClip music;
    public AudioClip winSound;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = $"Lives: {lifeNum}";
        win.text = null;
        source.clip = music;
        source.loop = true;
        source.Play();
    }

    void Update() 
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

    }

    void OnTriggerEnter2D(Collider2D other) {
        if((other.gameObject.CompareTag("UFO")) && (gameOver == false))
        {
            other.gameObject.SetActive(false);
            lifeNum -= 1;
            lives.text = $"Lives: {lifeNum}";
        }

        if(lifeNum == 0)
        {
            Destroy(this);
            score.text = null;
            lives.text = null;
            win.text = "You lose. Too bad!";

        }
    }

    void OnCollisionEnter2D (Collision2D collision) 
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if((scoreValue == 4) && (teleportOnce == false))
        {
            transform.position = new Vector2(65.0f, 0.0f);
            lifeNum = 3;
            lives.text = $"Lives: {lifeNum}";
            teleportOnce = true;
        }

        if(scoreValue == 8)
        {
            source.loop = false;
            source.clip = winSound;
            source.Play();
            gameOver = true;
            scoreValue = 9;
            score.text = null;
            lives.text = null;
            win.text = "You win! Game created by Jim Elso";
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {    
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}

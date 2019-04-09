using UnityEngine;

public class Coin : MonoBehaviour
{
    private static int score = 0;

    [SerializeField] private float upForce;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    // Components
    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource sound;
    private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        player = Global.Instance.Player;
    }

    private void Update()
    {
        UpdateText();
        if (col.enabled == false)
        {
            rb.velocity += Vector2.up * upForce * Time.deltaTime;
        }
        if (sr.color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player && col.enabled == true)
        {
            sound.Play();
            score++;
            col.enabled = false;
            anim.SetBool("spin", false);
        }
    }

    private void UpdateText()
    {
        scoreText.text = "Score: " + score;
    }
}

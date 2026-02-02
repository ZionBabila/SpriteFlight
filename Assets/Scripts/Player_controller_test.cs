using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class Player_controller_test : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    public GameObject boosterFlame;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMuliplier = 10f;
    Rigidbody2D rb;
    public UIDocument uiDocument;
    private Label scoreText;
    public GameObject explosionEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
      
    }
    void MovePlayer()
    {
          boosterFlame.SetActive(Mouse.current.leftButton.isPressed);

      
        if (Mouse.current.leftButton.isPressed)
        {
            //Camera.main.ScreenToWorldPoint() function to translate the mouse’s position from screen space to a point in the game world
            // Calculate mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Debug.Log("Mouse position: " + mousePos);
            Vector2 direction = (mousePos - transform.position).normalized;
            // Move player in direction of mouse
            transform.up = direction;
            rb.AddForce(direction * thrustForce);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }
    void UpdateScore()
    {
        
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMuliplier);
        scoreText.text = "Score: " + score;
        //Debug.Log("Score: " + score);
          // if (Mouse.current.leftButton.wasPressedThisFrame)
        // {
        //     boosterFlame.SetActive(true);
        //     Debug.Log("Booster flame activated");
        // }
        // else if (Mouse.current.leftButton.wasReleasedThisFrame)
        // {
        //     boosterFlame.SetActive(false);
        // }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

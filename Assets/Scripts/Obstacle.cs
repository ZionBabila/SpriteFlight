using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSpeed  = 50f;
    public float maxSpeed = 150f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float maxSpinSpeed = 10f;
    public GameObject bounceEffectPrefab;  // Assign your impact particle effect prefab in the Inspector
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        
        rb = GetComponent<Rigidbody2D>();
        
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the point of contact from the collision
        Vector2 contactPoint = collision.GetContact(0).point;
        
        // Spawn the bounce effect if we have one assigned
        if (bounceEffectPrefab != null)
        {
            GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);
            // Clean up the effect after 1 second
            Destroy(bounceEffect, 1f);
        }
    }
}

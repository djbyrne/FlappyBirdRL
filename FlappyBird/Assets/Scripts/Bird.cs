using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float velocity = 1.4F;
    private new Rigidbody2D rigidbody;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Flap();
        }
    }

    void Flap()
    {
        rigidbody.velocity = Vector2.up * velocity;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
    }
}

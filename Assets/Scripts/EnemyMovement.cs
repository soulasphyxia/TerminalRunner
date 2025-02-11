using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 0.5f;
    private Vector2 moveVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveVector.y = -1;
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveVector);
    }
}

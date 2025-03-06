using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float speed = 0.5f;
    private Vector2 moveVector;

    public float Speed
    {
        get { return speed; } set { speed = value; } 
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveVector.y = -1;
        rb.MovePosition(rb.position + speed * Time.deltaTime * moveVector);
    }
}

using UnityEngine;

public class IAPlayer : MonoBehaviour
{
    public Transform ball;
    public Transform goal;
    public Transform opponentGoal;
    private float speed = 5f;
    private float defenseDistance = 2f;
    private float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float moveDirection = 0f;
    
    [SerializeField] private AudioSource KickSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToBall = Vector2.Distance(transform.position, ball.position);
        GameObject player = GameObject.FindGameObjectWithTag("Lamine Yamal");
        float playerDistanceToBall = player != null ? Vector2.Distance(player.transform.position, ball.position) : float.MaxValue;

        moveDirection = 0f;

        if (BallHeadingToGoal())
        {
            DefendGoal();
        }
        else if (ShouldAttack(distanceToBall, playerDistanceToBall))
        {
            Attack();
        }
        else if (PlayerTooClose(player))
        {
            StayNearGoal();
        }
        else
        {
            PositionStrategically();
        }

        if (ShouldJump())
        {
            Jump();
        }

        ApplyMovement();
    }

    void DefendGoal()
    {
        moveDirection = Mathf.Sign(ball.position.x - transform.position.x);
    }

    void StayNearGoal()
    {
        moveDirection = Mathf.Sign(goal.position.x - transform.position.x);
    }

    void Attack()
    {
        moveDirection = Mathf.Sign(opponentGoal.position.x - transform.position.x);
    }

    void PositionStrategically()
    {
        float ballX = ball.position.x;
        float myX = transform.position.x;
        float goalX = goal.position.x;
        float opponentGoalX = opponentGoal.position.x;

        if (ballX < 0)
        {
            moveDirection = Mathf.Sign(goalX + 2 - myX);
        }
        else if (ballX > 0)
        {
            moveDirection = Mathf.Sign(ballX - myX);
        }
    }

    void ApplyMovement()
    {
        float targetX = transform.position.x + moveDirection * speed * Time.deltaTime;
        if (targetX > -4)
        {
            rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    bool BallHeadingToGoal()
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        return ball.position.x < transform.position.x && ball.position.x > goal.position.x - defenseDistance && ballRb.linearVelocity.x < 0;
    }

    bool ShouldAttack(float distanceToBall, float playerDistanceToBall)
    {
        float distanceBallToOpponentGoal = Vector2.Distance(ball.position, opponentGoal.position);
        float distanceBallToOwnGoal = Vector2.Distance(ball.position, goal.position);

        return distanceToBall < playerDistanceToBall && distanceBallToOpponentGoal < distanceBallToOwnGoal;
    }

    bool PlayerTooClose(GameObject player)
    {
        if (player == null) return false;
        float playerX = player.transform.position.x;
        return playerX < goal.position.x + 2f;
    }

    bool ShouldJump()
    {
        return Mathf.Abs(ball.position.y - transform.position.y) > 1.5f && Mathf.Abs(ball.position.x - transform.position.x) < 2f;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SueloInvisible"))
        {
            isGrounded = true;
        }
    }
}

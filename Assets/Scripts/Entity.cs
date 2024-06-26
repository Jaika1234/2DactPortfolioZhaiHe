using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackPower;
    [SerializeField] protected float knockbackDuration = 0.7f;
    protected bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius = 1.2f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.8f;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public int knockbackDir { get; private set; }

    public System.Action onFlipped;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();

        if (sr == null || anim == null || rb == null || fx == null || stats == null || cd == null)
        {
            Debug.LogError("One or more components are not assigned to the Entity.");
        }
    }

    protected virtual void Update()
    {
    }
    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {

    }
    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }
    public virtual void DamageImpact() => StartCoroutine("HitKnockback");

    public virtual void SetupKnockbackDir(Transform _damageDir)
    {
        if (_damageDir.position.x > transform.position.x)
            knockbackDir = -1;
        else if (_damageDir.position.x < transform.position.x)
            knockbackDir = 1;

    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        rb.velocity = new Vector2(knockbackPower.x * knockbackDir, knockbackPower.y);
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
        //* 攻撃受けてたら、動きをIDLEに変換する予定
    }
    public virtual void Die()
    {
    }
    #region Velocity
    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
            onFlipped();

    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    public virtual void SetupDefailtFacingDir(int _direction)
    {
        facingDir = _direction;

        if (facingDir == -1)
            facingRight = false;
    }
    #endregion

    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }

}

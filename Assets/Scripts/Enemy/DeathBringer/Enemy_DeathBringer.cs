using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy_DeathBringer : Enemy
{
    #region Staes
    public DeathBringerIdleState idleState { get; private set; }
    public DeathBringerBattleState battleState { get; private set; }
    public DeathBringerTeleportState teleportState { get; private set;}
    public DeathBringerAttackState attackState { get; private set; }
    public DeathBringerSpellCastState spellCastState { get; private set;}
    public DeathBringerDeadState deadState { get; private set; }
    //public DeathBringerStunnedState stunnedState { get; private set; }
    #endregion
    public bool boosFightBegun;


    [Header("Spell Cast Details")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] protected GameObject skeletonPrefab;
    public bool canSummonSkeleton = true;
    public int amountOfSpell;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;


    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize ;
    public float chanceToTeleport;
    public float deafultChanceToTeleport = 30f;


    [Header("Game Finish")]
    [SerializeField] private GameObject exitDoor;



    protected override void Awake()
    {
        base.Awake();

        SetupDefailtFacingDir(-1);

        idleState = new DeathBringerIdleState(this ,stateMachine ,"Idle",this);

        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, stateMachine, "Idle", this);
        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);


    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    //public override bool CanBeStunned()
    //{
    //    if (base.CanBeStunned())
    //    {
    //        stateMachine.ChangeState(stunnedState);
    //        return true;
    //    }

    //    return false;
    //}
    public override void Die()
    {
        base.Die();

        exitDoor.SetActive(true);
        stateMachine.ChangeState(deadState);
    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * 2.5f;

        Vector3 spellPosition = new(player.transform.position.x + xOffset , player.transform.position.y + 1.5f);

        GameObject newSpell = Instantiate(spellPrefab,spellPosition,Quaternion.identity);
        if (canSummonSkeleton)
        {
            GameObject newSkeleton = Instantiate(skeletonPrefab, spellPosition, Quaternion.identity);
        }

        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
    }
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position=  new Vector3(x, y);
        transform.position = new Vector3(transform.position.x,transform.position.y - GroundBelow().distance+(cd.size.y/2));

        if(!GroundBelow()||SomeThingIsAround()) 
        {
            FindPosition();
        }
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomeThingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position,new Vector3 (transform.position.x,transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = deafultChanceToTeleport;
            return true;
        }
            
        return false;
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            return true;
        }
        return false;
    }

}

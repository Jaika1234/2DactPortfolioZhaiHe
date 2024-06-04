using System.Collections;
using UnityEngine;

public class ArcherTrackingState : EnemyState
{
    private Transform player;
    private Enemy_Archer enemy;
    private int moveDir;

    public ArcherTrackingState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Archer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        stateTimer = enemy.battleTime;

        player = PlayerManager.instance.player.transform;




        if (Vector2.Distance(player.transform.position, enemy.transform.position)< enemy.attackDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }

        if(enemy.IsWallDetected()||!enemy.IsGroundDetected())
        {
            enemy.Tracking = true;
            stateMachine.ChangeState(enemy.jumpState);
        }

        if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 22)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();
        enemy.Tracking = false;
    }

}
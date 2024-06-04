using UnityEngine;
public class DeathBringerIdleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;
    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Vector2.Distance(player.transform.position, enemy.transform.position)<10)
            enemy.boosFightBegun= true;


       if(Input.GetKeyDown(KeyCode.P))
        {
            stateMachine.ChangeState(enemy.teleportState);
        }

       if(stateTimer<0 && enemy.boosFightBegun)
            stateMachine.ChangeState(enemy.battleState);

    }


}

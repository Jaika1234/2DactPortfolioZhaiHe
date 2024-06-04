using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Cursor.lockState = CursorLockMode.None;
        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePosition = Input.mousePosition;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
                player.Flip();
            else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
                player.Flip();
        }
        if (Input.GetButtonUp("Fire2")||Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        if (xInput!=0 && player.facingDir != xInput)
        {
            player.Flip();
        }
    }
}

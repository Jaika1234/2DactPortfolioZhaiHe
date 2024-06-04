using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if ((Input.GetButtonDown("Fire2")||Input.GetKeyDown(KeyCode.Mouse1)) && HasNoSword())
        {
            stateMachine.ChangeState(player.aimSwordState);
        }

        if (Input.GetButtonDown("Counter"))
        {
            stateMachine.ChangeState(player.counterAttack);
        }
        if (Input.GetButtonDown("Jump") && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            stateMachine.ChangeState(player.primaryAttack);
            Debug.Log("joystickFire1 button pressed");
        }
    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        else player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        {
            return false;
        }
    }
}

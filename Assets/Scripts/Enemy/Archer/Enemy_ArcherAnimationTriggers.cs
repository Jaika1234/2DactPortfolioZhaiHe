using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy.Archer
{
    public class Enemy_ArcherAnimationTriggers : Enemy_AnimationTriggers
    {
        private Enemy_Archer enemy => GetComponentInParent<Enemy_Archer>();

    }
}
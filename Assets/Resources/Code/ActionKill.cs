using UnityEngine;
using System.Collections;

public class ActionKill : ActionObject
{
    public SENDER_TYPE excludeType;

    public override void Action(SenderInfo sender)
    {
        if (sender.s_Type != excludeType)
        {
            IDamageable entity;
            if (sender.s_Transform.gameObject.IsDamageable(out entity))
            {
                entity.Damage(9999, DAMAGE_TYPE.MELEE, SenderInfo.Object());
            }
        }
    }
}

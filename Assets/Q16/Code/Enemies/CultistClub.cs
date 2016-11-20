using UnityEngine;
using System.Collections;

public class CultistClub : MonoBehaviour
{
    CultistBehaviour cb;
    SenderInfo sender;

	void Start ()
    {
        cb = GetComponentInParent<CultistBehaviour>();
        sender = cb.Sender(SENDER_TYPE.ENEMY);
	}

    void OnTriggerEnter(Collider col)
    {
        IDamageable entity;
        if (col.gameObject.IsDamageable(out entity))
        {
            entity.Damage(20, DAMAGE_TYPE.MELEE, sender);

            if (entity.Type() == SENDER_TYPE.PLAYER)
            {
                Vector3 dir = cb.transform.forward;
                dir.y = 0.5f;
                col.GetComponentInParent<PlayerInput>().AddForce(dir, 15.0f);
            }

        }
    }
}

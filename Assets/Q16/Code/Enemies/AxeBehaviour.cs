using UnityEngine;
using System.Collections;

public class AxeBehaviour : MonoBehaviour
{
    GolemBehaviour gb;
    Collider c;
    Cooldown cd;
    SenderInfo sender;

	void Start ()
    {
        gb = GetComponentInParent<GolemBehaviour>();
        sender = gb.Sender(SENDER_TYPE.ENEMY);
        c = GetComponent<Collider>();
        cd = new Cooldown(0.5f);
	}
	
	void Update ()
    {
        if (c.enabled)
        {
            cd.UpdateTimer();

            if (cd.ActionReady())
            {
                DeactivateAxe();
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        IDamageable entity;
        if (col.gameObject.IsDamageable(out entity))
        {
            entity.Damage(15, DAMAGE_TYPE.MELEE, sender);

            if (entity.Type() == SENDER_TYPE.PLAYER)
            {
                Vector3 dir = gb.transform.forward;
                dir.y = 0.2f;
                col.GetComponentInParent<PlayerInput>().AddForce(dir, 10.0f);
            }
        }
    }

    public void ActivateAxe()
    {
        c.enabled = true;
        cd = new Cooldown(0.3f);
    }

    void DeactivateAxe()
    {
        c.enabled = false;
    }
}

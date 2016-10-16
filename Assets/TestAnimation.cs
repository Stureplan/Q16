using UnityEngine;
using System.Collections;

public class TestAnimation : EnemyBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    

	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.CrossFade("Idle", 0.5f);
        agent = GetComponent<NavMeshAgent>();

    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            agent.SetDestination(player.transform.position);
            
        }
    }
}

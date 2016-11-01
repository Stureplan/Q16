using UnityEngine;
using System.Collections;

public class DetectInteraction : MonoBehaviour
{
    SenderInfo sender;
    Transform cameraTransform;

	void Start ()
    {
	    switch (tag)
        {
            case "Player":
                sender.s_Type = SENDER_TYPE.PLAYER;
                sender.s_Tag = tag;
                sender.s_Transform = transform;
                cameraTransform = transform.GetComponentInChildren<Camera>().transform;
                break;

            case "Enemy":
                sender.s_Type = SENDER_TYPE.ENEMY;
                sender.s_Tag = tag;
                sender.s_Transform = transform;
                break;

            default:
                sender.s_Type = SENDER_TYPE.OBJECT;
                sender.s_Tag = tag;
                sender.s_Transform = transform;
                break;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Interactable")
        {
            InteractableObject iObject = col.GetComponent<InteractableObject>();

            if (iObject.iType == INTERACTION_TYPE.TRIGGER)
            {
                iObject.Interact(sender);

            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Interactable")
        {
            InteractableObject iObject = col.transform.GetComponent<InteractableObject>();

            if (iObject.iType == INTERACTION_TYPE.PUSH)
            {
                iObject.Interact(sender);

            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.transform.tag == "Interactable")
        {
            InteractableObject iObject = col.transform.GetComponent<InteractableObject>();

            iObject.Interact(sender);
        }
    }


    void Update ()
    {
        if (sender.s_Type == SENDER_TYPE.PLAYER)
        {
            if (Input.GetButtonDown("Use"))
            {
                CheckForUse(cameraTransform.position, cameraTransform.forward);
            }
        }
    }

    void CheckForUse(Vector3 pos, Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, 2.0f))
        {
            if (hit.collider.tag == "Interactable")
            {
                InteractableObject iObject = hit.transform.GetComponent<InteractableObject>();

                if (iObject.iType == INTERACTION_TYPE.USE)
                {
                    iObject.Interact(sender);
                }
            }
        }
    }
}

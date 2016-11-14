using UnityEngine;
using System.Collections;

public class CultistAnimEvents : MonoBehaviour
{
    CultistBehaviour cb;
    public GameObject lightningPrefab;
    Vector3 lightningTop;
    Vector3 lightningBot;
    Vector3 lightningMid1;
    Vector3 lightningMid2;


    // Use this for initialization
    void Start ()
    {
        cb = transform.parent.GetComponent<CultistBehaviour>();
    }

    void Update()
    {

    }
	
    public void SetCanMoveActive()
    {
        cb.SetCanMove(true);
    }

    public void SetCanMoveInactive()
    {
        cb.SetCanMove(false);
    }

    public void SetWeaponActive()
    {
        cb.SetWeaponHitbox(true);
    }

    public void SetWeaponInactive()
    {
        cb.SetWeaponHitbox(false);
    }

    public void Lightning()
    {
        GameObject go = (GameObject)Instantiate(lightningPrefab, transform.position, Quaternion.identity);
        LightningFX lFX = go.GetComponent<LightningFX>();
        lFX.SetWeaponPos(cb.GetWeaponPos());
        lFX.ArrangeLightning();
        cb.SpawnWeaponFireFX();
    }

    public void SpawnFireball()
    {
        cb.SpawnWeaponFireball();
    }
}

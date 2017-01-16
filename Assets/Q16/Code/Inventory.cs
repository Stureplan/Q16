using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    Weapon currentWeapon;

    private Dictionary<Item, bool> itemObjects;
    private Dictionary<WPN_TYPE, Weapon> weaponObjects;

    int amtOfSlowmo = 0;

    void Start()
    {
        SetupItems();
        SetupWeapons();
        
        // START WITH SG ACTIVE
        SetHasWeapon(WPN_TYPE.SHOTGUN, true);     
        weaponObjects[WPN_TYPE.SHOTGUN].gameObject.SetActive(true);
        currentWeapon = weaponObjects[WPN_TYPE.SHOTGUN];
    }

    void SetupItems()
    {
        itemObjects = new Dictionary<Item, bool>();

        //itemObjects.Add(Item.Get("Skeleton Key"), false);
        //itemObjects.Add(Item.Get("Flesh Key"), false);
    }

    void SetupWeapons()
    {
        weaponObjects = new Dictionary<WPN_TYPE, Weapon>();

        Weapon[] temp = GetComponentsInChildren<Weapon>(true);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].SetHasWeapon(false);
            weaponObjects.Add(temp[i].type, temp[i]);
        }
    }

    public void FireWeapon(Vector3 pos, Vector3 dir)
    {
        currentWeapon.Fire(pos, dir);
    }

    public void ChangeWeapon(WPN_TYPE weapon)
    {
        if (currentWeapon.type == weapon)
        {
            return;
        }

        if (HasWeapon(weapon))
        {
            currentWeapon.gameObject.SetActive(false);
            weaponObjects[weapon].gameObject.SetActive(true);

            currentWeapon = weaponObjects[weapon];
            currentWeapon.PlayAnimation("WeaponSwitch");
            
            MessageLog.Notify("Changed weapon to " + currentWeapon.name);
        }
        else
        {
            MessageLog.Notify("No weapon in that slot");
        }
    }

    public void NextWeapon()
    {
        int current = (int)currentWeapon.type;

        Weapon[] temp = GetComponentsInChildren<Weapon>(true);
        for (int i = 0; i < temp.Length; i++)
        {
            if (i > current && temp[i].HasWeapon())
            {
                ChangeWeapon( (WPN_TYPE)i );

                break;
            }
        }

    }

    public bool CheckWeaponCD()
    {
        return currentWeapon.CheckCD();
    }

    public void SetItem(Item item, bool hasItem)
    {
        itemObjects[item] = hasItem;
    }

    public bool HasItem(Item item)
    {
        return itemObjects.ContainsKey(item);
    }

    public void SetHasWeapon(WPN_TYPE weapon, bool hasWpn)
    {
        weaponObjects[weapon].SetHasWeapon(hasWpn);
    }

    public void SetHasAllWeapons(bool hasWpns)
    {
        foreach(WPN_TYPE key in weaponObjects.Keys)
        {
            weaponObjects[key].SetHasWeapon(hasWpns);
        }
    }

    bool HasWeapon(WPN_TYPE key)
    {
        return weaponObjects[key].HasWeapon();
    }

    public void IncreaseDecreaseSlowmo(int amt)
    {
        amtOfSlowmo += amt;
    }

    public int GetAmtOfSlowmo()
    {
        return amtOfSlowmo;
    }
}

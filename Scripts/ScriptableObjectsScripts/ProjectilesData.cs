using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Count of Ammo Available

[CreateAssetMenu (menuName = "ProjectileData")]
public class ProjectilesData : ScriptableObject
{
    public enum Weapon { None, Basic, Rocket, Laser, Ultimate};
    Weapon weaponType;

    [Header("Weapon Type Projectiles")]
    [SerializeField] GameObject basic;
    [Range(50, 200)] [SerializeField] float basicForce = 100;
    [SerializeField] float basicDamage;
    [Space]

    [SerializeField] GameObject rocket; //isActuallyCresent
    [Range(50, 200)] [SerializeField] float rocketForce = 100;
    [SerializeField] float rocketDamage;
    [SerializeField] int rocketCount;
    [Space]

    [SerializeField] GameObject laser; //is tri laser
    [Range(50, 200)] [SerializeField] float laserForce = 100;
    [SerializeField] float laserDamage;
    [SerializeField] int laserCount;
    [Space]

    [SerializeField] GameObject ultimate;
    [Range(50, 200)] [SerializeField] float ultimateForce = 100;
    [SerializeField] float ultimateDamage;
    [SerializeField] int ultimateCount; //Mega Laser
    [Space]

    //weapon active
    GameObject activeWeaponType;
    float weaponForce;
    float weaponDamage;

    public void InitateData() // to be called in start wherever the data is being used
    {
        weaponType = Weapon.Basic;
        DetermineActiveWeapon();
        DetermineForce();
        DetermineDamage();
    }

    private void DetermineForce()
    {
        switch(weaponType)
        {
            case Weapon.Basic:
                weaponForce = basicForce;
                break;
            case Weapon.Laser:
                weaponForce = laserForce;
                break;
            case Weapon.Rocket:
                weaponForce = rocketForce;
                break;
            case Weapon.Ultimate:
                weaponForce = ultimateForce;
                break;
            default:
                weaponForce = 100f;
                break;
        }
    }

    private void DetermineDamage()
    {
        switch (weaponType)
        {
            case Weapon.Basic:
                weaponDamage = basicDamage;
                break;
            case Weapon.Laser:
                weaponDamage = laserDamage;
                break;
            case Weapon.Rocket:
                weaponDamage = rocketDamage;
                break;
            case Weapon.Ultimate:
                weaponDamage = ultimateDamage;
                break;
            default:
                weaponDamage = basicDamage;
                break;
        }
    }

    private void DetermineActiveWeapon()
    {
        switch (weaponType)
        {
            case Weapon.Basic:
                activeWeaponType = basic;
                break;
            case Weapon.Laser:
                activeWeaponType = laser;
                break;
            case Weapon.Rocket:
                activeWeaponType = rocket;
                break;
            case Weapon.Ultimate:
                activeWeaponType = ultimate;
                break;
            default:
                activeWeaponType = basic;
                break;
        }
    }

    public GameObject GetCurrentActiveWeapon()
    {
        DetermineActiveWeapon();
        return activeWeaponType;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weaponType = newWeapon;
    }

    public float GetForce()
    {
        //Check for what force is currently active;
        DetermineForce();
        return weaponForce;
    }

    public float GetWeaponDamage()
    {
        DetermineDamage();
        return weaponDamage;
    }
   
}


//TODO: make a method to INIT at the start of the 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int weaponIndex = 0;

    public PlayerMovement playerMovement;
    public int nbOfWeapons = 3;
    public float shootCooldown = 0.5f;
    public bool canShoot = true;


    public GameObject knife;
    public GameObject gun;

    public GameObject bulletPrefab;
    public GameObject shootGunPos;

    public Animator animator;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        knife.SetActive(false);
        gun.SetActive(false);
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canShoot == true)
        {
            Attack(weaponIndex);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextWeapon();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            SelectPreviousWeapon();
        }

        animator.SetInteger("GunIndex", weaponIndex);
    }

    public void SelectNextWeapon()
    {
        if (weaponIndex < (nbOfWeapons - 1))
        {
            weaponIndex++; ;
        }
        else weaponIndex = 0;

        ChangeWeaponVisual();
    }


    public void SelectPreviousWeapon()
    {
        if (weaponIndex > 0)
        {
            weaponIndex--;
        }
        else weaponIndex = (nbOfWeapons - 1);

        ChangeWeaponVisual();
    }


    public void ChangeWeaponVisual()
    {
        if(weaponIndex == 2)
        {
            gun.SetActive(true);
            knife.SetActive(false);
        }
        else if (weaponIndex == 1)
        {
            knife.SetActive(true);
            gun.SetActive(false);
        }
        else if (weaponIndex == 0)
        {
            knife.SetActive(false);
            gun.SetActive(false);
        }
    }

    public void Attack(int _weaponIndex)
    {
        if(_weaponIndex == 2)
        {
            Instantiate(bulletPrefab, shootGunPos.transform.position, shootGunPos.transform.rotation);
            animator.Play("PlayerShoot");
            StartCoroutine(WaitShootCooldown());
        }
        else if(_weaponIndex == 1)
        {
            //
        }
    }

    private IEnumerator WaitShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}

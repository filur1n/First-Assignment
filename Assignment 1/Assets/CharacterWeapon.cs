using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
   [SerializeField] private GameObject projectilePrefab;
   [SerializeField] private Transform shootingStartPosition;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.V))
      {
         GameObject mewProjectile = Instantiate(projectilePrefab, shootingStartPosition.position, shootingStartPosition.rotation);
         //mewProjectile.transform.position = shootingStartPosition.position;
         mewProjectile.GetComponent<Projectile>().Initialize();  
      }
   }
}

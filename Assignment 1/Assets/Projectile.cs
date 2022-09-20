using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody projectileBody;
    private bool isActive;

    public void Initialize()
    {
        isActive = true;
       //projectileBody.AddForce(transform.forward * 500 + transform.up * 100);
       GameObject.Destroy(gameObject, 1f);
    }

    private void Update()
    {
        if (isActive)
        {
            projectileBody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        DestructionFree destruction = collisionObject.GetComponent<DestructionFree>(); // checks if the game object who receives the collision has the "no no" script
        if (destruction == null) // if the result is NO
        {
            Destroy(collisionObject);
            
            TurnManager.GetInstance().ChangeTurn();
            // GameObject damageIndicator = Instantiate(damageIndicatorPrefab);
            //  damageIndicator.transform.position = collision.GetContact(0);
            // TurnManager.GetInstance().Changeturn();
        }
    }
}
       
       
       


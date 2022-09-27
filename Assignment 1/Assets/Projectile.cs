using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody projectileBody;
    private bool isActive;
    public float life = 10;
    private int damageCount = 5;

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


    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("htisometinhrt");
        GameObject collisionObject = collision.gameObject;
        DestructionFree destruction = collisionObject.GetComponent<DestructionFree>(); // checks if the game object who receives the collision has the "no no" script
        if (destruction == null) // if the result is NO
        {
            Destroy(collisionObject);
            
            // TurnManager.GetInstance().ChangeTurn();
            // GameObject damageIndicator = Instantiate(damageIndicatorPrefab);
            //  damageIndicator.transform.position = collision.GetContact(0);
            // TurnManager.GetInstance().Changeturn();
        }

        if (collision.transform.tag =="Player")
        {
            Debug.Log("Collision");
            collision.transform.GetComponent<ThirdPersonMovement>().TakeDamage(damageCount);
            Destroy(gameObject, life);
        }
    }
}
       
       
       


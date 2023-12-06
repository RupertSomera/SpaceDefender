using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    public float healthAmount = 100f;
    private Transform target;
    public Image healthbar;
    private float maxHealth = 100f;
    public int scoreValue = 10;


    private void OnEnable()
    {
        HealthReset();
        LookAtTarget();
        
    }

    private void LookAtTarget()
    {
        Quaternion newRotation;
        //Make the enemy face the target
        //If we do not have a specific target, make the object look at the center
        if(target == null)
        {
            newRotation = Quaternion.LookRotation(transform.position, Vector3.forward);
        }
        else
        {
            newRotation = Quaternion.LookRotation(transform.position - target.transform.position, Vector3.forward);
        }

        //Since rotation is only based on the z-axis
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = newRotation;
    }

    private void Move()
    {
        //Since the object has been rotated, just make it move 
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(healthAmount <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.AddScore(scoreValue);
        }
        if(collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log("Plantet Attacked");

        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float dmg)
    {
        healthAmount -= dmg;
        healthbar.fillAmount = healthAmount / 100f;

    }
    public void HealthReset()
    {
        healthAmount = maxHealth;
        healthbar.fillAmount = 1.0f;
    }

}

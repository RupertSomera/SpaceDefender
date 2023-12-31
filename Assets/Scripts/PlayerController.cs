using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed = 2.0f;
	[SerializeField]
	private float radius = 1.0f;
	[SerializeField]
	private float fireRate = 0.1f;

    [SerializeField]
    //What is the ID of the pooled object that we want as a bullet
    private string bulletId;
    //private GameObject bullet;

	private float currentSpeed; 
	private float timeCounter;

    private void Start()
    {
        //Repeat calling the function "ShootBullet" every fireRate seconds
        //after the initial delay of 0.001f seconds
        InvokeRepeating("ShootBullet", 0.001f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

	private void HandleMovement()
	{
		//Get the player input to determine the direction of the movement
		float movementInput = Input.GetAxis("Horizontal");
		currentSpeed = movementInput * Time.deltaTime * maxSpeed;
		timeCounter += currentSpeed;

		//circular motion based on the movement radius
		float x = Mathf.Cos(timeCounter) * radius;
		float y = Mathf.Sin(timeCounter) * radius;

		transform.position = new Vector2(x, y);

        HandleRotation();
	}

    private void HandleRotation()
    {
        //Define a quaternion that will make the player face outwards of the circle
        Quaternion newRotation = Quaternion.LookRotation(-transform.position, Vector3.forward);
        //Disregard the x and y rotation since we are working on 2D
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = newRotation;
    }

    private void ShootBullet()
    {
        //Instead of manually instantiating a bullet, we need to have it pooled to save up memory and for better performance
        //Instantiate(bullet, transform.position, transform.rotation);

        //Get a prefab from the object pool manager
        GameObject pooledBullet = ObjectPoolManager.Instance.GetPooledObject(bulletId);
        if(pooledBullet != null)
        {
            //Modify the bullet's position and rotation
            pooledBullet.transform.position = transform.position;
            pooledBullet.transform.rotation = transform.rotation;
            //Enable the gameObject
            pooledBullet.SetActive(true);
        }
    }


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
	// Did the snake eat something?
	bool ate = false;

	//Did user died?
	bool isDied = false;
	bool isAlive;

	// Tail Prefab
	public GameObject tailPrefab;
	

	// Current Movement Direction
	// (by default it moves to the right)
	Vector2 dir = Vector2.right;

	// Keep Track of Tail
	List<Transform> tail = new List<Transform>();


	public GameObject gameOverUI;

	// Use this for initialization
	void Start()
	{
		// Move the Snake every 
		InvokeRepeating("Move", 0.1f, 0.1f);
		isAlive = true;
	}

	// Update is called once per frame
	void Update()
	{

		if (!isDied)
		{
			// Move in a new Direction?
			if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
				dir = Vector2.right;
			else if (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S)))
				dir = -Vector2.up;    // '-up' means 'down'
			else if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
				dir = -Vector2.right; // '-right' means 'left'
			else if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W)))
				dir = Vector2.up;
		}
		else
		{
			if (Input.GetKey(KeyCode.R))
			{
				//clear the tail
				tail.Clear();

				//reset to origin
				transform.position = new Vector3(0, 0, 0);

				//make snake alive
				isDied = false;
			}

		}

        
	}

	void Move()
	{
		if (!isDied)
		{
			// Save current position (gap will be here)
			Vector2 v = transform.position;

			// Move head into new direction (now there is a gap)
			transform.Translate(dir);

			// Ate something? Then insert new Element into gap
			if (ate)
			{
				// Load Prefab into the world
				GameObject g = (GameObject)Instantiate(tailPrefab,
								  v,
								  Quaternion.identity);

				// Keep track of it in our tail list
				tail.Insert(0, g.transform);

				// Reset the flag
				ate = false;
			}
			else if (tail.Count > 0)
			{   // Do we have a Tail?
				// Move last Tail Element to where the Head was
				tail.Last().position = v;

				// Add to front of list, remove from the back
				tail.Insert(0, tail.Last());
				tail.RemoveAt(tail.Count - 1);
			}
		}
	}


	private void gameOver()
	{
		isDied = false;
		gameOverUI.SetActive(true);
	}

	public void restart()
	{
		SceneManager.LoadScene(0);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// Food?
		if (coll.name.StartsWith("Food"))
		{
			// Get longer in next Move call
			ate = true;

			// Remove the Food
			Destroy(coll.gameObject);
		}
		else
		{   // Collided with Tail or Border
			isDied = true;
		}
       

	}
}

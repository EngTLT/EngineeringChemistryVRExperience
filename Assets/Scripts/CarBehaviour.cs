using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlls the movement of the traffic that moves through the scenery
public class CarBehaviour : MonoBehaviour {
	public GameObject[] path;
	public float speed;

	int current, count;
	Rigidbody rigid;
	// Use this for initialization
	void Start () {
		current = 0;
		count = 0;
		rigid = GetComponent<Rigidbody>();
		rigid.velocity = (path[current].transform.position - transform.position).normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(transform.position.x - path[current].transform.position.x) > 2 && Mathf.Abs(transform.position.z - path[current].transform.position.z) > 2) {//if car has not reached waypoint
			//Vector3 pos = Vector3.MoveTowards(transform.position, path[current].transform.position, speed * Time.deltaTime);
			//rigid.MovePosition(pos);
			//rigid.velocity = (path[current].transform.position - transform.position).normalized * speed;
			
			if(count > 10) {
				rigid.velocity = (path[current].transform.position - transform.position).normalized * speed;
				transform.forward = rigid.velocity;
				count = 0;
			}
			count++;
		}
		else {
			if (current >= path.Length-1)
				Destroy(this.gameObject);

			current++;

			rigid.velocity = (path[current].transform.position - transform.position).normalized * speed;
		}
	}
}

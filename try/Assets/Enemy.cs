using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject pathGO;

	Transform targetPathNode;
	int pathNodeIndex = 0;

	float speed = 5f;

	public int health = 1;

	// Use this for initialization
	void Start () {
		pathGO = GameObject.Find("Path");
	}

	void GetNextPathNode(){
		targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
		pathNodeIndex++;
	}
	
	// Update is called once per frame
	void Update () {
		if(targetPathNode == null){
			GetNextPathNode ();
			if(targetPathNode == null){
				// We've run out of path !
				ReachedGoal();
				return;
			}
		}

		Vector3 dir = targetPathNode.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame) {
			// We reached the node
			targetPathNode = null;
		} 
		else {
			//Move towards  node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			this.transform.rotation = Quaternion.LookRotation(dir);

		}

	}

	void ReachedGoal(){
		Destroy(gameObject);
	}
}
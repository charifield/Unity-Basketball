using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float bounceLevel = 3000;
    public float bounceCount = 0;
    public bool scored = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.name == "Hoop")
        {
            bounceCount += 1;
            if(bounceCount <= 3)
                GetComponent<AudioSource>().Play();

            GetComponent<Rigidbody>().AddForce(Vector3.up * bounceLevel);
            if (bounceLevel >= 3000)
                bounceLevel -= 3000;
        }

        if (obj.gameObject.name == "Destroyer")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.StartsWith("Fall") && scored == false)
        {
            Game.score = 0;
        }
        if (other.gameObject.name.StartsWith("Scor"))
        {
            scored = true;
        }
    }
}

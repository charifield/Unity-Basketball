using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public GameObject ball;

    //Variables
    public double startSpawnRate = 2.3f;
    public double Level = 1;
    
    //Ball Throw Variables
    public float upPower = 200;
    public float forwardPower = 200;
    

    // Use this for initialization
    void Start () {

        this.StartCoroutine(startSpawner());
        return;
    }
	
	// Update is called once per frame
	void Update () {

        //Increase Difficulty
        if (Game.score > Level)
            Level += 1;
    }



    IEnumerator startSpawner()
    {

        while (true)
        {
            var newBall = Instantiate(ball);
            newBall.transform.position = this.transform.position;

            //Random Left and Right
            newBall.transform.position = new Vector3(Random.Range(-55f, 55f), newBall.transform.position.y, newBall.transform.position.z);

            //Get Random Spin
            var random1 = Random.Range(-2000.0f, 2000.0f);
            var random2 = Random.Range(-2000.0f, 2000.0f);

            //newBall.GetComponent<Rigidbody>().AddForce(0, 500, -500);
            newBall.GetComponent<Rigidbody>().AddTorque(transform.up * random1, ForceMode.Impulse);
            newBall.GetComponent<Rigidbody>().AddTorque(transform.forward * random2, ForceMode.Impulse);
            newBall.GetComponent<Rigidbody>().AddForce(transform.up * upPower, ForceMode.Impulse);
            newBall.GetComponent<Rigidbody>().AddForce(-transform.forward * forwardPower, ForceMode.Impulse);
            

            GetComponent<AudioSource>().Play();


            //Get Random Wait time
            float waitTime = Random.Range(0.6f, 1.2f);
            yield return new WaitForSeconds(waitTime);
        }


        yield return null;
    }
}

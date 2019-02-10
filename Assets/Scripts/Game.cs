using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Score
    public static float score;
    private static float scoreCombo;    

    public Text scoreText;
    public GameObject newScore;

    //Public variables
    public GameObject player;
    public GameObject myCamera;
    public AudioClip swoosh;
    public AudioClip combo;

    //Private Variables
    private static Game instance;
    private bool dragging = false;
    private Vector3 movePosition;

    //NewScoreInfo
    private bool showNewScore = false;
    private Vector3 scoreStartLocation;

    public static void addScore(bool resetCombo = false)
    {
        var screenScoreText = instance.newScore.GetComponent<TextMesh>();

        if (resetCombo)
            scoreCombo = 0;

        scoreCombo += 1;
        score += scoreCombo;
        screenScoreText.text = "+" + scoreCombo;

        //If Combo
        if(scoreCombo <= 2)
        {
            instance.GetComponent<AudioSource>().pitch = Mathf.Pow(2f, 0f);
            instance.GetComponent<AudioSource>().clip = instance.swoosh;
            instance.GetComponent<AudioSource>().Play();
        }
        else if(scoreCombo > 2)
        {
            instance.myCamera.GetComponent<EZCameraShake.CameraShaker>().ShakeOnce(4f, 4f, .1f, 1f);
            instance.GetComponent<AudioSource>().pitch = Mathf.Pow(2f, scoreCombo / 12.0f);
            instance.GetComponent<AudioSource>().clip = instance.combo;
            instance.GetComponent<AudioSource>().Play();
        }

        if (score < 10)
        {
            instance.scoreText.text = "0" + score;
        }
        else
        {
            instance.scoreText.text = score.ToString();
        }

        //Set Visible
        Color color = instance.newScore.GetComponent<Renderer>().material.color;
        color.a = 1.0f;
        instance.lerpScore = 0;
        instance.newScore.GetComponent<Renderer>().material.color = color;
        instance.newScore.transform.position = new Vector3(instance.newScore.transform.position.x, instance.scoreStartLocation.y, instance.newScore.transform.position.z);
        instance.showNewScore = true;
    }




    // Use this for initialization
    void Start()
    {
        instance = this;

        //Set transparent
        Color color = newScore.GetComponent<Renderer>().material.color;
        color.a = 0.0f;
        newScore.GetComponent<Renderer>().material.color = color;
        scoreStartLocation = newScore.transform.position;
    }



    // Update is called once per frame
    private float lerpScore = 0;
    void Update()
    {
        if(showNewScore && lerpScore < 1)
        {
            lerpScore += Time.deltaTime;
            newScore.transform.position = Vector3.Lerp(scoreStartLocation, new Vector3(newScore.transform.position.x, newScore.transform.position.y + 10, newScore.transform.position.z), lerpScore);
        }
        else if(lerpScore >= 1)
        {
            showNewScore = false;
            Color color = newScore.GetComponent<Renderer>().material.color;
            color.a = 0.0f;
            newScore.GetComponent<Renderer>().material.color = color;
        }


        //Update Mouse Info
        if (Input.GetMouseButtonDown(0))
            dragging = true;
        if (Input.GetMouseButtonUp(0))
            dragging = false;


        //Detect Mouse Dragging
        if (dragging == true)
        {
            // Vector3 mouse = Input.mousePosition;

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6.33f); //
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x - Camera.main.transform.position.y, (transform.position.y - Camera.main.transform.position.y),

                    (transform.position.z - Camera.main.transform.position.z)));
            //player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(objPosition.x, player.transform.position.y, player.transform.position.z), Time.deltaTime);
            //player.transform.position = new Vector3((point.x + 10) * 2, player.transform.position.y, player.transform.position.z);
            player.transform.position = new Vector3(-(point.x + 10) * 2, player.transform.position.y, player.transform.position.z);

        }



        //Detect if Touched
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            movePosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

            //if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                //Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                //player.transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
                //player.transform.position = new Vector3(touchedPos.x, player.transform.position.y, player.transform.position.z);
                //player.transform.position = new Vector3((movePosition.x - 5) * 25, player.transform.position.y, player.transform.position.z);
                player.transform.position = new Vector3((movePosition.x) * 25, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}

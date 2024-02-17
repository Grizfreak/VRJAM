using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AttackForward : MonoBehaviour
{
    public Collider ColliderChest;
    public Collider ColliderAttack;

    public Collider ColliderHead;
    public GameObject WaterBall;
    // Start is called before the first frame update
    public GameObject WaterTube;
    private float theStartTime;
    private float theEndTime;
    //take in entry a text element
    public Text deltaTimeText;
    private float deltaTimeBigAttack;
    private int startingCollider;

    public Collider ColliderPont;
    void Start()
    {
        startingCollider = 0;
        theStartTime = 0.0f;
        theEndTime = 0.0f;
        deltaTimeBigAttack = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        //display cooldown to 0 using deltaTimeText
        deltaTimeText.text =
        Time.time - deltaTimeBigAttack > 5.0f ? "0.00" :
        (5.0f - (Time.time - deltaTimeBigAttack)).ToString("F2")
        + "s";
    }

    //when the collider of the attack is triggered
    private void OnTriggerEnter(Collider other)
    {

        if (other == ColliderAttack && theStartTime != 0.0f)
        {
            //set end time
            theEndTime = Time.time;

            if (startingCollider == 1 && (theEndTime - theStartTime < 0.1f))
            {
                Debug.Log("BALLL");
                //instantiate the water ball
                GameObject waterBall = Instantiate(WaterBall, transform.position, transform.rotation);
                //set the velocity of the water ball
                waterBall.GetComponent<Rigidbody>().velocity = transform.forward * 20;
            }
            else if (startingCollider == 2 && (theEndTime - theStartTime < 1.0f))
            {
                //check if last time was more than 5 second on deltatime
                if (Time.time - deltaTimeBigAttack > 5.0f)
                {
                    //instantiate the water tube
                    GameObject waterTube = Instantiate(WaterTube, new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z + 1.0f), transform.rotation);
                    deltaTimeBigAttack = Time.time;
                    theStartTime = 0.0f;
                    theEndTime = 0.0f;
                }

            }
            else
            {
                //reset start and end time
                theStartTime = 0.0f;
                theEndTime = 0.0f;
            }

        }
        if (Time.time - theStartTime > 2.0f)
        {
            startingCollider = 0;
            theStartTime = 0.0f;
            theEndTime = 0.0f;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if the collider is the passive collider one
        if (other == ColliderChest)
        {
            startingCollider = 1;
            //set start time and reset end time
            theStartTime = Time.time;
            theEndTime = 0.0f;
            Debug.Log("from chest");
        }

        if (other == ColliderHead)
        {
            startingCollider = 2;
            //set start time and reset end time
            theStartTime = Time.time;
            theEndTime = 0.0f;
            Debug.Log("from head");
        }
    }
}

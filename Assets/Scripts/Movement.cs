using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject availableWaypoints;
    GameObject currentObjective;
    GameObject nextObjective;
    bool isSearching = false;
    GameObject playerstats; 
    public static float remainingDistance = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerstats = GameObject.FindGameObjectWithTag("PlayerStat");
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        // Get the available waypoints
        availableWaypoints = GameObject.FindGameObjectWithTag("Waypoints");
        // Get the closest entry waypoint
        currentObjective = GetRandomWaypoint(availableWaypoints.GetNamedChild("Entrances"));
        // Set the agent's destination to the closest entry waypoint
        agent.SetDestination(currentObjective.transform.position);
        // Get a random exit waypoint
        nextObjective = GetRandomWaypoint(availableWaypoints.GetNamedChild("Exits"));
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < remainingDistance && nextObjective && !isSearching)
        {
            // Reset agent's path
            agent.ResetPath();
            // Set Trigger for animation
            GetComponent<Animator>().SetTrigger("Search");
            isSearching = true;
            // Freeze position on X and Z axis
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //Wait for the animation to finish
            StartCoroutine(WaitForAnimation());
        }

        if (agent.remainingDistance < remainingDistance && !nextObjective)
        {
            // Destroy the agent
            Destroy(gameObject);
            // Remove gold from the player
            playerstats.GetComponent<PlayerUpdate>().RemoveGold(1);
            
        }
    }

    GameObject GetRandomWaypoint(GameObject waypoints)
    {
        int randomIndex = Random.Range(0, waypoints.transform.childCount);
        return waypoints.transform.GetChild(randomIndex).gameObject;
    }

    IEnumerator WaitForAnimation()
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(6);
        // Set the agent's destination to the next waypoint
        agent.SetDestination(nextObjective.transform.position);
        // Get a random exit waypoint
        nextObjective = null;
        isSearching = false;
    }

    public void Die()
    {
        agent.ResetPath();
        Debug.Log("Died");
        // Set Trigger for animation
        GetComponent<Animator>().SetTrigger("Touche");
        // Freeze position on X and Z axis
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        // update the player stats
        GameObject.FindGameObjectWithTag("PlayerStat").GetComponent<PlayerUpdate>().AddKill();
        // Destroy the agent
        Destroy(gameObject, 3);
    }
}

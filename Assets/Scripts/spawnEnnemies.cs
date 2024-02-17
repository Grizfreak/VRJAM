using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ennemy;
    public float spawnTime = 9.0f;
    void Start()
    {
        // spawn 1 ennemy every 3 seconds
        StartCoroutine(SpawnEnnemy(spawnTime));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnnemy(float time)
    {
        while (true)
        {
            // spawn an ennemy
            Instantiate(ennemy, transform.position, Quaternion.identity);
            // wait for x seconds
            yield return new WaitForSeconds(time);
        }
    }
}

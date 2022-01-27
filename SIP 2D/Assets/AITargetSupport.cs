using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetSupport : MonoBehaviour
{
    //create a list of GameObjects for players in raidus
    public List<GameObject> players = new List<GameObject>();
    public GameObject targetSupport;
    //A variable for how big the raidus to check will be
    public float radius;
    public float supportCheckTime;
    void Start()
    {
        //Run a constant coroutine based on how often the developer chooses to check the radius for non NPC players. 
        StartCoroutine(RadiusCheck(supportCheckTime));
    }
    //Check the radius around our item

    IEnumerator RadiusCheck(float waitTime) 
    {
        while (true)
        {
            //Wait for declared seconds before running.
            yield return new WaitForSeconds(waitTime);
            //Clear the list.
            players.Clear();
            //Cast a circle around us to detect colliders
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, radius, Vector2.up);
            //For each collider we hit
            foreach (RaycastHit2D item in hit)
            {
                //If the collider exists in our list
                if (players.Contains(item.transform.gameObject))
                {
                    //Do nothing
                }
                //If the collider does not exist in our list
                else
                {
                    //Add the player to the list.
                    players.Add(item.transform.gameObject);
                }
            }
            //Select a target player.
            SelectWeakestPlayer();
            //Consol log to show function ran successfully.
            Debug.Log("RaidusCheck Ran.");
        }
    }
    //Select weakest nearby character here.
    void SelectWeakestPlayer() 
    {
        //For each player in the list, compare and choose the weakest player
        //Based on win ratio
        for (int i = 0; i < players.Count - 1; i++)
        {
            //If there is not another player to compare
            if (i + 1 == players.Count)
            {
                //Do Nothing.
            }
            //If there is another player to comapre
            else
            {
                //If the current player has a weaker win ratio than the next player
                if (players[i].GetComponent<PlayerStats>().winRatio > players[i + 1].GetComponent<PlayerStats>().winRatio)
                {
                    //Current target is the current player
                    targetSupport = players[i];
                }
                else
                {
                    //Current target is the next player.
                    targetSupport = players[i + 1];
                }
            }
            
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ThiefController : MonoBehaviour
{
    int itemsStolen = 0;    //Counts how many items were already stolen.
    float timePassed = 0.0f;    //Amount of time passed for the thief to attempt action.
    bool is5SecondsGone = true;    //Flag for determining if 5 seconds has already passed. 
    bool wantToSteal = false;   //Flag for determining if the thief would like to steal or not.
    bool stealFromPlayer = false;   //Flag for determining if the thief wants to steal from player or not.
    public NavMeshAgent thiefAI;    //Refers to the NavMeshAgent component of the thief.
    bool robbed = false;    //Determines if the thief has robbed the player or not. This is how the GOAP will find out if the player was robbed or not by calling the HasRobbed() method. 

    //Called by GOAP in every frame to check if the player was robbed by the thief or not.
    public bool HasRobbed()
    {
        return robbed;
    }

    //Ths method is called by GOAP to notify the thief that it now knows the player was robbed.
    public void ThieveryDiscovered()
    {
        robbed = false; //Once the thief found out of it's guilt, then sets the flag to false.
    }
 
    void Update()
    {
        //Condition is true if 5 seconds are already gone and the thief has not yet decided to steal.
        if (is5SecondsGone && !wantToSteal)
        {
            //float value used for determining if the thief wants to keep on wandering or to start stealing from the agent.
            float wonderStealChance = Random.value;
            
            //There is 33% chance the player would like to steal only if less than 2 items were already stolen. 
            if (wonderStealChance <= 0.33f && itemsStolen < 2)
            {
                //Used for determining if the thief would like to stean from the player's inventory or from the caravan
                float caravanInventoryChance = Random.value;

                //There is a 50 - 50 chance the player might either steal from the inventory or from the caravan.
                //In the below statement, the thief would like to steal from the player's inventory
                if (caravanInventoryChance <= 0.5f)
                {
                    //Please feel free to uncomment the below line for debugging purposes.
                    //Debug.Log("Thief moves to the player");
                    stealFromPlayer = true; //Sets the condition to true so that it is known the thief would like to steal from the inventory.
                    
                    //Sets the destination of the thief to be at the current posiyion of the agent.
                    thiefAI.SetDestination(new Vector3(GameObject.Find("Agent").transform.position.x-1.0f, 0.4f, GameObject.Find("Agent").transform.position.z));
                    wantToSteal = true; //Sets condition to true in order to make it clear that the thief has now decided to steal.
                }
                else //Otherwise, the thief would like to steal from the caravan.
                {
                    //Please uncomment the below line for debugging purposes.
                    //Debug.Log("Thief moves to caravan");
                    stealFromPlayer = false;    //Sets condition to false so that it is known the thief does not want to steal from the agent
                    thiefAI.SetDestination(new Vector3(0, 0.4f, 0));    //Sets the destination of the thief to be the caravan
                    wantToSteal = true; //Sets condition to true in order to make it clear that the thief has now decided to steal.
                }
            }
            else //Otherwise
            {
                //Make the thief wonder around at any position on the floor (not including the alcove)
                thiefAI.SetDestination(new Vector3(Random.Range(-10.0f, 10.0f), 0.4f, Random.Range(-10.0f, 10.0f)));
                is5SecondsGone = false; //Indicates that 5 seconds did not elapse yet.
            }
        }

        if (!wantToSteal) //If the thief does not wat to steal
        {
            //Please uncomment below line for debugging purposes
            //Debug.Log("elapsed time: " + timePassed);
            
            timePassed += Time.deltaTime;  //Adding Time.fixedDeltaTime seconds to the timePassed variable to make it clear that this amount of time is already passed.
            
            if (timePassed >= 5.0f)  // if 5 second or around that has alredy elapse
            {
                timePassed = 0.0f;  //Reset the time to 0
                is5SecondsGone = true;  //Set this condition to true to make it clear that 5 seconds already elapsed.
            }
        }
        else //otherwise
        {
            // Are we close enough to the caravan?
            if (transform.position.x <= 1.5f && transform.position.x >= -0.5f && transform.position.z <= 1.0f && transform.position.z >= -1.2f && !stealFromPlayer)
            {
                //Please feel free to uncomment the below line for debugging purposes
                //Debug.Log("Thief stole from caravan");
                /*steal from the game state, which means agent needs to replan.*/
                int[] state = GameObject.Find("ProgressTracker").GetComponent<TableManager>().GetCurrentState();    //Thief gets the current state of the player from the UI table
                List<int> caravanIndices = new List<int>(); //Stores all the indices of the world state at the caravan section
                int stateIndex =  0;    //Stores the selected index from where the thief randomly decides to rob the player

                for (int integer = 7; integer < 14; integer++)  //Adding all caravan indices of the size-14 world state int[] array.
                {
                    caravanIndices.Add(integer);
                }

                do //This do-while loop keeps on executing until it finds an index such that currentWorldState[index] > 0 given that the caravanIndices list is still not empty.
                {
                    //temp variable used for storing the numbers from 7 to 13 which will be passed to the stateIndex if contained in the caravanIndices list.
                    int selectedInt = Random.Range(7, 14);

                    if (caravanIndices.Contains(selectedInt))   //Checks if the randomly selected integer is inside the caravanIndices list
                    {
                        stateIndex = selectedInt;   //If so, then assign it to the stateIndex variable

                        caravanIndices.Remove(selectedInt); //Remove that randomly selected integer from the list since it has already been scanned through 
                    }
                } while (state[stateIndex] <= 0 && caravanIndices.Count != 0);  //Still did not find a spice to steal from the caravan and the thief did not finish loking through it yet.

                
                // otherwise, stateIndex is index of the random spice we can steal
                if (state[stateIndex] > 0)
                {
                    //Please uncomment below line for debugging purposes
                    //Debug.Log("Thief stealing from state index: " + stateIndex);
                    
                    state[stateIndex]--;    //decrements from the amount of a spice type the thief wanted to steal from
                    
                    //Records in the UI table the current state of the agent.
                    GameObject.Find("ProgressTracker").GetComponent<TableManager>().PopulateTable(state);
                    itemsStolen++;  //Increment by 1 to indicate that the thief has stolen an item.
                    robbed = true;  //Set robbed = true to let the GOAP know that an item has been stolen from the agent.                   
                }

                wantToSteal = false;    //Reset wantToSteal to false after the thief has already stolen a unit of a spice
                is5SecondsGone = false; //Reset is5SecondsGone to false just in case 5 seconds is still not gone yet at the time of the stealing
            }
            else if (stealFromPlayer)
            {
                //Please feel free to uncomment below line for debugging purposes. 
                //Debug.Log("Thief follows player cuz he moved away");
                // If we aren't close : set target again (only for player)
                thiefAI.SetDestination(new Vector3(GameObject.Find("Agent").transform.position.x - 1.0f, 0.4f, GameObject.Find("Agent").transform.position.z)); // note : setting destination at every frame may be costly
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //If the thief collided with the player and wants to steal from the inventory
        if(other.gameObject.CompareTag("Agent") && wantToSteal &&stealFromPlayer)
        {
            int[] state = GameObject.Find("ProgressTracker").GetComponent<TableManager>().GetCurrentState();    //Thief gets the current state of the player from the UI table
            List<int> inventoryIndices = new List<int>();   //Stores all indices in the world state at the inventory section
            int stateIndex = 0;

            for(int integer = 0; integer < 7; integer++)    //Adding all invetory indices of the size-14 world state int[] array.
            {
                inventoryIndices.Add(integer);
            }

            do //This do-while loop keeps on executing until it finds an index such that currentWorldState[index] > 0 given that the inventoryIndices list is still not empty.
            {
                int selectedInt = Random.Range(0, 7);

                if(inventoryIndices.Contains(selectedInt))  //Checks if the randomly selected integer is inside the inventoryIndices list
                {
                    stateIndex = selectedInt;   //If so, then assign it to the stateIndex variable

                    inventoryIndices.Remove(selectedInt);   //Remove that randomly selected integer from the list since it has already been scanned through 
                }
                
            } while(state[stateIndex] <= 0 && inventoryIndices.Count != 0); //If the inventory does not contain any spice of the type and there is still more for the thief to explore.
            

            
            // otherwise, stateIndex is index of the lowest spice we can steal
            if (state[stateIndex] > 0)
            {
                //Please feel free to uncomment below line for debugging purposes only.
                //Debug.Log("Thief stealing from state index: " + stateIndex);
                state[stateIndex]--;    //Decrements the amount of that type of spice after thief decided to steal it.
                GameObject.Find("ProgressTracker").GetComponent<TableManager>().PopulateTable(state);   //Record on the UI table the new state after the thief robbed the player.
                itemsStolen++;  //Incremement by one the counter of the number of items thief has already stolen to make sure thief steals no more than 2 items.
                robbed = true;  //Set robbed to true for GOAP to see that the thief has already stolen from the player.
            }

            wantToSteal = false;    //Reset wantToSteal to false after the thief has already stolen a unit of a spice
            is5SecondsGone = false; //Reset is5SecondsGone to false just in case 5 seconds is still not gone yet at the time of the stealing
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    bool isReplanning = false;  //Flag for the GOAP to tell the agent that the planner is in the proccess of planning
    GameObject trader1, trader2, trader3, trader4, trader5, trader6, trader7, trader8;
    public NavMeshAgent agent; // this is used for referring to the NavMeshAgent component of the player.
    List<Segment> fullPath = new List<Segment>(); //List of all segments used to track the path from current state to goal state.
    IEnumerator coroutine;  //coroutine variable.
    bool playerIsActive = false;    //Flag for determining if the player is trading/moving or not
    bool goingToCaravan = false; //Boolean condition for determining if player is going to the caravan or not.
    bool isInsideCoroutine = false; //bool for determining if a thread is inside a coroutine or not so tha no other threads can get into co-routine.
    int[] currentState = new int[14]; //This stores the current state of the player.
    string currentTransaction = "";  //Global variable for referring to the name of the current transaction.
    bool transactionDone = false;   //Boolean for indicating that whether transaction is done or not, so that the first line in the scroll view can be removed or not once it has been performed.

    void Start()
    {
        trader1 = GameObject.FindGameObjectWithTag("Trader1");
        trader2 = GameObject.FindGameObjectWithTag("Trader2");
        trader3 = GameObject.FindGameObjectWithTag("Trader3");
        trader4 = GameObject.FindGameObjectWithTag("Trader4");
        trader5 = GameObject.FindGameObjectWithTag("Trader5");
        trader6 = GameObject.FindGameObjectWithTag("Trader6");
        trader7 = GameObject.FindGameObjectWithTag("Trader7");
        trader8 = GameObject.FindGameObjectWithTag("Trader8");
    }

    //Called from GOAP to give the player the list of segments that needs to be followed in order to reach the goal.
    public void SetPathList(List<Segment> pathsList)
    {
        fullPath = pathsList;
    }


    void Update()
    {
        //If there are still elements in the fullPath list
        if (fullPath.Count != 0)
        {
            //If the transaction was already performed
            if (transactionDone)
            {
                //Remove the topmost action line from the scroll view given that it has been already executing.
                GameObject.Find("Displayer").GetComponent<TransactionsDisplayer>().DeletePerformedTransaction();
                transactionDone = false;    //Reset this boolean variable to false after removing the corresponding line to the action performed from the scroll view. 
            }

            //If the player is not yet active
            if (!playerIsActive)
            {
                Segment segment = fullPath[0];  //Get the first element of the fullPath list.
                fullPath.RemoveAt(0);   //Remove that first element from the list.

                //Populates the table with the player's xurrent state.
                GameObject.Find("ProgressTracker").GetComponent<TableManager>().PopulateTable(currentState);
                
                currentTransaction = segment.GetTransactionName(); //Gets the name of the current segment's transaction 
                currentState = segment.GetCurrentState();   //Gets the world state of the current segment

                if (currentTransaction.Equals("Trader1"))
                {
                    agent.SetDestination(new Vector3(trader1.transform.position.x - 1.0f, transform.position.y, trader1.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader2"))
                {
                    agent.SetDestination(new Vector3(trader2.transform.position.x - 1.0f, transform.position.y, trader2.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader3"))
                {
                    agent.SetDestination(new Vector3(trader3.transform.position.x - 1.0f, transform.position.y, trader3.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader4"))
                {
                    agent.SetDestination(new Vector3(trader4.transform.position.x - 1.0f, transform.position.y, trader4.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader5"))
                {
                    agent.SetDestination(new Vector3(trader5.transform.position.x - 1.0f, transform.position.y, trader5.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader6"))
                {
                    agent.SetDestination(new Vector3(trader6.transform.position.x - 1.0f, transform.position.y, trader6.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader7"))
                {
                    agent.SetDestination(new Vector3(trader7.transform.localPosition.x - 1.0f, transform.localPosition.y, trader7.transform.localPosition.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals("Trader8"))
                {
                    agent.SetDestination(new Vector3(trader8.transform.position.x - 1.0f, transform.position.y, trader8.transform.position.z));
                    playerIsActive = true;
                }
                else if (currentTransaction.Equals(""))
                {
                    //This will be ignored by the A.I agent.
                    GameObject.Find("Displayer").GetComponent<TransactionsDisplayer>().DeletePerformedTransaction();
                }
                else //Player is going to the caravan
                {
                    agent.SetDestination(new Vector3(0.0f, transform.position.y, 0.0f));    //Move to position of caravan
                    playerIsActive = true;  //Set playerIsActive to true since the player will be moving or trading
                    goingToCaravan = true;  //Set gointToCaravan to true since the player is going to the caravan
                }


            }
            else //Otherwise
            {
                //This if-block statement will be executed at any time after the GOAP sets the isReplanning condition variable to true.
                if (isReplanning)
                {
                    //https://docs.unity3d.com/540/Documentation/ScriptReference/NavMeshAgent.ResetPath.html
                    agent.GetComponent<NavMeshAgent>().ResetPath(); //This stops the agent's path and then the agen will not move anywhere until the SetDestination method will be called on it. Take a look on the above link for more details on it
                    goingToCaravan = false; //Sets the goingToCaravan bool condition to false since it is not desired for the player agent to move, and even to the caravan
                    playerIsActive = false; //Sets the playerIsActive to false since it is not desired that it will do anything when in the process of replanning
                    isReplanning = false;   //Sets the isReplanning to false after the player agent already knows now to remain inactive.
                }
                else //Otherwise
                {
                    //If the player has reached the destination to the caravan
                    if (goingToCaravan && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || Mathf.Abs(agent.velocity.sqrMagnitude) < 0.2f))
                    {
                        goingToCaravan = false; //Sets goingToCaravan to false after the player has reached the caravan.
                        playerIsActive = false; //Sets playerIsActive to false after the player has reached the destination
                        transactionDone = true; //Sets transactionDone to true so that the transaction performed will be removed from the scroll view in the next frame.
                    }
                    //Otherwise, if no threads are inside coroutine and the agent is near a trader's position, 
                    else if (!isInsideCoroutine && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || Mathf.Abs(agent.velocity.sqrMagnitude) < 0.2f))
                    {
                        //Starts a function Wait as a coroutine
                        coroutine = Wait(0.5f);
                        StartCoroutine(coroutine);
                    }
                }
            }
        }
        else //This else statement is so that the goal state will show on the UI table as well as having the scroll view empty from all the action lines, including the last one.
        {
            //Checks if the transaction was already performed in the last frame
            if (transactionDone)
            {
                //Dynamically removing the last action to reach the goal from the scroll view.
                GameObject.Find("Displayer").GetComponent<TransactionsDisplayer>().DeletePerformedTransaction();
                transactionDone = false; //Reset this boolean variable to false after removing the corresponding line to the action performed from the scroll view.
            }
            
            //If player is going to caravan and has reached destination
            if (goingToCaravan && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath || Mathf.Abs(agent.velocity.sqrMagnitude) < 0.2f))
            {
                GameObject.Find("ProgressTracker").GetComponent<TableManager>().PopulateTable(currentState);    //Change the state on the UI table
                goingToCaravan = false; //Sets goingToCaravan to false after the player has reached the caravan.
                transactionDone = true; //Sets transactionDone to true so that the transaction performed will be removed from the scroll view in the next frame.
            }
        }
    }
    
    //Used for the GOAP to notify the Agent that it is in the process of replanning and that it has to stop moving.
    public void SetIsReplanning(bool b)
    {
        isReplanning = b;
    }


    //https://docs.unity3d.com/560/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
    IEnumerator Wait(float waitingTime) //This method will make the player wait 0.5 seconds while there is athread inside the co-routine
    {
        isInsideCoroutine = true;   //Sets isInsideCoroutine to true so that no other threads can get inside the corountine
        yield return new WaitForSeconds(waitingTime);   //Returns the amount of seconds the player needs to wait in order to do the trading
        playerIsActive = false; //Sets playerIsActive to false after finishing the trade.
        isInsideCoroutine = false;  //Sets IsInsideCoroutine to false after the player finished the trading process.
        transactionDone = true; //Sets transactionDone to true 
    }
}

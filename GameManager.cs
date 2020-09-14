using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject trader;   //Public reference to the trader
    public GameObject alcoves;  //Public reference to the set of alcoves
    public GameObject thief;    //Public reference to the thief
    public GameObject agent;    //Public reference to the AI-agent
    HashSet<int> tradersNumbers = new HashSet<int>();   //Stores all the numbers from 1 to 8 in order to keep track of whether a trader with a randomly generated int exists or not.
    bool isPaused = false;  //KeyCode.Spacebar has not been pressed on yet to pause the whole simulation of the game once.
    float currentSimulationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        currentSimulationSpeed = Time.timeScale;
        PositionTraders();  //Call method to randomly position traders on different alcoves.
        SetStartingPositionAndActivateThief(); //Calls this method to set the thief active and assign it's starting position randomly
    }


    void Update()
    {
        //This if-block statement is executed if the space bar was clicked and the game is still not paused yet.
        if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            Time.timeScale = 0.0f;  //Sets the timescale to 0 in order to pause the game.
            isPaused = true;    //Sets isPaused to true so thT 
        }
        //This if-block statement is executed if the space bar was clicked and the game is already paused.
        else if (Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            Time.timeScale = currentSimulationSpeed;  //Sets the timescale to whatever it was before pausing the game.
            isPaused = false;   //Sets isPaused to false after the game is unpause
        }
        //This if-block statment is executed if pressing on the PLUS button on the keyboard
        else if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
        {
            Time.timeScale *= 2.0f; //increasing the simulation speed by a factor of 2.
            currentSimulationSpeed = Time.timeScale;  //Setting the current timescale to be the current simulation speed.
        }
        //This if-block statment is executed if pressing on the MINUS button on the keyboard
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {
            Time.timeScale /= 2.0f; //decreasing the simulation speed by a factor of 2.
            currentSimulationSpeed = Time.timeScale;    //Setting the current timescale to be the current simulation speed.
        }
    }

    //Randomly positions each trader at a random on a different alcove 
    void PositionTraders()
    {
        //loops through each alcove
        foreach(Transform child in alcoves.transform)
        { 
            int number; //An int variable used for storing a random integer between 1 and 8.
            GameObject myTrader;    //Used to refer to an instantiated trader

            //This do-while loop will keep on executing as long as the random int generated is not in the hashset.
            do
            {
                number = Random.Range(1, 9);

            } while (tradersNumbers.Contains(number));

            tradersNumbers.Add(number); //Adds that number to the Hashset if it has not been added yet.

            //Instantiate a trader at the position of the current alcove
            myTrader = Instantiate(trader, new Vector3(child.localPosition.x, 1.0f, child.localPosition.z), Quaternion.identity);
            myTrader.tag = "Trader" + number;   //Tag it with the random int generated in the current loop.
            myTrader.AddComponent<NavMeshObstacle>();   //Adds the NavMeshObstacle component to this trader's gameobject.
        }
    }

    //Sets the starting position of the thief and then activates it there
    void SetStartingPositionAndActivateThief()
    {
        thief.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 0.4f, Random.Range(-10.0f, 10.0f));
        thief.SetActive(true);
    }
}

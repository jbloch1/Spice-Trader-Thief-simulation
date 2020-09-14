using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/* A static class where all transaction methods are defined. The worldState argument is an int array of 14 elements whose 
 * first 7 elements corresponds to the number of each spice inside the inventory and the next 7 elements corresponds to the 
 * number of each spice inside the caravan. The first element in both the inventory and the caravan sections correspond to the number of Turmerics.
 * The second element in both the inventory and the caravan sections correspond to the number of Saffron units.
 * The third element in both the inventory and the caravan sections correspond to the number of Cardamom units. The fourth element in both the inventory and the
 * caravan sections correspond to the number of Cinnamon units. The fifth element in both the inventory and the caravan sections correspond to the number
 * of Cloves unit. The sixth element in both the inventory and the caravan sections correspond to the number of pepper units. The seventh element in both
 * the inventory and the caravan section correspond to the number of Sumac units. Each transaction method returns it's own effect (another int array) which is the
 * new world state. I ommited taking out a pepper and a sumac from the caravan for optimality. It would be useless to just
 * take out 1 pepper and 1 sumac from a caravan since it is unneccessary for trading. Also, each method returns an effect of type object which will be converted to int[] in 
 * methods within the GOAP that uses it because if I made the return type of each transaction an int[] and I want to return a null if the preconditions are not satisified, 
 * the program for some reason gives me an error, so in this way I avoid it.
 */
public static class Transactions
{

    /* In this private static method, the sum of all spices currently in the inventory is computed.
     * It is called by the transaction methods that needs to know the total amount of spices in the inventory. 
     */
    static int ComputeTotalSpicesInInventory(int[] worldState)
    {
        int totalSpices = 0;    /*This is a counter that is initialized to 0 at the beginning of the method and will be used to 
                                  store the total sum of all spices in the inventory.*/ 

        //This for loop loops through each element of the inventory section in the world state array and adds it to the above counter.
        for (int index = 0; index < worldState.Length / 2; index++)
        {
            totalSpices += worldState[index];
        }

        return totalSpices; //Returns the sum of all the spices in the inventory.
    }


    /* This transaction method is used to give the player-agent two units of Turmeric 
     * spice to store in the first element of the inventory.
     * */
    public static object Trader1(int[] worldState)
    {
        int[] effect = new int[14]; //This is used to store the effect of this method on the world state array after giving the agent two units of Turmerics.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState); 

        //Precondition: If there are currently at most a total of two units of spice in the inventory
        if (totalInventorySpices <= 2)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                {
                    effect[index] = worldState[index] + 2;
                }
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed
    }

    /*
     * This transaction method takes from the player agent's invetory 2 Turmeric units and gives in exchange 1 Saffron unit.
     * The agent will then store this Saffron unit in the inventory.
     */
    public static object Trader2(int[] worldState)
    {
        /* This is used to store the effect of this method on the world state array after trading 
         * 2 units of Turmeric spice with Trader 2 for 1 unit of Saffron.
         */
        int[] effect = new int[14];


        /* Precondition: If there are at least 2 units of Turmeric spice in the inventory.
         */
        if (worldState[0] >= 2)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                    effect[index] = worldState[index] - 2;

                else if (index == 1)
                    effect[index] = worldState[index] + 1;

                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }

        return null;    //Otherwise return null if this transaction was unable to be performed
    }


    /* This transaction method takes 2 Saffron units from the player-agent's inventory in exchange for
     * 1 unit of Cardamom. The agent will then store this Cardamom unit in the inventory.  
     */
    public static object Trader3(int[] worldState)
    {
        /* This is used to store the effect of this method on the world state array after trading 
         * 2 units of Saffron spice with Trader 3 for 1 Cardamom.
         */
        int[] effect = new int[14];


        /* Precondition: If there are at least 2 units of Saffron spice in the inventory.
         */
        if (worldState[1] >= 2)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 1)
                {
                    effect[index] = worldState[index] - 2;
                }
                else if (index == 2)
                {
                    effect[index] = worldState[index] + 1;
                }
                else
                {
                    effect[index] = worldState[index];
                }
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed
    }

    /* This transaction method takes 4 Turmeric units from the player-agent's inventory
     * in exchange of 1 cinnamon unit, which the agent will store in the inventory.
     */
    public static object Trader4(int[] worldState)
    {
        /* This is used to store the effect of this method on the world state array after trading 
         * 4 units of Turmeric spice with Trader 4 for 1 Cinnamon.
         */
        int[] effect = new int[14];

        /* Precondition: If there are 4 units of Turmeric spice in the inventory.
         */
        if (worldState[0] == 4)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                {
                    effect[index] = worldState[index] - 4;
                }
                else if (index == 3)
                {
                    effect[index] = worldState[index] + 1;
                }
                else
                {
                    effect[index] = worldState[index];
                }
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed
    }

    /* This transaction method takes from the agent's inventory 1 unit of Cardamom spice as well as 1 unit of Turmeric spice
     * in exchange of 1 unit of Cloves, which will then be stored in the player's inventory*/
    public static object Trader5(int[] worldState)
    {
        /* This is used to store the effect of this method on the world state array after trading 
         * 1 unit of Turmeric spice and 1 unit of Cardamom spice with Trader 5 for 1 Cloves spice.
         */
        int[] effect = new int[14];

        
        //Preconditions: If there are at least 1 unit of Turmeric and at least 1 unit of Cardamom in the inventory.
        if (worldState[0] >= 1 && worldState[2] >= 1)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                    effect[index] = worldState[index] - 1;
                else if (index == 2)
                    effect[index] = worldState[index] - 1;
                else if (index == 4)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed   
    }

    /* This transaction method takes 2 units of Turmeric spice, 1 unit of Saffron and 1 unit of Cinnamon from the agent's inventory in exchange of 1 unit of Pepper.
     */
    public static object Trader6(int[] worldState)
    {
        /* This is used to store the effect of this method on the world state array after trading 
         * 2 units of Turmeric spice, 1 unit of Saffron and 1 unit of Cinnamon in exchange for 1 pepper.
         */
        int[] effect = new int[14];

        /*
         * Preconditions: If there are 2 units of Turmeric, 1 unit of Saffron and 1 unit of cinnamon in the inventory
         */
        if (worldState[0] == 2 && worldState[1] == 1 && worldState[3] == 1)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                    effect[index] = worldState[index] - 2;
                else if (index == 1)
                    effect[index] = worldState[index] - 1;
                else if (index == 3)
                    effect[index] = worldState[index] - 1;
                else if (index == 5)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed
    }

    /*This transaction method takes in 4 units of Cardamom from the agent's inventory in exchange of 1 unit of Sumac.
     * This 1 unit of Sumac will be stored in the agent's inventory.
     */
    public static object Trader7(int[] worldState)
    {
        /*
         * This is used to store the effect of this method on the worldState after trading 4 units of Cardamom in exchange for 1 sumac.
         */
        int[] effect = new int[14];

        //Precondition: If there are 4 Cardamoms in the inventory
        if (worldState[2] == 4)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 2)
                    effect[index] = worldState[index] - 4;
                else if (index == 6)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    /*This transaction method takes in 1 saffron, 1 cinnamon, and 1 cloves unit from the agent's inventory in exchange of 1 sumac.
     * This 1 unit of Sumac will be stored in the agent's inventory.
     */
    public static object Trader8(int[] worldState)
    {
        /*
         * This is used to store the effect of this method on the world state array after trading 1 unit of Saffron,
         * 1 cinnamon, and 1 cloves unit from the agent's inventory in exchange for 1 sumac.
         */
        int[] effect = new int[14];

        
        //Preconditions: If there is 1 unit of saffron, 1 unit of cardamom, and 1 unit of cinnamon
        if (worldState[1] == 1 && worldState[3] == 1 && worldState[4] == 1)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 1)
                    effect[index] = worldState[index] - 1;
                else if (index == 3)
                    effect[index] = worldState[index] - 1;
                else if (index == 4)
                    effect[index] = worldState[index] - 1;
                else if (index == 6)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Turmeric from the inventory and putting it into the caravan.
    public static object Put1Turmeric(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after taking a Turmeric unit from the inventory and putting it into the caravan. 
        
        //Precondition: If the agent has at least one unit of Turmeric in the inventory
        if (worldState[0] > 0)
        {
            //Perform the transacton.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 0)
                    effect[index] = worldState[index] - 1;
                else if (index == 7)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Saffron from the inventory and putting it into the caravan.
    public static object Put1Saffron(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after taking a Saffron unit from the inventory and putting it into the caravan. 

        //Precondition: If the agent has at least one unit of Saffron in the inventory
        if (worldState[1] > 0)
        { 
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 1)
                    effect[index] = worldState[index] - 1;
                else if (index == 8)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Cardamom from the inventory and putting it into the caravan.
    public static object Put1Cardamom(int[] worldState)
    {
        //Effect after taking a Cardamom unit from the inventory and putting it into the caravan. 
        int[] effect = new int[14];

        //Precondition: If the agent has at least one unit of Cardamom in the inventory
        if (worldState[2] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 2)
                    effect[index] = worldState[index] - 1;
                else if (index == 9)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Cinnamon from the inventory and putting it into the caravan.
    public static object Put1Cinnamon(int[] worldState)
    {
        //Effect after taking a Cinnamon unit from the inventory and putting it into the caravan. 
        int[] effect = new int[14];

        //Precondition: If the agent has at least one unit of Cinnamon in the inventory
        if (worldState[3] > 0)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 3)
                    effect[index] = worldState[index] - 1;
                else if (index == 10)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Cloves from the inventory and putting it into the caravan.
    public static object Put1Cloves(int[] worldState)
    {
        //Effect after taking a Cloves unit from the inventory and putting it into the caravan. 
        int[] effect = new int[14];

        //Precondition: If the agent has at least one unit of Cloves in the inventory
        if (worldState[4] > 0)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 4)
                    effect[index] = worldState[index] - 1;
                else if (index == 11)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Pepper from the inventory and putting it into the caravan.
    public static object Put1Pepper(int[] worldState)
    {
        //Effect after taking 1 Pepper unit from the inventory and putting it into the caravan.
        int[] effect = new int[14];

        //Precondition: If the agent has at least one unit of Pepper in the inventory
        if (worldState[5] > 0)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 5)
                    effect[index] = worldState[index] - 1;
                else if (index == 12)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for taking 1 unit of Sumac from the inventory and putting it into the caravan.
    public static object Put1Sumac(int[] worldState)
    {
        //Effect after taking 1 Sumac unit from the inventory and putting it into the caravan.
        int[] effect = new int[14];

        //Precondition: If the agent has at least one unit of Sumac in the inventory
        if (worldState[6] > 0)
        {
            //Perform the transaction
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 6)
                    effect[index] = worldState[index] - 1;
                else if (index == 13)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for getting out 1 Turmeric from the caravan and putting it into the inventory.
    public static object Get1Turmeric(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after Getting out 1 Turmeric unit from the caravan and putting it into the inventory.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState);

        //Preconditions: If the total amount of spices in the inventory is currently less than 4, and there is at least 1 Turmeric in the caravan
        if (totalInventorySpices < 4 && worldState[7] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 7)
                    effect[index] = worldState[index] - 1;
                else if (index == 0)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for getting out 1 Saffron from the caravan and putting it into the inventory.
    public static object Get1Saffron(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after Getting out 1 Saffron unit from the caravan and putting it into the inventory.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState);

        //Preconditions: If the total amount of spices in the inventory is currently less than 4, and there is at least 1 Saffron in the caravan
        if (totalInventorySpices < 4 && worldState[8] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 8)
                    effect[index] = worldState[index] - 1;
                else if (index == 1)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for getting out 1 Cardamom from the caravan and putting it into the inventory.
    public static object Get1Cardamom(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after Getting out 1 Cardamom unit from the caravan and putting it into the inventory.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState);

        //Preconditions: If the total amount of spices in the inventory is currently less than 4, and there is at least 1 Cardamom in the caravan
        if (totalInventorySpices < 4 && worldState[9] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 9)
                    effect[index] = worldState[index] - 1;
                else if (index == 2)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for getting out 1 Cinnamon from the caravan and putting it into the inventory.
    public static object Get1Cinnamon(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after Getting out 1 Cinnamon unit from the caravan and putting it into the inventory.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState);

        //Preconditions: If the total amount of spices in the inventory is currently less than 4, and there is at least 1 Cinnamon in the caravan
        if (totalInventorySpices < 4 && worldState[10] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 10)
                    effect[index] = worldState[index] - 1;
                else if (index == 3)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }

    //This transaction method is used for getting out 1 Cloves from the caravan and putting it into the inventory.
    public static object Get1Cloves(int[] worldState)
    {
        int[] effect = new int[14]; //Effect after Getting out 1 Cloves unit from the caravan and putting it into the inventory.

        //Getting the total number of spices in the inventory and storing the result in the totalInventorySpices variable.
        int totalInventorySpices = ComputeTotalSpicesInInventory(worldState);

        //Preconditions: If the total amount of spices in the inventory is currently less than 4, and there is at least 1 Cloves in the caravan
        if (totalInventorySpices < 4 && worldState[11] > 0)
        {
            //Perform the transaction.
            for (int index = 0; index < worldState.Length; index++)
            {
                if (index == 11)
                    effect[index] = worldState[index] - 1;
                else if (index == 4)
                    effect[index] = worldState[index] + 1;
                else effect[index] = worldState[index];
            }
            return effect;  //Return the transaction effect.
        }
        return null;    //Otherwise return null if this transaction was unable to be performed.
    }
}

//This class represents part of a path to reach the goal.
public class Segment
{
    int priority;   //This is going to be the fCost of the current state in this instance.
    string transactionName;  //This is going to be the name of one of the transaction methods that generated the current state in this segment.
    int[] currentState; //This stores the current state of the planner
    Segment parent; //This will refer to the segement from where it's current state generated the current state of this segment.

    public Segment(int priority, string transactionName, int[] currentState)
    {
        this.priority = priority;
        this.transactionName = transactionName;
        this.currentState = currentState;
    }

    public void SetParent(Segment seg)
    {
        parent = seg;
    }
    

    public int GetPriority()
    {
        return priority;
    }

    public int[] GetCurrentState()
    {
        return currentState;
    }

    public string GetTransactionName()
    {
        return transactionName;
    }

    public Segment GetParent()
    {
        return parent;
    }
}

//Here is where all the planning of the AI agent occurs.
public class GOAP : MonoBehaviour
{
     //This will refer to the world state of the agent.
     /* The first 7 elements corresponds to the amount of each spice inside the inventory and the next 7 elements corresponds to the 
     * amount of each spice inside the caravan.The first element in both the inventory and the caravan sections correspond to the amount of Turmerics.
     * The second element in both the inventory and the caravan sections both correspond to the amount of Saffrons.
     * The third element in both the inventory and the caravan sections correspond to the amount of Cardamoms.The fourth element in both the inventory and the
     * caravan sections correspond to the amount of Cinnamon spices. The fifth element in both the inventory and the caravan sections correspond to the amount
     * of Cloves unit. The sixth element in both the inventory and the caravan sections correspond to the amount of pepper units. The sixth element in both
     * the inventory and the caravan section correspond to the amount of Sumac spices.*/
    int[] worldState = new int[14];
    
    GameObject agent;   //Refers to the agent from the inspector

    //http://www.unitygeek.com/delegates-events-unity/
    delegate object Transaction(int[] precondition);    //Delegate is some kind of a pointer to a method that will allow me to treat a method as a variable and pass it as argument to any function that takes this method as parameter.
    List<Segment> segmentsOpenList = new List<Segment>();   //This is the open list.
    List<Segment> segmentsClosedList = new List<Segment>(); //This is the closed list.
    List<Transaction> transactions = new List<Transaction>();   //This is a list of all the transaction methods in the public static Transactions class 
    List<Segment> fullPath = new List<Segment>();   //This is a list of segments that will eventually lead the agent from start to goal.
    bool wasRobbed = false; //This is a boolean for the GOAP to remind itself that the agent has been robbed and that it's time to replan at a next frame.

    // Start is called before the first frame update
    void Start()
    {
        agent = GameObject.Find("Agent");
        FillInActionsList();
        BestFirstSearch();  //Calls function to plan agent's behavior.

        /*Calls the SetPathList method in the AgentController method passing to it 
          as argument the whole completed path.*/
        agent.GetComponent<AgentController>().SetPathList(fullPath);    
    }

    //Here I add all the Transactions class transaction methods into the transactions list.
    public void FillInActionsList()
    {
        transactions.Add(Transactions.Trader1);
        transactions.Add(Transactions.Trader2);
        transactions.Add(Transactions.Trader3);
        transactions.Add(Transactions.Trader4);
        transactions.Add(Transactions.Trader5);
        transactions.Add(Transactions.Trader6);
        transactions.Add(Transactions.Trader7);
        transactions.Add(Transactions.Trader8);
        transactions.Add(Transactions.Put1Turmeric);
        transactions.Add(Transactions.Put1Saffron);
        transactions.Add(Transactions.Put1Cardamom);
        transactions.Add(Transactions.Put1Cinnamon);
        transactions.Add(Transactions.Put1Cloves);
        transactions.Add(Transactions.Put1Pepper);
        transactions.Add(Transactions.Put1Sumac);
        transactions.Add(Transactions.Get1Turmeric);
        transactions.Add(Transactions.Get1Saffron);
        transactions.Add(Transactions.Get1Cardamom);
        transactions.Add(Transactions.Get1Cloves);
    }

    void Update()
    {
        //If the GOAP still remembers that the agent was robbed
        if(wasRobbed)
        {
            wasRobbed = false;  //It will set wasRobbed to false before doing the replan.

            Replan();   //Calls the Replan method to replan everything from the current state of the agent due to robbery.
        }

        //If the thief robbed the player
        if(GameObject.Find("Thief").GetComponent<ThiefController>().HasRobbed())
        {
            wasRobbed = true;   //Goap sets this condition to true so that it will remember that the player was robbed and then call the Replan method in the next frame.
            GameObject.Find("Thief").GetComponent<ThiefController>().ThieveryDiscovered();  //GOAP lets the ThiefController know that it just found out the thief robbed the player
            worldState = GameObject.Find("ProgressTracker").GetComponent<TableManager>().GetCurrentState(); //Calls method to ask ProgressTracker to return current state in UI table.
            GameObject.Find("Displayer").GetComponent<TransactionsDisplayer>().Reset();  //Clears the whole scroll view from all the plans that are now useless after robbery has happened. 
            agent.GetComponent<AgentController>().SetIsReplanning(true);    //Let's the AgentController know that it's time to replan everything from the current state.
        }

    }

    //Used for replanning from the state that the player was robbed.
    void Replan()
    {
        fullPath.Clear();   //Clearing the whole paths list.

        //Passing the empty list as argument to the SetFullPath method in the AgentController to stop the player from executing these plans.
        agent.GetComponent<AgentController>().SetPathList(fullPath);    
        
        BestFirstSearch();  //Call BFS to execute the replanning.
        
        //Passes the new full path list containing the whole new plan from the current state for the player to reach the goal.
        agent.GetComponent<AgentController>().SetPathList(fullPath);
    }

    //This method is used for generating all the successors of the current state and instantiating an instance of class Segment for each one.
    void GenerateSuccessors(Segment segment)
    {
        int[] newState; //This will be the world state of the successor of the current.
        object obj; //This is used to store the result of each transaction method.
        int gCost = 0;  //gCost
        int hCost = 0;  //hCost
        int fCost = 0;  //fCost
        List<Segment> sortedSegments = new List<Segment>(); //Ths will be used as temporary.
        int[] spiceValue = new int[7];   /*There are 7 elements in this array since there are 7 spices. Turmeric will have a value of 1, 
            whereas other spice values are the sum of the values of each spice used to change for them + 1*/
        spiceValue[0] = 1;  //Turmeric will have a value of 1.
        spiceValue[1] = 2 * spiceValue[0] + 1;  //Saffron will have a spice value of 3.
        spiceValue[2] = 2 * spiceValue[1] + 1; //Cardamom will have a spice value of 7.
        spiceValue[3] = 4 * spiceValue[0] + 1; //Cinnamon will have a spice value of 5.
        spiceValue[4] = spiceValue[0] + spiceValue[2] + 1; //Cloves will have a spice value of 9.
        spiceValue[5] = 2 * spiceValue[0] + spiceValue[1] + spiceValue[3] + 1; //Pepper will have a spice value of 11
        spiceValue[6] = spiceValue[1] + spiceValue[3] + spiceValue[4] + 1; //Sumac will have a spice value of 17 (This is how ai agent trades with trader 8 to get a sumac.)

        int[] goalState = new int[7] { 2, 2, 2, 2, 2, 2, 2 };   //This is the desired goal, which is to have 2 of each spice in the caravan.

        //Attempting each transaction method in order to produce a successor.
        foreach (Transaction transaction in transactions)
        {
            //This will store the return value of the current transaction.
            obj = transaction(segment.GetCurrentState());

            if (obj != null)    //If the return value is not null
            {
                newState = (int[])obj;  //Parse the result into an int[] datatype.

                /*This is my heuristic hCost function: sigma(spiceValue[i] * Max(goalState[i] - newState[i] - newState[i+7], 0) + 
                 * Max(goalState[i] -newState[i+7])) from i = 0 to i = 6 where the i = 0 is for Turmeric, i = 1 is for Saffron, i = 2 is for Cardamom,
                 * i = 3 is for Cinnamon, i = 4 is for Cloves, i = 5 is for Pepper and i = 6 is for Sumac. For the first term where I multiply
                 * spiceValue[i] by Max(goalState[i] - newState[i] - newState[i+7], 0), it is used for taking into account that the player either keeps all the spices that 
                 * it still has either in it's inventory or caravan, then multiplying by the spiceValue is just for making the heuristic optimal by making the most complicated spices such as 
                 * pepper and sumac more expensive while making the easier spices such as turmeric, Saffron, Cardamom, Cinnamon and Cloves cheaper. So now the other term Max(goalState[i] -newState[i+7]) ensures that whatever 
                 * number of the spice type units the player has in the inventory, two units of each have to be added to the caravan at it's corresponding element to reach the goal state. 
                 * It's because the first term has to do with the fact that the player stores these spices and it is still unknown where the player keeps the spices.
                 */
                hCost = (spiceValue[0] * Mathf.Max(goalState[0] - newState[0] - newState[7], 0) + Mathf.Max(goalState[0] - newState[7],0)) + 
                    (spiceValue[1] * Mathf.Max(goalState[1] - newState[1] - newState[8], 0) + Mathf.Max(goalState[1] - newState[8], 0))
                    + (spiceValue[2] * Mathf.Max(goalState[2] - newState[2] -newState[9], 0) + Mathf.Max(goalState[2] - newState[9], 0)) + 
                    (spiceValue[3] * Mathf.Max(goalState[3] - newState[3] - newState[10], 0) + Mathf.Max(goalState[3] - newState[10], 0))
                    + (spiceValue[4] * Mathf.Max(goalState[4] - newState[4] - newState[11], 0) + Mathf.Max(goalState[4] - newState[11],0)) + 
                    (spiceValue[5] * Mathf.Max(goalState[5] - newState[5] - newState[12], 0) + Mathf.Max(goalState[5] - newState[12], 0))
                    + (spiceValue[6] * Mathf.Max(goalState[6] - newState[6] - newState[13],0) + Mathf.Max(goalState[6] - newState[13], 0));
                    
                fCost = gCost + hCost;  //Calculating the fCost
                
                /*Creating a new instance of class Segment for the successor of the current world state which is the new world state
                 * passing as argument the fCost, the name of the transaction method that used the current world state to generate it's current successor.
                 */
                Segment newSegment = new Segment(fCost, transaction.Method.Name, newState);

                CompareState(newSegment);  //Calls the method to see if there is a list with a segment that contains the same world state but only with a lower fCost

                newSegment.SetParent(segment);  //Setting the parent of the new segment which stores the new world state to be the current segment which stores the current world state.
            }
        }

        //Takes in all the elements from the segmentsOpenList in ascending order of their fCost(i.e. their priority).
        sortedSegments = segmentsOpenList.OrderBy(seg => seg.GetPriority()).ToList();
        segmentsOpenList.Clear();   //Clears the segmentsOpenList.
        segmentsOpenList.AddRange(sortedSegments);  //Puts all the elements it had before back in, only in a sorted ascending order of the fCost
    }

    //This method is used to check if the open and closed lists of the segements contain the same state with a lower priority than what the segment passed in has.
    void CompareState(Segment segment)
    {
        bool isInsideOpen = false;  //Boolean condition for determining if the openlist has a state with the same contents as the segment argument does.
        
        foreach(Segment seg in segmentsOpenList)
        {
            bool isEqual = true;    //Start by assuming that the openList contains a state with the same contents as the state stored inside the argumet segment. 
            
            
            for(int index = 0; index < 14; index++)
            {
                //Sets isEqual to false if there is a content that is not the same in both objects.
                if(segment.GetCurrentState()[index] != seg.GetCurrentState()[index])
                {
                    isEqual = false;
                }
            }

            //If the openlist contains a segment with a world state that has all same contents as the one in argument segment
            if (isEqual)
            {
                isInsideOpen = true;    //Specifying that there is a segment in the openlist that contains the same state as the argument does.
                
                //If the argument's state has lower fCost than the one already inside the openlist, add that the argument into the openlist. Else, discard of it.
                if (seg.GetPriority() > segment.GetPriority())
                {
                    segmentsOpenList.Add(segment);
                    break;
                }
            }
        }


        bool isInsideClosed = false;    //Boolean condition for determining if the closedlist has a state with the same contents as the segment argument does.
        foreach (Segment seg in segmentsClosedList)
        {
            bool isEqual = true;    //Start by assuming that the closedList contains a state with the same contents as the state stored inside the argumet segment. 
            
            for (int index = 0; index < 14; index++)
            {
                //Sets isEqual to false if there is a content that is not the same in both objects.
                if (segment.GetCurrentState()[index] != seg.GetCurrentState()[index])
                {
                    isEqual = false;
                }
            }

            //If the closedlist contains a segment with a world state that has all same contents as the one in argument segment
            if (isEqual)
            {
                isInsideClosed = true;  //Specifying that there is a segment in the closedlist that contains the same state as the argument does.

                //If the argument's state has lower fCost than the one already inside the closedlist, add that the argument into the closedlist. Else, discard of it.
                if (seg.GetPriority() > segment.GetPriority())
                {
                    segmentsOpenList.Add(segment);
                    break;
                }
            }
        }


        //If the argument segment is in neither the open nor the closed list, add the argument segment to the open list.
        if (!isInsideOpen && !isInsideClosed)
        {
            segmentsOpenList.Add(segment);
        }
    }


    //Here the Best First Search method is used to do the planning of the A.I. agent
    //https://www.geeksforgeeks.org/best-first-search-informed-search/
    public void BestFirstSearch()
    {
        //Enters the current woldstate into the open list of the segments, with a priority fCost of 0, and no action name.
        segmentsOpenList.Add(new Segment(0, "", worldState));

        //While the open list of all the segments is not empty
        while (segmentsOpenList.Count != 0) 
        { 
            Segment segment = segmentsOpenList[0];  //Get the first element in the open list and store it in a variable segment of type Segment
            segmentsOpenList.RemoveAt(0);   //Remove the first element from the open list.

            //If the goal state was reached
            if(GoalIsReached())
            {
                segmentsOpenList.Clear();   //Clears the open list
                segmentsClosedList.Clear(); //Clears the closed list

                Segment current = segment;  
                List<Segment> temp = new List<Segment>();   //Temporary list used for making the start state as the first element in the full paths list and then making the las element in the fullpath list as the goal state
                
                //While current is not null
                while (current != null)
                {
                    temp.Add(current);  //Add it to the temp list
                    current = current.GetParent();  //Refer current to its parent
                }
                
                //Add the nodes in reverse order from temp.count-1 to 0 into the fullPathList.
                for(int tempIndex = temp.Count -1; tempIndex > 0; tempIndex--)
                {
                    fullPath.Add(temp[tempIndex]);

                    //Add current transaction description to scrollview
                    GameObject.Find("Displayer").GetComponent<TransactionsDisplayer>().AddToDisplay(temp[tempIndex].GetTransactionName());
                }
                break;  //Break out of loop.
            }
            else //Otherwise
            {
                GenerateSuccessors(segment);    //Generate the successors of the current segment
                worldState = segment.GetCurrentState(); //Gets it's worldstate.
            }
        }
    }

    //Returns true or false if there are at least 2 of each spice in the caravan or not respectively. 
    bool GoalIsReached()
    {
        return worldState[7] >= 2 && worldState[8] >= 2 && worldState[9] >= 2 &&
            worldState[10] >= 2 && worldState[11] >= 2 && worldState[12] >= 2
            && worldState[13] >= 2;
    }
}


using UnityEngine;
using UnityEngine.UI;

//This class is used for referring to and for adding numbers to the UI table
public class TableManager : MonoBehaviour
{
    GameObject turmericsInInventory;    //gameobject reffering to a cell on the table which is used for storing the amount of Turmeric units in the inventory
    GameObject saffronsInInventory;  //gameobject reffering to a cell on the table which is used for storing the amount of Saffron units in the inventory
    GameObject cardamomsInInventory;     //gameobject reffering to a cell on the table which is used for storing the amount of Cardamom units in the inventory
    GameObject cinnamonsInInventory; //gameobject reffering to a cell on the table which is used for storing the amount of Cinnamon units in the inventory
    GameObject clovesInInventory;   //gameobject reffering to a cell on the table which is used for storing the amount of Cloves units in the inventory
    GameObject peppersInInventory;  //gameobject reffering to a cell on the table which is used for storing the amount of Pepper units in the inventory
    GameObject sumacsInInventory;   //gameobject reffering to a cell on the table which is used for storing the amount of Sumac units in the inventory
    GameObject turmericsInCaravan;  //gameobject reffering to a cell on the table which is used for storing the amount of Turmeric units in the caravan 
    GameObject saffronsInCaravan;   //gameobject reffering to a cell on the table which is used for storing the amount of Saffron units in the caravan
    GameObject cardamomsInCaravan;  //gameobject reffering to a cell on the table which is used for storing the amount of Cardamom units in the caravan
    GameObject cinnamonsInCaravan;  //gameobject reffering to a cell on the table which is used for storing the amount of Cinnamon units in the caravan
    GameObject clovesInCaravan;     //gameobject reffering to a cell on the table which is used for storing the amount of Cloves units in the caravan
    GameObject peppersInCaravan;    //gameobject reffering to a cell on the table which is used for storing the amount of Pepper units in the caravan
    GameObject sumacsInCaravan; //gameobject reffering to a cell on the table which is used for storing the amount of Sumac units in the caravan 


    // Start is called before the first frame update
    void Start()
    {
        turmericsInInventory = transform.GetChild(0).gameObject;
        saffronsInInventory = transform.GetChild(1).gameObject;
        cardamomsInInventory = transform.GetChild(2).gameObject;
        cinnamonsInInventory = transform.GetChild(3).gameObject;
        clovesInInventory = transform.GetChild(4).gameObject;
        peppersInInventory = transform.GetChild(5).gameObject;
        sumacsInInventory = transform.GetChild(6).gameObject;
        turmericsInCaravan = transform.GetChild(7).gameObject;
        saffronsInCaravan = transform.GetChild(8).gameObject;
        cardamomsInCaravan = transform.GetChild(9).gameObject;
        cinnamonsInCaravan = transform.GetChild(10).gameObject;
        clovesInCaravan = transform.GetChild(11).gameObject;
        peppersInCaravan = transform.GetChild(12).gameObject;
        sumacsInCaravan = transform.GetChild(13).gameObject;
    } 

    //This method is called with the current state to be displayed on the UI.
    public void PopulateTable(int[] currentState)
    {
        turmericsInInventory.GetComponent<Text>().text = "" + currentState[0];
        saffronsInInventory.GetComponent<Text>().text = "" + currentState[1];
        cardamomsInInventory.GetComponent<Text>().text = "" + currentState[2];
        cinnamonsInInventory.GetComponent<Text>().text = "" + currentState[3];
        clovesInInventory.GetComponent<Text>().text = "" + currentState[4];
        peppersInInventory.GetComponent<Text>().text = "" + currentState[5];
        sumacsInInventory.GetComponent<Text>().text = "" + currentState[6];
        turmericsInCaravan.GetComponent<Text>().text = "" + currentState[7];
        saffronsInCaravan.GetComponent<Text>().text = "" + currentState[8];
        cardamomsInCaravan.GetComponent<Text>().text = "" + currentState[9];
        cinnamonsInCaravan.GetComponent<Text>().text = "" + currentState[10];
        clovesInCaravan.GetComponent<Text>().text = "" + currentState[11];
        peppersInCaravan.GetComponent<Text>().text = "" + currentState[12];
        sumacsInCaravan.GetComponent<Text>().text = "" + currentState[13];
    }

    //This method is called to get the current state on the UI table and returns an int array of 14 elements storing the numbers of each of the gameobjects. 
    public int[] GetCurrentState()
    {
        int[] currentState = new int[14];

        currentState[0] = int.Parse(turmericsInInventory.GetComponent<Text>().text);
        currentState[1] = int.Parse(saffronsInInventory.GetComponent<Text>().text);
        currentState[2] = int.Parse(cardamomsInInventory.GetComponent<Text>().text);
        currentState[3] = int.Parse(cinnamonsInInventory.GetComponent<Text>().text);
        currentState[4] = int.Parse(clovesInInventory.GetComponent<Text>().text);
        currentState[5] = int.Parse(peppersInInventory.GetComponent<Text>().text);
        currentState[6] = int.Parse(sumacsInInventory.GetComponent<Text>().text);
        currentState[7] = int.Parse(turmericsInCaravan.GetComponent<Text>().text);
        currentState[8] = int.Parse(saffronsInCaravan.GetComponent<Text>().text);
        currentState[9] = int.Parse(cardamomsInCaravan.GetComponent<Text>().text);
        currentState[10] = int.Parse(cinnamonsInCaravan.GetComponent<Text>().text);
        currentState[11] = int.Parse(clovesInCaravan.GetComponent<Text>().text);
        currentState[12] = int.Parse(peppersInCaravan.GetComponent<Text>().text);
        currentState[13] = int.Parse(sumacsInCaravan.GetComponent<Text>().text);

        return currentState;
    }
}


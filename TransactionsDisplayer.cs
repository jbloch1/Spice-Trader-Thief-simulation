using UnityEngine;
using UnityEngine.UI;

public class TransactionsDisplayer : MonoBehaviour
{
    //Method called for adding to the scrollview's display a description of a transaction
    public void AddToDisplay(string transactionName)    //I made the first character of each line a blank space to make it display nicely on the scroll view. 
    {
        string transaction;

        if (transactionName.Equals("Trader1"))
        {
            transaction = " Gets 2 turmerics from Trader 1.";
        }
        else if (transactionName.Equals("Trader2"))
        {
            transaction = " Trades 2 turmerics for 1 saffron with Trader 2.";
        }
        else if (transactionName.Equals("Trader3"))
        {
            transaction = " Trades 2 saffrons for 1 cardamom with Trader 3.";
        }
        else if (transactionName.Equals("Trader4"))
        {
            transaction = " Trades 4 turmerics for 1 cinnamon with Trader 4.";
        }
        else if (transactionName.Equals("Trader5"))
        {
            transaction = " Trades 1 cardamom and 1 turmeric for 1 cloves unit with Trader 5.";
        }
        else if (transactionName.Equals("Trader6"))
        {
            transaction = "Trades 2 turmerics, 1 saffron and 1 cinnamon for 1 pepper with Trader 6.";
        }
        else if (transactionName.Equals("Trader7"))
        {
            transaction = " Trades 4 cardamoms for 1 sumac with Trader 7.";
        }
        else if (transactionName.Equals("Trader8"))
        {
            transaction = "Trades 1 saffron, 1 cinnamon and 1 cloves unit for 1 sumac with Trader 8.";
        }
        else if (transactionName.Equals("Put1Turmeric"))
        {
            transaction = " Puts 1 turmeric unit into the caravan";
        }
        else if (transactionName.Equals("Put1Saffron"))
        {
            transaction = " Puts 1 saffron unit into the caravan";
        }
        else if (transactionName.Equals("Put1Cardamom"))
        {
            transaction = " Puts 1 cardamom unit into the caravan";
        }
        else if (transactionName.Equals("Put1Cinnamon"))
        {
            transaction = " Puts 1 cinnamon unit into the caravan";
        }
        else if (transactionName.Equals("Put1Cloves"))
        {
            transaction = " Puts 1 cloves unit into the caravan";
        }
        else if (transactionName.Equals("Put1Pepper"))
        {
            transaction = " Puts 1 pepper unit into the caravan";
        }
        else if (transactionName.Equals("Put1Sumac"))
        {
            transaction = " Puts 1 sumac unit into the caravan";
        }
        else if (transactionName.Equals("Get1Turmeric"))
        {
            transaction = " Gets 1 turmeric unit from the caravan";
        }
        else if (transactionName.Equals("Get1Saffron"))
        {
            transaction = " Gets 1 saffron unit from the caravan";
        }
        else if (transactionName.Equals("Get1Cardamom"))
        {
            transaction = " Gets 1 cardamom unit from the caravan";
        }
        else if (transactionName.Equals("Get1Cinnamon"))
        {
            transaction = " Gets 1 cinnamon unit from the caravan";
        }
        else if (transactionName.Equals("Get1Cloves"))
        {
            transaction = " Gets 1 cloves unit from the caravan";
        }
        else
        {
            transaction = "";
        }

        //Adds the line that discribes a transaction to the scroll view.
        gameObject.GetComponent<Text>().text += transaction + System.Environment.NewLine;
    }
    
    //Deletes the first line (i.e. action alreay performed line) on the scroll view.
    //https://answers.unity.com/questions/598015/removing-the-first-line-of-a-string.html
    public void DeletePerformedTransaction()
    {
        if(gameObject.GetComponent<Text>().text.Length > 0)
        {
            int index = gameObject.GetComponent<Text>().text.IndexOf(System.Environment.NewLine);
            gameObject.GetComponent<Text>().text = gameObject.GetComponent<Text>().text.Substring(index + System.Environment.NewLine.Length);
        }   
    }

    //Method called from GOAP to clear the scroll view from all useless planners.
    public void Reset()
    {
        gameObject.GetComponent<Text>().text = string.Empty;
    }
}

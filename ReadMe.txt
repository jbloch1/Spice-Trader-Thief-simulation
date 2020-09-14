When openning the unity editor of the game, the main static play consists of a large area, with a caravan in the 
center surrounded by three small walls. On each edge of the floor where the caravan is centered, there are 2 alcoves. Also,
the AI player-agent which is modelled as the red capsule is positioned next to a wall of the caravan.
I make the thief and each of the traders inactive in the scene mode when game is not run.

After clicking the play button to start running the game, there will be 8 traders each modelled as a purple capsule which is bigger than the player, 
and each one of them are positioned on a different alcove tagged with a different name. One of them is tagged as Trader1, the second one is tagged as Trader2, 
the third is tagged as Trader3, the fourth is tagged as Trader4, the fifth is tagged as Trader5, the sixth is tagged as Trader6, 
the seventh is tagged as Trader7, and the eighth trader is tagged as Trader8. Each trader is randomly positioned on 
a different alcove throughout each playthrough. Then the thief is modelled as the smallest capsule which is colored in green and is
randomly positioned anywhere on the area just not on the alcoves. 


In this folder, a separate document describing the world state, the goal and all the actions is provided in the current folder 
called separateDocument.pdf. 

The only two actions that I did not define is the action of taking out 1 unit of pepper from the caravan and also 
I did not define the action of taking out 1 unit of sumac from the caravan because I figured that these two actions would 
be useless since Pepper and Sumac do not need to be traded with any of the traders in order to achieve a different spice
and that they would make the planning run more slow than if they were not defined. Therefore, I think it is enough to simply define
just 20 actions wwhere the first 8 of them is for doing the trading, the next 7 of them is for putting 1 unit of a spice from the 
inventory and into the caravan. Then the next 5 is for getting 1 item of a different type of spice from the caravan. 
I call all these actions transactions in my codes, I hope it's ok.


I thought this assignment has a lot of challenging aspects when building the Planner for the AI player agent.



I think the game works well. Off course there can always be some flaws. My player agent is able to go from having all
zeroes in the inventory and in the caravan to achieving the goal of keeping two units of each spice(can be more than 2) in the caravan, 
regardless of what is inside the inventory by using the A* algorithm in best first search method.

When running the game, you will find out that sometimes after the player finishes to trade with a trader, it gets stuck trying to pass 
through the trader. If it does for a long time, stop the game and then replay it again and you will see that this can coincidently 
happen but not on every playthrough. I am not sure if it's me or if it's just unity itself.


Another flaw that I see in the code is that sometimes, when the player is at the alcove and is about to trade with a trader and then 
the thief stole from the player at that position, like say the world state of the player already has only two turmerics in the inventory
and is at the alcove of Trader2, and is about to trade the two Turmerics with a Saffron. After the thieving at the 
position close Trader2 on the alcove occurs, the player now has only one turmeric in the inventory because the other one was stolen. So the player is being 
instructed to move from the position close to Trader2 to the position closer to Trader1 to get two Turmerics but at the same time, it may also 
coincidently happen that the player stops a short distance along the way, then it shows in the scroll view that the player has already done the task by having the top line on 
the scrollview be removed and the UI table then shows a world state of having 3 turmerics in the inventory and then moves back to Trader2 
to get 1 Saffron in exchange of two of the Turmerics that the player already has in the inventory, and now the world state of the player is to 
have 1 Turmeric and 1 Saffron in the inventory. I am currently not sure how to fix this bug but I do hope it's not a major issue and that
it might be a problem with unity. Unless maybe I did not check well if the player has still a path or not.



I am not sure the payer and the thief are allowed to enter into the alcove but I anyways let them go in and they both do 
not penenetrate the traders nor the walls. I even posted a question on the discussion board regarding this today at 12:17 pm to make sure
but since there was no reply, I just decided to let the players enter the alcove.

I also am not sure if the UI table also has to look like a 1D array with 14 elements in it with the first seven elements referring to the 
inventory and the other seven to the caravan. I just made the UI table look exactly the same as what was asked in the criteria for this assignment.

I hope my comments in the coe are clear as I spent a lot of time making it look nice and clear and understandable. Same in the separatedDocument, 
which is called SeparatedDocument.pdf.




 
 
 
 




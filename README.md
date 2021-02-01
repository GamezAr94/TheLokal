# Coffee Shop Game - A* Path Finding

## Details
Implementation of A* path finding algorithm into a coffee shop game.
1. The customers (*black circles*) appear outside the coffee shop and they will find the path from their position to the cashier position.
2. The customers (*black circles*) will make a line to order a coffee (the first in the line is always the firs customer to enter in the restaurant), 
3. Once the first customer (*black circle*) finish ordering, it will find the path from their position to a chair,
4. the rest of the customer will advance to the next position in line (and they will change their color to red).
5. Few seconds after they order, a coffee will apear in the counter and the player (*pink square*) can grab the coffee and bring it to the customers,
6. Every customers has their own type of coffee when they appear. 
7. If there are zero empty chairs the customers will wait until a chair is available and the customer will find a path to that chair.
8. few seconds after the customer is sitting in a chair it will leave the restauran and it will change its color to blue.

## Instructions
1. the players moves with the arrows and the A, S, D, W keys
2. right click with the mouse to appear customers
3. place the player in front of a drink in the counter and press SPACE key to grab it.

## Technical Specifications 
- C#
- Unity2D
- A* path finding
- Queues
- Enumerators 
- Isometric view
- Grid
- Adobe Illustrator

## What I learn
- How to implement the A* algorithm to a game
- Use queues to remove the first customer to the line.

## Screen Shot
> empty objects as chairs, cashier position and green circles as a places where the coffee will appear
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/theLokal_scene.png)

> Customers (black circles) appearing from outside the restaurant
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/theLokal_customer_coming.png)

> Customers doing a line to order a coffee
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/theLokal_customer_line.png)

> The coffees taken by the player will be displayed in the UI. customer leaving after few seconds of being in the chair (*Blue Circle*)
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/theLokal_customer_grabing_coffees.png)

## Assets Creaated for this proyect 
> chairs
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/Furniture/chairs.png)

> UI 
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/UI_Sprites.png)

> Cashier
![alt text](https://github.com/GamezAr94/TheLokal/blob/master/The%20Lokal/Assets/Sprites/Zara_Sprites.png)

## IMPORTANT
```
The game isn't finished, the player can leaves the drinks and there is not many sprites in the game such as chairs and UI
game performance is slow, ***THIS GAME IS MEANT TO BE AN EXPERIMENT TO IMPLEMENT A PATH FINDING ALGORITHM*** and that goal was successfully achieved 
this algorithm is slow because it compares almost every cell, the algorithm will compare all cells at runtime every time a customer is created.
***More features will be added to this game in the future***
```

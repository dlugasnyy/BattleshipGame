# BattleshipGame
- four grids, two for each player
- the grids are square – 10×10 – and the individual squares in the grid are identified by letter and number
- on one grid the player arranges ships and records the shots by the opponent. On the other grid the player records their own shots.
- before the game, each pleayer sets their ships on the first grid. 
- each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically. 
- the number of squares for each ship is determined by the type of the ship. The ships cannot overlap (i.e., only one ship can occupy any given square in the grid). 
- the types and numbers of ships allowed are the same for each player. 
- the 5 ships are:  Carrier (occupies 5 spaces), Battleship (4), Cruiser (3), Submarine (3), and Destroyer (2).  

After the ships have been positioned, the game proceeds in a series of rounds. In each round, each player takes a turn to announce a target square in the opponent's grid which is to be shot at. The opponent announces whether or not the square is occupied by a ship. 
If it is a "hit", the player who is hit marks this on their own or "ocean" grid (with a red peg in the pegboard version). 
The attacking player marks the hit or miss on their own "tracking" or "target" grid in order to build up a picture of the opponent's fleet.

When all of the squares of a ship have been hit, the ship's owner announces the sinking of the Carrier, Submarine, Cruiser/Destroyer/Patrol Boat, or the titular Battleship. If all of a player's ships have been sunk, the game is over and their opponent wins. If all ships of both players are sunk by the end of the round, the game is a draw.

-first thoughts:
 - player should have their own ships
 - player should have 2 boards, one where he can see positions of his ships and second where he see hits and misses on an opponent board
 - player have some methods that can fire shoots and place ships
 - board should have some information about position of placed ships
 - game should contain 2 players and there should be a possibility to start a game and finish it whether there is a draw or one of the players had win

-about algorithm:
 - I wanted to imitate a real game as much as it is possible. So Player 1 should know only what he could see in a real life, it means only coordinates he hitted correctly and coordinates he missed. The same applies to Player 2.
 - Keeping that in mind I wanted to create algorithm thas firstly shoots and random positions and then if it hitted some ship at that positions it ads this possition to the list.
 - if position was correct in next turn algorithm looks for the 4 nearest free possitions to the previous one ( one up, one down, one left, one right).
 - it choose randomly one from the (maximum) for positions. If it choose correctly and ship was hit, this positions is added to the list. If not, then it tries every positions that is left (from those maximum 4 positions). Eventualy it will find position with ship on it.
 - in next turn when we already have 2 positions on list we search for another positions in the same orientation. It means if on the list we have positions e.g. (5,3) and (6,3) then it will only look for positions ( X,3).

-about OOP:
 - for now player should have mthods to select a position to shoot, communicate to another player if position was hit, if ships was destroyed, and method to process shot (mark on own board if shot was hit or missed).
 - i have to add logic to the game class to manage plays and turns;
 - probably move some LINQ to board class.
 - there could be another class for firing board that inherits from Board.cs but I gave up that idea. Maybe if I would changed algorithm in some way and board would need some new methods then I would create that class.
 - the same applies to ship class, i decided to only create one class and every ship is just a new object. I think it was unnecessary for now to how separate class for each ship 


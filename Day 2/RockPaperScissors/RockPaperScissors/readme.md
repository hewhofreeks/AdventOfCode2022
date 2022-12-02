Second day of Advent of Code was all about Rock-Paper-Scissors. One of my favorite games.

Logic for it is very simple, so for calculating the winner/losser's score of a Match I created an object to handle all of that.

Handling the different scores for what you're throwing as well as what you earn as a result was easily handled in an enum value, which I could pass around as we're looking at who won what.

During Part 2 to calculate what I need to win/lose/tie it was simple enough to do some math manipulation on our challenger enum value as well. 

`Rock (1) > Paper (2) > Scissors (3) > Rock (1)`

If we needed a win we could just increment our enum and wrap around if it got to high. If we needed a loss we could handle it in the reverse way.
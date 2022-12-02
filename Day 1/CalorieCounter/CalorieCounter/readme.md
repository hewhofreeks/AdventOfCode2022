The first problem in our advent of code is counting calories that the current elves have taken on their journey. 

Part 1 states we must know the highest calorie count among all the elves. This is a simple problem we solve by iterating through our file, 
	counting all the calories for each elf, and then comparing it against the current maximum. We've all written this.

Part 2 adds on to this and wants us to add the totals of the three highest calorie counts among elves. 
	In order to be a little effecient with our memory, I didn't want to just keep a total list, sort it, and grab the top three values.
	Instead I strive to just keep a running list of our top three elves' calorie counts. To do this effeciently I used a min-heap data structure
	to add values to, and since it is always nice and organized we can just dequeue our min-priority elf since he has the lowest value.
	This gives us a nice running total of our top three elves.
	Once we're through the list we can just dequeue those values and add 'em together!


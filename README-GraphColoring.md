# Graph Coloring

## The Idea
1. Users will be able to see graphs in the game.
2. The graphs are built into the game by developers. Developers can create graphs by instantiating spheres and cylinders.
3. Developers need to write a script for each graph to check whether the graph is correctly colored. This can be done by hard-coding the checks. That is, name the vertices V1, V2, ... Vn.  Write some comments in the code to list all the adjacency. Then, write a for loop to iterate through all the vertices and check that V1.Materials.Color != V2.Materials.Color if V1 and V2 are adjacent.
4. If the coloring is right, the graph will be highlighted in green.
5. Otherwise, the graph will be highlighted in red.

## Coloring the Graphs
1. There are three colors that can be used to color a vertex: Red, Yellow, and Blue.
2. To color a vertex, pick up a pen of a certain color using the right virtual hand.
3. Point the pen to a vertex. Ensure that the user is close enough to the vertex. This distance can be set on line 8 in penRaycast.cs.
4. If the pen is directed at a vertex, the vertex will be colored with the color of the pen.


## To Do
1. Create an interface system that allows users to choose the color of the pen without releasing the current pen and picking up a new pen from the table.

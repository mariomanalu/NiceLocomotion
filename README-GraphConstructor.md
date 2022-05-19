# Graph Constructor

## Instantiating a vertex

1. Point the left hand in a particular direction.
2. Press X on the left controller.

## Instantiating an edge incident to two vertices

1. Point to a vertex. 
2. Once the vertex is highlighted in yellow, press Y on the left controller.
3. The vertex will now be highlighted in green.
4. Point to a second vertex until it is highlighted in yellow.
5. Press Y again.

## Customization
1. The upper bound on the number of nodes in this exhibit is set on line 31 in the graphConstructor.cs file.
2. Change this number to increase or decrease the number of nodes to prevent the user from instantiating too many nodes.

## To Do:
1. Make vertices interactable. This is easy to do. Just add the VR Grab Interactable component into the node prefab. What is challenging, however, is to make a graph interactable because there is no "graph" object at the moment.
2. Create one parent object when two vertices are incident to the same edge. Right now, when there are two vertices incident to one edge, the structure of the objects is as follows:
	- Node 1
	- Node 2
	- Edge
	Ideally the structure of the objects should be:
	- Graph:
			- Node 1
			- Node 2
			- Edge
	This would require graph searching algorithms: either Breadth-First Search or Depth-First Search to determine all the nodes and edges of a graph.
3. Implement a way to assign weight to edges.

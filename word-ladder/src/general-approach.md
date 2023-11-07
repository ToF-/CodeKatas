# General Approach

How would we solve this problem by hand?

Let's suppose we are given a word list as short as this one:

_cab_, _car_, _cat_, _cog_, _cot_, _cow_, _dab_, _dag_, _dam_, _doc_, _dog_, _dot_.

We want to find the shortest path between the words _dog_ and _cat_.

On a sheet of paper we form a graph where the nodes are words and edges are traced betwen words that differ by one and only one letter. Or, we can draw the following diagram:


|       | _cab_ | _car_ | _cat_ | _cog_ | _cot_ | _cow_ | _dab_ | _dag_ | _dam_ | _doc_ | _dog_ | _dot_ |
|-------|---    |---    |---    |---    |---    |---    |---    |---    |---    |---    |---    |---    |
| _cab_ |       |   *   |   *   |       |       |       |   *   |       |       |       |       |       |
| _car_ |   *   |       |   *   |       |   *   |       |       |       |       |       |       |       |
| _cat_ |   *   |   *   |       |       |       |       |       |       |       |       |       |       |
| _cog_ |       |       |       |       |   *   |  *    |       |       |       |       |       |       |
| _cot_ |       |       |   *   |   *   |       |   *   |       |       |       |       |       |   *   |
| _cow_ |       |       |       |   *   |   *   |       |       |       |       |       |       |       |
| _dab_ |   *   |       |       |       |       |       |       |   *   |   *   |       |       |       |
| _dag_ |       |       |       |       |       |       |   *   |       |   *   |       |   *   |       |
| _dam_ |       |       |       |       |       |       |   *   |   *   |       |       |       |       |
| _doc_ |       |       |       |       |       |       |       |       |       |       |   *   |   *   |
| _dog_ |       |       |       |   *   |       |       |       |   *   |       |   *   |       |   *   |
| _dot_ |       |       |       |       |   *   |       |       |       |       |   *   |   *   |       |


We want to search the graph in a "breadth-first" manner: 

1. starting from the __end__ node _cat_, we see that the nodes _cab_, _car_ and _cot_ adjacent to this node, so we draw an arrow from these nodes back to _cat_ and mark them as the next nodes to examine
2. starting from the first node that we have to examine: _cab_, we have _car_ and _cat_ which have already been visited, and  _dab_, which hasn't, so we draw an arrow from _dab_ to _cab_  and we mark _dab_ as the next node to visit after _cot_
3. from the node _cot_, we see the nodes _cog_ _cow_ and _dot_, which we link to _cot_ and mark as to be visited
4. from the node _dab_, we see the nodes _dag_ and _dam_
5. from the node _cog_, we see the node _dog_, we draw an arrow from _dog_ to _cog_ and our search is done, as we have found the target word.

![example with label](/images/example-with-label.png)

The path from _dog_ to _cat_ is obtained by starting at the node _dog_ and following the arrows: _{ dog, cog, cot, cat }_.

This method guarantees that the shortest path has been found.

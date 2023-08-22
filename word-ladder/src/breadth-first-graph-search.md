# Breadth-First Graph Search
An interesting way of designing a solution for this problem is to try to solve it by hand on a small data set. 

Let's suppose we are given a list that is limited to 9 words : _cat_, _cab_, _cog_, _cot_, _dab_, _dam_, _doc_, _dog_, _dot_.

We want to find the shortest path between the words _cat_ and _dog_.

On a sheet of paper we form a graph where the nodes are words and edges are traced betwen words that differ by one and only one letter.

![example](/images/example.png)

On another sheet of paper, we note _cat_, the first word to examine. Then we search the graph in a "breadth-first" manner:

1. starting from the node _cat_, we reach the nodes adjacent to this node, and add _cab_ and _cot_ on our list of nodes to examine
2. from the node _cab_, we reach the node _dab_, and add it to our list of nodes to examine
3. from the node _cot_, we reach the nodes _cog_ and _dot_, that we add to our list
4. from the node _dab_, we reach the nodes _dag_ and _dam_, that we add to our list
5. from the node _cog_, we reath the node _dog_ and search is done, as we have found the target word.

The solution is: { _cat_, _cot_, _cog_, _dog_ }.


![example with label](/images/example-with-label.png)

We can translate this approach into pseudo code:

```
word-ladder(word Source, word Target, dictionary Graph)
    list Visit ← []
    dictionary Path ← empty
    append Source to Visit
    word Back ← ∅
    word Current ← ∅
    while Current ≠ Target and Visit ≠ [] do
        Current ← head(Visit)
        Visit ← tail(Visit)
        insert key Current, value Back into dictionary Path
        words Neighbors ← lookup for key Current in dictionary Graph
        for word Neighbor  in Neighbors do 
            append Neighbor into Visit
        end
    end
    list R ← []
    word C ← T
    do 
        insert C into R
        C ← lookup for key C in dictionary Path 
    until C = S
    return R
end
```

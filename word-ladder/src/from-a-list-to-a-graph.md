# From a List to a Graph

The breadth-first search algorithm works fine provided we give it a graph of adjacent words. But we don't have this information yet.
How to create such a graph ? If we go naïvely about it:

- for every word _A_ in the list, look at every word _B_ in the list; if _A_ is adjacent to _B_ then add the edge from _A_ to _B_ in the graph.

our program will have to make millions of comparisons to create the graph.

We can reduce this number, by using storing some intermediate information about the adjacent words of a word.

For example the word _cat_ belongs to 3 groups of words:

- words that start with _ca_…
- words that begin with a _c_ and end with a _t_
- words that end with …_at_

Thus we can insert 3 edges in the graph:

- an edge from the node _ca\__ to the node _cat_
- an edge from the node _c\_t_ to the node _cat_
- an edge from the node _\_at_ to the node _cat_ 

then, when we meet the word _cot_ we create 3 new edges :
- from _co\__ to _cot_
- from _c\_t_ to _cot_
- from _\_ot_ to _cot_ 

and now the the edge _c\_t_ has egdes to _cat_ and _cot_, which means that the node _cot_ can be found from the node _cat_.



![cat and cot](/images/cat-cot.png)

We can create a graph that includes words and groups, where an edge between an word and a group means belonging. 
Such a graph can be built with a dictionary:
```
build-graph(list Words)
    dictionary Graph ← empty
    for Word in Words do
        insert key Word, value ∅ in the Graph
        for (index I,char C) in Word do
            word Group ← replace position I by '_' in Word
            if Group is not in Graph then insert key Group, [] in Graph
            Words ← lookup key Group in Graph
            append Word to Words
            udate key Group, value Words in Graph
        end
    end
    return Graph
end
```

With construction, we can finding the adjacent words of a word is done by looking at each groups the word belongs to:
```
neighbors(word Word, dictionary Graph)
    list Result ← []
    Groups ← lookup key Word in Graph
    for Group in Groups do
        Neighbors ← lookup key Group in Graph
        for Neighbor in Neighbors do
            if Neighbor ≠ Word then append Neighbor into Result
        end
    end
    return Result
end
```


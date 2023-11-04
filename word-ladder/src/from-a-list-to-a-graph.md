# From a List to a Graph

The breadth-first search algorithm works fine provided we are can find the adjacents words of a given word. The naïve way to go about it is to do a sequential search on the list of words we have been given.

```
adjacents(Words, Source) {
    Result ← ø
    for Word in Words {
        differences ← 0
        for i in 0 to length(Word) - 1 {
            if Word[i] != Source[i] {
                differences ← differences + 1
                if differences > 1 {
                    break
                }
            }
        }
        if differences = 1 {
            insert(Result, Word)
        }
    }
    return Result
}
```


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


# From a List to a Graph

The breadth-first search algorithm works but it's not efficient fine provided we are can find the adjacents words of a given word. Consider this implementation in Python:
```python
import sys

wordFilePath = sys.argv[1]
source = sys.argv[2]
target = sys.argv[3]
words = []

def adjacents(words, source):
    result = []
    for word in words:
        differences = 0
        for i in range(0, len(word)):
            if word[i] != source[i]:
                differences = differences + 1
                if differences > 1:
                       break
        if differences == 1:
            result.append(word)
    return result

def wordLadder(target, source, words):
    visit = []
    paths = {}
    visit.append(source)
    paths[source] = "*****"
    while visit:
        current = visit.pop(0)
        if current == target:
            break
        adjs = adjacents(words, current)
        for adjacent in adjs:
            if not paths.get(adjacent):
                paths[adjacent] = current
                visit.append(adjacent)
    if current == target:
        while current != "*****":
            print(current)
            current = paths.get(current)
    else:
        print("no path")

with open(wordFilePath) as wordFile:
    for line in wordFile:
        words.append(line.strip())

wordLadder(target, source, words)
```
When given a list of 5757 five-letter words its profiled execution results in the following:
```
~/Coding/KT/word-ladder: python3 -m cProfile python/wordladder-list.py data/www-cs-faculty.stanford.edu_~knuth_sgb-words.txt backs horse
horse
gorse
goose
goosy
gooky
booky
books
bocks
backs
         20877682 function calls in 19.822 seconds
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


# From a List to a Graph

The program would be faster if we could optimize the search for adjacent words by using a dictionary, i.e a fast mapping between a word and its adjacent words.

A naïve approach to create such a dictionary via a nested loop:
```
Dictionary ← ø
for Source in Words
    for Target in Words
        if adjacent(Source, Target)
Dictionary[Source] = append(Dictionary[Source],Target)
```
which would amount to more 34 millions comparisons.

A faster approach uses the fact that, N being the size of each words in the list, all words that have N-1 letters in common are adjacent. For example the words _cab_, _car_ and _cat_ are adjacents since they all belong to the group _ca\__, the group of all words starting with _ca_. The word _cab_ is also adjacent with _dab_ since, they both belong to the group _\_ab_, and so on.

To create the final dictionary, we first initiate a dictionary of all groups, by creating and updating the groups they belong to:
```
Groups ← ø
for Word in Words
    for I in 0 to length(Word)-1
        Group = replace(Word, i, '_')
        Groups[Group] = append(Groups[Group], Word)
```
Thus given the words _cab_, _car_, _cat_, _cog_, _cot_, _cow_, _dab_, _dag_, _dam_, _doc_, _dog_, _dot_, we would obtain the following dictionary:
\_ab : cab, dab\
\_ar : car\
\_at : cat\
\_oc : doc\
\_og : cog, dog\
\_ot : cot\
\_ow : cow\
c\_b : cab\
c\_g : cog\
c\_r : car\
c\_t : cat, cot\
c\_w : cow\
d\_b : dab\
d\_c : doc\
d\_g : dag, dog\
d\_m : dam\
d\_t : dot\
ca\_ : cab, car, cat\
co\_ : cog, cot, cow\
da\_ : dab, dag, dam\
do\_ : doc, dog, dot\

The adjacent words dictionary is created by gathering, for each word in the list, the words associated with each group of this word:
```
Adjacents ← ø
for Word in Words
    for I in 0 to length(Word)-1
        Group = replace(Word, i, '_')
        Neighbors = Groups[Group]
        for Neighbor in Neighbors
            if Neighbor <> Word
                Adjacents[Word] = append(Adjacents[Word], Neighbor)
```
This would result, for our example list, in the following dictionary:

cab : dab\
car : cab, cat\
cat : cot\
cog : cot, cow\
cot : cog, cow\
cow : cog, cot\
dab : cab, dag\
dag : dab\
dam : dab, dag\
doc : dog, dot\
dog : doc, dot\
dot : doc, dog\

The following python program, `buildgroups.py` creates such a dictionary and prints it once converted in json:
```python
import sys
import json

wordFilePath = sys.argv[1]

words = []

def underscore(word, i):
    chars = list(word)
    chars[i] = '_'
    return "".join(chars)

def adjacentGraph(words):
    keymap = {}
    for word in words:
        for i in range(0, len(word)):
            key = underscore(word, i)
            values = keymap.get(key, [])
            values.append(word)
            keymap[key] = values
    adjacents = {}
    for word in words:
        for i in range(0, len(word)):
            key = underscore(word, i)
            key_adjacents = keymap.get(key, [])
            values = adjacents.get(word, [])
            for key_adjacent in key_adjacents:
                if key_adjacent != word and not key_adjacent in values:
                    values.append(key_adjacent)
            adjacents[word] = values
    return adjacents

with open(wordFilePath) as wordFile:
    for line in wordFile:
        words.append(line.strip())

result = json.dumps(adjacentGraph(words))
print(result)
```

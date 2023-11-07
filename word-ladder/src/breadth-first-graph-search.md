# Breadth-First Graph Search
We can translate this approach, called breath-first search, into python code:

`words` is a list of words\
`source` is the source word\
`target` is the target word\
`visit` is a FIFO queue where items are appended at the end anp popped at the start\
`Paths` is a key/value dictionary mapping a word to its predecessor

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
At the center of this search process is the function `adjacents` which should find all the words in the list that are adjacent to a given word. This is a simple but not efficient way to go about it as it does a sequential search on the list of words.

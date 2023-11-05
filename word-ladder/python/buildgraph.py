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

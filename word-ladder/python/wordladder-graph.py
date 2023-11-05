import sys
import json

wordFilePath = sys.argv[1]
source = sys.argv[2]
target = sys.argv[3]
words = {}
comp = 0

def adjacents(words, source):
    global comp
    comp = comp + 1
    return words.get(source)

def wordLadder(target, source, words):
    global comp
    visit = []
    paths = {}
    visit.append(source)
    paths[source] = "*****"
    while visit:
        current = visit.pop(0)
        comp = comp + 1
        if current == target:
            break
        adjs = adjacents(words, current)
        for adjacent in adjs:
            if not paths.get(adjacent):
                paths[adjacent] = current
                visit.append(adjacent)
    print(f'{len(paths)} path steps')
    if current == target:
        while current != "*****":
            print(current)
            current = paths.get(current)
    else:
        print("no path")



with open(wordFilePath) as wordFile:
    words = json.loads(wordFile.read())

wordLadder(target, source, words)

print(f'{comp} comparisons')



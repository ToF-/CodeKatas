# Breadth-First Graph Search
We can translate this approach, called breath-frist search, into pseudo code:

`Words` is a list of words\
`Source` is the source word\
`Target` is the target word\
`Visit` is a FIFO queue where items are appended at the end anp popped at the start\
`Paths` is a key/value dictionary mapping a word to its predecessor

```
wordladder(Target, Source, Words) {
    Visit ← ∅
    Paths ← ø
    append(Visit, Target)
    insert(Paths, Source, ∅)
    Current ← Source
    while not empty(Visit) {
        Current ← pop(Visit)
        if Current = Target {
            break
        }
        for Adjacent in adjacents(Words, Current) {
            if not contains(Paths, Adjacent) then {
                insert(Paths, Adjacent, Current)
                append(Visit, Adjacent)
            }
        }
    }
    if Current = Target {
        while Current ≠ ∅ do
            print Current
            Current = lookup(Paths, Current)
        } 
    else {
        prendint "no path"
    }
}
```
At the center of this search process is the function `adjacents` which should find all the words in the list that are adjacent to a given word. A simple but not efficient way to go about it is to do a sequential search on the list of words we have been given:

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


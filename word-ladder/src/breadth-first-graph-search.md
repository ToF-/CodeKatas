# Breadth-First Graph Search
We can translate this approach, called breath-frist search, into pseudo code:

`Words` is a list of words\
`Source` is the source word\
`Target` is the target word\
`Visit` is a FIFO queue where items are appended at the end anp popped at the start\
`Paths` is a key/value dictionary mapping a word to its predecessor

`Adjacents(W,L)` is a function that returns the words that are adjacent to `W` in the list `L`

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
and now the remaining difficulty is in the function `adjacents` which should find all the words in the list that are adjacent to a given word.

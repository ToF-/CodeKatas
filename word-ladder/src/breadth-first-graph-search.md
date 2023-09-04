# Breadth-First Graph Search
We can translate this approach into pseudo code:

`L` is a list of words

`S` is the source word

`T` is the target word

`Q` is a FIFO queue where items are appended at the end anp popped at the start

`G` is a key/value dictionary mapping a word to its predecessor

`Adjacents(W,L)` is a function that returns the words that are adjacent to `W` in the list `L`

```
wordladder(T, S, L, G)
    Q ← ∅
    append(Q, T)
    insert(G, S, ∅)
    C ← S
    while not empty(Q) do
        C ← pop(Q)
        if C = T then break
        for N in Adjacents(C, L) do
            if not contains(G, N) then
                insert(G, N, C)
                append(Q, N)
            end
        end
    end
    if C = T then
        while C ≠ ∅ do
            print C
            C = lookup(G, C)
        end
    else
        print "no path"
    end
```
and now the main difficulty is in the function `Adjacents` which should find all the words in the list that are adjacent to a given word.

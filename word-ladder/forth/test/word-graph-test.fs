\ word-graph-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/word-graph.fs

CR .( word graph ) CR
.(   initially the graph is empty and no word can be found. ) CR
T{
    SS" horse" PREDECESSOR@ ?FALSE
}T
.(   after adding a target word, this word can be found, and its predecessor is itself. ) CR
T{
    SS" horse" SET-TARGET-WORD
    SS" horse" PREDECESSOR@ ?TRUE SS" horse" ?S
    SS" horse" IS-TARGET-WORD? ?TRUE
}T
.(   a word with a predecessor can be added in the graph. ) CR
T{
    SS" horse" SS" worse" ADD-ADJACENT-WORD
    SS" worse" PREDECESSOR@ ?TRUE SS" horse" ?S
    SS" worse" IS-TARGET-WORD? ?FALSE
}T
.(   a path from a word until the target can be followed. ) CR
T{
    SS" worse" SS" morse" ADD-ADJACENT-WORD
    CREATE FOO 8 CELLS ALLOT VARIABLE FOO-PTR FOO FOO-PTR ! 
    : TRACK-SMALL-STRING ( ss -- ) FOO-PTR @ ! CELL FOO-PTR +! ;
    SS" morse" ' TRACK-SMALL-STRING WORD-PATH-EXECUTE
    FOO 0 CELLS + @ SS" morse" ?S
    FOO 1 CELLS + @ SS" worse" ?S
    FOO 2 CELLS + @ SS" horse" ?S
}T
.(   after clearing the word-graph, no word can be found. ) CR
T{
    WORD-GRAPH-CLEAR
    SS" horse" PREDECESSOR@ ?FALSE
}T
.(   visit a word in the graph fills the visit queue with adjacent words not visited yet. ) CR
T{
    SS" cells" SET-TARGET-WORD
    CLEAR-VISIT-QUEUE
    VISIT-QUEUE Q-EMPTY? ?TRUE
    SS" cells" VISIT-ADJACENTS
    VISIT-QUEUE Q-EMPTY? ?FALSE
    VISIT-QUEUE Q-POP SS" bells" ?S
    VISIT-QUEUE Q-POP SS" dells" ?S
    VISIT-QUEUE Q-POP SS" fells" ?S
    VISIT-QUEUE Q-POP SS" hells" ?S
    VISIT-QUEUE Q-POP SS" jells" ?S
    VISIT-QUEUE Q-POP SS" sells" ?S
    VISIT-QUEUE Q-POP SS" tells" ?S
    VISIT-QUEUE Q-POP SS" wells" ?S
    VISIT-QUEUE Q-POP SS" yells" ?S
    VISIT-QUEUE Q-POP SS" calls" ?S
    VISIT-QUEUE Q-POP SS" cills" ?S
    VISIT-QUEUE Q-POP SS" culls" ?S
    VISIT-QUEUE Q-POP SS" ceils" ?S
    VISIT-QUEUE Q-POP SS" cello" ?S
}T

BYE

.(   the word graph, group dictionary and visit queue can be used to find explore the words. ) CR
T{
    CLEAR-VISIT-QUEUE
    s" brain" ADD-TO-VISIT
    SEARCH-ADJACENT-WORDS!
    s" train" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    s" grain" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    s" drain" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    s" bruin" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    s" brawn" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    s" braid" PAD PREDECESSOR@>S PAD COUNT S" brain" ?STR
    VISIT-QUEUE Q-POP PAD WORDKEY>S PAD COUNT S" drain" ?STR
    VISIT-QUEUE Q-POP PAD WORDKEY>S PAD COUNT S" grain" ?STR
    VISIT-QUEUE Q-POP PAD WORDKEY>S PAD COUNT S" train" ?STR
    VISIT-QUEUE Q-POP PAD WORDKEY>S PAD COUNT S" bruin" ?STR
    VISIT-QUEUE Q-POP PAD WORDKEY>S PAD COUNT S" brawn" ?STR
}T
.(   the word graph, group dictionary and visit queue can be used to find the shortest path between two words. ) CR
T{
    s" trait" s" brain" SEARCH-PATH! ?TRUE
    s" brain" .WORD-PATH CR

}T

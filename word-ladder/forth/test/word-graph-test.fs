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
    CREATE FOO 30 CELLS ALLOT VARIABLE FOO-PTR FOO FOO-PTR !
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
    [BEGIN] VISIT-QUEUE Q-EMPTY? 0= [WHILE] VISIT-QUEUE Q-POP [REPEAT]
    SS" cello" ?S SS" ceils" ?S SS" culls" ?S SS" cills" ?S SS" calls" ?S SS" yells" ?S SS" wells" ?S
    SS" tells" ?S SS" sells" ?S SS" jells" ?S SS" hells" ?S SS" fells" ?S SS" dells" ?S SS" bells" ?S
    SS" cello" PREDECESSOR@ ?TRUE SS" cells" ?S
    SS" bells" PREDECESSOR@ ?TRUE SS" cells" ?S
}T
.(   the word graph, group dictionary and visit queue can be used to find the shortest path between two words. ) CR
T{
    SS" cells" SS" brain" FIND-SHORTEST-PATH ?TRUE
    FOO FOO-PTR !
    SS" brain" ' TRACK-SMALL-STRING WORD-PATH-EXECUTE
    FOO 0 CELLS + @ SS" brain" ?S
    FOO 1 CELLS + @ SS" braid" ?S
    FOO 2 CELLS + @ SS" brand" ?S
    FOO 3 CELLS + @ SS" brans" ?S
    FOO 4 CELLS + @ SS" brats" ?S
    FOO 5 CELLS + @ SS" beats" ?S
    FOO 6 CELLS + @ SS" belts" ?S
    FOO 7 CELLS + @ SS" bells" ?S
    FOO 8 CELLS + @ SS" cells" ?S
    SS" devil" SS" angel" FIND-SHORTEST-PATH ?FALSE
}T

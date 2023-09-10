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
    SS" horse" ADD-TARGET-WORD
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

BYE

.(   the path from a word until the start word can be displayed. ) CR
T{
    s" worse" IS-START-WORD? ?FALSE
    s" morse" IS-START-WORD? ?FALSE
    s" horse" IS-START-WORD? ?TRUE
    s" worse" s" morse" ADD-ADJACENT-WORDS
    CR s" horse" .WORD-PATH CR
    CR s" morse" .WORD-PATH CR
}T
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

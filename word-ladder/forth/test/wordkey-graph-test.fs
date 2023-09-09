\ wordkey-graph-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/wordkey-graph.fs

CR .( wordkey graph ) CR
.(   a wordkey with no predecessor can be added in the graph. ) CR
T{
    s" horse" ADD-START-WORD
    s" horse" PAD PREDECESSOR@>S PAD COUNT S" " ?STR
}T
.(   a wordkey with a predecessor can be added in the graph. ) CR
T{
    s" horse" s" worse" ADD-ADJACENT-WORDS
    s" worse" PAD PREDECESSOR@>S PAD COUNT S" horse" ?STR
}T
.(   the path from a word until the start word can be displayed. ) CR
T{
    s" worse" s" morse" ADD-ADJACENT-WORDS
    s" morse" .WORD-PATH CR
}T
.(   the wordkey graph, group dictionary and visit queue can be used to find explore the words. ) CR
T{
    CLEAR-VISIT-QUEUE
    s" brain" ADD-TO-VISIT
    hex  SEARCH-ADJACENT-WORDS!
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

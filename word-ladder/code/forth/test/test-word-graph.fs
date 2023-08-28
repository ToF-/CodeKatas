\ test-word-graph.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/word-graph.fs

CR
.( word graph ) CR

T{ .( after adding a word in the word graph its predecessor is the empty word. ) CR
    WORD-GRAPH wg
    S" FOO" wg WG-ADD-WORD
    S" FOO" wg pad WG-PRED@ 0 ?S DROP
}T

T{ .( after setting predecessor in the word graph the predecessor can be found. ) CR
    S" BAR" S" FOO" wg WG-PRED!
    S" FOO" wg pad WG-PRED@ S" BAR" ?STR
}T

wg D-CLEAR-VALUES
S" BAT" wg WG-ADD-WORD
S" CAB" wg WG-ADD-WORD
S" CAT" wg WG-ADD-WORD
S" COT" wg WG-ADD-WORD
S" DAB" wg WG-ADD-WORD
S" DOG" wg WG-ADD-WORD
S" EEL" wg WG-ADD-WORD
S" FOG" wg WG-ADD-WORD
S" FOX" wg WG-ADD-WORD
S" FLY" wg WG-ADD-WORD

QUEUE q
T{ .( after searching adjacent words the words have a predecessor. ) CR
    KEY" CAT" q Q-APPEND 
    q wg WG-ADJACENTS
    q Q-EMPTY? ?FALSE
    q Q-POP KEY" CAB" ?S
    q Q-POP KEY" BAT" ?S
    q Q-POP KEY" COT" ?S
}T

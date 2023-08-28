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

: wg,, 0 do wg wg-add-word loop ;
wg D-CLEAR-VALUES

s" BAT" s" CAB" s" CAT" s" COT" s" DAB" s" DOG" s" COG" s" FOG" s" FOX" s" FLY" 
10 wg,,

QUEUE q
T{ .( after searching adjacent words the words have a predecessor. ) CR
    KEY" CAT" q Q-APPEND 
    q wg WG-ADJACENTS
    q Q-EMPTY? ?FALSE
    q Q-POP KEY" CAB" ?S
    q Q-POP KEY" BAT" ?S
    q Q-POP KEY" COT" ?S
    KEY" CAB" wg WG-KEY-PRED@ KEY" CAT" ?S 
    KEY" BAT" wg WG-KEY-PRED@ KEY" CAT" ?S 
    KEY" COT" wg WG-KEY-PRED@ KEY" CAT" ?S 
}T

wg D-CLEAR-VALUES

s" BAT" s" CAB" s" CAT" s" COT" s" DAB" s" DOG" s" COG" s" FOG" s" FOX" s" FLY" 
10 wg,,

T{ .( after searching for a ladder the path is in the word graph. ) CR
    wg D-CLEAR-VALUES
    KEY" DOG" KEY" CAT" q wg WG-SEARCH-PATH
    KEY" CAT" 
    wg WG-KEY-PRED@ dup pad key>s s" COT" ?STR
    wg WG-KEY-PRED@ dup pad key>s s" COG" ?STR
    wg WG-KEY-PRED@ dup pad key>s s" DOG" ?STR
    DROP
}T

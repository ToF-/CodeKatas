REQUIRE word-graph.fs
WORD-GRAPH WG
QUEUE Q

: GET-ARGUMENT
    NEXT-ARG 2DUP WG WG-HAS-WORD? IF
        S>KEY
    ELSE
        TYPE SPACE ." is not in the list" CR
        0
    THEN ;

: MAIN-PROGRAM ( -- )
    S" ../../src/3-letter-words.txt" WG WG-READ-WORDS
    S" ../../src/4-letter-words.txt" WG WG-READ-WORDS
    S" ../../src/5-letter-words.txt" WG WG-READ-WORDS
    GET-ARGUMENT 
    GET-ARGUMENT 
    2DUP * IF 
        OVER Q WG WG-SEARCH-PATH
        IF 
            WG .WG-PATH CR 
        ELSE 
            ." no path found" CR 
        THEN 
    THEN ;

MAIN-PROGRAM
BYE

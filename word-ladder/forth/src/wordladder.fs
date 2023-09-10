\ wordladder.fs
REQUIRE letter-set.fs
REQUIRE group.fs
REQUIRE group-dictionary.fs
REQUIRE word-graph.fs



VARIABLE WL-SOURCE
VARIABLE WL-TARGET

: MAIN ( -- )
    NEXT-ARG READ-WORDS
    NEXT-ARG SMALL-STRING
    DUP WORD-EXIST? 0= IF
        SMALL-STRING-S TYPE ."  is not in the list" CR
    ELSE
        NEXT-ARG SMALL-STRING
        DUP WORD-EXIST? 0= IF
            SMALL-STRING-S TYPE ."  is not in the list" CR
        ELSE
            DUP -ROT
            FIND-SHORTEST-PATH IF
                ['] .SMALL-STRING WORD-PATH-EXECUTE
            ELSE
                ." no path" DROP
            THEN CR 
        THEN
    THEN ;



MAIN BYE

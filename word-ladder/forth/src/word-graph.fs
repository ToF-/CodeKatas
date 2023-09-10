\ word-graph.fs

REQUIRE ffl/hct.fs
REQUIRE small-string.fs
REQUIRE letter-set.fs
REQUIRE group.fs
REQUIRE queue.fs
REQUIRE group-dictionary.fs

50000 CONSTANT WORD-GRAPH-SIZE
WORD-GRAPH-SIZE HCT-CREATE WORD-GRAPH

QUEUE VISIT-QUEUE

: CLEAR-VISIT-QUEUE
    VISIT-QUEUE Q-EMPTY ;

: WORD-GRAPH-CLEAR ( -- )
    WORD-GRAPH-SIZE WORD-GRAPH HCT-INIT ;

: PREDECESSOR@ ( w -- x,t|f )
    SMALL-STRING-S WORD-GRAPH HCT-GET ;

: ADD-ADJACENT-WORD ( p,w -- )
    SMALL-STRING-S WORD-GRAPH HCT-INSERT ;

: SET-TARGET-WORD ( w -- )
    DUP ADD-ADJACENT-WORD ;

: IS-TARGET-WORD? ( w -- )
    DUP PREDECESSOR@ IF = ELSE FALSE THEN ;

: WORD-PATH-EXECUTE ( w,xt -- )
    BEGIN
        2DUP EXECUTE
        OVER IS-TARGET-WORD? 0=
    WHILE
        SWAP PREDECESSOR@ DROP
        SWAP
    REPEAT 2DROP ;

: VISIT-ADJACENTS ( w -- )
    DUP SMALL-STRING-LENGTH@ 0 ?DO         \ p
        DUP I NTH-GROUP                    \ p,g
        DUP GROUP-LETTERS@                 \ p,g,ad,l
        OVER + SWAP ?DO                    \ p,g
            I C@ OVER GROUP>WORD           \ p,g,w
            DUP PREDECESSOR@ 0= IF         \ p,g,w
                ROT DUP ROT                \ g,p,p,w
                DUP VISIT-QUEUE Q-APPEND   \ g,p,p,w
                ADD-ADJACENT-WORD          \ g,p
                SWAP                       \ p,g
            ELSE                           \ p,g,w,p
                2DROP                      \ p,g
            THEN                           \ p,g
        LOOP DROP                          \ p
    LOOP DROP ;

: FIND-SHORTEST-PATH ( t,w -- n )
    CLEAR-VISIT-QUEUE
    WORD-GRAPH-CLEAR
    SWAP DUP SET-TARGET-WORD
    VISIT-QUEUE Q-APPEND
    FALSE SWAP
    BEGIN
        VISIT-QUEUE Q-EMPTY? 0=
    WHILE
        VISIT-QUEUE Q-HEAD@ OVER <> IF
            VISIT-QUEUE Q-POP
            VISIT-ADJACENTS
        ELSE
            SWAP DROP TRUE SWAP
            VISIT-QUEUE Q-EMPTY
        THEN
    REPEAT DROP ;


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
                ROT                        \ g,w,p
                DUP ROT                    \ g,p,p,w
                ADD-ADJACENT-WORD          \ g,p
                SWAP                       \ p,g
            ELSE
                DROP                       \ p,g
            THEN
        LOOP DROP                          \ p
    LOOP DROP ;

\ 50000 CONSTANT WORD-GRAPH-SIZE
\ WORD-GRAPH-SIZE HCT-CREATE WORD-GRAPH
\ 
\ : ADD-START-WORD ( add,l -- )
\     2DUP S>WORDKEY -ROT
\     WORD-GRAPH HCT-INSERT ;
\ 
\ CREATE WORD-S CELL ALLOT
\ 
\ : IS-START-WORD? ( add,l -- f )
\     2DUP WORD-GRAPH HCT-GET IF
\         WORD-S WORDKEY>S
\         WORD-S COUNT COMPARE 0=
\     ELSE
\         2DROP FALSE
\     THEN ;
\         
\         
\ : PREDECESSOR@>S ( add,l,dest -- )
\     -ROT WORD-GRAPH HCT-GET
\     IF SWAP WORDKEY>S ELSE 0 SWAP C! THEN ;
\ 
\ : HAS-PREDECESSOR? ( add,l -- f )
\     PAD PREDECESSOR@>S
\     PAD COUNT NIP ;
\ 
\ : ADD-ADJACENT-WORDS ( add,l,add,l -- )
\     2SWAP S>WORDKEY -ROT WORD-GRAPH HCT-INSERT ;
\ 
\ : .WORD-PATH ( add,l -- )
\     BEGIN
\         2DUP TYPE SPACE
\         2DUP IS-START-WORD? 0=
\     WHILE
\         PAD PREDECESSOR@>S
\         PAD COUNT
\     REPEAT 2DROP ;
\ 
\ QUEUE VISIT-QUEUE
\ 
\ : CLEAR-VISIT-QUEUE ( -- )
\     VISIT-QUEUE Q-EMPTY ;
\    
\ : ADD-TO-VISIT ( add,l -- )
\     S>WORDKEY VISIT-QUEUE Q-APPEND ;
\ 
\ CREATE SOURCE-S CELL ALLOT
\ CREATE GROUP-S CELL ALLOT
\ CREATE LETTERS-S 100 ALLOT
\ CREATE ADJACENT-S CELL ALLOT
\ 
\ : SOURCE-WORD ( kw -- add,l )
\     SOURCE-S DUP -ROT
\     WORDKEY>S COUNT ;
\ 
\ : TH-GROUP-LETTERS ( ad,l,i -- add,l )
\     GROUP-S S>GROUP
\     GROUP-S COUNT GROUP-LETTERS
\     LETTERS-S LETTER-SET>S
\     LETTERS-S COUNT ;
\ 
\ : LETTER-ADJACENT-WORD ( c -- add,l )
\     GROUP-S COUNT ROT
\     ADJACENT-S GROUP-CHAR>S
\     ADJACENT-S COUNT ;
\ 
\ : ADD-ADJACENT-WORD ( pred,l,srce,l -- )
\     2OVER 2OVER COMPARE IF
\         2DUP HAS-PREDECESSOR? 0= IF
\             ADD-ADJACENT-WORDS
\             ADJACENT-S COUNT
\             S>WORDKEY VISIT-QUEUE Q-APPEND
\         ELSE
\             2DROP 2DROP
\         THEN
\     ELSE
\         2DROP 2DROP
\     THEN ;
\ 
\ : SEARCH-ADJACENT-WORDS! ( -- )
\     VISIT-QUEUE Q-POP SOURCE-WORD
\     DUP 0 ?DO
\         2DUP I TH-GROUP-LETTERS
\         OVER + SWAP ?DO
\             I C@ LETTER-ADJACENT-WORD
\             SOURCE-S COUNT 2SWAP
\             ADD-ADJACENT-WORD
\     LOOP LOOP 2DROP ;
\ 
\ : .WORD-GRAPH-ITEM ( w,add,l -- )
\     TYPE ."  -> " PAD WORDKEY>S PAD COUNT TYPE SPACE ;
\ 
\ : SEARCH-PATH! ( src,l,tgt,l -- )
\     CLEAR-VISIT-QUEUE 
\     2SWAP
\     2DUP ADD-START-WORD
\     S>WORDKEY VISIT-QUEUE Q-APPEND
\     FALSE -ROT
\     BEGIN
\         VISIT-QUEUE Q-EMPTY? 0=
\     WHILE
\         VISIT-QUEUE Q-HEAD@ PAD WORDKEY>S
\         PAD COUNT TYPE SPACE
\         2DUP PAD COUNT COMPARE 0= IF
\             ." B I N G O ! " key drop
\             ROT DROP TRUE -ROT
\             VISIT-QUEUE Q-EMPTY
\         ELSE
\             SEARCH-ADJACENT-WORDS!
\         THEN
\     REPEAT 2DROP ;
\ 

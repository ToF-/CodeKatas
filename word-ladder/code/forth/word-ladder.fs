\ word-ladder.fs

REQUIRE associative-array.fs

1000 CONSTANT LINE-MAX
CREATE LINE-BUFFER LINE-MAX ALLOT

: ADD-WORD ( adr,l,aa -- )
    -ROT S>CELL 0 SWAP ROT AA-UPDATE ;

: FIND-WORD ( adr,l,aa -- v,t|f )
    -ROT S>CELL SWAP AA-FIND ;

: UPDATE-WORD ( v,adr,l,aa -- )
    -ROT S>CELL SWAP AA-UPDATE ;

: READ-WORDS ( adr,l -- )
    >R
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER LINE-MAX
        ROT READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP R@ ADD-WORD
    REPEAT DROP
    CLOSE-FILE THROW R> DROP ;

: (ADJACENT?) ( adr,u,adr,u -- f )
    OVER + SWAP ROT DROP
    0 -ROT DO
        OVER C@ I C@ <> IF 1+ THEN
        SWAP 1+ SWAP
    LOOP
    NIP
    1 = ;

: ADJACENT? ( adr,u,adr,u )
    ROT 2DUP 2>R -ROT
    2R> = IF
        (ADJACENT?)
    ELSE
        2DROP 2DROP FALSE
    THEN ;

: NEIGHBORS ( adr,u,wl,ar -- )
    DUP AA-EMPTY         \ adr,u,wl,ar
    SWAP DUP             \ adr,u,ar,wl,wl
    AA-LIMIT SWAP AA-CONTENT ?DO \ adr,u,ar
        -ROT 2DUP I COUNT 
        ADJACENT? IF
            ROT I OVER 0 -ROT AA-ADD
        ELSE
            ROT
        THEN
    AA-CELL +LOOP
    DROP 2DROP ;
        
\ 

\ 
\ : WL-NEIGHBORS ( adr,u,wl,ar -- )
\    DUP AR-EMPTY
\    SWAP DUP WL-NEXT
\    SWAP WL-WORDS DO
\         -ROT 2DUP I COUNT 
\         ADJACENT? IF
\             ROT I OVER AR-ADD
\         ELSE
\             ROT
\         THEN
\         I C@ 1+
\     +LOOP 
\     DROP 2DROP ;
\ 
\ : .(ADJACENTS)
\     NEXT-WORD @ MAIN-LIST DO
\         I COUNT 2DUP TYPE SPACE [CHAR] { EMIT SPACE
\         NEXT-WORD @ MAIN-LIST DO
\             I COUNT 2OVER 2OVER
\             ADJACENT? IF TYPE SPACE ELSE 2DROP THEN
\             I C@ 1+
\         +LOOP
\         [CHAR] } EMIT CR
\         NIP 1+
\     +LOOP ;
\ 
\ : .ADJACENTS
\     DICTIONARY-EMPTY? 0= IF .(ADJACENTS) THEN ;
\     CLOSE-FILE THROW R> DROP ;

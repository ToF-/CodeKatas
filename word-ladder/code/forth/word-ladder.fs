\ word-ladder.fs
50000 CONSTANT MAX-WORDS


: WL-CREATE ( <name> u )
    CREATE DUP , 0 , ALLOT ;

: WL-EMPTY? ( wl -- f )
    CELL + @ 0= ;

: WL-EMPTY ( wl -- )
    CELL+ OFF ;

: WL-CAPACITY? ( u,wl -- f )
    DUP CELL+ @ ROT 1+ + SWAP @ < ;

: WL>WORDS ( wl -- adr )
    2 CELLS + ;

: WL-NEXT ( wl -- adr )
    DUP WL>WORDS SWAP CELL+ @ + ;

: WL-NEXT! ( u,wl -- adr )
    SWAP 1+ SWAP
    CELL+ +! ;

: (WL-ADD) ( adr,u,wl -- )
    2>R
    2R@ WL-NEXT 2DUP C!
    1+ SWAP CMOVE
    2R> WL-NEXT! ;

: WL-ADD ( adr,u,wl -- )
    2DUP WL-CAPACITY? IF
       (WL-ADD)
    ELSE
        DROP 2DROP
        S" wl-add: out of capacity"
        EXCEPTION THROW
    THEN ;

: (WL-FIND) ( adr,u,wl -- ad|f )
    DUP WL-NEXT
    SWAP WL>WORDS
    FALSE -ROT DO
        DROP 2DUP
        I COUNT COMPARE IF
            FALSE
        ELSE
            I LEAVE
        THEN
        I C@ 1+
    +LOOP
    -ROT 2DROP ;

: WL-FIND ( adr,u,wl -- ad|f )
    DUP WL-EMPTY? IF
        DROP 2DROP FALSE
    ELSE
        (WL-FIND)
    THEN ;

1000 CONSTANT LINE-MAX
CREATE LINE-BUFFER LINE-MAX ALLOT

: WL-READ-FILE ( adr,u,wl -- )
    >R
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER LINE-MAX
        ROT READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP R@ WL-ADD
    REPEAT DROP
    CLOSE-FILE THROW R> DROP ;


\ : SAME-SIZE? ( addr,count,addr,count -- f )
\     NIP ROT DROP = ;
\ 
\ : (ADJACENT?) ( addr,count,addr,count -- f )
\     DROP -ROT 0 -ROT
\     OVER + SWAP DO
\         OVER C@ I C@ <> IF 1+ THEN
\         SWAP 1+ SWAP
\     LOOP
\     NIP
\     1 = ;
\ 
\ : ADJACENT? ( addr,count,addr,count )
\     2OVER 2OVER SAME-SIZE? IF
\         (ADJACENT?)
\     ELSE
\         2DROP 2DROP FALSE
\     THEN ;
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

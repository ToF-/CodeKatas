\ word-ladder.fs
50000 CONSTANT MAX-WORDS


: AA-CREATE ( <name> u -- )
    CREATE DUP , 0 , CELLS 2* ALLOT ;

: AA-CAPACITY ( aa - n )
    @ ;

: AA-SIZE ( aa -- n )
    CELL+ @ ;

: AA-CELLS ( aa -- ad )
    2 CELLS + ;

: AA-EMPTY? ( aa -- f )
    AA-SIZE 0= ;

: AA-EMPTY ( aa -- )
    CELL+ OFF ;

: AA-NEXT! ( aa -- )
    CELL+ 1 SWAP +! ;

: AA-CAPACITY? ( ar -- f )
    DUP AA-SIZE 1+
    SWAP AA-CAPACITY < ;

: AA-NEXT ( aa -- adr )
    DUP AA-CELLS
    SWAP AA-SIZE 2* CELLS + ;

: (AA-FIND) ( k,aa -- ad|0)
    DUP AA-NEXT SWAP AA-CELLS
    0 -ROT DO
        OVER I @ = IF
            DROP I LEAVE
        THEN
    2 CELLS +LOOP
    NIP ;
        
: AA-FIND ( k,aa -- v|f )
    DUP AA-EMPTY? IF
        2DROP FALSE
    ELSE
        (AA-FIND) ?DUP IF 
            CELL+ @ TRUE
        ELSE
            FALSE
        THEN 
    THEN ;

: (AA-ADD) ( v,k,aa -- )
    DUP AA-CAPACITY? IF
        DUP AA-NEXT      
        ROT OVER !      
        ROT SWAP CELL+ ! 
        AA-NEXT! 
    ELSE
        2DROP
        S" aa-add: out of capacity"
    THEN ;

: AA-ADD-UPDATE ( v,k,aa )
    DUP AA-EMPTY? IF
        (AA-ADD)
    ELSE
        2DUP (AA-FIND) ?DUP IF
            -ROT 2DROP CELL+ !
        ELSE
            (AA-ADD)
        THEN
    THEN ;

: AR-CREATE ( <name> u -- )
    CREATE DUP , 0 , CELLS ALLOT ;

: AR-SIZE ( ar -- n )
    CELL + @ ;

: AR-CAPACITY ( ar -- n )
    @ ;

: AR-CELLS ( ar -- addr )
    2 CELLS + ;

: AR-EMPTY? ( ar -- f )
    AR-SIZE 0= ;

: AR-EMPTY ( ar -- )
    CELL + OFF ;

: AR-CAPACITY? ( ar -- f )
    DUP AR-SIZE 1+
    SWAP AR-CAPACITY < ;

: AR-NEXT ( ar -- addr )
    DUP AR-CELLS
    SWAP AR-SIZE CELLS + ;

: AR-NEXT! ( ar -- )
    CELL+ 1 SWAP +! ;

: (AR-ADD) ( n,ar -- )
    DUP AR-NEXT
    SWAP AR-NEXT!
    ! ;

: AR-ADD ( n,ar -- )
    DUP AR-CAPACITY? IF
        (AR-ADD)
    ELSE
        2DROP
        S" ar-add: out of capacity"
    THEN ;

: AR-EXIST? ( n,ar -- )
    DUP AR-EMPTY? IF
        2DROP FALSE
    ELSE
        DUP AR-NEXT SWAP AR-CELLS 0 -ROT
        DO
            OVER I @ = IF
                DROP TRUE
                LEAVE
            THEN
        CELL +LOOP NIP
    THEN ;
: WL-CREATE ( <name> u -- )
    CREATE DUP , 0 , ALLOT ;

: WL-EMPTY? ( wl -- f )
    CELL + @ 0= ;

: WL-EMPTY ( wl -- )
    CELL+ OFF ;

: WL-CAPACITY? ( u,wl -- f )
    DUP CELL+ @ ROT 1+ + SWAP @ < ;

: WL-WORDS ( wl -- adr )
    2 CELLS + ;

: WL-NEXT ( wl -- adr )
    DUP WL-WORDS SWAP CELL+ @ + ;

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
    SWAP WL-WORDS
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

: WL-NEIGHBORS ( adr,u,wl,ar -- )
   DUP AR-EMPTY
   SWAP DUP WL-NEXT
   SWAP WL-WORDS DO
        -ROT 2DUP I COUNT 
        ADJACENT? IF
            ROT I OVER AR-ADD
        ELSE
            ROT
        THEN
        I C@ 1+
    +LOOP 
    DROP 2DROP ;

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

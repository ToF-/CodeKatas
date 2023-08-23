\ word-ladder.fs
50000 CONSTANT MAX-WORD-BUFFER
CREATE WORD-BUFFER MAX-WORD-BUFFER ALLOT
VARIABLE NEXT-WORD
WORD-BUFFER NEXT-WORD !

: CLEAR-DICTIONARY
    WORD-BUFFER MAX-WORD-BUFFER ERASE
    WORD-BUFFER NEXT-WORD ! ;

: DICTIONARY-EMPTY? ( -- f )
    NEXT-WORD @ WORD-BUFFER = ;

: ADD-WORD ( addr,count -- )
    NEXT-WORD @
    2DUP C!
    1+ SWAP CMOVE
    NEXT-WORD DUP @ DUP C@ 1+ + SWAP ! ;
   
: (WORD-EXIST?) ( addr, count -- f )
    FALSE NEXT-WORD @ WORD-BUFFER 
        DO
        DROP 2DUP I COUNT COMPARE 
        0= IF
            TRUE LEAVE
        ELSE
            FALSE
        THEN
        I C@ 1+
    +LOOP
    -ROT 2DROP ;

: WORD-EXIST? ( addr, count -- f )
    DICTIONARY-EMPTY? IF
        2DROP FALSE
    ELSE
        (WORD-EXIST?)
    THEN ;

: SAME-SIZE? ( addr,count,addr,count -- f )
    NIP ROT DROP = ;

: (ADJACENT?) ( addr,count,addr,count -- f )
    DROP -ROT 0 -ROT 
    OVER + SWAP DO
        OVER C@ I C@ <> IF 1+ THEN
        SWAP 1+ SWAP
    LOOP 
    NIP
    1 = ;

: ADJACENT? ( addr,count,addr,count )
    2OVER 2OVER SAME-SIZE? IF
        (ADJACENT?)
    ELSE
        2DROP 2DROP FALSE
    THEN ;


1000 CONSTANT LINE-MAX
CREATE LINE-BUFFER LINE-MAX ALLOT

: READ-WORDS ( addr,count -- )
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER LINE-MAX
        ROT READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP ADD-WORD
    REPEAT DROP
    CLOSE-FILE THROW ;

: .(ADJACENTS) 
    NEXT-WORD @ WORD-BUFFER DO
        I COUNT 2DUP TYPE SPACE [CHAR] { EMIT SPACE
        NEXT-WORD @ WORD-BUFFER DO
            I COUNT 2OVER 2OVER
            ADJACENT? IF TYPE SPACE ELSE 2DROP THEN
            I C@ 1+ 
        +LOOP
        [CHAR] } EMIT CR
        NIP 1+
    +LOOP ;

: .ADJACENTS
    DICTIONARY-EMPTY? 0= IF .(ADJACENTS) THEN ;

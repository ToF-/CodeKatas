\ word-ladder.fs
50000 CONSTANT MAX-WORDS


: WL-CREATE ( <name> u )
    CREATE DUP , 0 , ALLOT ; 

: WL-EMPTY? ( wl -- f )
    2 CELLS + @ 0= ;

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

: WORD-LIST ( <name> u -- addr )
    CREATE HERE 2 CELLS + OVER + , HERE CELL + ,
    ALLOT ;

: WORD-LIST-MAX ( list -- n )
    @ ;

: WORD-LIST>WORDS ( list -- addr )
    2 CELLS + ;

: WORD-LIST-NEXT ( list -- addr )
    CELL+ ;

: WORD-LIST-NEXT@ ( list -- addr )
    WORD-LIST-NEXT @ ;

: WORD-LIST-NEXT! ( list,count -- )
    1+ SWAP WORD-LIST-NEXT +! ;

: WORD-LIST-CLEAR ( list -- )
    DUP WORD-LIST>WORDS
    OVER WORD-LIST-MAX OVER - ERASE
    DUP WORD-LIST>WORDS SWAP WORD-LIST-NEXT  ;

: WORD-LIST-EMPTY? ( list -- f )
    DUP WORD-LIST-NEXT@
    SWAP WORD-LIST>WORDS = ;

: (ADD-WORD-L) ( addr,count,list -- )
    2DUP WORD-LIST-NEXT@ -ROT      \ addr,count,next@,list,count
    WORD-LIST-NEXT!                \ addr,count,next@
    2DUP C! 1+ SWAP CMOVE ;

: WORD-LIST-CAPACITY? ( count,list -- f )
    DUP WORD-LIST-MAX -ROT
    WORD-LIST-NEXT@ + > ;

: ADD-WORD-L ( addr,count, list -- )
    OVER 1+ OVER
    WORD-LIST-CAPACITY? IF
        (ADD-WORD-L)
    ELSE
        S" word-list out of capacity" 
        EXCEPTION THROW 
    THEN ;

: FIND-WORD-L ( addr, count, list -- addr|f )
    DUP WORD-LIST-EMPTY? 0= IF
        FALSE OVER DUP WORD-LIST-NEXT@ 
        OVER WORD-LIST>WORDS DO
            DROP 2DUP I COUNT COMPARE
            IF FALSE ELSE I LEAVE THEN
            I C@ 1+
        +LOOP
    ELSE
        DROP FALSE
    THEN
    -ROT 2DROP ;


CREATE MAIN-LIST MAX-WORDS ALLOT
VARIABLE NEXT-WORD
MAIN-LIST NEXT-WORD !

: CLEAR-DICTIONARY
    MAIN-LIST MAX-WORDS ERASE
    MAIN-LIST NEXT-WORD ! ;

: DICTIONARY-EMPTY? ( -- f )
    NEXT-WORD @ MAIN-LIST = ;

: ADD-WORD ( addr,count -- )
    NEXT-WORD @
    2DUP C!
    1+ SWAP CMOVE
    NEXT-WORD DUP @ DUP C@ 1+ + SWAP ! ;
   
: (WORD-EXIST?) ( addr, count, list -- f )
    FALSE NEXT-WORD @ MAIN-LIST 
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
    NEXT-WORD @ MAIN-LIST DO
        I COUNT 2DUP TYPE SPACE [CHAR] { EMIT SPACE
        NEXT-WORD @ MAIN-LIST DO
            I COUNT 2OVER 2OVER
            ADJACENT? IF TYPE SPACE ELSE 2DROP THEN
            I C@ 1+ 
        +LOOP
        [CHAR] } EMIT CR
        NIP 1+
    +LOOP ;

: .ADJACENTS
    DICTIONARY-EMPTY? 0= IF .(ADJACENTS) THEN ;

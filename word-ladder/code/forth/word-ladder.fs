\ word-ladder.fs
50000 CONSTANT MAX-WORD-BUFFER 
CREATE WORD-BUFFER MAX-WORD-BUFFER ALLOT
VARIABLE NEXT-WORD
WORD-BUFFER NEXT-WORD !

: CLEAR-DICTIONARY
    WORD-BUFFER MAX-WORD-BUFFER ERASE
    WORD-BUFFER NEXT-WORD ! ;

: STORE-WORD ( addr,count -- )
    NEXT-WORD @
    2DUP C!
    1+ SWAP CMOVE
    NEXT-WORD DUP @ DUP C@ 1+ + SWAP ! ; 
    
: WORD-EXIST? ( addr, count -- f )
    WORD-BUFFER 0                   \ ta,tc,ca,f
    BEGIN
        OVER NEXT-WORD @ < WHILE    \ ta,tc,ca,f
        DROP COUNT                  \ ta,tc,ca,cc
        2OVER 2OVER                 \ ta,tc,ca,cc,ta,tc,ca,cc
        COMPARE 0= IF               \ ta,tc,ca,cc
            2DROP NEXT-WORD @ 1
        ELSE
            + 0
        THEN
    REPEAT
    NIP -ROT 2DROP ;

: ADD-WORD ( addr,count -- )
    STORE-WORD ;

1000 CONSTANT MAX-LINE
CREATE LINE-BUFFER MAX-LINE ALLOT
VARIABLE FD-IN

: READ-WORDS ( addr,count -- )
    R/O OPEN-FILE THROW FD-IN !
    BEGIN
        LINE-BUFFER MAX-LINE FD-IN @ READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP ADD-WORD
    REPEAT DROP
    FD-IN @ CLOSE-FILE THROW ;

: FIND-ADJACENT-WORDS ( addr,count -- a1,c1,..,n )
    2DROP
    0 ;

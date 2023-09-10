\ letter-set.fs

0  CONSTANT LS-EMPTY
CHAR a 1- CONSTANT LS-MIN
CHAR z 1+ LS-MIN - CONSTANT MAX-LETTER-SET
CREATE LS-CHARS-BUFFER MAX-LETTER-SET ALLOT

: LS-LENGTH@ ( ls -- n )
    0 SWAP
    MAX-LETTER-SET 0 DO
        DUP 1 AND IF SWAP 1+ SWAP THEN
        2/
    LOOP DROP ;

: CHAR>LETTER ( c -- n )
    LS-MIN - ;

: LETTER>CHAR ( n -- c )
    LS-MIN + ;

: LS-ADD-CHAR ( c,ls -- ls' )
    SWAP CHAR>LETTER 1 SWAP LSHIFT OR ;

: LS-CHARS>C! ( c -- )
    LS-CHARS-BUFFER COUNT
    DUP >R + C!  R> 1+
    LS-CHARS-BUFFER C! ;

: LS-CHARS ( ls -- addr,l )
    0 LS-CHARS-BUFFER C!
    MAX-LETTER-SET 0 DO
        DUP 1 AND IF
            I LETTER>CHAR LS-CHARS>C!
        THEN
        2/
    LOOP
    DROP
    LS-CHARS-BUFFER COUNT ;



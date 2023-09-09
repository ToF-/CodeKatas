\ letter-set.fs

27 CONSTANT MAX-LETTER
CHAR a 1- CONSTANT LS-MIN

: LS-EMPTY ( -- ls )
    0 ;

: C>LETTER ( c -- n )
    LS-MIN - ;

: LETTER>C ( n -- c )
    LS-MIN + ;

: LETTER-SET>S ( ls,addr -- )
    DUP -ROT
    MAX-LETTER 0 DO
        OVER 1 AND IF
            1+ I LETTER>C OVER C!
        THEN
        SWAP 2/ SWAP
    LOOP
    NIP OVER - SWAP C! ;

: LS-ADD-LETTER ( c,ls -- ls' )
    SWAP C>LETTER 1 SWAP LSHIFT OR ;

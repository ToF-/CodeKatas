\ wordladder.fs

5 CONSTANT BITS/LETTER
31 CONSTANT LETTER-MASK

: C>LETTER-VALUE ( c -- n )
    TOUPPER [CHAR] A - 1+ ;

: LETTER-VALUE>C ( n -- c )
    1- [CHAR] A + ;

: S-REVERSE! ( addr, count -- )
    1- OVER +
    BEGIN
        2DUP U< WHILE   \ start,end
        2DUP C@ >R C@   \ start,end,cs
        OVER C!         \ start,end
        OVER R> SWAP C! \ start,end
        SWAP 1+ SWAP 1-
    REPEAT 2DROP ;

: S-CHARS ( addr, count -- c0,c1..cCount-1)
    OVER + SWAP DO I C@ LOOP ;

: S>WORD-KEY ( addr, count -- u )
    2DUP S-REVERSE! DUP >R
    S-CHARS 0
    R> 0 DO
        BITS/LETTER LSHIFT
        SWAP C>LETTER-VALUE OR
    LOOP ;

: WORD-KEY>S ( addr, u -- addr, count )
    0 -ROT BEGIN                   \ count,addr,u
        DUP WHILE                  \ count,addr,u
        DUP LETTER-MASK AND        \ count,addr,u,l
        LETTER-VALUE>C             \ count,addr,u,c
        2>R 2DUP + 2R>             \ count,addr,addr,u,c
        ROT C!                     \ count,addr,u
        BITS/LETTER RSHIFT         \ count,addr,u'
        ROT 1+ -ROT                \ count',addr,u'
    REPEAT DROP SWAP 
    2DUP S-REVERSE! ;

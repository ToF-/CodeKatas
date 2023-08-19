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

: CHAR>>WORD-KEY ( c,u -- u' )
    BITS/LETTER LSHIFT
    SWAP C>LETTER-VALUE OR ;

: S>WORD-KEY ( addr, count -- u )
    2DUP S-REVERSE! 
    DUP >R S-CHARS 0 R>
    0 DO CHAR>>WORD-KEY LOOP ;

: WORD-KEY>>CHAR ( u -- u',c )
    DUP LETTER-MASK AND LETTER-VALUE>C
    SWAP BITS/LETTER RSHIFT SWAP ;

: WORD-KEY>S ( addr, u -- addr, count )
    0 SWAP BEGIN        \ addr,count,u
        DUP WHILE       
        WORD-KEY>>CHAR  \ addr,count,u',c
        2OVER + C!      
        SWAP 1+ SWAP    \ addr,count',u'
    REPEAT DROP
    2DUP S-REVERSE! ;

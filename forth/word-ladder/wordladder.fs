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

: SIZE>>WORD-KEY ( n,u -- u' )
    2 LSHIFT SWAP 2 - OR ;

: (S>WORD-KEY) ( addr, count -- u )
    DUP -ROT
    2DUP S-REVERSE!
    DUP >R S-CHARS
    0 R> 0 DO CHAR>>WORD-KEY LOOP
    SIZE>>WORD-KEY ;

: S>WORD-KEY ( addr, count -- u )
    DUP 3 6 WITHIN IF
        (S>WORD-KEY)
    ELSE
        s" word size must be 3,4 or 5"
        EXCEPTION THROW
    THEN ;

: WORD-KEY>>CHAR ( u -- u',c )
    DUP LETTER-MASK AND LETTER-VALUE>C
    SWAP BITS/LETTER RSHIFT SWAP ;

: WORD-KEY>>SIZE ( u -- u',n )
    DUP 2 RSHIFT SWAP
    3 AND 2 + ;

: WORD-KEY>S ( u,addr -- addr, count )
    SWAP WORD-KEY>>SIZE   \ addr,u,count
    ROT SWAP 2DUP 2>R     \ u,addr,count [addr,count]
    OVER + SWAP DO
        WORD-KEY>>CHAR I C!
    LOOP DROP
    2R> 2DUP S-REVERSE! ;

: WORD-KEY-LETTER ( u,i -- n )
    SWAP WORD-KEY>>SIZE    \ i,u',s
    1- ROT -               \ u,offset
    BITS/LETTER * RSHIFT
    LETTER-MASK AND ;

: WILDCARD! ( u,i -- u' )
    OVER WORD-KEY>>SIZE NIP
    1- SWAP - BITS/LETTER * 2 + 
    LETTER-MASK SWAP LSHIFT OR ;

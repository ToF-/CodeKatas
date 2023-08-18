\ wordladder.fs

5 CONSTANT BITS/LETTER
2 BASE ! 11111 CONSTANT LETTER-MASK DECIMAL

VARIABLE LETTERS/WORD
5 LETTERS/WORD !

: #LETTERS/WORD
    LETTERS/WORD @ ;

: CHAR>LETTER-VALUE ( char -- lv )
    TOUPPER [CHAR] A - 1+ ;

: LETTER-VALUE>CHAR ( lv -- char )
   [CHAR] A + 1- ;

: S>WORD-KEY ( addr,count -- wk )
    0 -ROT
    OVER + SWAP DO
        BITS/LETTER LSHIFT
        I C@ CHAR>LETTER-VALUE OR
    LOOP ;

: WORD-KEY>S ( wk,addr -- addr,count )
    #LETTERS/WORD OVER +
    BEGIN
        2DUP <= WHILE
            
    DO
    #LETTERS/WORD 0 DO
        OVER  BITS/LETTER RSHIFT    \ wk,addr,wk'
        -ROT SWAP LETTER-MASK AND   \ wk',addr,lv
        LETTER-VALUE>CHAR           \ wk',addr,c
        OVER #LETTERS/WORD + 1- I - C!      \ wk',addr
    LOOP NIP #LETTERS/WORD ;
        
: UPPERCASE! ( addr,count -- )
    OVER + SWAP DO
        I C@ TOUPPER I C!
    LOOP ;

: LETTER>INDEX ( char --  1..26 )
    [CHAR] A - 1+ ;

: INDEX>LETTER ( n -- char )
    1- [CHAR] A + ;

: S>KEY ( addr,count -- flvalue )
    2DUP UPPERCASE!
    0 -ROT 
    OVER + SWAP
    DO BITS/LETTER LSHIFT I C@ LETTER>INDEX 31 AND + LOOP ;

: FLWORD>CHARS ( flvalue -- c4,c3,c2,c1,c0 )
    LETTERS/WORD @ 0 DO
        DUP 31 AND INDEX>LETTER
        SWAP BITS/LETTER RSHIFT
    LOOP DROP ;

: KEY>S ( flvalue, addr -- )
    >R FLWORD>CHARS R>
    DUP LETTERS/WORD @ + SWAP DO I C! LOOP ;

: MASK-POS \ n -- ((WS-1)-n) * BL
    1+ LETTERS/WORD @ SWAP - BITS/LETTER * ;

: L-MASK ( n -- bit pattern )
    MASK-POS 31 SWAP LSHIFT ;

: MASK ( n -- bitmask zeroing byte n )
    L-MASK -1 XOR AND ;

: VAL:ROOT-KEY ( flv, n -- index, flv )
    2DUP L-MASK AND OVER MASK-POS RSHIFT
    -ROT MASK ;

: ROOT-KEY|VAL ( flv,l,n -- flv )
    MASK-POS LSHIFT OR ; 

: NEIGHBOR? ( flw,fwl -- f )
    0 -ROT
    LETTERS/WORD @ 0 DO
        2DUP I MASK
        SWAP I MASK
        = IF ROT 1+ -ROT THEN
    LOOP
    2DROP
    1 = ;


REQUIRE ffl/act.fs

ACT-CREATE FLWORDS

' - FLWORDS ACT-COMPARE!

: ADD-WORD ( addr,count -- )
    S>KEY 0 SWAP FLWORDS ACT-INSERT ;

: IS-WORD? ( addr,count -- flag )
    S>KEY FLWORDS ACT-HAS? ;
    
    

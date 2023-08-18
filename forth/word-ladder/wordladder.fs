\ wordladder.fs

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
    DO 5 LSHIFT I C@ LETTER>INDEX 31 AND + LOOP ;

: FLWORD>CHARS ( flvalue -- c4,c3,c2,c1,c0 )
    5 0 DO
        DUP 31 AND INDEX>LETTER
        SWAP 5 RSHIFT
    LOOP DROP ;

: KEY>S ( flvalue, addr -- )
    >R FLWORD>CHARS R>
    DUP 5 + SWAP DO I C! LOOP ;

: MASK-POS ( n -- n )
    4 SWAP - 5 * ;

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
    5 0 DO
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
    
    

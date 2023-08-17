\ wordladder.fs

: S>FLWORD ( addr,count -- flvalue )
    0 -ROT 
    OVER + SWAP
    DO 8 LSHIFT I C@ 255 AND + LOOP ;

: FLWORD>CHARS ( flvalue -- c4,c3,c2,c1,c0 )
    5 0 DO
        DUP 255 AND
        SWAP 8 RSHIFT
    LOOP DROP ;

: FLWORD>S ( flvalue, addr -- )
    >R FLWORD>CHARS R>
    DUP 5 + SWAP DO I C! LOOP ;

: MASK ( n -- bitmask zeroing byte n )
    8 * 255 SWAP LSHIFT -1 XOR AND ;




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
    S>FLWORD 0 SWAP FLWORDS ACT-INSERT ;

: IS-WORD? ( addr,count -- flag )
    S>FLWORD FLWORDS ACT-HAS? ;
    
    

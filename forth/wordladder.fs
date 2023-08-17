\ wordladder.fs
2 BASE !
00011111 CONSTANT AZ-MASK
DECIMAL

: LVALUE ( char -- lvalue )
    AZ-MASK AND 1- ;

: S>5L-VALUE ( addr,count -- flvalue )
    0 -ROT 
    OVER + SWAP
    DO 26 * I C@ LVALUE + LOOP ;

: 5L-VALUE-CHARS ( flvalue -- ch4,ch3,ch2,ch1,ch0 )
    5 0 DO
        26 /MOD SWAP [CHAR] A + SWAP
    LOOP DROP ;

: .5L ( flvalue -- )
    5L-VALUE-CHARS 
    5 0 DO EMIT LOOP ;

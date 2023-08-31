\ wl-wordgroup.fs
REQUIRE ./wl-word.fs

CHAR ~ CONSTANT WILDCARD
255 CONSTANT G-INFO-MASK
15 4 LSHIFT -1 XOR CONSTANT ZERO-INDEX

CREATE WL-GROUP CELL ALLOT

: G>INDEX@ ( l -- n )
    4 RSHIFT 15 AND ;

: G<INDEX! ( l -- n )
    4 LSHIFT OR ;

: GROUP-INDEX ( wg -- n )
    G>INDEX@ ;

: FIND-GROUP-INDEX ( wg -- n )
    WL-GROUP !
    0 WL-GROUP COUNT 15 AND
    OVER + SWAP ?DO
        I C@ WILDCARD = IF
            LEAVE
        THEN
        1+
    LOOP ;
    
: WG ( <cccc> -- w )
    WW DUP FIND-GROUP-INDEX 
    4 LSHIFT OR ;

: L-OFFSET ( c,n -- w )
    1+ 8 * LSHIFT ;

: R-OFFSET ( c,n -- w )
    1+ 8 * RSHIFT ;

: ZERO-MASK ( n -- w )
    255 SWAP L-OFFSET -1 XOR ;

: WILDCARD-MASK ( n -- w )
    WILDCARD SWAP L-OFFSET ;

: W>GROUP ( w,n -- w )
    TUCK
    DUP ZERO-MASK ROT AND
    SWAP WILDCARD-MASK OR
    SWAP G<INDEX! ;

: W>GROUP>LETTER ( g,n -- w,c )
    2DUP R-OFFSET 255 AND 
    -ROT W>GROUP SWAP ;

: GROUP>LETTER>W ( g,c -- w )
    OVER GROUP-INDEX       \ g,c,n
    ROT OVER ZERO-MASK AND \ c,n,g
    -ROT L-OFFSET OR
    ZERO-INDEX AND ;

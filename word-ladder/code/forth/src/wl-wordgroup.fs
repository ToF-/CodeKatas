\ wl-wordgroup.fs
REQUIRE ./wl-word.fs
REQUIRE ./wl-letterset.fs


CHAR ~ CONSTANT WILDCARD

255 CONSTANT G-INFO-MASK
128 CONSTANT G-BIT

\ mask for zeroing the index
15 4 LSHIFT -1 XOR CONSTANT ZERO-INDEX

\ a buffer for conversion
CREATE WL-GROUP CELL ALLOT

\ get the group index number
: GROUP-INDEX ( g -- n )
    4 RSHIFT 7 AND ;

\ set the group index number
: SET-GROUP-INDEX ( g -- n )
    8 OR 4 LSHIFT OR ;

\ a group-bit to 1 means it's a group
\ not a word
: IS-WORD-GROUP? ( g -- f )
    G-BIT AND ;

\ find the position of the wildcard
\ in a word
: FIND-GROUP-INDEX ( w -- n )
    WL-GROUP WL-WORD>S
    0                        \ accum
    WL-GROUP COUNT 15 AND
    OVER + SWAP ?DO
        I C@ WILDCARD = IF LEAVE THEN
        1+
    LOOP ;

\ declare a named word group
: WG ( <cccc> -- g )
    WW DUP FIND-GROUP-INDEX
    SET-GROUP-INDEX ;

\ shift a char at the nth position
: L-OFFSET ( c,n -- w )
    1+ 8 * LSHIFT ;

\ unshift a char from the nth position
: R-OFFSET ( c,n -- w )
    1+ 8 * RSHIFT ;

\ wipe char at position n
: ZERO-MASK ( n -- w )
    255 SWAP L-OFFSET -1 XOR ;

\ wildcar at position n
: WILDCARD-MASK ( n -- w )
    WILDCARD SWAP L-OFFSET ;

\ convert a word in a group with index n
: W>GROUP ( w,n -- g )
    TUCK
    DUP ZERO-MASK ROT AND
    SWAP WILDCARD-MASK OR
    SWAP SET-GROUP-INDEX ;

\ convert a word and position into a group and char
: W>GROUP>LETTER ( w,n -- g,c )
    2DUP R-OFFSET 255 AND 
    -ROT W>GROUP SWAP ;

\ convert a group and char back into a word
: GROUP>LETTER>W ( g,c -- w )
    OVER GROUP-INDEX       \ g,c,n
    ROT OVER ZERO-MASK AND \ c,n,g
    -ROT L-OFFSET OR
    ZERO-INDEX AND ;

\ a group displays like a word,
\ except the index number has to
\ be removed from the length byte
: .WL-GROUP ( wg -- )
    ZERO-INDEX AND .WL-WORD ;

\ wl-wordgroup.fs
REQUIRE ./wl-word.fs
REQUIRE ./wl-dict.fs
REQUIRE ./wl-letterset.fs

CHAR ~ CONSTANT WILDCARD
255 CONSTANT G-INFO-MASK
15 4 LSHIFT -1 XOR CONSTANT ZERO-INDEX

CREATE WL-GROUP CELL ALLOT

: G>INDEX@ ( l -- n )
    4 RSHIFT 7 AND ;

: G<INDEX! ( l -- n )
    8 OR 4 LSHIFT OR ;

: IS-WORD-GROUP? ( wg -- f )
    8 4 LSHIFT AND ;

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
    G<INDEX! ;

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

: W>GROUPS ( w -- g1,..gn,n )
    DUP WL-WORD-LENGTH >R
    R@ 0 ?DO                \ w
        DUP I W>GROUP       \ w,g
        SWAP
    LOOP                    \ ..,w
    DROP R> ;

: W>GROUP>LETTER ( w,n -- g,c )
    2DUP R-OFFSET 255 AND 
    -ROT W>GROUP SWAP ;

: GROUP>LETTER>W ( g,c -- w )
    OVER GROUP-INDEX       \ g,c,n
    ROT OVER ZERO-MASK AND \ c,n,g
    -ROT L-OFFSET OR
    ZERO-INDEX AND ;

: GROUP-LETTERS>WORDS ( ls,wg -- w1..wn,n )
    SWAP PAD LS>S               \ wg
    PAD COUNT DUP >R            \ wg,ad,n
    OVER + SWAP DO              \ wg
        DUP I C@                \ wg,wg,c
        GROUP>LETTER>W          \ wg,ww
        SWAP
    LOOP R>                     ;

: WL-GROUP-DICT ( -- gd )
    WL-DICT ;

: .WLGD-GROUP-LETTERS ( ls,g -- )
    PAD WL-WORD>S
    PAD COUNT 15 AND TYPE ."  -> "
    PAD LS>S
    PAD COUNT TYPE ;
    
: WLGD-UPDATE-GROUP ( c,g,gd -- )
    2DUP WLD-VALUE-OR-NIL     \ c,g,gd,ls
    -ROT 2SWAP LS-ADD-LETTER  \ g,gd,ls
    -ROT WLD-UPDATE ;

: WLGD-ADD-WORD ( w,gd -- )
    OVER WL-WORD-LENGTH 0 ?DO  \ w,gd
        2DUP SWAP              \ w,gd,gd,w
        I W>GROUP>LETTER       \ w,gd,gd,g,c
        SWAP ROT               \ w,gd,c,g,gd
        WLGD-UPDATE-GROUP      \ w,gd
    LOOP
    2DROP ;

: WLGD-LETTERS ( w,gd -- )
    WLD-VALUE-OR-NIL ;

: .WLGD ( gd -- )
    ['] .WLGD-GROUP-LETTERS
    SWAP ACT-EXECUTE ;

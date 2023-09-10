\ small-string.fs

CREATE SS-BUFFER CELL ALLOT

: SMALL-STRING ( add,l -- ss )
    SS-BUFFER CELL ERASE
    SS-BUFFER PLACE
    SS-BUFFER @ ;

: SMALL-STRING-S ( ss -- add,l )
    SS-BUFFER !
    SS-BUFFER COUNT ;

: SMALL-STRING-C! ( c,n,ss -- ss' )
    SMALL-STRING-S DROP + C!
    SS-BUFFER @ ;

: SMALL-STRING-C@ ( ss,n -- c )
    SWAP SMALL-STRING-S DROP + C@ ;



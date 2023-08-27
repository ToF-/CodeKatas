\ wordkey.fs

CREATE S>KEY-BUFFER CELL ALLOT
CREATE EXTRA-S>KEY-BUFFER CELL ALLOT

: (S>KEY) ( ad,l -- u )
    S>KEY-BUFFER DUP CELL ERASE
    2DUP C!
    1+ SWAP CMOVE
    S>KEY-BUFFER @ ;

: S>KEY ( ad,l -- u )
    DUP 8 < IF (S>KEY) ELSE
        S" S>KEY : string too large" EXCEPTION THROW 
    THEN ;

: KEY>S ( u,ad -- ad+1,l )
    DUP CELL ERASE
    TUCK !
    COUNT ;

: KEY"
    [CHAR] " WORD COUNT S>KEY ;


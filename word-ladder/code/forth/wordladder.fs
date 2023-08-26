\ wordladder.fs

: (ADJACENT?) ( ad1,ad2,l -- f )
    OVER + SWAP 2>R
    FALSE SWAP
    2R> ?DO             \ acc,ad1
        DUP C@ I C@ <> IF
           SWAP 1+ SWAP
        THEN
        1+
    LOOP
    DROP 1 = ;

: ADJACENT? ( ad,l,ad,l -- f )
    ROT OVER = IF
        (ADJACENT?)
    ELSE
        DROP 2DROP FALSE
    THEN ;

CREATE S>KEY-BUFFER CELL ALLOT

: (S>KEY) ( ad,l -- u )
    S>KEY-BUFFER DUP CELL ERASE
    2DUP C!
    1+ SWAP CMOVE
    S>KEY-BUFFER @ ;

: S>KEY ( ad,l -- u )
    DUP 8 < IF (S>KEY) ELSE
        CR ." S>KEY : string too large" CR
        2DROP 0
    THEN ;

: KEY>S ( u -- pad+1,l )
    PAD DUP CELL ERASE
    TUCK !
    COUNT ;

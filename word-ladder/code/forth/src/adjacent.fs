\ adjacent.fs

: (ADJACENT?) ( ad1,ad2,l -- f )
    ROT 0 SWAP 2SWAP
    OVER + SWAP ?DO
        DUP C@ I C@ <> IF SWAP 1+ SWAP THEN
        1+ 
    LOOP DROP 1 = ;

: ADJACENT? ( ad,l,ad,l -- f )
    ROT OVER = IF
        (ADJACENT?)
    ELSE
        DROP 2DROP FALSE
    THEN ;

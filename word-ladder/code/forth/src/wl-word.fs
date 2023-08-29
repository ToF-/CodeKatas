\ wl-word.fs

: LOWERCASE? ( c -- f )
    [CHAR] a [CHAR] z 1+ WITHIN ;

: WL-CHECK-WORD ( ad,l -- f )
    DUP 0 8 WITHIN IF 
        TRUE -ROT
        OVER + SWAP ?DO
            I C@ LOWERCASE? 0= IF
                DROP FALSE
                LEAVE
            THEN
        LOOP
    ELSE
        2DROP FALSE 
    THEN ;

CREATE WL-WORD-BUFFER CELL ALLOT

: S-COPY ( ad,l,dest -- )
    2DUP C! 1+ SWAP CMOVE ;

: S>WL-WORD ( ad,l -- w )
    2DUP WL-CHECK-WORD 0= IF
        S" invalid wl word"
        EXCEPTION THROW
    THEN
    WL-WORD-BUFFER OFF
    WL-WORD-BUFFER S-COPY
    WL-WORD-BUFFER @ ;

: WL-WORD ( <cccc> -- w )
    BL WORD COUNT S>WL-WORD ;

: WL-WORD>S ( w,ad -- )
    ! ; 

    

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

: C-MASK ( n -- b )
    255 AND ;

: NEXT-C ( n -- n' )
    8 RSHIFT ;

: NEXT>>C ( n -- n',c )
    DUP NEXT-C
    SWAP C-MASK ;

: WL-WORD-LENGTH ( w -- l )
    C-MASK ;

: (WL-ADJACENT?) ( w1,w2 -- f )
    0 -ROT 
    DUP WL-WORD-LENGTH 0 ?DO   \ acc,w1,w2
        NEXT>>C                \ acc,w1,w2',c2
        ROT NEXT>>C            \ acc,w2',c2,w1',c1
        ROT <> IF              \ acc,w2',w1'
            ROT 1+ -ROT
        THEN
    LOOP
    2DROP 1 = ;

: WL-ADJACENT? ( w1,w2 -- f )
    2DUP WL-WORD-LENGTH
    SWAP WL-WORD-LENGTH = IF
        (WL-ADJACENT?)
    ELSE
        2DROP FALSE
    THEN ;
    

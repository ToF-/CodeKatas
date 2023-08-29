\ groups.fs
REQUIRE ffl/act.fs

: S-COPY ( a,l,dest -- )
    2DUP C!
    1+ SWAP CMOVE ;

: NTH-C! ( c,i,dest -- )
    + C! ;

: S>NTH-GROUP ( ad,l,i,dest -- dest+1,l )
    DUP >R 2SWAP ROT      \ i,ad,l,dest
    S-COPY                \ i
    R@ [CHAR] ~ -ROT      \ c,i,dest
    1+ NTH-C!
    R> COUNT ;

0 CONSTANT LS-EMPTY

: NTH-BIT ( n -- cell )
    1 SWAP LSHIFT ;
    
: LS-ADD-LETTER ( c,ls  -- ls' )
    SWAP [CHAR] ` - NTH-BIT OR ;

: LS>S ( ls,pad -- ad,l )
    DUP ROT
    27 1 DO
        I NTH-BIT OVER AND IF
            SWAP 1+
            I [CHAR] ` + OVER C!
            SWAP
        THEN
    LOOP 
    DROP OVER - 
    OVER C! COUNT ;

: S>GROUP-LETTER ( ad,l,i,dest -- dest+1,l,c )
    OVER >R
    2SWAP OVER >R 2SWAP
    S>NTH-GROUP
    2R> SWAP + C@ ;

: GROUP-DICTIONARY ( <name> -- )
    ACT-CREATE ;

CREATE GD-BUFFER CELL 1+ ALLOT
CREATE GD-LETTER 1 ALLOT
2VARIABLE GD-SOURCE 

: GD-ADD-WORD ( ad,l,gd -- )
    OVER 0 8 WITHIN 0= IF s" word too large" EXCEPTION THROW THEN
    -ROT                       \ gd,ad,l
    2DUP GD-SOURCE 2!          \ gd,ad,l
    NIP 0 ?DO                  \ gd
        GD-SOURCE 2@           \ gd,ad,l
        I GD-BUFFER            \ gd,ad,l,i,dst
        S>GROUP-LETTER         \ gd,dst+1,l,c
        -ROT S>KEY             \ gd,c,k
        ROT 2DUP               \ c,k,gd,k,gd
        ACT-GET                \ c,k,gd,v/f
        0= IF 0 THEN           \ c,k,gd,ls
        -ROT 2SWAP             \ k,gd,c,ls
        LS-ADD-LETTER          \ k,gd,ls'
        ROT DUP >R             \ k,ls',gd
        ACT-INSERT R>          \ gd
    LOOP DROP ;

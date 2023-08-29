\ groups.fs

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

: S>GROUP-LETTER ( ad,l,i,dest -- ad,l,c )
    OVER >R
    2SWAP OVER >R 2SWAP
    S>NTH-GROUP
    2R> SWAP + C@ ;

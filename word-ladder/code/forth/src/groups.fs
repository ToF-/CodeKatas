\ groups.fs
REQUIRE ffl/act.fs
REQUIRE ./wordkey.fs
REQUIRE ./dict.fs

: S-COPY ( a,l,dest -- )
    2DUP C!
    1+ SWAP CMOVE ;

: NTH-C! ( c,i,dest -- )
    + C! ;

: CHECK-WORD-LENTGH ( n -- )
    0 8 WITHIN 0= IF 
        s" word too large" 
        EXCEPTION THROW
    THEN ;

: CHECK-GROUP-INDEX ( n,i -- )
    0 ROT WITHIN 0= IF 
        s" index too large" 
        EXCEPTION THROW 
    THEN ;


: GROUP-COUNT ( ad -- ad+1,count )
    COUNT 15 AND ;

: GROUP-INDEX ( ad -- n )
    COUNT 4 RSHIFT NIP ;

: S>NTH-GROUP ( ad,l,i,dest -- )
    >R
    OVER CHECK-WORD-LENTGH
    2DUP CHECK-GROUP-INDEX
    2DUP 4 LSHIFT OR R@ C!
    -ROT R@ 1+ SWAP CMOVE
    1+ [CHAR] ~ SWAP R> NTH-C! ;

0 CONSTANT LS-EMPTY

: NTH-BIT ( n -- cell )
    1 SWAP LSHIFT ;
    
: LS-ADD-LETTER ( c,ls  -- ls' )
    SWAP [CHAR] ` - NTH-BIT OR ;

: LS>S ( ls,pad -- )
    DUP ROT
    27 1 DO
        I NTH-BIT OVER AND IF
            SWAP 1+
            I [CHAR] ` + OVER C!
            SWAP
        THEN
    LOOP 
    DROP OVER - 
    OVER C! DROP ;

: S>GROUP-LETTER ( ad,l,i,dest -- c )
    2OVER 2OVER S>NTH-GROUP
    DROP NIP + C@ ;

: GROUP-DICTIONARY ( <name> -- )
    DICT ;

: GD-LETTER-SET ( ad,l,gd -- ls )
    -ROT S>KEY SWAP
    D-VALUE-OR-NIL ;

CREATE GD-BUFFER CELL 1+ ALLOT
CREATE GD-LETTER 1 ALLOT
2VARIABLE GD-SOURCE 

: GD-ADD-WORD ( ad,l,gd -- )
    OVER CHECK-WORD-LENTGH
    -ROT                       \ gd,ad,l
    2DUP GD-SOURCE 2!          \ gd,ad,l
    NIP 0 ?DO                  \ gd
        GD-SOURCE 2@           \ gd,ad,l
        I GD-BUFFER            \ gd,ad,l,i,dst
        S>GROUP-LETTER         \ gd,c
        GD-BUFFER GROUP-COUNT S>KEY  \ gd,c,k
        ROT 2DUP               \ c,k,gd,k,gd
        D-VALUE-OR-NIL         \ c,k,gd,ls
        -ROT                   \ c,ls,k,gd
        2SWAP LS-ADD-LETTER    \ k,gd,ls'
        -ROT DUP >R            \ ls',k,gd
        D-UPDATE R>            \ gd
    LOOP DROP ;


\ word-graph.fs
REQUIRE adjacent.fs
REQUIRE dict.fs
REQUIRE wordkey.fs

: WORD-GRAPH ( <name> -- )
    DICT ;

: WG-ADD-WORD ( ad,l,wg -- )
    -ROT S>KEY SWAP
    0 -ROT D-UPDATE ;

: WG-KEY-PRED@ ( k,wg -- k )
    D-VALUE ;

: WG-PRED@ ( ad,l,wg,dest -- dest+1,l )
    2SWAP S>KEY
    ROT WG-KEY-PRED@
    SWAP KEY>S ;

: WG-KEY-PRED! ( kp,k,wg -- )
    D-UPDATE ;

: WG-PRED! ( adp,lp,ad,l,wg -- )
    >R S>KEY        \ adp,lp,k
    -ROT S>KEY      \ k,fk
    SWAP R>         \ fk,k,wg
    WG-KEY-PRED! ;

CREATE K-BUFFER-A CELL ALLOT
CREATE K-BUFFER-B CELL ALLOT

: KEY-ADJACENT? ( t,k -- f )
    K-BUFFER-A KEY>S
    ROT K-BUFFER-B KEY>S 
    ADJACENT? ;
    
: (WG-ADD-ADJACENT) ( q,wg,t,k -- q,wg,t )
    2OVER 2OVER       \ q,wg,t,k,q,wg,t,k
    ROT WG-KEY-PRED!  \ q,wg,t,k,q
    Q-APPEND ;        \ q,wg

: WG-ADD-ADJACENT ( q,wg,t,v,k -- q,wg,t )
    SWAP 0= >R 
    2DUP KEY-ADJACENT? 
    R> AND IF
        (WG-ADD-ADJACENT)
    ELSE
        DROP
    THEN ;

: WG-ADJACENTS ( q,wg -- )
    2DUP                     \ q,wg,q,wg
    SWAP Q-POP               \ q,wg,wg,t
    SWAP ['] WG-ADD-ADJACENT \ q,wg,t,wg,xt
    SWAP ACT-EXECUTE DROP 2DROP ;

: .KEY-STEP ( v,k -- )
    OVER IF 
        PAD KEY>S TYPE SPACE ." -> " 
        DUP -1 = IF
            DROP ." nil"
        ELSE
            PAD KEY>S TYPE 
        THEN ." , " 
    ELSE
        2DROP
    THEN ;

: .KEY ( k -- )
    PAD KEY>S ." looking for neighbors of " TYPE CR ;

: WG-INIT-SEARCH-PATH ( s,wg -- )
    -1 -ROT WG-KEY-PRED! ;

: WG-SEARCH-PATH ( s,t,q,wg -- )
    ROT >R ROT >R
    R@ OVER WG-INIT-SEARCH-PATH
    OVER R> SWAP Q-APPEND
    BEGIN
        ['] .KEY-STEP OVER ACT-EXECUTE CR
        OVER Q-EMPTY? 0= WHILE
        OVER Q-HEAD@
        R@ = IF
            OVER Q-EMPTY
        ELSE
            2DUP WG-ADJACENTS 
        THEN
    REPEAT
    >R DROP 2DROP ;



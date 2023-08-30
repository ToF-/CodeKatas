\ wl-graph.fs
REQUIRE wl-word.fs
REQUIRE wl-dict.fs
REQUIRE queue.fs

-1 CONSTANT WLG-START

: WL-GRAPH ( <name> -- )
    WL-DICT ;

: WLG-ADD-WORD ( w,g -- )
    0 -ROT WLD-UPDATE ;

: WLG-PRED@ ( w,g -- w )
    WLD-VALUE ;

: WLG-PRED! ( p,w,g -- w )
    WLD-UPDATE ;

: WLG-START! ( w,g -- )
    WLG-START -ROT WLG-PRED! ;

: WLG-HAS-WORD? ( w,g -- f )
    ACT-HAS? ;

: .WLG-ITEM ( v,k -- )
    .WL-WORD
    DUP WLG-START = IF
        ." ->*" DROP
    ELSE ?DUP IF
        ." ->" .WL-WORD
    THEN THEN 9 EMIT ;

: (WLG-ADD-ADJACENT) ( w,g,k -- )
    ROT 2DUP WL-ADJACENT? IF
        SWAP ROT WLG-PRED!
    ELSE
        2DROP DROP
    THEN ;

: .WLG ( g -- )
    ['] .WLG-ITEM SWAP ACT-EXECUTE ; 

: WLG-ADD-ADJACENT ( w,g,v,k -- w,g )
    2OVER 2SWAP                \ w,g,w,g,v,k
    SWAP 0= IF                 \ w,g,w,g,k
        (WLG-ADD-ADJACENT)
    ELSE
        2DROP DROP
    THEN ;

: WLG-SEARCH-ADJACENTS! ( w,g -- )
    ['] WLG-ADD-ADJACENT
    OVER ACT-EXECUTE
    2DROP ;

: (WLG-QUEUE-ADJACENT) ( v,k,w,q -- )
    SWAP -ROT 2SWAP    \ k,q,v,w
    = IF Q-APPEND ELSE 2DROP THEN ;

: WLG-QUEUE-ADJACENT ( w,q,v,k -- w,q )
    2OVER (WLG-QUEUE-ADJACENT) ;

: WLG-QUEUE-ADJACENTS ( w,q,g -- )
    ASSERT( 2DUP <> )
    ['] WLG-QUEUE-ADJACENT
    SWAP 
    ACT-EXECUTE
    2DROP ;

: (WLG-SEARCH-PATH) ( q,g -- )
    OVER Q-POP SWAP             \ q,w,g
    2DUP WLG-SEARCH-ADJACENTS!  \ q,w,g
    SWAP -ROT                   \ w,q,g
    WLG-QUEUE-ADJACENTS ;

: .WL-WORD-QUEUE ( q -- )
    DUP CAR-LENGTH@ 0 ?DO I OVER CAR-GET .WL-WORD SPACE LOOP DROP ;

: WLG-SEARCH-PATH ( s,t,q,g -- )
    ROT >R                     \ s,q,g
    DUP WLD-CLEAR-VALUES       \ s,q,g
    ROT SWAP 2DUP              \ q,s,g,s,g
    WLG-START!                 \ q,s,g
    -ROT OVER                  \ g,q,s,q
    Q-APPEND                   \ g,q
    SWAP                       \ q,g
    BEGIN                      \ q,g
        OVER Q-EMPTY? 0= WHILE
        OVER Q-HEAD@ R@ = IF
            OVER Q-EMPTY
            R> DROP 0 >R
        ELSE 
            2DUP (WLG-SEARCH-PATH)
        THEN
    REPEAT
    2DROP R> 0= ;


\ wl-graph.fs
REQUIRE wl-word.fs
REQUIRE wl-dict.fs

-1 CONSTANT WLG-START

: WL-GRAPH ( <name> -- )
    WL-DICT ;

: WLG-ADD-WORD ( w,wlg -- )
    0 -ROT WLD-UPDATE ;

: WLG-PRED@ ( w,wlg -- w )
    WLD-VALUE ;

: WLG-PRED! ( p,w,wlg -- w )
    WLD-UPDATE ;

: WLG-START! ( w,wlg -- )
    WLG-START -ROT WLG-PRED! ;

: (WLG-ADD-ADJACENT) ( w,wlg,v,k -- )
    NIP             \ w,wlg,k
    ROT TUCK OVER   \ wlg,w,k,w,k
    WL-ADJACENT? IF \ wlg,w,k
        ROT WLG-PRED!
    ELSE
        2DROP DROP
    THEN ;

: WLG-ADD-ADJACENT ( w,wlg,v,k -- w,wlg )
    2OVER 2SWAP
    OVER 0= IF
        (WLG-ADD-ADJACENT)
    ELSE
        2DROP
    THEN ;

: WLG-SEARCH-ADJACENTS! ( w,wlg -- )
    ['] WLG-ADD-ADJACENT
    OVER ACT-EXECUTE
    2DROP ;

: (WLG-QUEUE-ADJACENT) ( v,k,w,q -- )
    SWAP -ROT 2SWAP    \ k,q,v,w
    = IF DUP CAR-DUMP Q-APPEND ELSE 2DROP THEN ;

: WLG-QUEUE-ADJACENT ( w,q,v,k -- w,q )
    2OVER (WLG-QUEUE-ADJACENT) ;

: WLG-QUEUE-ADJACENTS ( w,q,wlg -- )
    ['] WLG-QUEUE-ADJACENT
    SWAP ACT-EXECUTE
    2DROP ;

: (WLG-SEARCH-PATH) ( q,wlg -- )
    OVER Q-POP SWAP             \ q,w,wlg
    2DUP WLG-SEARCH-ADJACENTS!  \ q,w,wlg
    SWAP -ROT                   \ w,q,wlg
    WLG-QUEUE-ADJACENTS ;

: WLG-SEARCH-PATH ( s,t,q,wlg -- )
    >R R@ WLD-CLEAR-VALUES     \ s,t,q
    ROT DUP R@ WLG-START!      \ t,q,s
    OVER Q-APPEND              \ t,q
    R> ROT >R                  \ q,wlg
    BEGIN                      \ q,wlg
        OVER Q-EMPTY? 0= WHILE
        OVER Q-HEAD@ R@ = IF
            OVER Q-EMPTY
            R> DROP 0 >R
        ELSE 
            2DUP (WLG-SEARCH-PATH)
        THEN
    REPEAT
    2DROP >R 0= ;






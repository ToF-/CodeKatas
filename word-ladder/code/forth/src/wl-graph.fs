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
    

\ wl-dict.fs
REQUIRE ffl/act.fs   \ see https://irdvo.nl/FFL/docs/act.html

: WL-DICT ( <name> -- )
    ACT-CREATE ;

: WLD-UPDATE ( v,k,d -- )
    ACT-INSERT ;

: WLD-VALUE ( k,d -- v )
    ACT-GET 0= IF
        S" D-VALUE : key not found" 
        EXCEPTION THROW
    THEN ;

: WLD-VALUE-OR-NIL ( k,d -- v )
    ACT-GET 0= IF 0 THEN ;

: (WLD-CLEAR-VALUE) ( d,v,k -- d )
    NIP OVER 0 -ROT WLD-UPDATE ;

: WLD-CLEAR-VALUES ( d -- )
    ['] (WLD-CLEAR-VALUE) OVER 
    ACT-EXECUTE DROP ;

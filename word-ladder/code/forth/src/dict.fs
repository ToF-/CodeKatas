\ dict.fs
REQUIRE ffl/act.fs

: DICT ( <name> -- )
    ACT-CREATE ;

: D-UPDATE ( v,k,d -- )
    ACT-INSERT ;

: D-VALUE ( k,d -- v )
    ACT-GET 0= IF
        S" D-VALUE : key not found" 
        EXCEPTION THROW
    THEN ;

: (CLEAR-VALUE) ( d,v,k -- d )
    NIP OVER 0 -ROT D-UPDATE ;

: D-CLEAR-VALUES ( d -- )
    ['] (CLEAR-VALUE) OVER 
    ACT-EXECUTE DROP ;

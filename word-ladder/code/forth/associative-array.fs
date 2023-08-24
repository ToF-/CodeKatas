\ associative-array.fs

: AA-CREATE ( n <name> -- )
    CREATE DUP , 0 , 2* CELLS ALLOT ;

: AA-SIZE ( aa -- n )
    CELL+ @ ;

: AA-CAPACITY ( aa - n )
    @ ;

: AA-EMPTY? ( aa -- f )
    AA-SIZE 0= ;

: AA-DATA ( aa -- adr )
    2 CELLS + ;

: AA-LIMIT ( aa -- adr )
    DUP AA-SIZE 2* CELLS 
    SWAP AA-DATA + ;

: (AA-FIND) ( k,aa -- adr|0 )
    DUP AA-EMPTY? 0= IF
        DUP AA-LIMIT SWAP AA-DATA
        0 -ROT DO
            OVER I @ = IF
                DROP I LEAVE
            THEN
        2 CELLS +LOOP
        NIP
    ELSE
        2DROP 0
    THEN ;

: AA-FIND ( k,aa -- v,t|f )
    (AA-FIND) ?DUP IF
        CELL+ @ TRUE
    ELSE
        FALSE
    THEN ;

: AA-CAPACITY-CHECK ( aa -- )
    DUP AA-SIZE
    SWAP AA-CAPACITY >= IF
        S" associative array: out of capacity" EXCEPTION THROW
    THEN ;

: AA-ADD ( v,k,aa -- )
    DUP AA-CAPACITY-CHECK
    DUP AA-LIMIT >R  \ v,k,aa
    -ROT R@ !
    R> CELL+ !
    CELL+ 1 SWAP +! ;

: AA-UPDATE ( v,k,aa -- )
    2DUP (AA-FIND) ?DUP IF
        -ROT 2DUP
        CELL+ !
    ELSE
        AA-ADD
    THEN ;

: AA-EXECUTE ( xt,aa -- )
    DUP AA-LIMIT SWAP AA-DATA DO
        DUP I DUP @ SWAP CELL+ @ 
        ROT EXECUTE
    2 CELLS +LOOP
    DROP ;

CREATE S>CELL-BUFFER CELL ALLOT

: S>CELL ( ad,l -- n )
    DUP 7 > IF 
        s" s>cell : string to long" EXCEPTION THROW 
    THEN
    S>CELL-BUFFER CELL ERASE
    DUP S>CELL-BUFFER C!
    S>CELL-BUFFER 1+ SWAP CMOVE 
    S>CELL-BUFFER @ ;
    

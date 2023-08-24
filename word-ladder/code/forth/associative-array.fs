\ associative-array.fs

: AA-CREATE ( n <name> -- )
    CREATE DUP , 0 , 2* CELLS ALLOT ;

: AA-SIZE ( aa -- n )
    CELL+ @ ;

2 CELLS CONSTANT AA-CELL

: AA-CELLS ( -- n )
    2* CELLS ;

: AA-CELL! ( v,k,adr -- )
    2! ;

: AA-CELL@ ( adr -- v,k )
    2@ ;

: AA-CAPACITY ( aa - n )
    @ ;

: AA-EMPTY? ( aa -- f )
    AA-SIZE 0= ;

: AA-EMPTY ( aa -- )
    CELL+ OFF ;

: AA-CONTENT ( aa -- adr )
    CELL+ CELL+ ;

: AA-NEXT ( adr -- adr )
    AA-CELL + ;

: AA-LIMIT ( aa -- adr )
    DUP AA-SIZE AA-CELLS
    SWAP AA-CONTENT + ;

: AA-SIZE++ ( aa -- )
    CELL+ 1 SWAP +! ;

: (AA-FIND) ( k,aa -- adr|0 )
    DUP AA-LIMIT SWAP AA-CONTENT
    0 -ROT ?DO
        OVER I @ = IF
            DROP I LEAVE
        THEN
    AA-CELL +LOOP
    NIP ;

: AA-FIND ( k,aa -- v,t|f )
    (AA-FIND) ?DUP IF
        CELL+ @ TRUE
    ELSE
        FALSE
    THEN ;

: AA-NTH ( n,aa -- v,k )
    OVER OVER AA-SIZE >= IF
        S" aa-nth: out of bounds" EXCEPTION THROW
    THEN
    AA-CONTENT SWAP AA-CELLS + 2@ ;

: AA-CAPACITY-CHECK ( aa -- )
    DUP AA-SIZE
    SWAP AA-CAPACITY >= IF
        S" associative array: out of capacity" EXCEPTION THROW
    THEN ;

: (AA-SHIFT) ( aa -- )
    DUP AA-CONTENT
    DUP AA-NEXT
    ROT AA-SIZE AA-CELLS CMOVE> ;

: AA-INSERT ( v,k,aa -- )
    DUP AA-CAPACITY-CHECK
    DUP (AA-SHIFT)
    DUP 2SWAP ROT
    AA-CONTENT AA-CELL!
    AA-SIZE++ ;

: AA-ADD ( v,k,aa -- )
    DUP AA-CAPACITY-CHECK
    DUP 2SWAP ROT
    AA-LIMIT AA-CELL!
    AA-SIZE++ ;

: AA-UPDATE ( v,k,aa -- )
    2DUP (AA-FIND) ?DUP IF
        NIP AA-CELL!
    ELSE
        AA-ADD
    THEN ;

: AA-EXECUTE ( xt,aa -- )
    DUP AA-LIMIT SWAP AA-CONTENT ?DO
        DUP I AA-CELL@
        ROT EXECUTE
    AA-CELL +LOOP
    DROP ;

CREATE S>CELL-BUFFER CELL ALLOT
CREATE CELL>S-BUFFER CELL ALLOT

: S>CELL ( ad,l -- n )
    DUP 7 > IF 
        s" s>cell : string to long" EXCEPTION THROW 
    THEN
    S>CELL-BUFFER OFF
    DUP S>CELL-BUFFER C!
    S>CELL-BUFFER 1+ SWAP CMOVE 
    S>CELL-BUFFER @ ;

: CELL>S ( n -- ad,l )
    CELL>S-BUFFER !
    CELL>S-BUFFER 8 DUMP
    CELL>S-BUFFER COUNT ;

\ wordladder.fs
REQUIRE ffl/act.fs
REQUIRE ffl/car.fs

50000 CONSTANT WORDS-MAX

: (ADJACENT?) ( ad1,ad2,l -- f )
    OVER + SWAP 2>R
    FALSE SWAP
    2R> ?DO             \ acc,ad1
        DUP C@ I C@ <> IF
           SWAP 1+ SWAP
        THEN
        1+
    LOOP
    DROP 1 = ;

: ADJACENT? ( ad,l,ad,l -- f )
    ROT OVER = IF
        (ADJACENT?)
    ELSE
        DROP 2DROP FALSE
    THEN ;

CREATE S>KEY-BUFFER CELL ALLOT
CREATE EXTRA-S>KEY-BUFFER CELL ALLOT

: (S>KEY) ( ad,l -- u )
    S>KEY-BUFFER DUP CELL ERASE
    2DUP C!
    1+ SWAP CMOVE
    S>KEY-BUFFER @ ;

: S>KEY ( ad,l -- u )
    DUP 8 < IF (S>KEY) ELSE
        CR ." S>KEY : string too large" CR
        2DROP 0
    THEN ;

: KEY>S ( u,ad -- ad+1,l )
    DUP CELL ERASE
    TUCK !
    COUNT ;

: KEY"
    [CHAR] " WORD COUNT S>KEY ;

: KEY-ADJACENT? ( u,v -- f )
    S>KEY-BUFFER KEY>S
    ROT EXTRA-S>KEY-BUFFER KEY>S
    ADJACENT? ;

: WORD-DICTIONARY ( <name> -- )
    ACT-CREATE ;

: FIND-WORD ( k,dict -- v,t|f )
    ACT-GET ;

: ADD-WORD ( v,k,dict -- )
    ACT-INSERT ;

: NEW-WORD ( k,dict -- )
    FALSE -ROT ADD-WORD ;

: WORD-PREDECESSOR! ( p,k,dict -- )
    ADD-WORD ;

: WORD-PREDECESSOR@ ( k,dict -- p )
    ACT-GET DROP ;

1024 CONSTANT LINE-MAX
CREATE LINE-BUFFER LINE-MAX ALLOT

: READ-WORDS ( ad,l,dict -- )
    >R R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER LINE-MAX ROT READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP S>KEY R@ NEW-WORD
    REPEAT DROP
    CLOSE-FILE THROW 
    R> DROP ;


CREATE KEY-BUFFER WORDS-MAX CELLS ALLOT
VARIABLE KEY-BUFFER-MAX

: .KEY-VALUE ( v,k -- )
    PAD KEY>S TYPE SPACE ." -> " . CR ;

: .WORD-DICTIONARY ( dict -- )
    ['] .KEY-VALUE SWAP ACT-EXECUTE ;

: ADD-KEY-BUFFER ( v,k -- )
    NIP KEY-BUFFER-MAX @ !
    CELL KEY-BUFFER-MAX +! ;

: CLEAR-WORD-PREDECESSORS ( dict )
    KEY-BUFFER KEY-BUFFER-MAX !
    ['] ADD-KEY-BUFFER OVER ACT-EXECUTE
    KEY-BUFFER-MAX @ KEY-BUFFER ?DO
        I @ 
        OVER NEW-WORD
    CELL +LOOP DROP ;

: (ADJACENT-WORD!+) ( list,from,key -- list,from )
    2DUP KEY-ADJACENT? IF         \ list,from,key
        ROT DUP 2SWAP ROT !       \ list,from
        SWAP CELL+ SWAP 
    ELSE
        DROP
    THEN ;

: ADJACENT-WORD!+ ( list,from,value,key -- limit,from )
    SWAP 0= IF     
        (ADJACENT-WORD!+)
    ELSE
        DROP
    THEN ;
    
: ADJACENT-WORDS! ( list,key,dict -- limit,key )
    ['] ADJACENT-WORD!+ SWAP ACT-EXECUTE ;

: VISIT-ADJACENTS-WORDS ( dict,key,limit,list -- )
    ?DO 
        2DUP I @ ROT ACT-INSERT
    CELL +LOOP 2DROP ;
    
: FIND-ADJACENT-WORDS ( k,dict,list -- n )
    ROT >R 2DUP       \ dict,list,dict,list
    R> ROT            \ dict,list,list,key,dict
    ADJACENT-WORDS!   \ dict,list,limit,key
    SWAP ROT          \ dict,key,limit,list
    2DUP 2>R
    VISIT-ADJACENTS-WORDS
    2R> - CELL / ;

: (.LADDER) ( path,k -- )
    BEGIN
        ?DUP WHILE
        DUP PAD KEY>S TYPE SPACE
        OVER FIND-WORD DROP
    REPEAT DROP ;

: .LADDER ( k,path -- )
    SWAP (.LADDER) ;

: CLEAR-VISITS ( arr -- )
    CAR-CLEAR ;

: TO-VISIT? ( arr -- f )
    CAR-LENGTH@ 0 > ;

: TO-VISIT+! ( k,arr -- )
    CAR-APPEND ;

: TO-VISIT@ ( arr -- k )
    0 SWAP CAR-DELETE ;

\ wordladder.fs
REQUIRE ffl/act.fs

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

: FIND-WORD ( k,dict -- v,k|f )
    ACT-GET ;

: ADD-WORD ( v,k,dict -- )
    ACT-INSERT ;

: NEW-WORD ( k,dict -- )
    FALSE -ROT ADD-WORD ;

: VISIT-WORD ( k,dict -- )
    TRUE -ROT ADD-WORD ;
    
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

WORDS-MAX CONSTANT VISIT-MAX
CREATE VISIT-LIST WORDS-MAX CELLS ALLOT
VARIABLE TO-VISIT 

CREATE KEY-BUFFER WORDS-MAX CELLS ALLOT
VARIABLE KEY-BUFFER-MAX

ACT-CREATE VISIT-PATH

: .KEY-VALUE ( v,k -- )
    PAD KEY>S TYPE SPACE ." -> " . CR ;

: .WORD-DICTIONARY ( dict -- )
    ['] .KEY-VALUE SWAP ACT-EXECUTE ;

: ADD-KEY-BUFFER ( v,k -- )
    NIP KEY-BUFFER-MAX @ !
    CELL KEY-BUFFER-MAX +! ;

: CLEAR-WORD-VALUES ( dict )
    KEY-BUFFER KEY-BUFFER-MAX !
    ['] ADD-KEY-BUFFER OVER ACT-EXECUTE
    KEY-BUFFER-MAX @ KEY-BUFFER ?DO
        I @ 
        OVER NEW-WORD
    CELL +LOOP DROP ;

: (ADJACENT-WORD!) ( ad,t,k -- ad )
    2DUP
    KEY-ADJACENT? IF
        ROT DUP >R ! 
        R> CELL+ SWAP
    ELSE
        DROP
    THEN ;

: ADJACENT-WORD! ( ad,t,v,k -- ad )
    SWAP 0= IF      \ ad,t,k
        (ADJACENT-WORD!)
    ELSE
        DROP
    THEN ;
    
: VISIT-ADJACENTS-WORDS ( limit,start,dict -- )
    -ROT ?DO 
        DUP  I @ ROT VISIT-WORD 
    CELL +LOOP DROP ;
    
: FIND-ADJACENT-WORDS ( k,dict,ad -- n )
    OVER >R
    DUP 2SWAP ['] ADJACENT-WORD! SWAP ACT-EXECUTE
    DROP SWAP
    2DUP R> VISIT-ADJACENTS-WORDS
    - CELL / ;

: (.LADDER) ( path,k -- )
    DUP IF
        DUP PAD KEY>S TYPE SPACE
        OVER FIND-WORD DROP
        RECURSE
    ELSE
        2DROP
    THEN ;

: .LADDER ( k,path -- )
    SWAP (.LADDER) ;


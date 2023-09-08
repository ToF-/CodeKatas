\ wl-dictionary.fs
REQUIRE ffl/act.fs
REQUIRE ./wl-queue.fs
REQUIRE ./wl-word.fs
REQUIRE ./wl-wordgroup.fs
REQUIRE ./wl-letterset.fs

-1 CONSTANT WLD-START

: WL-DICTIONARY ( <name> -- )
    ACT-CREATE ;

: WLD-EMPTY ( d -- )
    ACT-CLEAR ;


: WLD-PRED! ( p,t,d -- )
    ACT-INSERT ;

: WLD-ADD-WORD ( w,d -- )
    0 -ROT WLD-PRED! ;

: WLD-HAS-WORD? ( w,d -- f )
    ACT-HAS? ;

: WLD-PRED@ ( w,d -- p )
    ACT-GET 0= IF   
        S" WLD-PRED@ : not found"
        THROW EXCEPTION
    THEN ;

: WLD-IS-START? ( w,d -- f )
    WLD-PRED@ WLD-START = ;

: WLD-START! ( w,d -- )
    WLD-START -ROT WLD-PRED! ;

: WLD-LETTER-SET@ ( g,d -- ls )
    ACT-GET 0= IF LS-EMPTY THEN ;

: WLD-LETTER-SET! ( ls,g,d -- )
    ACT-INSERT ;

: WLD-UPDATE-WORD-GROUP ( c,g,d -- )
    2DUP WLD-LETTER-SET@            \ c,g,d,ls
    -ROT 2SWAP LS-ADD-LETTER        \ g,d,ls'
    -ROT WLD-LETTER-SET! ;

: WLD-UPDATE-WORD-GROUPS ( w,d -- )
    OVER WL-WORD-LENGTH 0 ?DO  \ w,d
        2DUP SWAP              \ w,d,d,w
        I W>GROUP>LETTER       \ w,d,d,g,c
        SWAP ROT 
        WLD-UPDATE-WORD-GROUP 
    LOOP
    2DROP ;

: (WLD-CLEAR-PRED) ( k,d -- )
    0 -ROT ACT-INSERT ;

: WLD-CLEAR-PRED ( d,v,k -- d )
    NIP DUP IS-WORD-GROUP? 0= IF
        OVER (WLD-CLEAR-PRED)
    ELSE
        DROP
    THEN ;

: WLD-CLEAR-PREDS ( d -- )
    ['] WLD-CLEAR-PRED OVER ACT-EXECUTE DROP ;

: WLD-MARK-ADJACENT  ( q,w,d,x -- )
    ROT 2DUP                     \ q,d,x,w,x,w
    <> IF                        \ q,d,x,w
        -ROT 2DUP SWAP           \ q,w,d,x,x,d
        WLD-PRED@ 0= IF          \ q,w,d,x
            DUP 2SWAP SWAP -ROT  \ q,x,w,x,d
            WLD-PRED!
            SWAP Q-APPEND
        ELSE
            2DROP 2DROP
        THEN
    ELSE
        2DROP 2DROP
    THEN ;

\ for each group of the word
\ find the letter set of this group in the dictionary
\ for each letter of the group
\ form the word with this group and letter
\ if the word is different from w
\ find the prececessor of the word in the dictionary
\ if the word has no predecessor
\ set the precedessor of this word to w
\ then add this word to the queue
: WLD-FIND-AJACENTS! ( w,q,d -- )
    SWAP -ROT                    \ q,w,d
    OVER WL-WORD-LENGTH 0 ?DO    \ q,w,d
        OVER I W>GROUP           \ q,w,d,g
        2DUP SWAP                \ q,w,d,g,g,d
        WLD-LETTER-SET@ PAD LS>S \ q,w,d,g
        PAD COUNT                \ q,w,d,g,ad,l
        OVER + SWAP ?DO          \ q,w,d,g
            DUP I C@             \ q,w,d,g,g,c
            GROUP>LETTER>W       \ q,w,d,g,x
            4 PICK               \ q,w,d,g,x,q
            4 PICK               \ q,w,d,g,x,q,w
            ROT                  \ q,w,d,g,q,w,x
            4 PICK               \ q,w,d,g,q,w,x,d
            SWAP                 \ q,w,d,g,q,w,d,x
            WLD-MARK-ADJACENT    \ q,w,d,g
        LOOP
        DROP
    LOOP
    DROP 2DROP ;

: .WORD-QUEUE ( w -- )
   .WL-WORD SPACE ;

: WLD-FIND-PATH! ( t,s,q,d -- f )
    OVER Q-EMPTY
    DUP WLD-CLEAR-PREDS
    ROT >R                    \ t,q,d
    ROT DUP 2OVER             \ q,d,t,t,q,d
    -ROT Q-APPEND             \ q,d,t,d
    WLD-START!                \ q,d
    BEGIN
        \ OVER ['] .WORD-QUEUE SWAP CAR-EXECUTE CR
        OVER Q-EMPTY? 0=
    WHILE
        \ OVER Q-HEAD@ .WL-WORD SPACE
        OVER Q-HEAD@ R@ = IF
            OVER Q-EMPTY
            R> DROP 0 >R
        ELSE
            2DUP OVER Q-POP
            -ROT
            WLD-FIND-AJACENTS!
        THEN
    REPEAT
    2DROP R> 0= ;

: .WLD-ELEMENT ( v,k -- )
    2DUP hex 16 .R space 16 .R space decimal
    DUP IS-WORD-GROUP? IF
        .WL-GROUP SPACE ."  -> "
       PAD LS>S PAD COUNT TYPE
    ELSE
       .WL-WORD SPACE
        DUP WLD-START = IF
            DROP ."  -> *"
        ELSE 
            ."  -> "
            .WL-WORD
        THEN
    THEN
    CR ;

: .WL-DICTIONARY ( d -- )
    ['] .WLD-ELEMENT SWAP ACT-EXECUTE ;

: .WLD-GROUP-ELEMENT ( v,k -- )
    DUP IS-WORD-GROUP? IF
        .WL-GROUP SPACE ."  -> "
        PAD LS>S PAD COUNT TYPE CR
    ELSE
        2DROP
    THEN ;

: .WL-GROUPS ( d -- )
    ['] .WLD-GROUP-ELEMENT SWAP ACT-EXECUTE ;

: (.WLD-PATH) ( w,g -- )
    BEGIN
        OVER WLD-START <> 
    WHILE
        OVER .WL-WORD SPACE
        TUCK WLD-PRED@
        SWAP
    REPEAT
    2DROP ;

: .WLD-PATH ( w,g -- )
    2DUP WLD-HAS-WORD? IF
        (.WLD-PATH)
    ELSE
        2DROP
    THEN ;

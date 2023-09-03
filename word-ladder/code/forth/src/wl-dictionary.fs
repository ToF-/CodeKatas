\ wl-dictionary.fs

-1 CONSTANT WLD-START

: WL-DICTIONARY ( <name> -- )
    ACT-CREATE ;

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

: WLD-UPDATE-WORD-GROUPS ( w,d -- )
    OVER WL-WORD-LENGTH 0 ?DO  \ w,d
        2DUP SWAP              \ w,d,d,w
        I W>GROUP>LETTER       \ w,d,d,g,c
        >R 2DUP R>             \ w,d,d,g,d,g,c
        -ROT SWAP              \ w,d,d,g,c,g,d
        WLD-LETTER-SET@        \ w,d,d,g,c,ls
        LS-ADD-LETTER          \ w,d,d,g,ls'
        SWAP ROT               \ w,d,ls',g,d
        WLD-LETTER-SET!        \ w,d
    LOOP
    2DROP ;

: WLD-MARK-ADJACENT  ( q,w,d,x -- )
    ROT 2DUP                     \ q,d,x,w,x,w
    <> IF                        \ q,d,x,w
        -ROT 2DUP SWAP           \ q,w,d,x,x,d
        WLD-PRED@ 0= IF          \ q,w,d,x
            DUP 2SWAP            \ q,x,x,w,d
            SWAP -ROT            \ q,x,w,x,d
            WLD-PRED!            \ q,x
            SWAP Q-APPEND
        ELSE
            2DROP 2DROP
        THEN
    ELSE
        2DROP 2DROP
    THEN ;

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
    \ for each groups of the word
    \ find the letter set of this group in the dictionary
    \ for each letter of the group
    \ form the word with this group and letter
    \ if the word is different from w
    \ find the prececessor of the word in the dictionary
    \ if the word has no predecessor
    \ set the precedessor of this word to w
    \ add this word to the queue

: .WLD-ELEMENT ( v,k -- )
    DUP IS-WORD-GROUP? IF
       .WLGD-GROUP-LETTERS
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



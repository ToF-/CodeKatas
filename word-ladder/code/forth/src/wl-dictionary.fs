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

     

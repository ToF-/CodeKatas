\ wl-word.fs

\ control the word we can form
: LOWERCASE? ( c -- f )
    [CHAR] a [CHAR] z 1+ WITHIN ;

\ a word should be 7 long max,
\ lowercase letters only
: WL-CHECK-WORD ( ad,l -- f )
    DUP 0 8 WITHIN IF 
        TRUE -ROT
        OVER + SWAP ?DO
            I C@ DUP LOWERCASE? 0=
            SWAP [CHAR] ~ <> AND IF
                DROP FALSE
                LEAVE
            THEN
        LOOP
    ELSE
        2DROP FALSE 
    THEN ;

\ for conversions
CREATE WL-WORD-BUFFER CELL ALLOT

\ copy the counted string to dest
: S-COPY ( ad,l,dest -- )
    2DUP C! 1+ SWAP CMOVE ;

\ convert a counted string in a word
: S>WL-WORD ( ad,l -- w )
    2DUP WL-CHECK-WORD 0= IF
        S" invalid wl word"
        EXCEPTION THROW
    THEN
    WL-WORD-BUFFER OFF
    WL-WORD-BUFFER S-COPY
    WL-WORD-BUFFER @ ;

\ declare a named word
: WW ( <cccc> -- w )
    BL WORD COUNT S>WL-WORD ;

\ span the word on a string buffer
: WL-WORD>S ( w,ad -- )
    ! ; 

\ a char takes 8 bits
: C-MASK ( n -- b )
    255 AND ;

\ shifting the next char
: NEXT-C ( n -- n' )
    8 RSHIFT ;

\ shifting the word, returning next char
: NEXT>>C ( n -- n',c )
    DUP NEXT-C
    SWAP C-MASK ;

\ intial byte is length
: WL-WORD-LENGTH ( w -- l )
    C-MASK ;

\ two words are adjacent if they differ
\ by only one char
: (WL-ADJACENT?) ( w1,w2 -- f )
    0 -ROT 
    NEXT>>C DROP
    SWAP NEXT>>C
    0 ?DO                      \ acc,w1,w2
        NEXT>>C                \ acc,w1,w2',c2
        ROT NEXT>>C            \ acc,w2',c2,w1',c1
        ROT <> IF              \ acc,w2',w1'
            ROT 1+ -ROT
        THEN
    LOOP
    2DROP 1 = ;

\ two words have to be of same size
\ to be proven adjacent
: WL-ADJACENT? ( w1,w2 -- f )
    2DUP WL-WORD-LENGTH
    SWAP WL-WORD-LENGTH = IF
        (WL-ADJACENT?)
    ELSE
        2DROP FALSE
    THEN ;

\ display a word
: .WL-WORD ( w -- )
    WL-WORD-BUFFER !
    WL-WORD-BUFFER COUNT TYPE ;

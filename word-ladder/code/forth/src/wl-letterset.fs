\ wl-letterset.fs

\ the empty letter set
0 CONSTANT LS-EMPTY

\ a powerful function
: 2^ ( n -- 2^n )
    1 SWAP LSHIFT ;

\ converting a char into a letter
\ A = 1, Z = 26 .. a = 33, z=58
: C>LETTER ( c -- l )
    [CHAR] @ - ;

\ is a char in a letter set ?
: LS-HAS-LETTER? ( c,ls -- f )
    SWAP C>LETTER 2^ AND ;

\ add a char to a letter set
: LS-ADD-LETTER ( c,ls -- ls )
    SWAP C>LETTER 2^ OR ;

\ span the letter set on a strin
: LS>S ( ls,ad -- )
    DUP >R
    [CHAR] { [CHAR] @ DO
        OVER 1 AND IF
            1+ I OVER C!
        THEN
        SWAP 2/ SWAP
    LOOP
    R@ - R> C! DROP ;

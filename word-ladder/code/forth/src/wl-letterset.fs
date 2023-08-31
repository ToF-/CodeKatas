\ wl-letterset.fs

0 CONSTANT LS-EMPTY

: 2^ ( n -- 2^n )
    1 SWAP LSHIFT ;

: C>LETTER ( c -- l )
    [CHAR] @ - ;

: LS-HAS-LETTER? ( c,ls -- f )
    SWAP C>LETTER 2^ AND ; 

: LS-ADD-LETTER ( c,ls -- ls )
    SWAP C>LETTER 2^ OR ;

: LS>S ( ls,ad -- )
    DUP >R
    [CHAR] { [CHAR] @ DO
        OVER 1 AND IF
            1+ I OVER C!
        THEN
        SWAP 2/ SWAP
    LOOP
    R@ - R> C! DROP ;

            
            
        

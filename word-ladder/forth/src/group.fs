\ group.fs

CHAR ~ CONSTANT WILDCARD

: S>GROUP ( add,l,n,dest -- )
    >R -ROT R@ PLACE
    R> 1+ + WILDCARD SWAP C! ;

: GROUP-INDEX ( add,l -- n )
    OVER + SWAP 0 -ROT
    ?DO
        I C@ WILDCARD = IF LEAVE THEN
        1+
    LOOP ;

: GROUP-CHAR>S ( add,l,c,dest -- )
    SWAP >R DUP
    2OVER ROT PLACE
    -ROT GROUP-INDEX 1+ +
    R> SWAP C! ;

\ wl-env.fs
REQUIRE wl-graph.fs
REQUIRE wl-wordgroup.fs

100 CONSTANT MAX-LINE
CREATE LINE-BUFFER MAX-LINE ALLOT

: WLG-READ-WORDS ( ad,l,g -- )
    -ROT
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER MAX-LINE ROT
        READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP S>WL-WORD   \ g,fd,w
        ROT TUCK                     \ fd,g,w,g
        WLG-ADD-WORD
        SWAP
    REPEAT
    DROP
    CLOSE-FILE THROW
    DROP ;

: WLGD-READ-WORDS ( ad,l,gd -- )
    -ROT
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER MAX-LINE ROT
        READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP S>WL-WORD   \ gg,fd,w
        ROT TUCK                     \ fd,gd,w,gs
        WLGD-ADD-WORD
        SWAP
    REPEAT
    DROP
    CLOSE-FILE THROW
    DROP ;

: CHECK-WORD ( ad,l,g -- )
    -ROT S>WL-WORD DUP ROT
    WLG-HAS-WORD? 0= IF
        PAD WL-WORD>S s"  not in the list" PAD +PLACE
        PAD COUNT
        EXCEPTION THROW
    ELSE
        DROP
    THEN ;

: CHECK-ARGS ( ad,l,ad,l,g -- f )
    >R R@ CHECK-WORD
    R> CHECK-WORD ;


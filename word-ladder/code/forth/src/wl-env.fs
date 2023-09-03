\ wl-env.fs
REQUIRE wl-wordgroup.fs
REQUIRE wl-dictionary.fs

100 CONSTANT MAX-LINE
CREATE LINE-BUFFER MAX-LINE ALLOT

: WLD-READ-WORDS ( ad,l,d -- )
    -ROT
    R/O OPEN-FILE THROW
    BEGIN
        DUP LINE-BUFFER MAX-LINE ROT
        READ-LINE THROW
    WHILE
        LINE-BUFFER SWAP S>WL-WORD   \ d,fd,w
        ROT 2DUP                     \ fd,w,d,w,d
        WLD-ADD-WORD                 \ fd,w,d
        TUCK                         \ fd,d,w,d
        WLD-UPDATE-WORD-GROUPS       \ fd,d
        SWAP                         \ d,fd
    REPEAT
    DROP
    CLOSE-FILE THROW
    DROP ;

: CHECK-WORD ( ad,l,g -- )
    -ROT S>WL-WORD DUP ROT
    WLD-HAS-WORD? 0= IF
        PAD WL-WORD>S s"  not in the list" PAD +PLACE
        PAD COUNT
        EXCEPTION THROW
    ELSE
        DROP
    THEN ;

: CHECK-ARGS ( ad,l,ad,l,g -- f )
    >R R@ CHECK-WORD
    R> CHECK-WORD ;


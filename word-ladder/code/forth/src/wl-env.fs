\ wl-env.fs
REQUIRE wl-graph.fs

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


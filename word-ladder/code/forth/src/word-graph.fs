\ word-graph.fs
REQUIRE dict.fs
REQUIRE wordkey.fs

: WORD-GRAPH ( <name> -- )
    DICT ;

: WG-ADD-WORD ( ad,l,wg -- )
    -ROT S>KEY SWAP
    0 -ROT D-UPDATE ;

: WG-PRED@ ( ad,l,wg,dest -- dest+1,l )
    2SWAP S>KEY
    ROT D-VALUE
    SWAP KEY>S ;

: WG-PRED! ( adp,lp,ad,l,wg -- )
    >R S>KEY -ROT S>KEY SWAP R> D-UPDATE ;





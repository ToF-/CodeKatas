\ test-word-graph.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/word-graph.fs

CR
.( word graph ) CR

T{ .( after adding a word in the word graph its predecessor is the empty word. ) CR
    WORD-GRAPH wg
    S" FOO" wg WG-ADD-WORD
    S" FOO" wg pad WG-PRED@ 0 ?S DROP
}T

T{ .( after setting predecessor in the word graph the predecessor can be found. ) CR
    S" BAR" S" FOO" wg WG-PRED!
    S" FOO" wg pad WG-PRED@ S" BAR" ?STR
}T

\   T{ .( path execute in the word graph follow the word predecessors. ) CR
\       S" QUX" S" BAR" wg WG-PRED!
\       S" LOO" S" QUX" wg WG-PRED!
\       S" FOO" ' DROP wg WG-PATH-EXECUTE
\       PAD KEY>S S" LOO" ?STR
\       PAD KEY>S S" QUX" ?STR
\       PAD KEY>S S" BAR" ?STR
\       .s 
\   }T

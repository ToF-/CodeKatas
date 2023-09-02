\ wl-env-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-graph.fs
REQUIRE ../src/wl-env.fs
REQUIRE ../src/wl-wordgroup.fs

CR .( wl-env ) CR
.(   a word graph can be read from a file. ) CR
T{
    s\" echo \"bee\ncat\ndog\nwax\n\" >sample.txt" system
    WL-GRAPH G
    S" sample.txt" G WLG-READ-WORDS
    WW dog G WLG-HAS-WORD? ?TRUE
    WW fly G WLG-HAS-WORD? ?FALSE
    WW wax G WLG-HAS-WORD? ?TRUE
}T
.(   a group dictionnary can be loaded with words. ) CR
T{
    WL-GROUP-DICT GD
    S" sample.txt" GD WLGD-READ-WORDS
    WW ~ee gd WLGD-LETTERS PAD LS>S PAD COUNT S" b" ?STR
}T
.(   arguments are checked in the graph before search. ) CR
T{
    : check-non-existing-word s" foo" G CHECK-WORD ;
    ' check-non-existing-word catch [if] TRUE [then] ?TRUE
}T


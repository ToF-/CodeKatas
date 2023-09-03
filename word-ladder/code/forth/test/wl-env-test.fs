\ wl-env-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-graph.fs
REQUIRE ../src/wl-env.fs
REQUIRE ../src/wl-wordgroup.fs
REQUIRE ../src/wl-dictionary.fs

CR .( wl-env ) CR
.(   a word graph can be read from a file. ) CR
T{
    s\" echo \"bee\ncat\ndog\nwax\ncot\ncog\n\" >sample.txt" system
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
    WG ~ee gd WLGD-LETTERS PAD LS>S PAD COUNT S" b" ?STR
}T
.(  a group dictionary can be loaded with words and their word groups. ) CR
T{
    WL-DICTIONARY dct
    S" sample.txt" dct WLD-READ-WORDS
    WG ~ee dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" b" ?STR
    WW bee dct WLD-HAS-WORD? ?TRUE
    dct .WL-DICTIONARY
}T
.(   arguments are checked in the graph before search. ) CR
T{
    : check-non-existing-word s" foo" G CHECK-WORD ;
    ' check-non-existing-word catch [if] TRUE [then] ?TRUE
}T

